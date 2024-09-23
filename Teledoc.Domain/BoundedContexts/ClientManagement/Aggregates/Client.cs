using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using Teledoc.Domain.BoundedContexts.ClientManagement.Events.Client;
using Teledoc.Domain.BoundedContexts.ClientManagement.Exceptions;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.ClientType;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.ClientType.Enums;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Composite;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.INN;
using Teledoc.SharedKernel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Teledoc.Domain.BoundedContexts.ClientManagement.Aggregates
{
    public class Client : AggregateRoot
	{
		public INN INN { get; private set; } 
		public string Name { get; private set; } = string.Empty;
		public ClientType ClientType { get; private set; }
		public IEnumerable<Founder> Founders { get; private set; } = default!;
		public int ClientTypeId { get; set; }
		public Client()
		{
		}

		#region Aggregate Methods

		public Client(string inn, string name, string clientType, IEnumerable<Founder> founders)
		{
			INN = (INN)CheckAndAssign(INN.Create(inn));

			Name = name;
			CreatedAt = DateTime.UtcNow;
			UpdatedAt = DateTime.UtcNow;
			ClientType = ClientType.FromString(clientType);

			ClientTypeId = ClientType.ClientTypeIdValue;
			Founders = founders;

			ClientType = (ClientType)CheckAndAssign(ClientType.ValidateFoundersForClientType(ClientType, founders));
			
			if (_businessLogicErrors.Any())
				throw new DomainBusinessLogicException(_businessLogicErrors);

			RaiseEvent(new ClientCreatedEvent(Id, Name, INN, ClientType, CreatedAt));
		}
		public Client(string inn, string name, int clientType)
		{
			INN = (INN)CheckAndAssign(INN.Create(inn));
			Name = name;
			ClientType = ClientType.FromValue(clientType);

			if (_businessLogicErrors?.Any() == true)
				throw new DomainBusinessLogicException(_businessLogicErrors);

			RaiseEvent(new ClientCreatedEvent(Id, Name, INN, ClientType, CreatedAt));
		}

		public void UpdateClient(string inn, string name, ClientType clientType)
		{
			INN = (INN)CheckAndAssign(INN.Create(inn));

			Name = name;

			ClientType = clientType;

			UpdatedAt = DateTime.UtcNow;

			RaiseEvent(new ClientUpdatedEvent(Id, Name, INN, ClientType, UpdatedAt));
		}

		public void UpdateClient(string inn, string name, 
			string clientType, IEnumerable<Founder> founders)
		{
			INN = (INN)CheckAndAssign(INN.Create(inn));

			Name = name;

			ClientType.FromString(clientType);

			Founders = founders;

			UpdatedAt = DateTime.UtcNow;

			RaiseEvent(new ClientUpdatedEvent(Id, Name, INN, ClientType, UpdatedAt));
		}

		public void UpdateClient(string inn, string name, string clientType)
		{
			INN = (INN)CheckAndAssign(INN.Create(inn));

			Name = name;

			ClientType = ClientType.FromString(clientType);

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
