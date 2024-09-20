using Teledoc.SharedKernel;

namespace Teledoc.Domain.BoundedContexts.ClientManagement.Events.Founder
{
	public class FounderDeletedEvent : DomainEvent
	{
		public int FounderId { get; }

		public FounderDeletedEvent(int founderId)
		{
			FounderId = founderId;
		}
	}
}
