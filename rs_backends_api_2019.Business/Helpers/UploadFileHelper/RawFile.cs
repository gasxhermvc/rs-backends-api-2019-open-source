using rs_backends_api_2019.Business.Helpers.UploadFileHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.UploadFileHelper
{
    public class RawFile : IRawFile
    {
        public string fileName { get; set; }

        public byte[] rawBytes { get; set; }

        public object data { get; set; }
    }
}
