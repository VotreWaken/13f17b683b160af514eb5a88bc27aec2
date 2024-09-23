using Teledoc.Domain.BoundedContexts.ClientManagement.Aggregates;

namespace Teledoc.Domain.BoundedContexts.ClientManagement.Interfaces
{
	public interface IFounderRepository
	{
		Task AddFounderAsync(Founder founder);
		Task AddFoundersAsync(IEnumerable<Founder> founders);
		Task<IEnumerable<Founder>> GetFoundersByClientIdAsync(int clientId);
		Task UpdateFounderAsync(Founder founder);
		Task DeleteFounderAsync(int founderId);
	}
}
