using MediatR;
using Teledoc.Application.Commands;
using Teledoc.Application.Results;
using Teledoc.Domain.BoundedContexts.ClientManagement.Exceptions;
using Teledoc.Domain.BoundedContexts.ClientManagement.Interfaces;
using Teledoc.Domain.BoundedContexts.ClientManagement.Aggregates;
namespace Teledoc.Application.EventHandlers
{
    public class CreateClientCommandHandler : IRequestHandler<ClientCreateCommand, CommandResult>
	{
		private readonly IClientRepository _clientRepository;
		private readonly IFounderRepository _founderRepository;
		public CreateClientCommandHandler(IClientRepository clientRepository, 
			IFounderRepository founderRepository)
		{
			_clientRepository = clientRepository;
			_founderRepository = founderRepository;
		}

		public async Task<CommandResult> Handle(ClientCreateCommand request, 
			CancellationToken cancellationToken)
		{
			try
			{
				var client = new Client( request.INN, request.Name,
				request.ClientType, request.Founders );
				await _clientRepository.AddClientAsync(client);
			
				return CommandResult.Success();
			}
			catch (DomainException ex)
			{
				return CommandResult.BusinessFail(ex.Message);
			}
		}
	}
}
