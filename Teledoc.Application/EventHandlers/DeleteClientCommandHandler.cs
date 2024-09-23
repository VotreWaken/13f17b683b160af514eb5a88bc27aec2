using MediatR;
using Teledoc.Application.Commands;
using Teledoc.Application.Results;
using Teledoc.Domain.BoundedContexts.ClientManagement.Exceptions;
using Teledoc.Domain.BoundedContexts.ClientManagement.Interfaces;

namespace Teledoc.Application.EventHandlers
{
	public class DeleteClientCommandHandler : IRequestHandler<ClientDeleteCommand, CommandResult>
	{
		private readonly IClientRepository _clientRepository;

		public DeleteClientCommandHandler(IClientRepository clientRepository)
		{
			_clientRepository = clientRepository;
		}

		public async Task<CommandResult> Handle(ClientDeleteCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var client = await _clientRepository.GetClientByIdAsync(request.Id);

				if (client == null)
				{
					return CommandResult.NotFound(request.Id);
				}

				await _clientRepository.DeleteClientAsync(request.Id);

				return CommandResult.Success();
			}
			catch (DomainException ex)
			{
				return CommandResult.BusinessFail(ex.Message);
			}
		}
	}
}
