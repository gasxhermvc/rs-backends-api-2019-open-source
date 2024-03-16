using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace rs_backends_api_2019.Business.Helpers.LoggerHelper.Middleware.Routes
{
    /// <summary>
    /// Support Routes external middleware
    /// Keeping logging 404
    /// </summary>
    public class NotFoundMiddleware : IRouter
    {
        private readonly ILoggerFactory _loggerFactory;

        private readonly IRouter _defaultRouter;

        public NotFoundMiddleware(IRouter defaultRouter, ILoggerFactory loggerFactory)
        {
            this._loggerFactory = loggerFactory;
            this._defaultRouter = defaultRouter;
        }

        public async Task RouteAsync(RouteContext context)
        {
            this._loggerFactory.CreateLogger("Middleware.Routes.NotFound").LogWarning("404");

            await _defaultRouter.RouteAsync(context);
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return _defaultRouter.GetVirtualPath(context);
        }
    }
}
