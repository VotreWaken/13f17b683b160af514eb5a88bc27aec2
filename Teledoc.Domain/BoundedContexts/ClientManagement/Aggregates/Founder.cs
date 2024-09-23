using Teledoc.Domain.BoundedContexts.ClientManagement.Events.Founder;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Composite;
using Teledoc.SharedKernel;

namespace Teledoc.Domain.BoundedContexts.ClientManagement.Aggregates
{
    public class Founder : AggregateRoot
	{
		public Founder()
		{

		}
		public INN INN { get; private set; }
		public UserFullName FullName { get; private set; }
		public int ClientId { get; set; }
		public IEnumerable<Client>? Clients { get; set; } = default!;

		#region Aggregate Methods

		public Founder(string inn, string fullname)
		{
			INN = (INN)CheckAndAssign(INN.Create(inn));
			FullName = (UserFullName)CheckAndAssign(UserFullName.CreateFromString(fullname));
		}

		public Founder(string inn, string firstname, string lastname, string patronymic)
		{
			INN = (INN)CheckAndAssign(INN.Create(inn));
			FullName = (UserFullName)CheckAndAssign(UserFullName.Create(firstname, lastname, patronymic));
			RaiseEvent(new FounderCreatedEvent(Id, INN, FullName, CreatedAt));
		}

		public void UpdateFounder(string inn, string firstname, string lastname, string patronymic)
		{
			INN = (INN)CheckAndAssign(INN.Create(inn));
			FullName = (UserFullName)CheckAndAssign(UserFullName.Create(firstname, lastname, patronymic));
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
