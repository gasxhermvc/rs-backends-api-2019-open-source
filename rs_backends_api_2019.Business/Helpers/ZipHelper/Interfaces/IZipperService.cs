using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.ZipHelper.Interfaces
{
    public interface IZipperService
    {
        IZipperProvider ZipArchive();
    }
}
