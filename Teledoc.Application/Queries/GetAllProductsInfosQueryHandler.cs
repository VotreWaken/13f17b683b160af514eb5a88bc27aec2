using MediatR;
using Teledoc.Application.Results;
using Teledoc.Domain.BoundedContexts.ClientManagement.Exceptions;
using Teledoc.Infrastructure.Repository;

namespace Teledoc.Application.Queries
{
	public class GetAllProductsInfosQueryHandler : IRequestHandler<GetAllProductsInfosQuery, CommandResult>
	{
		private readonly IClientRepository _clientRepository;

		public GetAllProductsInfosQueryHandler(IClientRepository context)
		{
			_clientRepository = context;
		}

		public async Task<CommandResult> Handle(GetAllProductsInfosQuery request, CancellationToken cancellationToken)
		{
			try
			{
				return CommandResult.Success(await _clientRepository.GetAllClientsAsync());
			}
			catch (DomainException ex)
			{
				return CommandResult.BusinessFail(ex.Message);
			}
		}
	}
}
