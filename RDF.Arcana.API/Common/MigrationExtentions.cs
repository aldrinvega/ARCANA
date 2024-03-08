using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Common
{
    public static class MigrationExtentions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using ArcanaDbContext dbContext = scope.ServiceProvider.GetRequiredService<ArcanaDbContext>();

            dbContext.Database.Migrate();
        } 
    }
}
