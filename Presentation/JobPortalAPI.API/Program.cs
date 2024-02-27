using FluentValidation.AspNetCore;
using JobPortalAPI.API.Registrations;
using JobPortalAPI.Application.AutoMapper;
using JobPortalAPI.Application.Validators;
using JobPortalAPI.Infrastructure.Registration;
using JobPortalAPI.Persistence.Registration;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using System.Collections.ObjectModel;
using System.Data;
using Serilog.Core;
using JobPortalAPI.API.Extensions;
using JobPortalAPI.API.Middlewares;
using Serilog.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<CreateUserValidator>());
builder.Services.AddPersistanceServices();
builder.Services.AddInfrastructureService();
builder.Services.AddPresentationService();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddEndpointsApiExplorer();

Logger? log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"), sinkOptions: new MSSqlServerSinkOptions
    {
        TableName = "MySerilog",
        AutoCreateSqlTable = true
    },
    null, null, LogEventLevel.Warning, null,
    columnOptions: new ColumnOptions
    {
        AdditionalColumns = new Collection<SqlColumn>
        {
                new SqlColumn(columnName:"UserName", SqlDbType.NVarChar)
        }
    },
     null, null
     )
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();


app.UseSerilogRequestLogging();
app.UseHttpLogging();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("UserName", username);
    await next();
});

app.MapControllers();

app.Run();