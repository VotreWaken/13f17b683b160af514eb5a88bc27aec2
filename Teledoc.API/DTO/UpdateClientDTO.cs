using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Basic;

namespace Teledoc.API.DTO
{
	public class UpdateClientDTO
	{
		public int Id { get; set; }
		public string INN { get; set; }
		public string Name { get; set; }
		public string ClientType { get; set; }
		public List<FounderDTO> Founders { get; set; }
	}
}
