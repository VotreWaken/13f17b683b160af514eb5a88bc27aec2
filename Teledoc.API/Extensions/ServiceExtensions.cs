using Teledoc.Application.Commands;
using Teledoc.Application.Mappings;
using Teledoc.Infrastructure.Configuration;
using Teledoc.Infrastructure.Repository;

namespace Teledoc.API.Extensions
{
	public static class ServiceExtensions
	{
		public static void AddCustomServices(this IServiceCollection services, IConfiguration configuration)
		{
			var sqlServerSettings = configuration.GetSection("SqlServerSettings").Get<ClientConfiguration>();
			services.Configure<ClientConfiguration>(configuration.GetSection("SqlServerSettings"));
			services.AddClientServices(sqlServerSettings);

			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ClientCreateCommand).Assembly));

			services.AddTransient<IClientRepository, ClientRepository>();
			services.AddTransient<IFounderRepository, FounderRepository>();
		}

		public static void AddAutoMapper(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(MappingClient));
		}

		public static void AddCorsPolicy(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("AllowAll",
					builder =>
					{
						builder.AllowAnyOrigin()
							   .AllowAnyMethod()
							   .AllowAnyHeader();
					});
			});
		}
	}
}
