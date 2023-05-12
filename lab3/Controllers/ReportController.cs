using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using lab3.Interfaces;
using System.Globalization;
using lab3.Models;
using System.Collections.Generic;

namespace lab3.Controllers
{
	[Route("api/report")]
	public class ReportController : Controller
	{
		private readonly IReportService _reportService;

		public ReportController(IReportService reportService)
		{
			_reportService = reportService;
		}

		[HttpGet]
		public async Task<ActionResult<List<ReportRow>>> GetReport(string dateFrom, string dateTo)
		{
			DateTime dtFrom;
			DateTime dtTo;

			if (!DateTime.TryParseExact(dateFrom, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtFrom)
				|| !DateTime.TryParseExact(dateTo, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtTo))
				return BadRequest("Date format must be dd.MM.yyyy");

			if (dtTo < dtFrom)
				return BadRequest("dateTo must be greater than or equal to dateFrom");

			var result = await _reportService.GetReport(dtFrom, dtTo);

			return Ok(result);
		}
	}
}
