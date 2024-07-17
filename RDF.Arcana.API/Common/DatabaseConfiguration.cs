//// DatabaseConfiguration.cs

//using RDF.Arcana.API.Data;

//namespace RDF.Arcana.API.Common
//{
//	public static class DatabaseConfiguration
//	{
//		public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration, string environment)
//		{
//			var config = new ConfigurationBuilder()
//			.SetBasePath(AppContext.BaseDirectory)
//			//.AddJsonFile("appsettings.json")
//			.AddJsonFile($"appsettings.{environment}.json", optional: true)
//			.Build();

//			var connectionString = config.GetConnectionString(GetConnectionStringKey(environment));

//			services.AddDbContext<ArcanaDbContext>(options =>
//				options.UseSqlServer(connectionString));
//		}

//		private static string GetConnectionStringKey(string environment)
//		{
//			switch (environment.ToLowerInvariant())
//			{
//				case "development":
//					return "Testing";
//				case "production":
//					return "Production";
//				default:
//					return "Production";
//			}
//		}
//	}
//}