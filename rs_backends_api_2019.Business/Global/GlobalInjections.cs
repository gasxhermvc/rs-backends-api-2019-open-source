using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Global
{
    public static class GlobalInjections
    {
        private static IApplicationBuilder _applicationBuilder;

        private static IHostingEnvironment _hostingEnvironment;

        private static IServiceCollection _serviceCollection;

        private static IHttpContextAccessor _httpContextAccessor;

        private static IConfiguration _configuration;

        private static ILoggerFactory _loggerFactory;

        private static IMemoryCache _memoryCache;

        public static void configure(IApplicationBuilder applicationBuilder)
        {
            _applicationBuilder = applicationBuilder;
            _hostingEnvironment = _applicationBuilder.ApplicationServices.GetService<IHostingEnvironment>();
            _serviceCollection = _applicationBuilder.ApplicationServices.GetService<IServiceCollection>();
            _httpContextAccessor = _applicationBuilder.ApplicationServices.GetService<IHttpContextAccessor>();
            _configuration = _applicationBuilder.ApplicationServices.GetService<IConfiguration>();
            _loggerFactory = _applicationBuilder.ApplicationServices.GetService<ILoggerFactory>();
            _memoryCache = _applicationBuilder.ApplicationServices.GetService<IMemoryCache>();
        }

        public static IApplicationBuilder getApplicationBuilder()
        {
            return _applicationBuilder;
        }

        public static IHostingEnvironment getHostingEnvironment()
        {
            return _hostingEnvironment;
        }

        public static IServiceCollection getServiceCollection()
        {
            return _serviceCollection;
        }

        public static IHttpContextAccessor getHttpContextAccessor()
        {
            return _httpContextAccessor;
        }

        public static IConfiguration getConfiguration()
        {
            return _configuration;
        }

        public static ILoggerFactory getLoggerFactory()
        {
            return _loggerFactory;
        }

        public static IMemoryCache getMemoryCache()
        {
            return _memoryCache;
        }
    }
}
