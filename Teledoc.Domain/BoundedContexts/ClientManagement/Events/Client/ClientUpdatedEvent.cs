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
    public class ClientUpdatedEvent : DomainEvent
    {
        public int ClientId { get; protected set; }
        public string UpdatedName { get; protected set; }
        public INN UpdatedINN { get; protected set; }
        public ClientType UpdatedClientType { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        private ClientUpdatedEvent() { }

        public ClientUpdatedEvent(int clientId, string updatedName, 
            INN updatedINN, ClientType updatedClientType, 
            DateTime updatedAt)
        {
            AggregateId = clientId;
            ClientId = clientId;
            UpdatedName = updatedName;
            UpdatedINN = updatedINN;
            UpdatedClientType = updatedClientType;
            UpdatedAt = updatedAt;
        }
    }
}
