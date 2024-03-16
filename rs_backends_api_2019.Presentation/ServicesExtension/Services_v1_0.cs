using Microsoft.Extensions.DependencyInjection;
using rs_backends_api_2019.Business.Interfaces.Mock;
using rs_backends_api_2019.Business.Services.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs_backends_api_2019.Presentation.ServicesExtension
{
    public static class Services_v1_0
    {
        public static IServiceCollection AddServices_v1_0(this IServiceCollection services)
        {
            //*** Injection Mockup Service
            services.AddScoped<IFirstPageService, FirstPageService>();

            return services;
        }
    }
}
