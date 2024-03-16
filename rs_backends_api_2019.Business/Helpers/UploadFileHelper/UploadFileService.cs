using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using rs_backends_api_2019.Business.Global;
using rs_backends_api_2019.Business.Helpers.UploadFileHelper.Extensions;
using rs_backends_api_2019.Business.Helpers.UploadFileHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.UploadFileHelper
{
    public sealed class UploadFileService : IUploadFileService
    {
        private IConfiguration configuration;

        public UploadFileService()
        {
            configuration = GlobalInjections.getConfiguration();
        }

        // get storage path
        public string getStoragePath(string path = "")
        {
            return UploadFileExtension.clearForPath(UploadFileExtension.getStoragePath(path));
        }

        // create object IUploadFileProvider single
        public IUploadFileProvider make(IFormFileCollection collection)
        {
            IUploadFileProvider provider = new UploadFileProvider();

            return ((UploadFileProvider)provider).convertToUploadFileProvider(collection).FirstOrDefault();
        }

        // create List object IUploadFileProvider
        public List<IUploadFileProvider> makeCollection(IFormFileCollection collection)
        {
            IUploadFileProvider provider = new UploadFileProvider();

            return ((UploadFileProvider)provider).convertToUploadFileProvider(collection).ToList();
        }

        // get IUploadFileProvider from storage
        public List<IUploadFileProvider> store(string path)
        {
            IEnumerable<IRawFile> fileCollection = null;
            IUploadFileProvider provider = new UploadFileProvider();

            Console.WriteLine("step 1 => " + path);

            if (path.isFile())
            {
                var dir = System.IO.Path.GetDirectoryName(path);
                Console.WriteLine("step 2.1 => dir " + dir);

                var file = UploadFileExtension.clearForPath(path);
                Console.WriteLine("step 2.2 => file " + file);

                List<string> files = new List<string>();
                try
                {
                    files = System.IO.Directory.GetFiles(dir).Select(s => UploadFileExtension.clearForPath(s))
                    .Where(w => w == file).ToList();
                    Console.WriteLine("step 2.3 => files " + files);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("step 2.3 => Exception " + ex.ToString());
                    files.Add(file);
                }


                fileCollection = ((UploadFileProvider)provider).getFromStorage(files);
                path = System.IO.Path.GetDirectoryName(path);

                path = UploadFileExtension.clearForPath(path);

                Console.WriteLine("step 2.4 => " + path);

                if (path.StartsWith("/code/code"))
                {
                    path = path.Replace("/code/code", "/code");
                }
            }
            else
            {
                var files = System.IO.Directory.GetFiles(path).ToList();
                fileCollection = ((UploadFileProvider)provider).getFromStorage(files);
            }

            return ((UploadFileProvider)provider).convertToUploadFileProvider(fileCollection, path).ToList();
        }

        // create object IUploadFileSystemProvider from raw file
        public IUploadFileProvider make(IRawFile rawFile)
        {
            IUploadFileProvider provider = new UploadFileProvider();

            return ((UploadFileProvider)provider).convertToUploadFileProviderSingle(rawFile);
        }

        // check file exists
        public bool exists(string pathFile)
        {
            return pathFile.fileExists();
        }

        // check and convert to url
        public string link(string path)
        {
            Console.WriteLine("log in link() : " + path);

            //***เหมือน forder invGroups มีปัญหา
            //if(path.isFile() && path.fileExists())
            if (path.isFile())
            {
                var baseUrl = UploadFileExtension.getBaseUrl();
                var content = path.Replace(this.getStoragePath(), string.Empty);
                var link = UploadFileExtension.combineUrl(baseUrl, configuration["FixedUploadsPath"].Trim('/'), content);

                Console.WriteLine("fileExists Link = : " + link);

                return link;
            }
            else
            {
                Console.WriteLine("No fileExists : " + path);
            }

            return string.Empty;
        }

        public string notFoundImage()
        {
            var baseUrl = UploadFileExtension.getBaseUrl();
            var link = UploadFileExtension.combineUrl(baseUrl, configuration["FixedUploadsPath"].Trim('/'), "Images/not-found.jpg");
            return link;
        }
        // combine path
        public string combine(params string[] path)
        {
            return UploadFileExtension.combine(path);
        }

        // combine path
        public string combineUrl(params string[] path)
        {
            return UploadFileExtension.combineUrl(path);
        }
    }
}
