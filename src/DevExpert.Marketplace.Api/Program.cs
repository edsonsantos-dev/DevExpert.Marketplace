using System.Text.Json.Serialization;
using DevExpert.Marketplace.IoC;
using DevExpert.Marketplace.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterDatabaseServices();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.RegisterIoC();

var app = builder.Build();

app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
await app.Services.EnsureSeedData();
app.Run();