using Application.Services;
using Infrastructure.Extensions;
using Infrastructure.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApi.Extensions;

public static class WebApplicationBuilderExtension
{
    public static WebApplicationBuilder AddJWTAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var serviceProvider = builder.Services.BuildServiceProvider();
                var jwtGenerator = (serviceProvider.GetRequiredService<IJwtGenerator>() as JwtGenerator)!;

                options.MapInboundClaims = false;
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.Events = JwtGenerator.Events;
                options.Audience = JwtGenerator.ClaimsAudience;
                options.ClaimsIssuer = JwtGenerator.ClaimsIssuer;
                options.TokenValidationParameters = jwtGenerator.TokenValidationParameters;
            });
        return builder;
    }

    public static WebApplicationBuilder AddJwtGeneratorService(this WebApplicationBuilder builder)
    {
         builder.Services.AddJwtGenerator();
         return builder;
    }

    public static WebApplicationBuilder AddOneTimePasswordService(this WebApplicationBuilder builder)
    {
        builder.Services.AddOtpGenerator();
        return builder;
    }

    public static WebApplicationBuilder AddEmailSenders(this WebApplicationBuilder builder)
    {
        builder.Services.AddEmailSender();
        return builder;
    }

    public static WebApplicationBuilder AddCacheProvider(this WebApplicationBuilder builder)
    {
        builder.Services.addCustomCacheProvider();
        return builder;
    }
}