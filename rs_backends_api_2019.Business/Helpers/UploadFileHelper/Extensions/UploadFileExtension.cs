using Microsoft.AspNetCore.Hosting;
using rs_backends_api_2019.Business.Extensions;
using rs_backends_api_2019.Business.Global;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.UploadFileHelper.Extensions
{
    public static class UploadFileExtension
    {
        // get base url
        public static string getBaseUrl()
        {
            var context = GlobalInjections.getHttpContextAccessor();
            var config = GlobalInjections.getConfiguration();

            var Current = context.HttpContext;

            Console.WriteLine("-->" + $"{Current.Request.Scheme}://{Current.Request.Host}");

            if (!string.IsNullOrEmpty(config["FixedURL"]))
            {
                return $"{config["FixedURL"]}";
            }

            return $"{Current.Request.Scheme}://{Current.Request.Host}";
        }

        public static string getBaseUrlAPI()
        {
            var context = GlobalInjections.getHttpContextAccessor();
            var config = GlobalInjections.getConfiguration();

            var Current = context.HttpContext;

            Console.WriteLine("-->" + $"{Current.Request.Scheme}://{Current.Request.Host}");

            if (!string.IsNullOrEmpty(config["FixedURLPDF"]))
            {
                return $"{config["FixedURLPDF"]}";
            }

            return $"{Current.Request.Scheme}://{Current.Request.Host}";
        }


        // get base content root
        public static string getBasePathApplicationRoot()
        {
            var host = GlobalInjections.getHostingEnvironment();

            return clearForPath(host.ContentRootPath);
        }

        // get base path
        public static string getBasePathApplication()
        {
            return clearForPath(AppDomain.CurrentDomain.BaseDirectory);
        }

        // get storage path
        public static string getStoragePath()
        {
            var configuration = GlobalInjections.getConfiguration();
            var env = GlobalInjections.getHostingEnvironment();

            string storagePath = @configuration["Storage"];

            storagePath = (string.IsNullOrEmpty(storagePath)) ?
                combine(env.IsDevelopment() ? getBasePathApplicationRoot() : getBasePathApplication(), "Storage", "app") : storagePath;

            string _env = env.IsDevelopment() ? "True" : "false";

            Console.WriteLine("getStoragePath IsDevelopment : " + _env);

            Console.WriteLine("getStoragePath step 1 : " + storagePath);

            storagePath = clearForPath(storagePath);
            Console.WriteLine("getStoragePath step 2 : " + storagePath);

            storagePath = (!storagePath.Contains("Storage/app")) ? combine(storagePath, "Storage", "app") : storagePath;
            Console.WriteLine("getStoragePath step 3 : " + storagePath);

            return storagePath;
        }

        // get storage path and custom folder
        public static string getStoragePath(string path)
        {
            return combine(getStoragePath(), path);
        }

        // combine path file
        public static string combine(params string[] path)
        {
            //return UploadFileExtension.clearForPath(System.IO.Path.Combine(path));
            return string.Join("/", path).Replace("//", "/").Replace("\\\\", "/");
        }

        // combine url path
        public static string combineUrl(params string[] path)
        {
            path = path.Select(s => s).ToArray();

            string url = string.Join("/", path);

            return url;
        }

        // check string is file
        public static bool isFile(this string file)
        {
            FileInfo fileInfo = new FileInfo(file);

            Console.WriteLine("fileInfo  => " + fileInfo);

            var attribute = fileInfo.Attributes;
            var extenson = fileInfo.Extension;
            var exists = fileInfo.Exists;

            fileInfo = null;

            Console.WriteLine("case fileInfo attribute => " + attribute.ToString());

            //if (attribute == FileAttributes.Archive)
            //{
            //    return true;
            //}
            //else
            //{
            //    Console.WriteLine("case fileInfo attribute false => ");

            //}

            if (!string.IsNullOrEmpty(extenson))
            {

                return true;
            }
            else
            {
                Console.WriteLine("case fileInfo extenson false => ");

            }

            if (exists)
            {

                return true;
            }
            else
            {
                Console.WriteLine("case fileInfo exists false => ");
            }

            return false;
        }

        // check file is exists
        public static bool fileExists(this string file)
        {
            Console.WriteLine("case fileExists FileName => " + file);

            if (!System.IO.File.Exists(file))
            {
                Console.WriteLine("case fileExists false ");
            }
            else
            {
                Console.WriteLine("case fileExists true ");
            }

            return System.IO.File.Exists(file);
        }

        // check string is directory
        public static bool isDir(this string path)
        {
            FileInfo fileInfo = new FileInfo(path);

            var attribute = fileInfo.Attributes;
            var extension = fileInfo.Extension;
            var exists = fileInfo.Directory.Exists;

            fileInfo = null;

            if (attribute == FileAttributes.Directory)
            {
                return true;
            }

            if (string.IsNullOrEmpty(extension))
            {
                return true;
            }

            if (exists)
            {
                return true;
            }

            return false;
        }

        // check directroy is exists
        public static bool dirExists(this string path)
        {
            return System.IO.Directory.Exists(path);
        }

        // create a directory.
        public static void makeDirectory(string path)
        {
            if (path.isFile())
            {
                FileInfo fileInfo = new FileInfo(path);
                fileInfo.Directory.Create();
                fileInfo = null;
                return;
            }

            if (path.isDir() && !path.isFile())
            {
                path = (!path.EndsWith("/")) ? path + "/" : path;
                System.IO.Directory.CreateDirectory(path);
                return;
            }

            throw new ArgumentException(nameof(path));
        }

        // delete file
        public static bool deleteFile(this string file)
        {
            if (!file.isFile() || !file.fileExists())
            {
                return false;
            }

            System.IO.File.Delete(file);

            return true;
        }

        // Recursively delete a directory.
        // The directory itself may be optionally force.
        public static bool deleteDirectory(string path, bool force = false)
        {
            if (!path.dirExists())
            {
                return false;
            }

            var dirs = System.IO.Directory.GetDirectories(path).ToList();

            if (dirs != null && dirs.Count >= 1)
            {
                foreach (var dir in dirs)
                {
                    if (dir.isDir())
                    {
                        System.IO.Directory.Delete(dir, force);
                    }
                }
            }

            return true;
        }

        // delete all folder
        public static bool deleteDirectories(string path, bool force = false)
        {
            if (!path.dirExists())
            {
                return false;
            }

            System.IO.Directory.Delete(path, force);
            return true;
        }

        // get file size
        public static long size(string file)
        {
            FileInfo fileInfo = new FileInfo(file);
            var length = fileInfo.Length;

            fileInfo = null;

            return length;
        }

        // get extension from filename
        public static string getExtension(string path)
        {
            return System.IO.Path.GetExtension(path);
        }

        // get mime type from extension name
        public static string getMimeType(string extension)
        {
            return UploadFileMimeTypeExtension.GetMimeType(extension);
        }

        // get enum image format
        // ".bmp|.jpeg|.jpg|.gif|.emf|.exif|.icon|.memorybmp|.tiff|.wmf"
        public static ImageFormat getImageFormat(string extension)
        {
            switch (extension)
            {
                case ".bmp":
                    return ImageFormat.Bmp;
                case ".png":
                    return ImageFormat.Png;
                case ".jpeg":
                case ".jpg":
                    return ImageFormat.Jpeg;
                case ".gif":
                    return ImageFormat.Gif;
                case ".emf":
                    return ImageFormat.Emf;
                case ".exif":
                    return ImageFormat.Exif;
                case ".icon":
                    return ImageFormat.Icon;
                case "memorybmp":
                    return ImageFormat.MemoryBmp;
                case ".tiff":
                    return ImageFormat.Tiff;
                case ".wmf":
                    return ImageFormat.Wmf;
                default:
                    return ImageFormat.Jpeg;
            }
        }

        // get string image format of enum
        // ".bmp|.jpeg|.jpg|.gif|.emf|.exif|.icon|.memorybmp|.tiff|.wmf"
        public static string getFromImageFormat(ImageFormat format)
        {
            string description = string.Empty;

            if (format == ImageFormat.Bmp)
            {
                description = ".bmp";
            }
            else if (format == ImageFormat.Png)
            {
                description = ".png";
            }
            else if (format == ImageFormat.Jpeg)
            {
                description = ".jpg";
            }
            else if (format == ImageFormat.Gif)
            {
                description = ".gif";
            }
            else if (format == ImageFormat.Emf)
            {
                description = ".emf";
            }
            else if (format == ImageFormat.Exif)
            {
                description = ".exif";
            }
            else if (format == ImageFormat.Icon)
            {
                description = ".icon";
            }
            else if (format == ImageFormat.MemoryBmp)
            {
                description = ".memorybmp";
            }
            else if (format == ImageFormat.Tiff)
            {
                description = ".tiff";
            }
            else if (format == ImageFormat.Wmf)
            {
                description = ".wmf";
            }
            else
            {
                description = ".jpg";
            }

            return description;
        }

        // cleaning slash for path file
        public static string clearForPath(string path)
        {
            return path.Replace("\\", "/")
             .Replace("//", "/");
        }

        // get string unique name
        public static string uniqueName(string fileName, string extension)
        {
            return (HashExtension.HashSha1(Guid.NewGuid().ToString() + fileName)).ToLower() + extension;
        }
    }
}
