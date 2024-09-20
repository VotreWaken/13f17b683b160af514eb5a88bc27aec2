using MediatR;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Basic;
using Teledoc.Infrastructure.Repository;
using Teledoc.Application.Results;
using Teledoc.Domain.BoundedContexts.ClientManagement.Exceptions;
using static Teledoc.Application.Commands.ClientUpdateCommand;
using AutoMapper;
using Teledoc.Domain.BoundedContexts.ClientManagement.Aggregates;

namespace Teledoc.Application.Commands
{
	public class UpdateClientCommandHandler : IRequestHandler<ClientUpdateCommand, CommandResult>
	{
		private readonly IClientRepository _clientRepository;
		private readonly IFounderRepository _founderRepository;
		private readonly IMapper _mapper;

		public UpdateClientCommandHandler(IClientRepository context, IFounderRepository founderRepository, IMapper mapper)
		{
			_clientRepository = context;
			_founderRepository = founderRepository;
			_mapper = mapper;
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

				Console.WriteLine($"Client Type Id: {clientEntity.ClientTypeId}");

				var client = new Client(clientEntity.INN, clientEntity.Name, clientEntity.ClientTypeId);

				client.UpdateClient(request.INN, request.Name, request.ClientType);

				await _clientRepository.UpdateClientAsync(_mapper.Map<Teledoc.Infrastructure.Entities.Client>(client));

				await UpdateFoundersAsync(clientEntity, request.Founders);

				return CommandResult.Success();
			}
			catch (DomainException ex)
			{
				return CommandResult.BusinessFail(ex.Message);
			}
		}

		private async Task UpdateFoundersAsync(Infrastructure.Entities.Client client, IEnumerable<FounderUpdateCommand> founders)
		{
			var existingFounders = client.Founders.ToList();

			foreach (var founder in existingFounders)
			{
				if (!founders.Any(f => f.Id == founder.Id))
				{
					await _founderRepository.DeleteFounderAsync(founder.Id);
				}
			}

			foreach (var founder in founders)
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
	}
}
