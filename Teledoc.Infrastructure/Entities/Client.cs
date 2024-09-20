using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teledoc.Infrastructure.Entities
{
	[Table("Clients")]
	public class Client
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		[StringLength(12, MinimumLength = 10, ErrorMessage = "INN must be either 10 or 12 characters long.")]
		public string INN { get; set; }

		[Required]
		[StringLength(255)]
		public string Name { get; set; }

		[Required]
		public int ClientTypeId { get; set; }

		[ForeignKey("ClientTypeId")]
		public ClientType ClientType { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

		public List<Founder> Founders { get; set; } = new List<Founder>();
	}
}
