using Acme.Resource.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Acme.Resource.Data
{
	public class AcmeResourceDbContext : DbContext
	{
		public AcmeResourceDbContext(DbContextOptions<AcmeResourceDbContext> options) : base(options)
		{
		}

		public DbSet<Employee> Employees => Set<Employee>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			foreach (var entity in modelBuilder.Model.GetEntityTypes())
			{
				entity.SetTableName(entity.DisplayName());
			}
		}
	}
}
