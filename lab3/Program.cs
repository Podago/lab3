using lab3;
using lab3.Interfaces;
using lab3.Services;
using lab3.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var appSettings = builder.Configuration.Get<AppSettings>();

builder.Services
	.AddTransient(provider => appSettings);

builder.Services
	.AddTransient<IÑurrencyService, ÑurrencyService>()
	.AddTransient<IReportService, ReportService>();


builder.Services.AddDbContextFactory<CurrencyDbContext>(options => options.UseSqlServer(appSettings.DataBaseSettings.ConnectionString));

builder.Services.AddControllers();
builder.Services.AddHostedService<BackgroundWorkerService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
