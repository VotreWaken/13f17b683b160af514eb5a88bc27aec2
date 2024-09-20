using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teledoc.Infrastructure.Entities
{
	[Table("ClientTypes")]
	public class ClientType
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(255)]
		public string Name { get; set; }

		public ICollection<Client> Clients { get; set; }
	}
}
