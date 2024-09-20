using Teledoc.SharedKernel;

namespace Teledoc.Domain.BoundedContexts.ClientManagement.Events.Client
{
    public class ClientDeletedEvent : DomainEvent
    {
        public int ClientId { get; protected set; }

        private ClientDeletedEvent() { }

        public ClientDeletedEvent(int clientId)
        {
            AggregateId = clientId;
            ClientId = clientId;
        }
    }
}
