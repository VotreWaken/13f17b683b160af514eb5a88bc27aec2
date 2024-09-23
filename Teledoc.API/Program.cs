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

			builder.Services.AddControllers();

			builder.Services.AddCustomServices(configuration);
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
