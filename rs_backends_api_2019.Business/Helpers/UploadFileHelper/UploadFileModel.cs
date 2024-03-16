using rs_backends_api_2019.Business.Helpers.UploadFileHelper.Extensions;
using rs_backends_api_2019.Business.Helpers.UploadFileHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.UploadFileHelper
{
    public class UploadFileModel
    {
        public string ObjectID { get; } = Guid.NewGuid().ToString();

        public string FileName { get; set; }

        public long Length { get; set; }

        private string _FullName;

        public string FullName
        {
            get
            {
                return UploadFileExtension.clearForPath(this._FullName);
            }

            set
            {
                this._FullName = value;
            }
        }

        private string _Target;

        public string Target
        {
            get
            {
                return UploadFileExtension.clearForPath(this._Target);
            }

            set
            {
                this._Target = value;
            }
        }

        public string Extension { get; set; }

        public string MimeType { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public IRawFile File { get; set; }

        public DocumentTypes documentType { get; set; }
    }
}
