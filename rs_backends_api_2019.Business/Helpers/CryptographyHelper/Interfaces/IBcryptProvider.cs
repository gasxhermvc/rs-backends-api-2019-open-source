using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.CryptographyHelper.Interfaces
{
    public interface IBcryptProvider
    {
        string Hash(string input);

        bool Check(string input, string hashing);
    }
}
