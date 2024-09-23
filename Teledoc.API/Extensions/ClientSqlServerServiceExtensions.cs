using Teledoc.Infrastructure.DataContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Teledoc.Infrastructure.Configuration;

namespace Teledoc.API.Extensions
{
	public static class ClientSqlServerServiceExtensions
	{
		public static IServiceCollection AddClientServices(this 
			IServiceCollection services, Configuration settings)
		{
			if (settings == null)
			{
				throw new ArgumentNullException(nameof(settings), 
					"Client configuration cannot be null.");
			}

			var connectionString = new SqlConnectionStringBuilder()
			{
				DataSource = $"{settings.Url},{settings.Port}",
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


		public static IApplicationBuilder UseClientSqlServerMigration
			(this IApplicationBuilder app, ClientDbContext context)
		{
			context.Database.Migrate();
			return app;
		}
	}
}
