namespace Teledoc.Domain.BoundedContexts.ClientManagement.Aggregates
{
	public abstract class ClientEntity
	{
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	}

}
