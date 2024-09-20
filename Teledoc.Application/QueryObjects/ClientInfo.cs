using Teledoc.Domain.BoundedContexts.ClientManagement.Aggregates;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Basic;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Composite;

namespace Teledoc.Application.QueryObjects
{
	public class ClientInfo
	{
		public int Id { get; set; }
		public INN INN { get; set; }
		public string Name { get; set; } = string.Empty;
		public ClientType ClientType { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
		public List<Teledoc.Infrastructure.Entities.Founder> Founders { get; set; } = new List<Teledoc.Infrastructure.Entities.Founder>();
	}
}
