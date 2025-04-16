using DevExpert.Marketplace.Core.Configurations;
using DevExpert.Marketplace.Core.Data.Context;
using DevExpert.Marketplace.Core.Data.Extensions;
using DevExpert.Marketplace.Core.Domain.Images;
using DevExpert.Marketplace.Core.Helpers;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterDatabaseServices();
builder.RegisterDependency();
builder.LoadSettings();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<IdentityContext>();
builder.Services.AddControllersWithViews();

ImageService.IsWebApi = false;

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
    .WithStaticAssets();

await app.Services.EnsureSeedData();

app.Run();