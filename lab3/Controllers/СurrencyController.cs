using lab3.Interfaces;
using lab3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace lab3.Controllers
{
	[Route("api/currency")]
	public class СurrencyController : Controller
	{
		private readonly IСurrencyService _сurrencyService;

		public СurrencyController(IСurrencyService сurrencyService)
		{
			_сurrencyService = сurrencyService;
		}

		[HttpGet]
		public async Task<ActionResult<List<Currency>>> GetСurrencyByDay(string date)
		{
			DateTime dt;

			if (!DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
				return BadRequest("Date format must be dd.MM.yyyy");

			var result = await _сurrencyService.GetСurrencyByDay(dt);

			return Ok(result);
		}

		[HttpGet("range")]
		public async Task<ActionResult<List<Currency>>> GetСurrencyBetweenDays(string dateFrom, string dateTo)
		{
			DateTime dtFrom;
			DateTime dtTo;

			if (!DateTime.TryParseExact(dateFrom, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtFrom)
				|| !DateTime.TryParseExact(dateTo, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtTo))
				return BadRequest("Date format must be dd.MM.yyyy");

			if (dtTo < dtFrom)
				return BadRequest("dateTo must be greater than or equal to dateFrom");

			var result = await _сurrencyService.GetСurrencyBetweenDays(dtFrom, dtTo);

			return Ok(result);
		}
	}
}