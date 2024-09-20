using System.ComponentModel.DataAnnotations;

namespace Teledoc.API.DTO
{
	public class DeleteClientDTO
	{
		[Required(ErrorMessage = "Client ID is required.")]
		public int Id { get; set; }
	}
}
