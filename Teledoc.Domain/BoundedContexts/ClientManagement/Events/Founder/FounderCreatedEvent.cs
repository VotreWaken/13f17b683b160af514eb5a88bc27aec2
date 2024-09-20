using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Composite;
using Teledoc.SharedKernel;

namespace Teledoc.Domain.BoundedContexts.ClientManagement.Events.Founder
{
	public class FounderCreatedEvent : DomainEvent
	{
		public int FounderId { get; }
		public INN INN { get; }
		public UserFullName FullName { get; }
		public DateTime CreatedAt { get; }

		public FounderCreatedEvent(int founderId, INN inn,
			UserFullName fullName, DateTime createdAt)
		{
			FounderId = founderId;
			INN = inn;
			FullName = fullName;
			CreatedAt = createdAt;
		}
	}
}
