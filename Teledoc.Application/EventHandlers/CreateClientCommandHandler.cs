using MediatR;
using Teledoc.Application.Commands;
using Teledoc.Application.Results;
using Teledoc.Domain.BoundedContexts.ClientManagement.Exceptions;
using Teledoc.Infrastructure.Repository;

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
				var client = CreateClient(request);

				var clientTypeEntity = ClientTypeMapper.ToEntity(client.ClientType.Value);

				Teledoc.Infrastructure.Entities.Client clientEntity = new()
				{
					INN = client.INN.ToString(),
					Name = client.Name,
					ClientTypeId = clientTypeEntity.Id,
					CreatedAt = DateTime.UtcNow,
					UpdatedAt = DateTime.UtcNow,
				};

				var clientId = await _clientRepository.AddClientAsync(clientEntity);

				await AddFoundersAsync(request.Founders, clientId);

				return CommandResult.Success(clientId);
			}
			catch (DomainException ex)
			{
				return CommandResult.BusinessFail(ex.Message);
			}
		}

		private Domain.BoundedContexts.ClientManagement.Aggregates.Client CreateClient(ClientCreateCommand request)
		{
			return new Domain.BoundedContexts.ClientManagement.Aggregates.Client(
				request.INN,
				request.Name,
				request.ClientType
			);
		}
		private async Task AddFoundersAsync(IEnumerable<FounderCreateCommand> founders, int clientId)
		{
			foreach (var founder in founders)
			{
				var founderEntity = new Infrastructure.Entities.Founder
				{
					INN = founder.INN,
					FullName = founder.FullName,
					ClientId = clientId,
					CreatedAt = DateTime.UtcNow,
					UpdatedAt = DateTime.UtcNow,
				};

				await _founderRepository.AddFounderAsync(founderEntity);
			}
		}
	}
}
