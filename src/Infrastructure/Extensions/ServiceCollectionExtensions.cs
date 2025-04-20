using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services;
using Infrastructure.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtGenerator(this IServiceCollection service)=>
            service.AddScoped<IJwtGenerator, JwtGenerator>();

        public static IServiceCollection AddOtpGenerator(this IServiceCollection service) =>
            service.AddScoped<IOneTimePasswordService, OneTimePasswordService>();

        public static IServiceCollection AddEmailSender(this IServiceCollection service)=>
            service.AddScoped<IEmailSender, EmailSender>();

        public static IServiceCollection addCustomCacheProvider(this IServiceCollection service) =>
            service.AddScoped<ICacheProvider, CacheProvider>();
    }
}
