using rs_backends_api_2019.Business.Helpers.UploadFileHelper;
using rs_backends_api_2019.Business.Helpers.UploadFileHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.ZipHelper
{
    public class Zipper
    {
        public string ReplaceZipName(string zipName)
        {
            if (zipName.ToLower().EndsWith(".zip"))
            {
                return zipName;
            }

            return zipName.Replace(".zip", string.Empty) + ".zip";
        }

        public IUploadFileProvider Zip(string zipName, IUploadFileService uploadFileService, List<IUploadFileProvider> zipItems)
        {
            byte[] byteZipItems = null;

            zipName = this.ReplaceZipName(zipName);

            using (FileStream fs = new FileStream(@uploadFileService.getStoragePath(zipName), FileMode.Create))
            {
                using (var zip = new ZipArchive(fs, ZipArchiveMode.Update, true))
                {
                    foreach (var zipItem in zipItems)
                    {
                        zip.CreateEntryFromFile(zipItem.FullName, zipItem.FileName);
                    }
                }
                fs.Flush();
                fs.Close();
            }

            byteZipItems = System.IO.File.ReadAllBytes(@uploadFileService.getStoragePath(zipName));

            var file = uploadFileService.make(new RawFile
            {
                data = null,
                fileName = zipName,
                rawBytes = byteZipItems

            } as IRawFile);

            // deleted file
            file.delete();

            return file;
        }

        public static void unZip(string sourceZip, string destinationZip)
        {
            ZipFile.ExtractToDirectory(sourceZip, destinationZip);
        }
    }
}
