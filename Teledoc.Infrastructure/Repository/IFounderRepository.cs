using Teledoc.Infrastructure.Entities;

namespace Teledoc.Infrastructure.Repository
{
	public interface IFounderRepository
	{
		Task AddFounderAsync(Founder founder);
		Task<List<Founder>> GetFoundersByClientIdAsync(int clientId);
		Task UpdateFounderAsync(Founder founder);
		Task DeleteFounderAsync(int founderId);
	}
}
