using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Composite;
using Teledoc.SharedKernel;

namespace Teledoc.Domain.BoundedContexts.ClientManagement.Events.Founder
{
	public class FounderUpdatedEvent : DomainEvent
	{
		public int FounderId { get; }
		public INN INN { get; }
		public string FullName { get; }
		public DateTime UpdatedAt { get; }

		public FounderUpdatedEvent(int founderId, INN inn,
			string fullName, DateTime updatedAt)
		{
			FounderId = founderId;
			INN = inn;
			FullName = fullName;
			UpdatedAt = updatedAt;
		}
	}
}
