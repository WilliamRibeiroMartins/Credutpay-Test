using Credutpay_Test.Entities;
using Microsoft.EntityFrameworkCore;

namespace Credutpay_Test.Infrastructure
{
	public class AppDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Transfer> Transfers { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.HasMany(u => u.SentTransfers)
				.WithOne(t => t.FromUser)
				.HasForeignKey(t => t.FromUserId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<User>()
				.HasMany(u => u.ReceivedTransfers)
				.WithOne(t => t.ToUser)
				.HasForeignKey(t => t.ToUserId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
