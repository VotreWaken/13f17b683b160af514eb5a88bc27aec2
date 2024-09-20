using Teledoc.Infrastructure.Entities;

namespace Teledoc.Infrastructure.Repository
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
