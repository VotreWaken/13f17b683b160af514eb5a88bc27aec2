using MediatR;
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
				return CommandResult.Success(await _clientRepository.GetClientByIdAsync(request.Id));
			}
			catch (DomainException ex)
			{
				return CommandResult.BusinessFail(ex.Message);
			}
		}
	}
}
