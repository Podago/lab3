using lab3.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lab3.Interfaces
{
	public interface IReportService
	{
		Task<List<ReportRow>> GetReport(DateTime dateFrom, DateTime dateTo);
	}
}
