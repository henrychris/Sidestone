using System.Reflection;
using Sidestone.Host.Configuration;
using Sidestone.Host.Data;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment.EnvironmentName;

// The configuration providers in .NET form a hierarchy.
// The providers that are added later can override the settings from the providers that were added earlier.
builder
    .Configuration.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables()
    .Build();
builder.ConfigureSerilog(builder.Environment);

builder.Services.SetupConfigFiles();

// setup database
builder.Services.SetupDatabase<DataContext>();
builder.Services.SetupControllers();
builder.Services.AddSwagger();
builder.Services.SetupFilters();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// todo: setup identity
// todo: setup authentication
builder.Services.AddIdentity();
builder.Services.RegisterServices();
builder.Services.SetupJsonOptions();

var app = builder.Build();
app.RegisterSwagger();
app.RegisterMiddleware();

// seed db here if needed
await app.ApplyMigrationsAsync<DataContext>();
app.Run();

// this is here for integration tests
public partial class Program;
