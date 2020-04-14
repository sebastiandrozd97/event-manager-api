using EventmanagerApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace EventmanagerApi.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options)
			: base(options)
		{
		}
		
		public DbSet<OrganizedEvent> OrganizedEvents { get; set; }
	}
}