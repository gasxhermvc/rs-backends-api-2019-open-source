using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.CryptographyHelper.Interfaces
{
    public interface ICryptographyService
    {
        /// <summary>
        /// สร้างอินสแตนซ์เพื่อใช้งานการเข้ารหัสดวย AES-256-CBC
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        ICryptographyProvider CryptHelper(IConfiguration configuration);

        /// <summary>
        /// สร้างอินสแตนซ์เพื่อใช้งานการเข้ารหัสด้วย Hash แบบ BCrypt
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        IBcryptProvider BcryptHelper(IConfiguration configuration);
    }
}
