using MediatR;
using Teledoc.Application.Results;
using Teledoc.Domain.BoundedContexts.ClientManagement.Exceptions;
using Teledoc.Domain.BoundedContexts.ClientManagement.Aggregates;
using Teledoc.Domain.BoundedContexts.ClientManagement.Interfaces;

namespace Teledoc.Application.Commands
{
	public class UpdateClientCommandHandler : IRequestHandler<ClientUpdateCommand, CommandResult>
	{
		private readonly IClientRepository _clientRepository;
		private readonly IFounderRepository _founderRepository;

		public UpdateClientCommandHandler(IClientRepository context, IFounderRepository founderRepository)
		{
			_clientRepository = context;
			_founderRepository = founderRepository;
		}

		public async Task<CommandResult> Handle(ClientUpdateCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var clientEntity = await _clientRepository.GetClientByIdAsync(request.Id);

				if (clientEntity == null)
				{
					return CommandResult.NotFound(request.Id);
				}

				clientEntity.UpdatedAt = DateTime.UtcNow;

				clientEntity.UpdateClient(request.INN, request.Name, request.ClientType, request.Founders);

				await _clientRepository.UpdateClientAsync(clientEntity);

				return CommandResult.Success();
			}
			catch (Exception ex)
			{
				return CommandResult.BusinessFail(ex.Message);
			}
		}
	}
}