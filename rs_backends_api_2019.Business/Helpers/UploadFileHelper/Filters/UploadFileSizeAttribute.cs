using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using rs_backends_api_2019.Business.Global;
using rs_backends_api_2019.Business.Models.Defaults.BaseModel.Responses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.UploadFileHelper.Filters
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class UploadFileSizeAttribute : ActionFilterAttribute
    {
        private readonly IConfiguration _configuration;

        private long _TotalFileSize { get; set; }

        private long _FileSize { get; set; }

        private ErrorResponse errResponse { get; set; } = new ErrorResponse();

        public UploadFileSizeAttribute(long TotalFileSize = 0, long FileSize = 0)
        {
            _configuration = GlobalInjections.getConfiguration();
            _TotalFileSize = TotalFileSize;
            _FileSize = FileSize;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;

            if (httpContext.Request.HasFormContentType && httpContext.Request.Form.Files != null &&
               httpContext.Request.Form.Files.Count >= 1)
            {
                var totalFileSize = this._TotalFileSize == 0 ? long.Parse(_configuration["Uploads:totalFileSize"]) : this._TotalFileSize;
                var fileSize = this._FileSize == 0 ? long.Parse(_configuration["Uploads:fileSize"]) : this._FileSize;

                // check request size
                if (httpContext.Request.ContentLength >= totalFileSize)
                {
                    errResponse.error.message = this.GetTranslateOverTotalFileSize(totalFileSize);
                    errResponse.error.code = (int)HttpStatusCode.BadRequest;

                    context.Result = new BadRequestObjectResult(errResponse);
                    return;
                }

                var isOverFile = false;

                // check file size
                foreach (var file in httpContext.Request.Form.Files)
                {
                    if (file.Length >= fileSize)
                    {
                        isOverFile = true;
                        break;
                    }
                }

                if (isOverFile)
                {
                    errResponse.error.message = this.GetTranslateOverFileSize(fileSize);
                    errResponse.error.code = (int)HttpStatusCode.BadRequest;

                    context.Result = new BadRequestObjectResult(errResponse);
                    return;
                }
            }

            base.OnActionExecuting(context);
        }

        private string GetTranslateOverTotalFileSize(long limitSize)
        {
            return $"Allowed upload many file, which not over {(((double)limitSize / 1024.0) / 1024.0).ToString("N2")} MB size.";
        }

        private string GetTranslateOverFileSize(long limitSize)
        {
            return $"Not allowed file size over {(((double)limitSize / 1024.0) / 1024.0).ToString("N2")} MB size.";
        }
    }
}
