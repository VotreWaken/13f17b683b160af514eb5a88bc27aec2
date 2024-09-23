using Microsoft.EntityFrameworkCore;
using Teledoc.Domain.BoundedContexts.ClientManagement.Aggregates;
using Teledoc.Domain.BoundedContexts.ClientManagement.Interfaces;
using Teledoc.Infrastructure.DataContext;

namespace Teledoc.Infrastructure.Repository
{
	public class FounderRepository : IFounderRepository
	{
		private readonly ClientDbContext _context;

		public FounderRepository(ClientDbContext context)
		{
			_context = context;
		}

		public async Task AddFounderAsync(Founder founder)
		{
			_context.Founders.Add(founder);
			await _context.SaveChangesAsync();
		}

		public async Task AddFoundersAsync(IEnumerable<Founder> founders)
		{
			await _context.Founders.AddRangeAsync(founders);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Founder>> GetFoundersByClientIdAsync(int clientId)
		{
			return await _context.Founders
				.Where(f => f.Id == clientId)
				.ToListAsync();
		}

		public async Task UpdateFounderAsync(Founder founder)
		{
			_context.Founders.Update(founder);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteFounderAsync(int founderId)
		{
			var founder = await _context.Founders.FindAsync(founderId);
			if (founder != null)
			{
				_context.Founders.Remove(founder);
				await _context.SaveChangesAsync();
			}
		}
	}
}
