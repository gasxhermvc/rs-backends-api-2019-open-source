using rs_backends_api_2019.Business.Helpers.CryptographyHelper.Extensions;
using rs_backends_api_2019.Business.Helpers.LoggerHelper.Filters;
using rs_backends_api_2019.Business.Helpers.UploadFileHelper;
using rs_backends_api_2019.Business.Helpers.UploadFileHelper.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs_backends_api_2019.Presentation.ServicesExtension
{
    public static class RegisterHelpers
    {
        public static IServiceCollection AddHelpers(this IServiceCollection services, IHostingEnvironment hostEnv)
        {
            services.AddScoped<LogFactory, LogFactory>();

            services.AddScoped<IUploadFileService, UploadFileService>();

            //services.AddScoped<IinitialData, InitialData>();

            //services.AddScoped<IAuth, Auth>();

            //services.AddScoped<IPermission, Permission>();

            //*** Inject PDF
            //services.AddExportPDFServices(hostEnv);

            //=>Inject Cache helper
            //services.AddCacheFactory();

            //=>Inject Cryptography helper
            //services.AddCryptography();

            return services;
        }
    }
}
