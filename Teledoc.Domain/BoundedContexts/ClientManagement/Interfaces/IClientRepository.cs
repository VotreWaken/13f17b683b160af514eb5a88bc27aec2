using Teledoc.Domain.BoundedContexts.ClientManagement.Aggregates;

namespace Teledoc.Domain.BoundedContexts.ClientManagement.Interfaces
{
	public interface IClientRepository
	{
		public Task<Client> GetClientByIdAsync(int id);
		public Task<IEnumerable<Client>> GetAllClientsAsync();
		public Task<int> AddClientAsync(Client client);
		public Task UpdateClientAsync(Client client);
		public Task DeleteClientAsync(int id);
	}
}
