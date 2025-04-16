using DevExpert.Marketplace.Core.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace DevExpert.Marketplace.Core.Helpers;

public static class LoadHelper
{
    public static void LoadSettings(this WebApplicationBuilder builder)
    {
        Settings.Initialize(builder.Configuration.GetSection(nameof(Settings)).Get<Settings>());
        Jwt.Initialize(builder.Configuration.GetSection(nameof(Jwt)).Get<Jwt>());
    }
}