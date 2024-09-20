using MediatR;

namespace Teledoc.Application.Commands
{
	public class ClientDeleteCommand : IRequest<Unit>
	{
		public int Id { get; set; }

		public ClientDeleteCommand(int id)
		{
			Id = id;
		}
	}
}
