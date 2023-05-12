using lab3.Interfaces;
using lab3.Services;
using lab3.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

public class BackgroundWorkerService : BackgroundService
{
	readonly ILogger<BackgroundWorkerService> _logger;

	private readonly IСurrencyService _сurrencyService;

	private readonly AppSettings _appSettings;

	public BackgroundWorkerService(ILogger<BackgroundWorkerService> logger
		, IСurrencyService сurrencyService
		, AppSettings appSettings)
	{
		_logger = logger;
		_сurrencyService = сurrencyService;
		_appSettings = appSettings;
	}

	protected async override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			_logger.LogInformation("Task started {time}", DateTimeOffset.Now);

			await _сurrencyService.GetСurrencyByCurrentDay();

			await Task.Delay(_appSettings.ScheduleSettings.Period * 1000, stoppingToken);
		}
	}
}