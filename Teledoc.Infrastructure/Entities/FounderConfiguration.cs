using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Teledoc.Domain.BoundedContexts.ClientManagement.Aggregates;

namespace Teledoc.Infrastructure.Entities
{
	public class FounderConfiguration : IEntityTypeConfiguration<Founder>
	{
		public void Configure(EntityTypeBuilder<Founder> builder)
		{
			builder.HasKey(f => f.Id);

			builder.Ignore(f => f.Clients);

			builder.Property(f => f.Id)
				.ValueGeneratedOnAdd();

			builder.OwnsOne(c => c.INN, inn =>
			{
				inn.Property(i => i.Value)
					.HasColumnName("INN")
					.HasMaxLength(12)
					.IsRequired();

				inn.Ignore(i => i.SubjectCode);
				inn.Ignore(i => i.TaxOfficeCode);
				inn.Ignore(i => i.SerialNumber);
				inn.Ignore(i => i.CheckDigit);

				inn.HasCheckConstraint("CK_Founder_INN_Length", "LEN(INN) = 12");
			});

			builder.OwnsOne(f => f.FullName, inn =>
			{
				inn.Property(i => i.Value)
					.HasColumnName("UserName")
					.HasMaxLength(64)
					.IsRequired();

				inn.Ignore(i => i.First);
				inn.Ignore(i => i.Last);
				inn.Ignore(i => i.Patronymic);
			});

			builder.HasIndex(f => f.Id).IsUnique();
		}
	}
}
