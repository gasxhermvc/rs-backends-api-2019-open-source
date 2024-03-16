using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace rs_backends_api_2019.Business.Helpers.UploadFileHelper.FormBuilder
{
    public class FormUploadReadStream : Stream
    {
        private readonly Stream _inner;

        private readonly long _innerOffset;

        private readonly long _length;

        private long _position;

        private bool _disposed;

        public FormUploadReadStream(Stream inner, long offset, long length)
        {
            if (inner == null)
            {
                throw new ArgumentNullException(nameof(inner));
            }

            _inner = inner;
            _innerOffset = offset;
            _length = length;
            _inner.Position = offset;
        }

        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return _inner.CanSeek;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public override long Length
        {
            get
            {
                return _length;
            }
        }

        public override long Position
        {
            get { return _position; }
            set
            {
                ThrowIfDisposed();

                if (value < 0 || value > Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The Position must be within the length of the stream: " + Length.ToString());
                }

                VerifyPosition();
                _position = value;
                _inner.Position = _innerOffset + _position;
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (origin == SeekOrigin.Begin)
            {
                Position = offset;
            }
            else if (origin == SeekOrigin.End)
            {
                Position = Length + offset;
            }
            else
            {
                Position = Position + offset;
            }

            return Position;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            ThrowIfDisposed();
            VerifyPosition();

            var toRead = Math.Min(count, _length - _position);
            var read = _inner.Read(buffer, offset, (int)toRead);
            _position += read;

            return read;
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            VerifyPosition();

            var toRead = Math.Min(count, _length - Position);
            var read = await _inner.ReadAsync(buffer, offset, (int)toRead, cancellationToken);
            _position += read;

            return read;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Flush()
        {
            throw new NotSupportedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _disposed = true;
            }
        }

        private void VerifyPosition()
        {
            if (_inner.Position != _innerOffset + _position)
            {
                throw new InvalidOperationException("The inner stream position has changed unexpectedly.");
            }
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(FormUploadReadStream));
            }
        }
    }
}
