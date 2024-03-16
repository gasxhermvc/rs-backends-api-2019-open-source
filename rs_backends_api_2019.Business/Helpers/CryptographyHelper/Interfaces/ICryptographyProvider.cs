using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.CryptographyHelper.Interfaces
{
    public interface ICryptographyProvider
    {
        //=>ถอดรหัส
        string Decrypt(string cipherText);

        //=>เข้ารหัส
        string Encrypt(string plainText);
    }
}
