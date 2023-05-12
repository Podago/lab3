using lab3.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lab3.Interfaces
{
	public interface IСurrencyService
	{
		Task<List<Currency>> GetСurrencyByDay(DateTime date);

		Task<List<Currency>> GetСurrencyBetweenDays(DateTime dateFrom, DateTime dateTo);
	}
}
