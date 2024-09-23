using MediatR;
using Teledoc.Application.Results;
using Teledoc.Domain.BoundedContexts.ClientManagement.Aggregates;

namespace Teledoc.Application.Commands
{
	public class ClientCreateCommand : IRequest<CommandResult>
	{
		public string INN { get; set; }
		public string Name { get; set; }
		public string ClientType { get; set; }
		public IEnumerable<Founder> Founders { get; set; }
		public ClientCreateCommand(string iNN, string name, string clientType, IEnumerable<Founder> founders)
		{
			INN = iNN;
			Name = name;
			ClientType = clientType;
			Founders = founders;
		}
	}
}
