using lab3.Models;
using System.Collections.Generic;
using System.Linq;

namespace lab3.Extensions
{
	public static class DbCurrencyExtensions
	{
		public static Currency ToCurrency(this DbCurrency dbCurrency)
		{
			return new Currency
			{
				CurrencyName = dbCurrency.CurrencyName,

				Date = dbCurrency.Date,

				Value = dbCurrency.Value
			};
		}

		public static List<Currency> ToCurrency(this List<DbCurrency> dbCurrency)
		{
			return dbCurrency != null && dbCurrency.Any()
				? dbCurrency.Select(ToCurrency).ToList()
				: null;
		}
	}
}
