using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teledoc.Infrastructure.Entities
{
	[Table("Founders")]
	public class Founder
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(12)]
		public string INN { get; set; } = string.Empty;

		[Required]
		[StringLength(255)]
		public string FullName { get; set; } = string.Empty;

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

		public int ClientId { get; set; }

		[ForeignKey("ClientId")]
		public Client Client { get; set; }
	}
}
