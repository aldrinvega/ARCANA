using System.Text;
using System.Text.Json.Serialization;
using Carter;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Behaviors;
using RDF.Arcana.API.Data;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddValidatorsFromAssembly(ApplicationAssemblyReference.Assembly);

builder.Services.AddMediatR(x =>
{
    x.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    x.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddControllers(
    options =>
    {
        var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();

        options.Filters.Add(new AuthorizeFilter(policy));
    }
).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
//
// builder.Services.AddControllers().AddFluentValidation(UpdateUser

var connectionString = builder.Configuration.GetConnectionString("SQLSERVERDOCKER");

builder.Services.AddDbContext<ArcanaDbContext>(x =>
{
    if (connectionString != null)
    {
        x.UseSqlServer(connectionString, options =>
        {
            options.EnableRetryOnFailure();
        }).UseSnakeCaseNamingConvention();
    }

});

//builder.Services.AddDbContext<ArcanaDbContext>(
//    options => options.UseNpgsql(builder.Configuration.GetConnectionString("postgres")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RDF Arcana API", Version = "v1" });

    // Define the BearerAuth scheme in Swagger document
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    // Assign the BearerAuth scheme to globally apply to all operations
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication(authOptions =>
    {
        authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(jwtOptions =>
    {
        var key = builder.Configuration.GetValue<string>("JwtConfig:Key");
        var keyBytes = Encoding.ASCII.GetBytes(key);
        jwtOptions.SaveToken = true;
        jwtOptions.RequireHttpsMetadata = false;
        jwtOptions.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuer = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
            ValidAudience = builder.Configuration["JwtConfig:Audience"]
        };
        // jwtOptions.EventsType = typeof(MyAuthenticationFailureHandler);
    });

// builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();
// builder.Services.AddScoped<MyAuthenticationFailureHandler>();

builder.Services.Configure<CloudinaryOptions>(config.GetSection("Cloudinary"));


const string clientPermission = "_clientPermission";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: clientPermission, policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddCarter();

var app = builder.Build();


//AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
//your other scoped code


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.ApplyMigrations();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCarter();
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseCors(clientPermission);
// app.UseAuthorization();
app.MapControllers();
app.Run();
//await app.RunAsync();

