using Teledoc.Infrastructure.DataContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Teledoc.Infrastructure.Configuration;

namespace Teledoc.API.Extensions
{
	public static class OrderSqlServerServiceExtensions
	{
		public static IServiceCollection AddOrderServices(this IServiceCollection services, ClientConfiguration settings)
		{
			var connectionString = new SqlConnectionStringBuilder()
			{
				DataSource = settings.Url,
				InitialCatalog = settings.Database,
				UserID = settings.Username,
				Password = settings.Password,
				TrustServerCertificate = true,
			}.ConnectionString;

			services.AddDbContext<ClientDbContext>(options =>
			{
				options.UseSqlServer(connectionString);
				options.EnableDetailedErrors();
				options.LogTo(Console.WriteLine, LogLevel.Information);
			});

			return services;
		}


		public static IApplicationBuilder UseOrderSqlServerMigration(this IApplicationBuilder app, ClientDbContext context)
		{
			context.Database.EnsureCreated();
			context.Database.Migrate();
			return app;
		}
	}
}
