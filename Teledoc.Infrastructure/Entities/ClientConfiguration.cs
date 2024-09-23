using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Teledoc.Domain.BoundedContexts.ClientManagement.Aggregates;
using System.Reflection.Emit;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.ClientType;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Composite;

namespace Teledoc.Infrastructure.Entities
{
	public class ClientConfiguration : IEntityTypeConfiguration<Client>
	{
		public void Configure(EntityTypeBuilder<Client> builder)
		{
			builder.HasKey(c => c.Id);

			builder.Property(c => c.ClientTypeId)
				.HasConversion<int>()
				.IsRequired();

			builder.Ignore(c => c.ClientType);

			builder.HasOne<ClientType>()
				.WithMany()
				.HasForeignKey(c => c.ClientTypeId)
				.HasPrincipalKey(ct => ct.ClientTypeIdValue);

			builder.OwnsOne(c => c.INN, inn =>
			{
				inn.Property(c => c.Value)
					.HasColumnName("INN")
					.HasMaxLength(12)
					.IsRequired();

				inn.Ignore(i => i.SubjectCode);
				inn.Ignore(i => i.TaxOfficeCode);
				inn.Ignore(i => i.SerialNumber);
				inn.Ignore(i => i.CheckDigit);

				inn.HasCheckConstraint("CK_Client_INN_Length", "LEN(INN) = 12");
			});

			builder.HasMany(c => c.Founders)
				.WithOne()
				.HasForeignKey(f => f.ClientId);

			builder.HasIndex(c => c.Id).IsUnique();
		}
	}
}
