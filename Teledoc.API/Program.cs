using Teledoc.API.Extensions;
using Teledoc.API.Middleware;

namespace Teledoc
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var configuration = builder.Configuration;

			builder.Services.AddControllers()
				.AddJsonOptions(options =>
				{
					options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
				});

			builder.Services.AddCustomServices(configuration);
			builder.Services.AddAutoMapper();
			builder.Services.AddCorsPolicy();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			app.UseRouting();

			app.UseCustomMiddlewares(app.Environment);

			app.UseGlobalExceptionMiddleware();

			app.MapControllers();
			app.Run();
		}
	}
}
