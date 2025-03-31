using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace DevExpert.Marketplace.Application.Helpers;

public static class SettingsLoadHelper
{
    public static void LoadSettings(this WebApplicationBuilder builder)
    {
        Settings.Initialize(builder.Configuration.GetSection(nameof(Settings)).Get<Settings>());
    }
}