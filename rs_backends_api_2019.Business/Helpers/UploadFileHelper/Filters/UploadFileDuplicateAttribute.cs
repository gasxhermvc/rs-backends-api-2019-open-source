using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using rs_backends_api_2019.Business.Models.Defaults.BaseModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.UploadFileHelper.Filters
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class UploadFileDuplicateAttribute : ActionFilterAttribute
    {
        private ErrorResponse errResponse { get; set; } = new ErrorResponse();

        private bool _AllowDuplicate { get; set; }

        private string _ErrorMessage { get; set; }

        public UploadFileDuplicateAttribute(bool AllowDuplicate = false, string ErrorMessage = "")
        {
            this._AllowDuplicate = AllowDuplicate;
            this._ErrorMessage = ErrorMessage;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;

            if (!this._AllowDuplicate && httpContext.Request.HasFormContentType &&
                httpContext.Request.Form.Files != null && httpContext.Request.Form.Files.Count >= 1)
            {
                int duplicate = httpContext.Request.Form.Files
                    .Select(s => s.FileName.Trim())
                    .GroupBy(g => g)
                    .Count(w => w.Count() > 1);

                if (duplicate >= 1)
                {
                    errResponse.error.code = (int)HttpStatusCode.BadRequest;
                    errResponse.error.message = !(string.IsNullOrEmpty(this._ErrorMessage)) ?
                        this._ErrorMessage :
                        this.GetTranslate();

                    context.Result = new BadRequestObjectResult(errResponse);
                    return;
                }
            }

            base.OnActionExecuting(context);
        }

        private string GetTranslate()
        {
            return "Sorry, Files uploaded have name file duplicate.";
        }
    }
}
