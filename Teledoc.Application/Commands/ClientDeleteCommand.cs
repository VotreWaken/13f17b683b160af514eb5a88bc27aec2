using MediatR;
using Teledoc.Application.Results;

namespace Teledoc.Application.Commands
{
	public class ClientDeleteCommand : IRequest<CommandResult>
	{
		public int Id { get; set; }

		public ClientDeleteCommand(int id)
		{
			Id = id;
		}
	}
}
