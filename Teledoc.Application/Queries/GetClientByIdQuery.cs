using MediatR;
using Teledoc.Application.Results;

namespace Teledoc.Application.Queries
{
	public class GetClientByIdQuery : IRequest<CommandResult>
	{
		public int Id { get; set; }

		public GetClientByIdQuery(int id)
		{
			Id = id;
		}
	}
}
