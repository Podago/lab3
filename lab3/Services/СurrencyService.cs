using lab3.Extensions;
using lab3.Interfaces;
using lab3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
namespace lab3.Services
{
	public class СurrencyService : IСurrencyService
	{
		private readonly CurrencyDbContext _currencyDbContext;

		public СurrencyService(CurrencyDbContext currencyDbContext)
		{
			_currencyDbContext = currencyDbContext;
		}

		public async Task<List<Currency>> GetСurrencyBetweenDays(DateTime dateFrom, DateTime dateTo)
		{
			var dates = dateFrom.GetDatesToDate(dateTo).ToList();

			List<Currency> currencies = new List<Currency>();

			foreach (var date in dates)
			{
				var currency = await GetСurrencyByDay(date);
				currencies.AddRange(currency);
			}

			return currencies;
		}

		public async Task<List<Currency>> GetСurrencyByDay(DateTime date)
		{
			if (await _currencyDbContext.Currencies.FirstOrDefaultAsync(c => c.Date == date) != null)
			{
				var dbCurrencies = _currencyDbContext
					.Currencies
					.Where(c => c.Date == date)
					.ToList();

				return dbCurrencies.ToCurrency();
			}

			var dateString = date.ToString("dd.MM.yyyy");

			var url = $"https://www.cnb.cz/en/financial_markets/foreign_exchange_market/exchange_rate_fixing/daily.txt?date={dateString}";

			HttpClient client = new HttpClient();

			HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false);

			var exchangeRate = await response.Content.ReadAsStringAsync();

			var currencies = ParseCurrencyString(exchangeRate, date);

			await _currencyDbContext
				.Currencies
				.AddRangeAsync(currencies.ToDbCurrency());

			await _currencyDbContext.SaveChangesAsync();

			return currencies;
		}

		private List<Currency> ParseCurrencyString(string exchangeRateString, DateTime date)
		{
			var currencyString = exchangeRateString.Split('\n');

			var currencies = new List<Currency>();

			for (int i = 2; i < currencyString.Length; i++)
			{
				var column = currencyString[i].Split('|');

				if (column.Length != 5)
					continue;

				var currencyName = $"{column[0]} {column[1]}";
				var amount = column[2];
				var rate = column[4];

				decimal rateDecimal = decimal.Parse(rate, CultureInfo.InvariantCulture);
				decimal amountDecimal = decimal.Parse(amount, CultureInfo.InvariantCulture);

				var value = rateDecimal / amountDecimal;

				var currency = new Currency
				{
					Date = date,
					CurrencyName = currencyName,
					Value = value
				};

				currencies.Add(currency);
			}

			return currencies;
		}
	}
}