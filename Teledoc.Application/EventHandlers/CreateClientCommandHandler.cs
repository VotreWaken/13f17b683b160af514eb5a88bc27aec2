using MediatR;
using Teledoc.Application.Commands;
using Teledoc.Infrastructure.Repository;

namespace Teledoc.Application.EventHandlers
{
	public class CreateClientCommandHandler : IRequestHandler<ClientCreateCommand, int>
	{
		private readonly IClientRepository _clientRepository;
		private readonly IFounderRepository _founderRepository;
		public CreateClientCommandHandler(IClientRepository clientRepository, IFounderRepository founderRepository)
		{
			_clientRepository = clientRepository;
			_founderRepository = founderRepository;
		}

		public async Task<int> Handle(ClientCreateCommand request, CancellationToken cancellationToken)
		{
			var client = new Domain.BoundedContexts.ClientManagement.Aggregates.Client(
				request.INN,
				request.Name,
				request.ClientType
			);

			var clientTypeEntity = ClientTypeMapper.ToEntity(client.ClientType.Value);
			Console.WriteLine(clientTypeEntity.Name.ToString());
			Console.WriteLine(clientTypeEntity.Id.ToString());
			Console.WriteLine(client.ClientType.Value.ToString());

			Teledoc.Infrastructure.Entities.Client clientEntity = new Infrastructure.Entities.Client
			{
				INN = client.INN.ToString(),
				Name = client.Name,
				ClientTypeId = clientTypeEntity.Id,
				// ClientType = clientTypeEntity,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow,
			};

			var clientId = await _clientRepository.AddClientAsync(clientEntity);

			foreach (var founder in request.Founders)
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

			return clientId;
		}
	}
}
