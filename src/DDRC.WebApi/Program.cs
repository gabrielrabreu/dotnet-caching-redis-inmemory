using DDRC.WebApi.Scope.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDDRCControllers();
builder.Services.AddDDRCSwagger();
builder.Services.AddDDRCServices();

builder.Logging.AddDDRCSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDDRCSwagger();
}

app.MapControllers();

app.Run();
