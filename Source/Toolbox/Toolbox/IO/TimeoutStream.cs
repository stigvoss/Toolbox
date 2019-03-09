using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Toolbox.IO
{
    public class TimeoutStream : Stream
    {
        private const int DEFAULT_TIMEOUT_READ = 30000;
        private const int DEFAULT_TIMEOUT_WRITE = 30000;

        private readonly Stream _baseStream;

        private CancellationTokenSource _source;

        private int _readTimeout = DEFAULT_TIMEOUT_READ;
        private int _writeTimeout = DEFAULT_TIMEOUT_WRITE;

        public TimeoutStream(Stream stream)
        {
            _baseStream = stream;
            _source = new CancellationTokenSource();
        }

        public Stream BaseStream => _baseStream;

        public override bool CanRead => _baseStream.CanRead;

        public override bool CanSeek => _baseStream.CanSeek;

        public override bool CanWrite => _baseStream.CanWrite;

        public override long Length => _baseStream.Length;

        public override bool CanTimeout => true;

        public override int ReadTimeout
        {
            get => _readTimeout;
            set => _readTimeout = value;
        }

        public override int WriteTimeout
        {
            get => _writeTimeout;
            set => _writeTimeout = value;
        }

        public override long Position
        {
            get => _baseStream.Position;
            set => _baseStream.Position = value;
        }

        public override void Flush() => _baseStream.Flush();

        public override int Read(byte[] buffer, int offset, int count)
        {
            int result;

            if (_baseStream.CanRead && !_baseStream.CanTimeout)
            {
                try
                {
                    _source.CancelAfter(_readTimeout);
                    Task<int> task = _baseStream.ReadAsync(buffer, offset, count, 
                        _source.Token);
                    result = task.Result;
                }
                catch (AggregateException)
                {
                    _source = new CancellationTokenSource();
                    throw new TimeoutException("The operation timed out.");
                }
            }
            else
            {
                result = _baseStream.Read(buffer, offset, count);
            }

            return result;
        }

        public override long Seek(long offset, SeekOrigin origin) => _baseStream.Seek(offset, origin);

        public override void SetLength(long value) => _baseStream.SetLength(value);

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (_baseStream.CanWrite && !_baseStream.CanTimeout)
            {
                try
                {
                    _source.CancelAfter(_readTimeout);
                    Task task = _baseStream.WriteAsync(buffer, offset, count, _source.Token);
                    task.Wait();
                }
                catch (AggregateException)
                {
                    _source = new CancellationTokenSource();
                    throw new TimeoutException("The operation timed out.");
                }
            }
            else
            {
                _baseStream.Write(buffer, offset, count);
            }
        }
    }
}
