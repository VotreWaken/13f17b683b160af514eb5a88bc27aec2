using MediatR;
using Teledoc.Application.Results;

namespace Teledoc.Application.Commands
{
	public class ClientCreateCommand : IRequest<CommandResult>
	{
		public string INN { get; set; }
		public string Name { get; set; }
		public string ClientType { get; set; }
		public List<FounderCreateCommand> Founders { get; set; } = new List<FounderCreateCommand>();
		public ClientCreateCommand(string iNN, string name, string clientType, List<FounderCreateCommand> founders)
		{
			INN = iNN;
			Name = name;
			ClientType = clientType;
			Founders = founders;
		}
	}

	public class FounderCreateCommand
	{
		public string INN { get; set; }
		public string FullName { get; set; }

		public FounderCreateCommand(string inn, string fullName)
		{
			INN = inn;
			FullName = fullName;
		}
	}
}
