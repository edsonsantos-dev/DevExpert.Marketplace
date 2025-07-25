using System.Text;
using DevExpert.Marketplace.Core.Configurations;
using DevExpert.Marketplace.Core.Data.Context;
using DevExpert.Marketplace.Core.Data.Extensions;
using DevExpert.Marketplace.Core.Domain.Images;
using DevExpert.Marketplace.Core.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);

var app = builder.Build();

ConfigureMiddleware(app);

await app.Services.EnsureSeedData();
app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    builder.RegisterDatabaseServices();
    builder.RegisterDependency();
    builder.LoadSettings();

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "API",
            Version = "v1"
        });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Insira 'Bearer {seu token}'"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
    });

    builder.Services.AddDefaultIdentity<IdentityUser>(options => 
        options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<IdentityContext>();

    var key = Encoding.ASCII.GetBytes(Jwt.Instance!.Key);
    builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = Jwt.Instance!.Issuer,
                ValidAudience = Jwt.Instance!.Audience,
            };
        });

    builder.Services.AddAuthorization();

    ImageService.IsWebApi = true;
}

void ConfigureMiddleware(WebApplication app)
{
    var staticFileOptions = new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(
                Directory.GetCurrentDirectory(),
                Settings.Instance!.AppPath,
                Settings.Instance.RootPath)),
        RequestPath = "/DevExpert.Marketplace.App/wwwroot"
    };
    app.UseStaticFiles(staticFileOptions);
    
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
}