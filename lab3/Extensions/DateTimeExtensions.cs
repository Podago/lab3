using System.Collections.Generic;
using System;

namespace lab3.Extensions
{
	public static class DateTimeExtensions
	{
		public static IEnumerable<DateTime> GetDatesToDate(this DateTime dateFrom, DateTime dateTo)
		{
			if (dateTo < dateFrom)
				throw new ArgumentException("endDate must be greater than or equal to startDate");

			while (dateFrom <= dateTo)
			{
				yield return dateFrom;
				dateFrom = dateFrom.AddDays(1);
			}
		}
	}
}
