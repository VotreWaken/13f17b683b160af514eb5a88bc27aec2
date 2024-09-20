using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Domain.BoundedContexts.ClientManagement.Events.Founder;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Composite;
using Teledoc.SharedKernel;

namespace Teledoc.Domain.BoundedContexts.ClientManagement.Aggregates
{
	public class Founder : AggregateRoot
	{
		public int Id { get; private set; }
		public INN INN { get; private set; }
		public string FullName { get; private set; }
		public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

		#region Aggregate Methods

		public Founder(INN inn, string fullName)
		{
			INN = inn;
			FullName = fullName;
			RaiseEvent(new FounderCreatedEvent(Id, INN, FullName, CreatedAt));
		}

		public void UpdateFounder(string fullName)
		{
			FullName = fullName;
			UpdatedAt = DateTime.UtcNow;
			RaiseEvent(new FounderUpdatedEvent(Id, INN, FullName, UpdatedAt));
		}

		public void DeleteFounder()
		{
			RaiseEvent(new FounderDeletedEvent(Id));
		}

		#endregion

		#region Event Handling

		protected override void When(IDomainEvent @event)
		{
			switch (@event)
			{
				case FounderCreatedEvent e:
					OnFounderCreatedEvent(e);
					break;
				case FounderUpdatedEvent e:
					OnFounderUpdatedEvent(e);
					break;
				case FounderDeletedEvent e:
					OnFounderDeletedEvent(e);
					break;
			}
		}

		private void OnFounderCreatedEvent(FounderCreatedEvent @event) => Id = @event.FounderId;
		private void OnFounderUpdatedEvent(FounderUpdatedEvent @event) => UpdatedAt = @event.UpdatedAt;
		private void OnFounderDeletedEvent(FounderDeletedEvent @event) => Id = @event.FounderId;

		#endregion
	}
}
