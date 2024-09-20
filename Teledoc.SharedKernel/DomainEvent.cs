using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teledoc.SharedKernel
{
	public class DomainEvent : IDomainEvent
	{
		public int EventId { get; private set; }
		public int AggregateId { get; protected set; }

		protected DomainEvent()
		{

		}

		protected DomainEvent(int aggregateId) : this()
		{
			AggregateId = aggregateId;
		}
	}
}
