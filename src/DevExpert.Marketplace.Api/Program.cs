using DevExpert.Marketplace.App.Extensions;
using DevExpert.Marketplace.Application.Helpers;
using DevExpert.Marketplace.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterDatabaseServices();
builder.RegisterIoC();
builder.LoadSettings();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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