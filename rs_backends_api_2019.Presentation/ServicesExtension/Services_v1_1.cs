using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs_backends_api_2019.Presentation.ServicesExtension
{
    public static class Services_v1_1
    {
        public static IServiceCollection AddServices_v1_1(this IServiceCollection services)
        {
            //*** Injection v1.1 Service
            //services.AddScoped<IFilterService, FilterService>();

            //=>injection services

            return services;
        }
    }
}
