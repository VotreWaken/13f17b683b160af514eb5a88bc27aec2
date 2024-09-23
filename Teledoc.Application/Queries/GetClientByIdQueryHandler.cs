using Mapster;
using MediatR;
using Teledoc.Application.QueryObjects;
using Teledoc.Application.Results;
using Teledoc.Domain.BoundedContexts.ClientManagement.Exceptions;
using Teledoc.Domain.BoundedContexts.ClientManagement.Interfaces;
using Teledoc.Infrastructure.Repository;

namespace Teledoc.Application.Queries
{
	public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, CommandResult>
	{
		private readonly IClientRepository _clientRepository;

		public GetClientByIdQueryHandler(IClientRepository context)
		{
			_clientRepository = context;
		}

		public async Task<CommandResult> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var client = await _clientRepository.GetClientByIdAsync(request.Id);

				if (client == null)
				{
					return CommandResult.NotFound(request.Id);
				}

				var clientDto = client.Adapt<ClientInfo>();

				return CommandResult.Success(clientDto);
			}
			catch (DomainException ex)
			{
				return CommandResult.BusinessFail(ex.Message);
			}
		}
	}
}
