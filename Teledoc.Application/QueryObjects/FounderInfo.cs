using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teledoc.Application.QueryObjects
{
	public class FounderInfo
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string INN { get; set; }
		public string CreatedAt { get; set; }
		public string UpdatedAt { get; set; }
	}
}
