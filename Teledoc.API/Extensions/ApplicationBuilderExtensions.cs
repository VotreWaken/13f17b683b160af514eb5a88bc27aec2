using Teledoc.Infrastructure.DataContext;

namespace Teledoc.API.Extensions
{
	public static class ApplicationBuilderExtensions
	{
		public static void UseCustomMiddlewares(this IApplicationBuilder app, IHostEnvironment env)
		{
			using (var scope = app.ApplicationServices.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetRequiredService<ClientDbContext>();
				app.UseClientSqlServerMigration(dbContext);
			}

			app.UseCors("AllowAll");

			if (env.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseAuthorization();
		}
	}
}
