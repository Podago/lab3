using lab3.Interfaces;
using System.Threading.Tasks;
using System;
using System.Linq;
using lab3.Models;
using System.Collections.Generic;

namespace lab3.Services
{
	public class ReportService : IReportService
	{
		private readonly IСurrencyService _сurrencyService;

		public ReportService(IСurrencyService сurrencyService)
		{
			_сurrencyService = сurrencyService;
		}

		public async Task<List<ReportRow>> GetReport(DateTime dateFrom, DateTime dateTo)
		{
			var currencies = await _сurrencyService.GetСurrencyBetweenDays(dateFrom, dateTo);

			var result = currencies
				.GroupBy(c => c.CurrencyName)
				.Select(g => new ReportRow
				{
					Currency = g.Key,
					Min = g.Min(c => c.Value),
					Max = g.Max(c => c.Value)
				})
				.ToList();

			return result;
		}
	}
}
