using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.UploadFileHelper.Interfaces
{
    public interface IUploadFileService
    {
        string getStoragePath(string path = "");

        IUploadFileProvider make(IFormFileCollection collection);

        List<IUploadFileProvider> makeCollection(IFormFileCollection collection);

        List<IUploadFileProvider> store(string path);

        IUploadFileProvider make(IRawFile rawFile);

        bool exists(string pathFile);

        string link(string path);

        string notFoundImage();

        string combine(params string[] path);

        string combineUrl(params string[] path);
    }
}
