using rs_backends_api_2019.Business.Helpers.ZipHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.ZipHelper
{
    public class ZipperProvider : IZipperProvider
    {
        public Zipper Zip()
        {
            return new Zipper();
        }
    }
}
