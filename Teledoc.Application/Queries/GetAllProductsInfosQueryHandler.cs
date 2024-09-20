using MediatR;
using Teledoc.Infrastructure.Repository;

namespace Teledoc.Application.Queries
{
	public class GetAllProductsInfosQueryHandler : IRequestHandler<GetAllProductsInfosQuery, IEnumerable<Teledoc.Infrastructure.Entities.Client>>
	{
		private readonly IClientRepository _clientRepository;

		public GetAllProductsInfosQueryHandler(IClientRepository context)
		{
			_clientRepository = context;
		}

		public async Task<IEnumerable<Teledoc.Infrastructure.Entities.Client>> Handle(GetAllProductsInfosQuery request, CancellationToken cancellationToken)
		{
			return await _clientRepository.GetAllClientsAsync();
		}
	}
}
