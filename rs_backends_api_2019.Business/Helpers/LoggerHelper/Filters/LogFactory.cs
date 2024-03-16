using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rs_backends_api_2019.Business.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.LoggerHelper.Filters
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class LogFactory : ActionFilterAttribute
    {
        private LoggerEnum _loggerEnum { get; set; }

        private readonly ILoggerFactory _loggerFactory;

        public LogFactory(LoggerEnum loggerEnum)
        {
            _loggerEnum = loggerEnum;
            _loggerFactory = GlobalInjections.getLoggerFactory();
        }

        // Using before Activity
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (_loggerEnum == LoggerEnum.Activity)
            {
                _loggerFactory.CreateLogger("Activity").LogInformation("-");
            }

            base.OnActionExecuting(context);
        }

        // Using Service - Loggin for keeper result
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            if (_loggerEnum == LoggerEnum.Service)
            {
                _loggerFactory.CreateLogger("Service").LogInformation("{0}",
                    JsonConvert.SerializeObject(context.Result, Formatting.None));
            }

            base.OnResultExecuted(context);
        }
    }
}
