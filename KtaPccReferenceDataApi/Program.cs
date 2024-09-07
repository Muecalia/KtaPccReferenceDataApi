using KtaPccReferenceDataApi.Config;
using KtaPccReferenceDataApi.Infraestrutura.Context;
using KtaPccReferenceDataApi.Infraestrutura.Interfaces;
using KtaPccReferenceDataApi.Infraestrutura.Repositories;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using Serilog;
using TotalAgilityApi.RabbitMq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CONNECTION STRING
builder.Services.AddDbContext<KtaPccReferenceDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KtaPccReferenceDataConnetion"),
    sqlServerOptionsAction: sqlOption => { sqlOption.EnableRetryOnFailure(); sqlOption.CommandTimeout(3600); }));


//INJECÇÃO DE DEPENDÊNCIA
builder.Services.AddScoped<IProcessoRepository, ProcessoRepository>();
builder.Services.AddScoped<IEstatisticaRepository, EstatisticaRepository>();


//LOGS
var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// RabbitMQ
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMqSettings"));
builder.Services.AddTransient<IRabbitMqService, RabbitMqService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Unitel Kta Reference Data v1"));
}

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials

app.UseAuthorization();

//Map the Prometheus metrics endpoint
// Adiciona o middleware para métricas
app.UseMiddleware<MetricsMiddleware>();
app.UseHttpMetrics();
app.MapMetrics();

app.MapControllers();

app.Run();
