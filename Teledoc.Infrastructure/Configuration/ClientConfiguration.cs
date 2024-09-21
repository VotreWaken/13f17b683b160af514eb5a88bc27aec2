namespace Teledoc.Infrastructure.Configuration
{
	public class ClientConfiguration
	{
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string Url { get; set; } = string.Empty;
		public int Port { get; set; }
		public string Database { get; set; } = string.Empty;
	}
}
