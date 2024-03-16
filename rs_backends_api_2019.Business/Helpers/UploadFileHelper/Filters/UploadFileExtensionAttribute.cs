using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using rs_backends_api_2019.Business.Helpers.UploadFileHelper.Extensions;
using rs_backends_api_2019.Business.Models.Defaults.BaseModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.UploadFileHelper.Filters
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class UploadFileExtensionAttribute : ActionFilterAttribute
    {
        private ErrorResponse errResponse { get; set; } = new ErrorResponse();

        private DocumentTypes _DocumentType { get; set; }

        private string _AllowExtension { get; set; }

        private string _ErrorMessage { get; set; }

        public UploadFileExtensionAttribute(string AllowExtension = "*", string ErrorMessage = "")
        {
            _AllowExtension = AllowExtension;
            _DocumentType = DocumentTypes.None;
            _ErrorMessage = ErrorMessage;
        }

        public UploadFileExtensionAttribute(DocumentTypes DocumentType, string ErrorMessage = "")
        {
            _DocumentType = DocumentType;
            _ErrorMessage = ErrorMessage;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;

            if (httpContext.Request.HasFormContentType && httpContext.Request.Form.Files != null &&
                httpContext.Request.Form.Files.Count >= 1)
            {
                this._AllowExtension = this._DocumentType == DocumentTypes.None ?
                        this.GetDocumentFromTypes(this._DocumentType, this._AllowExtension) : this.GetDocumentFromTypes(this._DocumentType, this._AllowExtension);
                this._AllowExtension = UploadFileMimeTypeExtension.ClearExtension(this._AllowExtension);
                this._ErrorMessage = (!string.IsNullOrEmpty(this._ErrorMessage)) ? this._ErrorMessage : this.GetTranslate(this._DocumentType);

                bool isAllowed = true;
                var mimeTypes = this.GetMimeTypes(this._AllowExtension);

                foreach (var file in httpContext.Request.Form.Files)
                {
                    if (!mimeTypes.Any(a => file.FileName.ToLower().EndsWith(a)))
                    {
                        isAllowed = false;
                        break;
                    }
                }

                if (!isAllowed)
                {
                    errResponse.error.message = this._ErrorMessage;
                    errResponse.error.code = (int)HttpStatusCode.BadRequest;

                    context.Result = new BadRequestObjectResult(errResponse);
                    return;
                }
            }
            base.OnActionExecuting(context);
        }

        private string GetTranslate(DocumentTypes document)
        {
            string translate = string.Empty;

            switch (document)
            {
                case DocumentTypes.All:
                    translate = $"Please enter files with {UploadFileMimeTypeExtension.GetExtensionFromDocumentType("all", " , ")} extension only.";
                    break;
                case DocumentTypes.Document:
                    translate = $"Please enter files with {UploadFileMimeTypeExtension.GetExtensionFromDocumentType("document", " , ")} extension only.";
                    break;
                case DocumentTypes.Image:
                    translate = $"Please enter files with {UploadFileMimeTypeExtension.GetExtensionFromDocumentType("image", " , ")} extension only.";
                    break;
                case DocumentTypes.Archive:
                    translate = $"Please enter files with {UploadFileMimeTypeExtension.GetExtensionFromDocumentType("archive", " , ")} extension only.";
                    break;
                case DocumentTypes.Audio:
                    translate = $"Please enter files with {UploadFileMimeTypeExtension.GetExtensionFromDocumentType("audio", " , ")} extension only.";
                    break;
                case DocumentTypes.Video:
                    translate = $"Please enter files with {UploadFileMimeTypeExtension.GetExtensionFromDocumentType("video", " , ")} extension only.";
                    break;
                case DocumentTypes.None:
                    translate = $"Please enter files with {this._AllowExtension.Replace("|", " , ")} extension only.";
                    break;
                default:
                    translate = $"Please enter files with {UploadFileMimeTypeExtension.GetExtensionFromDocumentType("all", " , ")} extension only.";
                    break;
            }

            return translate;
        }

        private List<string> GetMimeTypes(string mimeTypes)
        {
            return mimeTypes.Split('|').Select(s => s.ToLower()).ToList();
        }

        private string GetDocumentFromTypes(DocumentTypes document, string customizeExtension = "")
        {
            string allowedExtension = string.Empty;

            if (customizeExtension == "*")
            {
                document = DocumentTypes.All;
            }

            if (document == DocumentTypes.All || document == DocumentTypes.None)
            {
                allowedExtension = UploadFileMimeTypeExtension.GetExtensionFromDocumentType("all", "|");
                allowedExtension = (!string.IsNullOrEmpty(customizeExtension) && document == DocumentTypes.None) ?
                          customizeExtension.Trim() : allowedExtension;
            }

            if (string.IsNullOrEmpty(allowedExtension))
            {
                switch (document)
                {
                    case DocumentTypes.Document:
                        allowedExtension = UploadFileMimeTypeExtension.GetExtensionFromDocumentType("document", "|");
                        break;
                    case DocumentTypes.Image:
                        allowedExtension = UploadFileMimeTypeExtension.GetExtensionFromDocumentType("image", "|");
                        break;
                    case DocumentTypes.Archive:
                        allowedExtension = UploadFileMimeTypeExtension.GetExtensionFromDocumentType("archive", "|");
                        break;
                    case DocumentTypes.Audio:
                        allowedExtension = UploadFileMimeTypeExtension.GetExtensionFromDocumentType("audio", "|");
                        break;
                    case DocumentTypes.Video:
                        allowedExtension = UploadFileMimeTypeExtension.GetExtensionFromDocumentType("video", "|");
                        break;
                }
            }

            return allowedExtension.Replace(",", "|");
        }
    }
}
