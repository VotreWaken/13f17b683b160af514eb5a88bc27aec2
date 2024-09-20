using AutoMapper;
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
		private readonly IMapper _mapper;
		public CreateClientCommandHandler(IClientRepository clientRepository, IFounderRepository founderRepository, IMapper mapper)
		{
			_clientRepository = clientRepository;
			_founderRepository = founderRepository;
			_mapper = mapper;
		}

		public async Task<CommandResult> Handle(ClientCreateCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var client = CreateClient(request);

				var clientEntity = _mapper.Map<Teledoc.Infrastructure.Entities.Client>(client);

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
