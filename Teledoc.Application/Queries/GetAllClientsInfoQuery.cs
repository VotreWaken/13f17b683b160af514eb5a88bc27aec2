using MediatR;
using Teledoc.Application.Results;

namespace Teledoc.Application.Queries
{
	public class GetAllClientsInfoQuery : IRequest<CommandResult>
	{
	}
}
