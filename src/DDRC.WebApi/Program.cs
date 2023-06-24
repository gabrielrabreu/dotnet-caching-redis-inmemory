using DDRC.WebApi.Data.Seed;
using DDRC.WebApi.Scope.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDDRCControllers();
builder.Services.AddDDRCSwagger();
builder.Services.AddDDRCServices(builder.Configuration);
builder.Services.AddDDRCCache(builder.Configuration);

builder.Logging.AddDDRCSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDDRCSwagger();
}

app.MapControllers();

app.Services.InitializeDatabase();
app.Services.SeedData();

app.Run();
