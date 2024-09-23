using MediatR;
using Teledoc.Application.Results;
using Teledoc.Domain.BoundedContexts.ClientManagement.Aggregates;

namespace Teledoc.Application.Commands
{
	public class ClientUpdateCommand : IRequest<CommandResult>
	{
		public int Id { get; set; }
		public string INN { get; set; }
		public string Name { get; set; }
		public string ClientType { get; set; }

		public IEnumerable<Founder> Founders { get; set; }
		public ClientUpdateCommand(int id, string INN, string name, string clientType, IEnumerable<Founder> founders)
		{
			Id = id;
			this.INN = INN;
			Name = name;
			ClientType = clientType;
			Founders = founders;
		}
	}
}
