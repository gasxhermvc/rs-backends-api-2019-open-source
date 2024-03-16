using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace rs_backends_api_2019.Business.Helpers.UploadFileHelper.FormBuilder
{
    public class FormUploadBuilder : IFormFile
    {
        // Stream.CopyTo method uses 80KB as the default buffer size.
        private const int DefaultBufferSize = 80 * 1024;

        private readonly Stream _baseStream;

        private readonly long _baseStreamOffset;

        public FormUploadBuilder(Stream baseStream, long baseStreamOffset, long length, string name, string filename)
        {
            _baseStream = baseStream;
            _baseStreamOffset = baseStreamOffset;
            Length = length;
            Name = name;
            FileName = filename;
        }

        public string ContentType
        {
            get { return Headers["Content-Type"]; }
            set { Headers["Content-Type"] = value; }
        }

        /// <summary>
        /// Gets the raw Content-Disposition header of the uploaded file.
        /// </summary>
        public string ContentDisposition
        {
            get { return Headers["Content-Disposition"]; }
            set { Headers["Content-Disposition"] = value; }
        }

        /// <summary>
        /// Gets the header dictionary of the uploaded file.
        /// </summary>
        public IHeaderDictionary Headers { get; set; }

        /// <summary>
        /// Gets the file length in bytes.
        /// </summary>
        public long Length { get; }

        /// <summary>
        /// Gets the name from the Content-Disposition header.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the file name from the Content-Disposition header.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Copies the contents of the uploaded file to the <paramref name="target"/> stream.
        /// </summary>
        /// <param name="target">The stream to copy the file contents to.</param>
        public void CopyTo(Stream target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            using (var readStream = OpenReadStream())
            {
                readStream.CopyTo(target, DefaultBufferSize);
                readStream.Close();
                readStream.Dispose();
            }
        }

        /// <summary>
        /// Asynchronously copies the contents of the uploaded file to the <paramref name="target"/> stream.
        /// </summary>
        /// <param name="target">The stream to copy the file contents to.</param>
        /// <param name="cancellationToken"></param>
        public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            using (var readStream = OpenReadStream())
            {
                await readStream.CopyToAsync(target, DefaultBufferSize, cancellationToken);
            }
        }

        /// <summary>
        /// Opens the request stream for reading the uploaded file.
        /// </summary>
        public Stream OpenReadStream()
        {
            return new FormUploadReadStream(_baseStream, _baseStreamOffset, Length);
        }
    }
}
