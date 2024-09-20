using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Composite;
using Teledoc.SharedKernel;

namespace Teledoc.Domain.BoundedContexts.ClientManagement.Events.Founder
{
	public class FounderUpdatedEvent : DomainEvent
	{
		public int FounderId { get; }
		public INN INN { get; }
		public UserFullName FullName { get; }
		public DateTime UpdatedAt { get; }

		public FounderUpdatedEvent(int founderId, INN inn,
			UserFullName fullName, DateTime updatedAt)
		{
			FounderId = founderId;
			INN = inn;
			FullName = fullName;
			UpdatedAt = updatedAt;
		}
	}
}
