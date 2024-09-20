using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Infrastructure.DataContext;
using Teledoc.Infrastructure.Entities;

namespace Teledoc.Infrastructure.Repository
{
	public class ClientRepository : IClientRepository
	{
		private readonly ClientDbContext _context;
		public ClientRepository(ClientDbContext context)
		{
			_context = context;
		}

		public async Task<Client> GetClientByIdAsync(int id)
		{
			return await _context.Clients
								 .Include(c => c.Founders)
								 .FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<IEnumerable<Client>> GetAllClientsAsync()
		{
			return await _context.Clients
								 .Include(c => c.Founders)
								 .ToListAsync();
		}

		public async Task<int> AddClientAsync(Client client)
		{
			await _context.Clients.AddAsync(client);
			await _context.SaveChangesAsync();
			return client.Id;
		}

		public async Task UpdateClientAsync(Client client)
		{
			_context.Clients.Update(client);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteClientAsync(int id)
		{
			var client = await GetClientByIdAsync(id);
			if (client != null)
			{
				_context.Clients.Remove(client);
				await _context.SaveChangesAsync();
			}
		}

	}
}
