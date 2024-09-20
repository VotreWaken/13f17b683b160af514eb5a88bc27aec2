using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			modelBuilder.Entity<Client>()
				.HasMany(c => c.Founders)
				.WithOne(f => f.Client)
				.HasForeignKey(f => f.ClientId);

			modelBuilder.Entity<Client>()
				.HasOne(c => c.ClientType)
				.WithMany(ct => ct.Clients)
				.HasForeignKey(c => c.ClientTypeId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<ClientType>()
				.Property(ct => ct.Id)
				.HasColumnName("Id")
				.ValueGeneratedOnAdd();

			modelBuilder.Entity<ClientType>().HasData(
				new ClientType { Id = 1, Name = "IndividualEntrepreneur" },
				new ClientType { Id = 2, Name = "LegalEntity" }
			);
		}
	}
}
