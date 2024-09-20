using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Composite;
using Teledoc.SharedKernel;

namespace Teledoc.Domain.BoundedContexts.ClientManagement.Events.Founder
{
	public class FounderCreatedEvent : DomainEvent
	{
		public int FounderId { get; }
		public INN INN { get; }
		public string FullName { get; }
		public DateTime CreatedAt { get; }

		public FounderCreatedEvent(int founderId, INN inn, 
			string fullName, DateTime createdAt)
		{
			FounderId = founderId;
			INN = inn;
			FullName = fullName;
			CreatedAt = createdAt;
		}
	}
}
