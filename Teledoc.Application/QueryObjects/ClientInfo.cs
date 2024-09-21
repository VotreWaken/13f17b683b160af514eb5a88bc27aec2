using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Basic;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Composite;

namespace Teledoc.Application.QueryObjects
{
	public class ClientInfo
	{
		public int Id { get; set; }
		public string INN { get; set; }
		public string Name { get; set; } = string.Empty;
		public string ClientType { get; set; }
		public string CreatedAt { get; set; }
		public string UpdatedAt { get; set; }
		public List<FounderInfo> Founders { get; set; } = new List<FounderInfo>();
	}
}
