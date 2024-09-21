using MediatR;
using Teledoc.Application.Mappings;
using Teledoc.Application.Results;
using Teledoc.Domain.BoundedContexts.ClientManagement.Exceptions;
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

				var clientDto = ClientEntityToDTOMapper.ToDto(client);

				return CommandResult.Success(clientDto);
			}
			catch (DomainException ex)
			{
				return CommandResult.BusinessFail(ex.Message);
			}
		}
	}
}
