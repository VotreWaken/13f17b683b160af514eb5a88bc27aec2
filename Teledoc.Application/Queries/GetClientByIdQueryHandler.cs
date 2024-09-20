using MediatR;
using Teledoc.Infrastructure.Repository;

namespace Teledoc.Application.Queries
{
	public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Teledoc.Infrastructure.Entities.Client>
	{
		private readonly IClientRepository _clientRepository;

		public GetClientByIdQueryHandler(IClientRepository context)
		{
			_clientRepository = context;
		}

		public async Task<Teledoc.Infrastructure.Entities.Client> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
		{
			return await _clientRepository.GetClientByIdAsync(request.Id);
		}
	}
}
