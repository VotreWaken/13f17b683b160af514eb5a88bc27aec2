using System.ComponentModel.DataAnnotations;

namespace Teledoc.API.DTO
{
	public class UpdateClientDTO
	{
		[Required(ErrorMessage = "Client ID is required.")]
		public int Id { get; set; }

		[Required(ErrorMessage = "INN is required.")]
		[StringLength(12, ErrorMessage = "INN must be 12 characters long.")]
		public string INN { get; set; }

		[Required(ErrorMessage = "Name is required.")]
		[StringLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Client type is required.")]
		public string ClientType { get; set; }

		[Required(ErrorMessage = "Founders list cannot be empty.")]
		public List<FounderDTO> Founders { get; set; }
	}
}
