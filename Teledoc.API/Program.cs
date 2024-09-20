
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;
using Teledoc.API.Extensions;
using Teledoc.Application.Commands;
using Teledoc.Infrastructure.Configuration;
using Teledoc.Infrastructure.DataContext;
using Teledoc.Infrastructure.Entities;
using Teledoc.Infrastructure.Repository;

namespace Teledoc
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var configuration = builder.Configuration;

			var sqlServerSettings = configuration.GetSection("SqlServerSettings").Get<ClientConfiguration>();

			builder.Services.AddControllers()
					.AddJsonOptions(options =>
					{
						options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
					});

			builder.Services.Configure<ClientConfiguration>(builder.Configuration.GetSection("SqlServerSettings"));
			builder.Services.AddOrderServices(sqlServerSettings);

			builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ClientCreateCommand).GetTypeInfo().Assembly));

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAll",
					builder =>
					{
						builder.AllowAnyOrigin()
							   .AllowAnyMethod()
							   .AllowAnyHeader();
					});
			});

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();


			builder.Services.AddTransient<IClientRepository, ClientRepository>();
			builder.Services.AddTransient<IFounderRepository, FounderRepository>();

			var app = builder.Build();

			using (var scope = app.Services.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetRequiredService<ClientDbContext>();
				app.UseOrderSqlServerMigration(dbContext);
			}


			app.UseCors("AllowAll");

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
