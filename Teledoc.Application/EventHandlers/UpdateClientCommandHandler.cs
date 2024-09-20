using MediatR;
using Teledoc.Infrastructure.DataContext;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Basic;
using Teledoc.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Teledoc.Application.Commands
{
	public class UpdateClientCommandHandler : IRequestHandler<ClientUpdateCommand, Unit>
	{
		private readonly IClientRepository _clientRepository;
		private readonly IFounderRepository _founderRepository;

		public UpdateClientCommandHandler(IClientRepository context, IFounderRepository founderRepository)
		{
			_clientRepository = context;
			_founderRepository = founderRepository;
		}

		public async Task<Unit> Handle(ClientUpdateCommand request, CancellationToken cancellationToken)
		{
			var client = await _clientRepository.GetClientByIdAsync(request.Id);
			if (client == null)
			{
				// throw new NotFoundException(nameof(Client), request.Id);
			}

			client.INN = request.INN;
			client.Name = request.Name;
			if (Enum.TryParse(request.ClientType, out ClientTypeEnum clientTypeEnum))
			{
				var clientTypeEntity = ClientTypeMapper.ToEntity(clientTypeEnum);
				client.ClientType = clientTypeEntity;
			}
			else
			{
				throw new ArgumentException("Invalid client type", nameof(request.ClientType));
			}

			if (request.Founders != null)
			{
				var existingFounders = client.Founders.ToList();
				foreach (var founder in existingFounders)
				{
					if (!request.Founders.Any(f => f.Id == founder.Id))
					{
						await _founderRepository.DeleteFounderAsync(founder.Id);
					}
				}

				foreach (var founder in request.Founders)
				{
					var founderEntity = existingFounders.FirstOrDefault(f => f.Id == founder.Id);
					if (founderEntity != null)
					{
						founderEntity.INN = founder.INN;
						founderEntity.FullName = founder.FullName;
						founderEntity.UpdatedAt = DateTime.UtcNow;
					}
					else
					{
						var newFounder = new Infrastructure.Entities.Founder
						{
							INN = founder.INN,
							FullName = founder.FullName,
							ClientId = client.Id,
							CreatedAt = DateTime.UtcNow,
							UpdatedAt = DateTime.UtcNow,
						};
						client.Founders.Add(newFounder);
					}
				}
			}

			client.UpdatedAt = DateTime.UtcNow;

			await _clientRepository.UpdateClientAsync(client);

			return Unit.Value;
		}
	}
}
