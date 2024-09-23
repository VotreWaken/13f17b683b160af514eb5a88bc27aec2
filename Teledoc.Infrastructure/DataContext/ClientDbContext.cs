using Microsoft.EntityFrameworkCore;
using Teledoc.Domain.BoundedContexts.ClientManagement.Aggregates;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.ClientType;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.ClientType.Enums;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Composite;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.INN;
using Teledoc.Infrastructure.Entities;

namespace Teledoc.Infrastructure.DataContext
{
	public class ClientDbContext : DbContext
	{
		public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options) { }

		public DbSet<Client> Clients { get; set; }
		public DbSet<Founder> Founders { get; set; }
		public DbSet<ClientType> ClientTypes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientDbContext).Assembly);

			modelBuilder.Entity<ClientType>().HasData(
				new
				{
					Value = ClientTypeEnum.IndividualEntrepreneur,
					Name = "IndividualEntrepreneur",
					ClientTypeIdValue = (int)ClientTypeEnum.IndividualEntrepreneur,
				},
				new
				{
					Value = ClientTypeEnum.LegalEntity,
					Name = "LegalEntity",
					ClientTypeIdValue = (int)ClientTypeEnum.LegalEntity,
				});

			base.OnModelCreating(modelBuilder);
		}
	}
}
