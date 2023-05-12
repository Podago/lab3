using System;

namespace lab3.Models
{
	public class DbCurrency
	{
		public int Id { get; set; }

		public DateTime Date { get; set; }

		public string CurrencyName { get; set; }

		public decimal Value { get; set; }
	}
}
