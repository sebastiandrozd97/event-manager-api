using EventmanagerApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EventmanagerApi.Data
{
	public class DataContext : DbContext
	{
		public static readonly ILoggerFactory MyLoggerFactory
			= LoggerFactory.Create(builder => { builder.AddConsole(); });
		
		public DataContext(DbContextOptions<DataContext> options)
			: base(options)
		{
		}
		
		public DbSet<OrganizedEvent> OrganizedEvents { get; set; }
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder
				.UseLoggerFactory(MyLoggerFactory);

		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<ApplicationUser>().Ignore(c => c.AccessFailedCount)
				.Ignore(c => c.EmailConfirmed)
				.Ignore(c => c.SecurityStamp)
				.Ignore(c => c.ConcurrencyStamp)
				.Ignore(c => c.PhoneNumber)
				.Ignore(c => c.PhoneNumberConfirmed)
				.Ignore(c => c.TwoFactorEnabled)
				.Ignore(c => c.LockoutEnd)
				.Ignore(c => c.LockoutEnabled)
				.Ignore(c => c.AccessFailedCount);

			modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUser");

			modelBuilder.Entity<OrganizedEvent>().ToTable("OrganizedEvent");

		}
	}
}