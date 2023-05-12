using lab3.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace lab3
{
	public class CurrencyModelBuilder
	{
		internal static void Build(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<DbCurrency>(entity =>
			{
				entity
					.ToTable("Currencies")
				.HasKey(x => x.Id);

				entity.Property(x => x.Id).IsRequired().HasColumnName("Id");

				entity.Property(x => x.Date).IsRequired().HasColumnName("Date");

				entity.Property(x => x.CurrencyName).IsRequired().HasColumnName("CurrencyName");

				entity.Property(x => x.Value).IsRequired().HasColumnName("Value");
			});
		}
	}
}