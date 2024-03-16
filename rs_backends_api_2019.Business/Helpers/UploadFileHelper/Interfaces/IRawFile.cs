using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.UploadFileHelper.Interfaces
{
    public interface IRawFile
    {
        string fileName { get; set; }

        byte[] rawBytes { get; set; }

        object data { get; set; }
    }
}
