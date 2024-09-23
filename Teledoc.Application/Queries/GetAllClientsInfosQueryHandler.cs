using Mapster;
using MediatR;
using Teledoc.Application.QueryObjects;
using Teledoc.Application.Results;
using Teledoc.Domain.BoundedContexts.ClientManagement.Aggregates;
using Teledoc.Domain.BoundedContexts.ClientManagement.Exceptions;
using Teledoc.Domain.BoundedContexts.ClientManagement.Interfaces;
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
				var clients = await _clientRepository.GetAllClientsAsync();

				var clientDtos = clients.Adapt<List<ClientInfo>>();

				return CommandResult.Success(clientDtos);
			}
			catch (DomainException ex)
			{
				return CommandResult.BusinessFail(ex.Message);
			}
		}
	}
}
