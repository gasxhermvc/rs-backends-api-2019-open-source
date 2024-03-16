using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.UploadFileHelper.Interfaces
{
    public interface IUploadFileProvider
    {
        string ObjectID { get; }

        string FileName { get; set; }

        long Length { get; set; }

        string FullName { get; set; }

        string Target { get; set; }

        string Extension { get; set; }

        string MimeType { get; set; }

        int Width { get; set; }

        int Height { get; set; }

        IRawFile File { get; set; }

        DocumentTypes documentType { get; set; }

        #region for object
        IUploadFileProvider resize(int width, int height);

        IUploadFileProvider resize(string size);

        IUploadFileProvider save();

        IUploadFileProvider save(string path, bool uniqueName = false);

        IUploadFileProvider save(ImageFormat format, bool uniqueName = false);

        IUploadFileProvider save(string path, ImageFormat format, bool uniqueName = false);

        IUploadFileProvider setPath(string path);

        string getLink();

        bool delete();
        #endregion
    }
}
