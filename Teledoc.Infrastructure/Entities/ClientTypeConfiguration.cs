using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.ClientType;

namespace Teledoc.Infrastructure.Entities
{
	public class ClientTypeConfiguration : IEntityTypeConfiguration<ClientType>
	{
		public void Configure(EntityTypeBuilder<ClientType> builder)
		{

			builder.HasKey(ct => ct.Value);

			builder.Property(ct => ct.Value)
				.HasConversion<int>()
				.IsRequired();

			builder.HasIndex(f => f.Value).IsUnique();
		}
	}
}
