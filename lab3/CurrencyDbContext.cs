using Microsoft.EntityFrameworkCore;
using lab3.Models;

namespace lab3
{
	public class CurrencyDbContext : DbContext
	{
		public DbSet<DbCurrency> Currencies { get; set; }

		public CurrencyDbContext(DbContextOptions<CurrencyDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			CurrencyModelBuilder.Build(modelBuilder);
		}
	}
}
