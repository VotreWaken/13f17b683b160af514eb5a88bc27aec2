using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
