using MediatR;
using Teledoc.Application.Mappings;
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
				var clients = await _clientRepository.GetAllClientsAsync();

				var clientDtos = clients.Select(client => ClientEntityToDTOMapper.ToDto(client)).ToList();

				return CommandResult.Success(clientDtos);
			}
			catch (DomainException ex)
			{
				return CommandResult.BusinessFail(ex.Message);
			}
		}
	}
}
