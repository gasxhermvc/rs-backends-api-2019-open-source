using Microsoft.AspNetCore.Http;
using rs_backends_api_2019.Business.Extensions;
using rs_backends_api_2019.Business.Global;
using rs_backends_api_2019.Business.Helpers.UploadFileHelper.Extensions;
using rs_backends_api_2019.Business.Helpers.UploadFileHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.UploadFileHelper
{
    public class UploadFileProvider : UploadFileModel, IUploadFileProvider
    {
        #region create object
        /// <summary>
        /// สร้างออปเจกต์ IUploadFileProvider แบบ IEnumerable จาก ออปเจกต์ IFormFileCollection
        /// </summary>
        /// <param name="fileCollection"></param>
        /// <param name="path"></param>
        /// <returns>IEnumerable<IUploadFileProvider></returns>
        public IEnumerable<IUploadFileProvider> convertToUploadFileProvider(IFormFileCollection fileCollection, string path = "")
        {
            List<IUploadFileProvider> files = new List<IUploadFileProvider>();

            path = (!string.IsNullOrEmpty(path)) ? path : this.getStoragePath();

            foreach (var file in fileCollection)
            {
                int width = 0, height = 0;
                var fullName = this.combine(path, file.FileName);
                var extension = UploadFileExtension.getExtension(file.FileName);
                var mimeType = UploadFileExtension.getMimeType(extension);
                var isImage = extension.IsImage();
                var rawFile = UploadFileBuilderExtension.createRawFileFromIFromFile(file, null);

                if (isImage)
                {
                    using (MemoryStream ms = new MemoryStream(rawFile.rawBytes))
                    {
                        using (Bitmap bit = new Bitmap(ms))
                        {
                            width = bit.Width;
                            height = bit.Height;

                            bit.Dispose();
                        }

                        ms.Close();
                        ms.Dispose();
                    }

                    files.Add(new UploadFileProvider
                    {
                        FileName = file.FileName,
                        Extension = extension,
                        FullName = fullName,
                        Length = rawFile.rawBytes.Length,
                        MimeType = mimeType,
                        Target = path,
                        File = rawFile,
                        Width = width,
                        Height = height,
                        documentType = UploadFileMimeTypeExtension.GetDocumentType(extension)
                    });
                }
                else
                {
                    files.Add(new UploadFileProvider
                    {
                        FileName = file.FileName,
                        Extension = extension,
                        FullName = fullName,
                        Length = rawFile.rawBytes.Length,
                        MimeType = mimeType,
                        Target = path,
                        File = rawFile,
                        Width = 0,
                        Height = 0,
                        documentType = UploadFileMimeTypeExtension.GetDocumentType(extension)
                    });
                }
            }

            return files;
        }

        /// <summary>
        /// สร้างออปเจกต์ IUploadFileProvider แบบ IEnumerable จาก ออปเจกต์ IFormFile แบบ IEumerable
        /// </summary>
        /// <param name="fileCollection"></param>
        /// <param name="path"></param>
        /// <returns>IEnumerable<IUploadFileProvider></returns>
        public IEnumerable<IUploadFileProvider> convertToUploadFileProvider(IEnumerable<IRawFile> fileCollection, string path = "")
        {
            List<IUploadFileProvider> files = new List<IUploadFileProvider>();

            path = (!string.IsNullOrEmpty(path)) ? path : this.getStoragePath();

            foreach (var file in fileCollection)
            {
                int width = 0, height = 0;
                var fullName = this.combine(path, file.fileName);
                var extension = UploadFileExtension.getExtension(file.fileName);
                var mimeType = UploadFileExtension.getMimeType(extension);
                var isImage = extension.IsImage();
                var rawFile = UploadFileBuilderExtension.createRawFileFromFile(fullName, null);

                if (isImage)
                {
                    using (MemoryStream ms = new MemoryStream(rawFile.rawBytes))
                    {
                        using (Bitmap bit = new Bitmap(ms))
                        {
                            width = bit.Width;
                            height = bit.Height;

                            bit.Dispose();
                        }

                        ms.Close();
                        ms.Dispose();
                    }

                    files.Add(new UploadFileProvider
                    {
                        FileName = file.fileName,
                        Extension = extension,
                        FullName = fullName,
                        Length = rawFile.rawBytes.Length,
                        MimeType = mimeType,
                        Target = path,
                        File = rawFile,
                        Width = width,
                        Height = height,
                        documentType = UploadFileMimeTypeExtension.GetDocumentType(extension)
                    });
                }
                else
                {
                    files.Add(new UploadFileProvider
                    {
                        FileName = file.fileName,
                        Extension = extension,
                        FullName = fullName,
                        Length = rawFile.rawBytes.Length,
                        MimeType = mimeType,
                        Target = path,
                        File = rawFile,
                        Width = width,
                        Height = height,
                        documentType = UploadFileMimeTypeExtension.GetDocumentType(extension)
                    });
                }
            }

            return files;
        }

        /// <summary>
        /// สร้างออปเจกต์ IUploadFileProvider แบบ IEnumerable จาก ออปเจกต์ IFormFileCollection
        /// </summary>
        /// <param name="fileCollection"></param>
        /// <param name="path"></param>
        /// <returns>IEnumerable<IUploadFileProvider></returns>
        public IUploadFileProvider convertToUploadFileProviderSingle(IRawFile rawFile, string path = "")
        {
            IUploadFileProvider file = null;

            path = (!string.IsNullOrEmpty(path)) ? path : this.getStoragePath();

            int width = 0, height = 0;
            var fileName = rawFile.fileName;
            var fullName = this.combine(path, fileName);
            var extension = UploadFileExtension.getExtension(fileName);
            var mimeType = UploadFileExtension.getMimeType(extension);
            var isImage = extension.IsImage();

            if (isImage)
            {
                using (MemoryStream ms = new MemoryStream(rawFile.rawBytes))
                {
                    using (Bitmap bit = new Bitmap(ms))
                    {
                        width = bit.Width;
                        height = bit.Height;

                        bit.Dispose();
                    }

                    ms.Close();
                    ms.Dispose();
                }

                file = new UploadFileProvider
                {
                    FileName = fileName,
                    Extension = extension,
                    FullName = fullName,
                    Length = rawFile.rawBytes.Length,
                    MimeType = mimeType,
                    Target = path,
                    File = rawFile,
                    Width = width,
                    Height = height,
                    documentType = UploadFileMimeTypeExtension.GetDocumentType(extension)
                };
            }
            else
            {
                file = new UploadFileProvider
                {
                    FileName = fileName,
                    Extension = extension,
                    FullName = fullName,
                    Length = rawFile.rawBytes.Length,
                    MimeType = mimeType,
                    Target = path,
                    File = rawFile,
                    Width = 0,
                    Height = 0,
                    documentType = UploadFileMimeTypeExtension.GetDocumentType(extension)
                };
            }

            return file;
        }


        /// <summary>
        /// สร้างออปเจกต์ IFormFile จากพาร์ทโฟลเดอร์ string
        /// </summary>
        /// <param name="files"></param>
        /// <param name="path"></param>
        /// <returns>IFormFile</returns>
        public IRawFile getFromStorage(string file)
        {
            IRawFile model = null;

            FileInfo fileInfo = new FileInfo(file);

            if (fileInfo.Exists)
            {
                model = fileInfo.ToIRawFile();
            }

            return model;
        }

        /// <summary>
        /// สร้างออปเจกต์ IFormFile แบบ IEnumerable จากพาร์ทโฟลเดอร์ string แบบ List
        /// </summary>
        /// <param name="files"></param>
        /// <param name="path"></param>
        /// <returns>IEnumerable<IFormFile></returns>
        public IEnumerable<IRawFile> getFromStorage(List<string> files)
        {
            List<IRawFile> fileCollection = new List<IRawFile>();

            foreach (var file in files)
            {
                FileInfo fileInfo = new FileInfo(file);

                if (fileInfo.Exists)
                {
                    IRawFile model = fileInfo.ToIRawFile();

                    fileCollection.Add(model);
                }
            }

            return fileCollection;
        }

        /// <summary>
        /// สร้างออปเจกต์ IUploadFileProvider โดยรีไซต์ขนาดรูปภาพตามส่วนสูง และความกว้างที่กำหนดขึ้นเอง ซึ่งจะใช้ได้กับไฟล์ที่เป็นประเภทรูปภาพเท่านั้น
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>IUploadFileProvider</returns>
        public IUploadFileProvider resize(int width, int height)
        {
            if (!this.Extension.IsImage())
            {
                throw new ArgumentException($"Please enter files with {UploadFileMimeTypeExtension.GetExtensionFromDocumentType("image", " , ")} extension only.");
            }

            Bitmap bitmap = this.File.ToBitmap(width, height);

            return this.ToIUploadFileProvider(bitmap);
        }

        /// <summary>
        /// สร้างออปเจกต์ IUploadFileProvider โดยรีไซต์ขนาดรูปภาพตามส่วนสูง และความกว้างที่กำหนดไว้ภายใน appsettings.json ซึ่งจะใช้ได้กับไฟล์ที่เป็นประเภทรูปภาพเท่านั้น
        /// โดยสามารถกำหนดเป็น sm หรือ md หรือ lg อย่างใดอย่างหนึ่งเท่านั้น
        /// </summary>
        /// <param name="size"></param>
        /// <returns>IUploadFileProvider</returns>
        public IUploadFileProvider resize(string size)
        {
            if (!this.Extension.IsImage())
            {
                throw new ArgumentException($"Please enter files with {UploadFileMimeTypeExtension.GetExtensionFromDocumentType("image", " , ")} extension only.");
            }

            var configuration = GlobalInjections.getConfiguration();

            if (configuration == null)
            {
                throw new ArgumentException(nameof(configuration));
            }

            var sizeOf = configuration["Uploads:resize." + size.ToLower()];

            if (string.IsNullOrEmpty(sizeOf))
            {
                throw new ArgumentException(nameof(sizeOf));
            }
            var splitSize = sizeOf.Split('x');

            var width = splitSize[0].ToInt();

            var height = splitSize[1].ToInt();

            Bitmap bitmap = this.File.ToBitmap(width, height);

            return this.ToIUploadFileProvider(bitmap);
        }

        /// <summary>
        /// บันทึกไฟล์ไปที่โฟลเดอร์ Storage ซึ่งเป็นโฟลเดอร์เก็บไฟล์เริ่มต้น
        /// ตัวอย่างโฟลเดอร์ที่บันทึกไฟล์ : {BaseAPP}/bin/{debug|release}/Storage/app/{save}
        /// </summary>
        /// <returns></returns>
        public IUploadFileProvider save()
        {
            UploadFileExtension.makeDirectory(this.Target);

            using (MemoryStream ms = new MemoryStream(this.File.rawBytes))
            {
                using (FileStream fs = new FileStream(this.FullName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    ms.CopyTo(fs);

                    fs.Close();
                    fs.Dispose();
                }

                ms.Close();
                ms.Dispose();
            }

            return this;
        }

        /// <summary>
        /// บันทึกไฟล์ไปที่โฟลเดอร์ที่ถูกกำหนดขึ้นใหม่ภายในเมท็อด
        /// ตัวอย่างโฟลเดอร์ที่บันทึกไฟล์ : {myDirectories}/{save}
        /// </summary>
        /// <param name="path">@string {path: บันทึกไปยังโฟลเดอร์ที่กำหนด ซึ่งจะใช้ชื่อไฟล์ตามไฟล์ต้นฉบับที่อัพโหลด|pathFile: บันทึกไปยังโฟลเดอร์ที่กำหนด พร้อมกับเปลี่ยนชื่อ หรือนามสกุลไฟล์}</param>
        /// <param name="uniqueName">@bool {false: ใช้ชื่อจากต้นฉบับไฟล์|true: ใช้ชื่อที่สุ่มขึ้นใหม่}</param>
        /// <returns>IUploadFileProvider</returns>
        public IUploadFileProvider save(string path, bool uniqueName = false)
        {
            IUploadFileProvider provider = this;
            path = UploadFileExtension.clearForPath(path);

            UploadFileExtension.makeDirectory(path);

            if (!path.isFile() && !uniqueName)
            {
                path = this.combine(path, provider.FileName);
            }

            if (path.isFile() && !uniqueName)
            {
                this.FileName = System.IO.Path.GetFileName(path);
                provider = this.ToIUploadFileProvider(path);
                path = provider.FullName;
            }

            if (uniqueName)
            {
                if (path.isFile())
                {
                    throw new ArgumentException(string.Format("{0} : path invalid.", nameof(path)));
                }

                this.FileName = UploadFileExtension.uniqueName(this.FileName, this.Extension);
                provider = this.ToIUploadFileProvider(path);
                path = provider.FullName;
            }

            using (MemoryStream ms = new MemoryStream(this.File.rawBytes))
            {
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    ms.CopyTo(fs);

                    fs.Close();
                    fs.Dispose();
                }

                ms.Close();
                ms.Dispose();
            }

            return provider;
        }

        /// <summary>
        /// บันทึกไฟล์ไปที่โฟลเดอร์ Storage ซึ่งเป็นโฟลเดอร์เก็บไฟล์เริ่มต้น
        /// โดยใช้กับการอัพโหลดไฟล์ภาพโดยเฉพาะ
        /// </summary>
        /// <param name="format">@enum {System.Drawing.Imaging.ImageFormat.Jpeg,Bmp,...,n เฉพาะประเภทรูปภาพเท่านั้น}</param>
        /// <param name="uniqueName">@bool {false: ใช้ชื่อจากต้นฉบับไฟล์|true: ใช้ชื่อที่สุ่มขึ้นใหม่}</param>
        /// <returns></returns>
        public IUploadFileProvider save(ImageFormat format, bool uniqueName = false)
        {
            IUploadFileProvider provider = this;
            UploadFileExtension.makeDirectory(this.Target);

            Bitmap bitmap = this.File.ToBitmap();

            if (uniqueName)
            {
                var extension = UploadFileExtension.getFromImageFormat(format);
                provider.FileName = UploadFileExtension.uniqueName(this.FileName, extension);
                provider.FullName = this.combine(provider.Target, provider.FileName);
                provider = this.ToIUploadFileProvider(bitmap, format);
            }
            else
            {
                provider = this.ToIUploadFileProvider(bitmap, format);
            }

            using (MemoryStream ms = new MemoryStream(provider.File.rawBytes))
            {
                using (FileStream fs = new FileStream(provider.FullName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    ms.CopyTo(fs);

                    fs.Close();
                    fs.Dispose();
                }

                ms.Close();
                ms.Dispose();
            }

            return provider;
        }

        /// <summary>
        /// บันทึกไฟล์ไปที่โฟลเดอร์ที่ถูกกำหนดขึ้นใหม่ภายในเมท็อด
        /// ตัวอย่างโฟลเดอร์ที่บันทึกไฟล์ : {myDirectories}/{save}
        /// </summary>
        /// <param name="path">@string {path: บันทึกไปยังโฟลเดอร์ที่กำหนด ซึ่งจะใช้ชื่อไฟล์ตามไฟล์ต้นฉบับที่อัพโหลด|pathFile: บันทึกไปยังโฟลเดอร์ที่กำหนด พร้อมกับเปลี่ยนชื่อ หรือนามสกุลไฟล์}</param>
        /// <param name="format">@enum {System.Drawing.Imaging.ImageFormat.Jpeg,Bmp,...,n เฉพาะประเภทรูปภาพเท่านั้น}</param>
        /// <param name="uniqueName">@bool {false: ใช้ชื่อจากต้นฉบับไฟล์|true: ใช้ชื่อที่สุ่มขึ้นใหม่}</param>
        /// <returns></returns>
        public IUploadFileProvider save(string path, ImageFormat format, bool uniqueName = false)
        {
            IUploadFileProvider provider = this;
            path = UploadFileExtension.clearForPath(path);

            UploadFileExtension.makeDirectory(path);

            Bitmap bitmap = this.File.ToBitmap();

            if (!path.isFile() && !uniqueName)
            {
                path = this.combine(path, provider.FileName);
            }

            if (path.isFile() && !uniqueName)
            {
                this.FileName = System.IO.Path.GetFileName(path);
                provider = this.ToIUploadFileProvider(bitmap, format, path);
                path = provider.FullName;
            }

            if (uniqueName)
            {
                if (path.isFile())
                {
                    throw new ArgumentException(string.Format("{0} : path invalid.", nameof(path)));
                }
                var extension = UploadFileExtension.getFromImageFormat(format);
                this.FileName = UploadFileExtension.uniqueName(this.FileName, extension);
                provider = this.ToIUploadFileProvider(bitmap, format, path);
                path = provider.FullName;
            }

            using (MemoryStream ms = new MemoryStream(provider.File.rawBytes))
            {
                using (FileStream fs = new FileStream(provider.FullName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    ms.CopyTo(fs);

                    fs.Close();
                    fs.Dispose();
                }

                ms.Close();
                ms.Dispose();
            }
            return provider;
        }

        /// <summary>
        /// กำหนดโฟลเดอร์ที่จะบันทึก
        /// </summary>
        /// <param name="path"></param>
        /// <returns>IUploadFileProvider</returns>
        public IUploadFileProvider setPath(string path)
        {
            if (path.isFile())
            {
                throw new ArgumentException(string.Format("{0} : path invalid.", nameof(path)));
            }

            this.Target = path;
            this.FullName = this.combine(this.Target, this.FileName);

            return this;
        }

        /// <summary>
        /// แปลงตำแหน่งจาก Folder เป็น Path URL
        /// </summary>
        /// <returns></returns>
        public string getLink()
        {
            var baseUrl = UploadFileExtension.getBaseUrl();
            var content = this.FullName.Replace(this.getStoragePath(), string.Empty).Trim('/');
            var url = UploadFileExtension.combineUrl(baseUrl, "Uploads", content);

            return url;
        }
        #endregion

        #region standard

        /// <summary>
        /// Determine if a directory exists.
        /// </summary>
        /// <returns>bool</returns>
        public bool dirExsist()
        {
            return this.Target.isDir() && System.IO.Directory.Exists(this.Target);
        }

        /// <summary>
        /// Determine if a file exists.
        /// </summary>
        /// <returns>bool</returns>
        public bool fileExsist()
        {
            return this.FullName.isFile() && System.IO.File.Exists(this.FullName);
        }

        /// <summary>
        /// Copy a file to a new location.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public void copy(string target)
        {
            System.IO.File.Copy(this.FullName, target);
            this.FullName = target;
        }

        /// <summary>
        /// Move a file to a new location.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public void move(string target)
        {
            System.IO.File.Move(this.FullName, target);
            this.FullName = target;
        }

        /// <summary>
        /// Delete the file at a given path.
        /// </summary>
        /// <returns>bool</returns>
        public bool delete()
        {
            return UploadFileExtension.deleteFile(this.FullName);
        }
        #endregion

        #region supported
        /// <summary>
        /// Get base path
        /// </summary>
        /// <param name="path"></param>
        /// <returns>string : {base_app}</returns>
        public string getBasePathApplicationRoot()
        {
            return UploadFileExtension.getBasePathApplicationRoot();
        }

        /// <summary>
        /// Get base path
        /// </summary>
        /// <param name="path"></param>
        /// <returns>string : {base_app}/bin/Debug/netcoreapp2.1</returns>
        public string getBasePathApplication()
        {
            return UploadFileExtension.getBasePathApplication();
        }

        /// <summary>
        /// Get storage path
        /// </summary>
        /// <param name="path"></param>
        /// <returns>string : {base_app}/bin/Debug/netcoreapp2.1/Storage/{path}</returns>
        public string getStoragePath(string path = "")
        {
            return this.combine(UploadFileExtension.getStoragePath(), path);
        }

        /// <summary>
        /// Get extension filename
        /// </summary>
        /// <returns>string : .png</returns>
        public string getExtension()
        {
            return this.Extension;
        }

        /// <summary>
        /// Get mime type of filename
        /// </summary>
        /// <returns>string : image/png</returns>
        public string getMimeType()
        {
            return this.MimeType;
        }

        /// <summary>
        /// Combine path file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string combine(params string[] path)
        {
            return UploadFileExtension.clearForPath(UploadFileExtension.combine(path));
        }
        #endregion
    }
}
