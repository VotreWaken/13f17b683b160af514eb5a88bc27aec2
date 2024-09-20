using MediatR;

namespace Teledoc.Application.Queries
{
	public class GetClientByIdQuery : IRequest<Teledoc.Infrastructure.Entities.Client>
	{
		public int Id { get; set; }

		public GetClientByIdQuery(int id)
		{
			Id = id;
		}
	}
}
