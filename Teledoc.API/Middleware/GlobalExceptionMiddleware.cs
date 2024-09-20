namespace Teledoc.API.Middleware
{
	public static class GlobalExceptionMiddleware
	{
		public static void UseGlobalExceptionMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<ExceptionMiddleware>();
		}
	}
}
