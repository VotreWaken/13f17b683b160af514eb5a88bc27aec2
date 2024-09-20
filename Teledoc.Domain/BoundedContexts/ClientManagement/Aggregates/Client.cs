using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Domain.BoundedContexts.ClientManagement.Events.Client;
using Teledoc.Domain.BoundedContexts.ClientManagement.Exceptions;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Basic;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Composite;
using Teledoc.SharedKernel;

namespace Teledoc.Domain.BoundedContexts.ClientManagement.Aggregates
{
    public class Client : AggregateRoot
	{
		public int Id { get; private set; }
		public INN INN { get; private set; }
		public string Name { get; private set; } = string.Empty;
		public ClientType ClientType { get; private set; }
		public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
		public List<Founder> Founders { get; private set; } = new List<Founder>();

		#region Aggregate Methods

		public Client(string inn, string name, string clientTypeString)
		{
			Console.WriteLine($"INN In Constuctor: {inn}");
			INN = (INN)CheckAndAssign(INN.Create(inn));
			Console.WriteLine("INN After Assign", INN?.ToString());

			if (Enum.TryParse<ClientTypeEnum>(clientTypeString, out var clientTypeEnum))
			{
				ClientType = ClientType.FromEnum(clientTypeEnum);
			}
			else
			{
				ClientType = ClientType.LegalEntity;
			}

			if (_businessLogicErrors?.Any() == true)
				throw new DomainBusinessLogicException(_businessLogicErrors);

			Name = name;
			RaiseEvent(new ClientCreatedEvent(Id, Name, INN, ClientType, CreatedAt));
		}

		public void UpdateClient(string name, ClientType clientType)
		{
			Name = name;
			ClientType = clientType;
			UpdatedAt = DateTime.UtcNow;
			RaiseEvent(new ClientUpdatedEvent(Id, Name, INN, ClientType, UpdatedAt));
		}

		public void DeleteClient()
		{
			RaiseEvent(new ClientDeletedEvent(Id));
		}

		#endregion

		#region Event Handling

		protected override void When(IDomainEvent @event)
		{
			switch (@event)
			{
				case ClientCreatedEvent e:
					OnClientCreatedEvent(e);
					break;
				case ClientUpdatedEvent e:
					OnClientUpdatedEvent(e);
					break;
				case ClientDeletedEvent e:
					OnClientDeletedEvent(e);
					break;
			}
		}

		private void OnClientCreatedEvent(ClientCreatedEvent @event) => Id = @event.ClientId;
		private void OnClientUpdatedEvent(ClientUpdatedEvent @event) => UpdatedAt = DateTime.UtcNow;
		private void OnClientDeletedEvent(ClientDeletedEvent @event) => Id = @event.ClientId;

		#endregion
	}
}
