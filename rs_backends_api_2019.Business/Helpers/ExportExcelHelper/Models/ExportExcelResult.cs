using rs_backends_api_2019.Business.Helpers.UploadFileHelper;
using rs_backends_api_2019.Business.Helpers.UploadFileHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.ExportExcelHelper.Models
{
    public class ExportExcelResult
    {
        /// <summary>
        /// export สำเร็จ / ล้มเหลว
        /// </summary>
        public bool success { get; set; } = false;

        /// <summary>
        /// bytes[] 
        /// </summary>
        public IRawFile file { get; set; } = new RawFile();

        /// <summary>
        /// Exception
        /// </summary>
        public Exception exception { get; set; } = null;
    }
}
