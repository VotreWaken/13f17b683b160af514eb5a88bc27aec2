using MediatR;
using Teledoc.Application.Commands;
using Teledoc.Infrastructure.DataContext;

namespace Teledoc.Application.EventHandlers
{
	public class DeleteClientCommandHandler : IRequestHandler<ClientDeleteCommand, Unit>
	{
		private readonly ClientDbContext _context;

		public DeleteClientCommandHandler(ClientDbContext context)
		{
			_context = context;
		}

		public async Task<Unit> Handle(ClientDeleteCommand request, CancellationToken cancellationToken)
		{
			var client = await _context.Clients.FindAsync(request.Id);

			if (client == null)
			{
				throw new KeyNotFoundException($"Client with Id {request.Id} not found.");
			}

			_context.Clients.Remove(client);
			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
