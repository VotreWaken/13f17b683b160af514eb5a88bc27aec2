using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teledoc.Infrastructure.Configuration
{
	public class ClientConfiguration
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Url { get; set; }
		public int Port { get; set; }
		public string Database { get; set; }
	}
}
