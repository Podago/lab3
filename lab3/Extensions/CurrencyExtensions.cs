using lab3.Models;
using System.Collections.Generic;
using System.Linq;

namespace lab3.Extensions
{
	public static class CurrencyExtensions
	{
		public static DbCurrency ToDbCurrency(this Currency currency)
		{
			return new DbCurrency
			{
				CurrencyName = currency.CurrencyName,

				Date = currency.Date,

				Value = currency.Value
			};
		}

		public static List<DbCurrency> ToDbCurrency(this List<Currency> currency)
		{
			return currency != null && currency.Any()
				? currency.Select(ToDbCurrency).ToList()
				: null;
		}
	}
}
