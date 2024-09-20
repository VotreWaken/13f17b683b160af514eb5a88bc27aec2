using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Basic;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Composite;

namespace Teledoc.API.DTO
{
	public class CreateClientDTO
	{
		public string INN { get; set; }
		public string Name { get; set; }
		public string ClientType { get; set; }
		public List<FounderDTO> Founders { get; set; }
	}

	public class FounderDTO
	{
		public string INN { get; set; }
		public string FullName { get; set; }
	}
}
