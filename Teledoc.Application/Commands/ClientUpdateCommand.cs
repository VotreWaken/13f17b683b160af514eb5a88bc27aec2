using MediatR;
using Teledoc.Application.Results;

namespace Teledoc.Application.Commands
{
	public class ClientUpdateCommand : IRequest<CommandResult>
	{
		public int Id { get; set; }
		public string INN { get; set; }
		public string Name { get; set; }
		public string ClientType { get; set; }

		public List<FounderUpdateCommand> Founders { get; set; } = new List<FounderUpdateCommand>();
		public ClientUpdateCommand(int id, string INN, string name, string clientType, List<FounderUpdateCommand> founders)
		{
			Id = id;
			this.INN = INN;
			Name = name;
			ClientType = clientType;
			Founders = founders;
		}

		public class FounderUpdateCommand
		{
			public int Id { get; set; }
			public string INN { get; set; }
			public string FullName { get; set; }

			public FounderUpdateCommand(string inn, string fullName)
			{
				INN = inn;
				FullName = fullName;
			}
		}
	}
}
