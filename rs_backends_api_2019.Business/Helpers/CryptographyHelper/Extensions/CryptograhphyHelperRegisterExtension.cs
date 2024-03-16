using Microsoft.Extensions.DependencyInjection;
using rs_backends_api_2019.Business.Helpers.CryptographyHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.CryptographyHelper.Extensions
{
    public static class CryptographyHelperRegisterExtension
    {
        /// <summary>
        /// อินเจกต์การใช้งานเข้ารหัส
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCryptography(this IServiceCollection services)
        {
            services.AddScoped<ICryptographyService, CryptographyService>();

            return services;
        }
    }
}
