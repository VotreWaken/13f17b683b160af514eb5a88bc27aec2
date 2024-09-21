using MediatR;
using Teledoc.Application.Results;
using Teledoc.Domain.BoundedContexts.ClientManagement.Exceptions;
using Teledoc.Infrastructure.Repository;

namespace Teledoc.Application.Queries
{
	public class GetAllClientsInfoQueryHandler : IRequestHandler<GetAllClientsInfoQuery, CommandResult>
	{
		private readonly IClientRepository _clientRepository;

		public GetAllClientsInfoQueryHandler(IClientRepository context)
		{
			_clientRepository = context;
		}

		public async Task<CommandResult> Handle(GetAllClientsInfoQuery request, CancellationToken cancellationToken)
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
