using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace rs_backends_api_2019.Business.Helpers.LoggerHelper.Filters.Routes
{
    /// <summary>
    /// Support attriubute OnAuthoization 
    /// Keeping logging 401
    /// </summary>
    public class AuthenticationFilter : IAuthorizationFilter, IOrderedFilter
    {
        private readonly ILoggerFactory _loggerFactory;

        public AuthenticationFilter(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public int Order => 5;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            StringValues authorizationToken = string.Empty;
            var authorization = context.HttpContext.Request?.Headers.TryGetValue("Authorization", out authorizationToken);

            if (string.IsNullOrEmpty(authorizationToken))
            {
                _loggerFactory.CreateLogger("Middleware.Routes.UnAuthorization").LogWarning("401");
            }
        }
    }
}
