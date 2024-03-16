using rs_backends_api_2019.Business.Helpers.ZipHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.ZipHelper
{
    public class ZipperService : IZipperService
    {
        public IZipperProvider ZipArchive()
        {
            return new ZipperProvider() as IZipperProvider;
        }
    }
}
