using Microsoft.AspNetCore.Http;
using rs_backends_api_2019.Business.Helpers.UploadFileHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.UploadFileHelper.Extensions
{
    public static class UploadFileBuilderExtension
    {
        // render new image size
        public static Bitmap ToBitmap(this IRawFile file)
        {
            Bitmap bitmap = null;

            using (MemoryStream ms = new MemoryStream(file.rawBytes))
            {
                Image image = Image.FromStream(ms, true, true);

                var width = image.Width;
                var height = image.Height;

                bitmap = new Bitmap(width, height);

                using (var g = Graphics.FromImage(bitmap))
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.TextRenderingHint = TextRenderingHint.AntiAlias;
                    g.DrawImage(image, 0, 0, width, height);
                }

                ms.Close();
                ms.Dispose();
            }

            return bitmap;
        }

        // render new image size and custom width , height
        public static Bitmap ToBitmap(this IRawFile file, int width, int height)
        {
            Bitmap bitmap = null;

            using (MemoryStream ms = new MemoryStream(file.rawBytes))
            {
                Image image = Image.FromStream(ms, true, true);

                bitmap = new Bitmap(width, height);

                using (var g = Graphics.FromImage(bitmap))
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.TextRenderingHint = TextRenderingHint.AntiAlias;
                    g.DrawImage(image, 0, 0, width, height);
                }

                ms.Close();
                ms.Dispose();
            }

            return bitmap;
        }

        // create bytes from Stream
        public static IRawFile createRawFileFromStream(MemoryStream stream, string fileName, object data = null)
        {
            return new RawFile()
            {
                fileName = fileName,
                rawBytes = stream.ToArray(),
                data = data,
            } as IRawFile;
        }

        // create bytes from IFormFile
        public static IRawFile createRawFileFromIFromFile(IFormFile formFile, object data = null)
        {
            IRawFile model = null;

            using (MemoryStream ms = new MemoryStream())
            {
                formFile.CopyTo(ms);

                model = createRawFileFromStream(ms, formFile.FileName, null);

                ms.Close();
                ms.Dispose();
            }

            return model;
        }

        // create bytes from file
        public static IRawFile createRawFileFromFile(string pathFile, object data = null)
        {
            IRawFile model = null;

            var bytes = System.IO.File.ReadAllBytes(pathFile);
            var fileName = System.IO.Path.GetFileName(pathFile);

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                model = createRawFileFromStream(ms, fileName, null);

                ms.Close();
                ms.Dispose();
            }

            return model;
        }

        // convert IRawFile object from FileInfo
        public static IRawFile ToIRawFile(this FileInfo file)
        {
            IRawFile rawFile = null;

            var fileName = file.Name;
            var extension = UploadFileExtension.getExtension(fileName);
            var mimeType = extension.GetMimeType();

            var bytes = System.IO.File.ReadAllBytes(file.FullName);

            //var ms = new MemoryStream();
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                rawFile = createRawFileFromStream(ms, fileName, null);

                ms.Close();
                ms.Dispose();
            }

            return rawFile;
        }

        // convert IRawFile object from IRawFile
        public static IRawFile ToIRawFile(this IRawFile file)
        {
            IRawFile rawFile = null;

            var fileName = file.fileName;
            var extension = UploadFileExtension.getExtension(fileName);
            var mimeType = extension.GetMimeType();

            //var ms = new MemoryStream();
            using (MemoryStream ms = new MemoryStream(file.rawBytes))
            {
                rawFile = createRawFileFromStream(ms, fileName, null);

                ms.Close();
                ms.Dispose();
            }

            return rawFile;
        }

        // convert IRawFile object from IRawFile
        public static IRawFile ToIRawFile(this IRawFile file, string fileName)
        {
            IRawFile rawFile = null;

            var extension = UploadFileExtension.getExtension(fileName);
            var mimeType = extension.GetMimeType();

            using (MemoryStream ms = new MemoryStream(file.rawBytes))
            {
                rawFile = createRawFileFromStream(ms, fileName, null);

                ms.Close();
                ms.Dispose();
            }

            return rawFile;
        }

        // convert IRawFile object from bitmap
        public static IRawFile ToIFormFile(this IRawFile file, Bitmap bitmap, string fileName = "")
        {
            IRawFile rawFile = null;

            var extension = UploadFileExtension.getExtension(file.fileName);
            var mimeType = UploadFileExtension.getMimeType(extension);
            var format = UploadFileExtension.getImageFormat(extension);
            fileName = string.IsNullOrEmpty(fileName) ? file.fileName : fileName;

            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, format);

                rawFile = createRawFileFromStream(ms, fileName, null);

                ms.Close();
                ms.Dispose();
            }

            return rawFile;
        }

        // convert IRawFile object from bitmap with image format
        public static IRawFile ToIFormFile(this IRawFile file, Bitmap bitmap, ImageFormat format, string fileName = "")
        {
            IRawFile rawFile = null;

            var extension = UploadFileExtension.getExtension(file.fileName);
            var mimeType = UploadFileExtension.getMimeType(extension);
            fileName = string.IsNullOrEmpty(fileName) ? file.fileName : fileName;

            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, format);

                rawFile = createRawFileFromStream(ms, fileName, null);

                ms.Close();
                ms.Dispose();
            }

            return rawFile;
        }

        // create new instane of UploadFileProvider custom path
        public static IUploadFileProvider ToIUploadFileProvider(this IUploadFileProvider provider, string path)
        {
            IUploadFileProvider objProvider = provider;
            UploadFileProvider model = new UploadFileProvider();

            int width = 0, height = 0;
            var fullName = (!string.IsNullOrEmpty(path) && path.isFile()) ? path : model.combine(path, objProvider.FileName);
            var fileName = System.IO.Path.GetFileName(fullName);
            var extension = UploadFileExtension.getExtension(fileName);
            var mimeType = extension.GetMimeType();
            var target = (!string.IsNullOrEmpty(path) && path.isFile()) ? System.IO.Path.GetDirectoryName(path) : path;
            var isImage = extension.IsImage();
            var file = provider.File.ToIRawFile(fileName);

            if (isImage)
            {
                using (MemoryStream ms = new MemoryStream(file.rawBytes))
                {
                    using (Bitmap bit = new Bitmap(ms))
                    {
                        width = bit.Width;
                        height = bit.Height;

                        bit.Dispose();
                    }
                }

                model.FileName = fileName;
                model.Extension = extension;
                model.FullName = fullName;
                model.Length = file.rawBytes.Length;
                model.MimeType = mimeType;
                model.Target = target;
                model.File = file;
                model.Width = width;
                model.Height = height;
                model.documentType = UploadFileMimeTypeExtension.GetDocumentType(extension);
            }
            else
            {
                model.FileName = fileName;
                model.Extension = extension;
                model.FullName = fullName;
                model.Length = file.rawBytes.Length;
                model.MimeType = mimeType;
                model.Target = target;
                model.File = file;
                model.Width = 0;
                model.Height = 0;
                model.documentType = UploadFileMimeTypeExtension.GetDocumentType(extension);
            }

            return model;
        }

        // create new instane of UploadFileProvider for resize with bitmap
        public static IUploadFileProvider ToIUploadFileProvider(this IUploadFileProvider provider, Bitmap bitmap)
        {
            IUploadFileProvider objProvider = provider;
            UploadFileProvider model = new UploadFileProvider();

            int width = bitmap.Width, height = bitmap.Height;
            var fullName = objProvider.FullName;
            var fileName = objProvider.FileName;
            var extension = UploadFileExtension.getExtension(fileName);
            var mimeType = objProvider.MimeType;
            var target = objProvider.Target;
            var isImage = extension.IsImage();
            var file = objProvider.File.ToIFormFile(bitmap);

            if (isImage)
            {
                model.FileName = fileName;
                model.Extension = extension;
                model.FullName = fullName;
                model.Length = file.rawBytes.Length;
                model.MimeType = mimeType;
                model.Target = target;
                model.File = file;
                model.Width = width;
                model.Height = height;
                model.documentType = UploadFileMimeTypeExtension.GetDocumentType(extension);
            }
            else
            {
                model.FileName = fileName;
                model.Extension = extension;
                model.FullName = fullName;
                model.Length = file.rawBytes.Length;
                model.MimeType = mimeType;
                model.Target = target;
                model.File = file;
                model.Width = 0;
                model.Height = 0;
                model.documentType = UploadFileMimeTypeExtension.GetDocumentType(extension);
            }

            return model;
        }

        // create new instane of UploadFileProvider  for resize with bitmap and custom image format
        public static IUploadFileProvider ToIUploadFileProvider(this IUploadFileProvider provider, Bitmap bitmap, ImageFormat format)
        {
            IUploadFileProvider objProvider = provider;
            UploadFileProvider model = new UploadFileProvider();

            var formatterExtension = UploadFileExtension.getFromImageFormat(format);
            int width = bitmap.Width, height = bitmap.Height;
            var fullName = objProvider.FullName;
            var fileName = objProvider.FileName;
            var extension = UploadFileExtension.getExtension(fileName);
            var mimeType = extension.GetMimeType();
            var target = objProvider.Target;
            var isImage = extension.IsImage();

            fileName = fileName.Replace(extension, formatterExtension);
            fullName = fullName.Replace(extension, formatterExtension);
            var file = objProvider.File.ToIFormFile(bitmap, format, fileName);

            if (isImage)
            {
                model.FileName = fileName;
                model.Extension = formatterExtension;
                model.FullName = fullName;
                model.Length = file.rawBytes.Length;
                model.MimeType = mimeType;
                model.Target = target;
                model.File = file;
                model.Width = width;
                model.Height = height;
                model.documentType = UploadFileMimeTypeExtension.GetDocumentType(formatterExtension);
            }
            else
            {

                model.FileName = fileName;
                model.Extension = formatterExtension;
                model.FullName = fullName;
                model.Length = file.rawBytes.Length;
                model.MimeType = mimeType;
                model.Target = target;
                model.File = file;
                model.Width = 0;
                model.Height = 0;
                model.documentType = UploadFileMimeTypeExtension.GetDocumentType(formatterExtension);
            }

            return model;
        }

        // create new instane of UploadFileProvider  for resize with bitmap and custom image format and custom path
        public static IUploadFileProvider ToIUploadFileProvider(this IUploadFileProvider provider, Bitmap bitmap, ImageFormat format, string path)
        {
            IUploadFileProvider objProvider = provider as UploadFileProvider;
            UploadFileProvider model = new UploadFileProvider();

            var formatterExtension = UploadFileExtension.getFromImageFormat(format);

            int width = bitmap.Width, height = bitmap.Height;
            var fullName = (!string.IsNullOrEmpty(path) && path.isFile()) ? path : model.combine(path, objProvider.FileName);
            var fileName = System.IO.Path.GetFileName(fullName);
            var extension = UploadFileExtension.getExtension(fileName);
            var mimeType = extension.GetMimeType();
            var target = (!string.IsNullOrEmpty(path) && path.isFile()) ? System.IO.Path.GetDirectoryName(path) : path;
            var isImage = extension.IsImage();

            fileName = fileName.Replace(extension, formatterExtension);
            fullName = fullName.Replace(extension, formatterExtension);
            var file = objProvider.File.ToIFormFile(bitmap, format, fileName);

            if (isImage)
            {
                model.FileName = fileName;
                model.Extension = formatterExtension;
                model.FullName = fullName;
                model.Length = file.rawBytes.Length;
                model.MimeType = mimeType;
                model.Target = target;
                model.File = file;
                model.Width = width;
                model.Height = height;
                model.documentType = UploadFileMimeTypeExtension.GetDocumentType(formatterExtension);
            }
            else
            {
                model.FileName = fileName;
                model.Extension = formatterExtension;
                model.FullName = fullName;
                model.Length = file.rawBytes.Length;
                model.MimeType = mimeType;
                model.Target = target;
                model.File = file;
                model.Width = 0;
                model.Height = 0;
                model.documentType = UploadFileMimeTypeExtension.GetDocumentType(formatterExtension);
            }

            return model;
        }
    }
}
