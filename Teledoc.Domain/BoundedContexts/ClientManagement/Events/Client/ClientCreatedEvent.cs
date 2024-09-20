using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Basic;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Composite;
using Teledoc.SharedKernel;

namespace Teledoc.Domain.BoundedContexts.ClientManagement.Events.Client
{
    public class ClientCreatedEvent : DomainEvent
    {
        public int ClientId { get; protected set; }
        public string Name { get; protected set; }
        public INN INN { get; protected set; }
        public ClientType ClientType { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        private ClientCreatedEvent() { }

        public ClientCreatedEvent(int clientId, string name, INN inn, 
            ClientType clientType, DateTime createdAt)
        {
            AggregateId = clientId;
            ClientId = clientId;
            Name = name;
            INN = inn;
            ClientType = clientType;
            CreatedAt = createdAt;
        }
    }
}
