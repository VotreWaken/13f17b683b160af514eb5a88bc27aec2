using MediatR;
using Teledoc.Infrastructure.Entities;

namespace Teledoc.Application.Queries
{
	public class GetAllProductsInfosQuery : IRequest<IEnumerable<Client>>
	{
	}
}
