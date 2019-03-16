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
        private CancellationTokenSource _source;

        private int _readTimeout = DEFAULT_TIMEOUT_READ;
        private int _writeTimeout = DEFAULT_TIMEOUT_WRITE;

        public TimeoutStream(Stream stream)
        {
            BaseStream = stream;
            _source = new CancellationTokenSource();
        }

        public Stream BaseStream { get; }

        public override bool CanRead => BaseStream.CanRead;

        public override bool CanSeek => BaseStream.CanSeek;

        public override bool CanWrite => BaseStream.CanWrite;

        public override long Length => BaseStream.Length;

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
            get => BaseStream.Position;
            set => BaseStream.Position = value;
        }

        public override void Flush() => BaseStream.Flush();

        public override int Read(byte[] buffer, int offset, int count)
        {
            int result;

            if (BaseStream.CanRead && !BaseStream.CanTimeout)
            {
                try
                {
                    _source.CancelAfter(_readTimeout);
                    Task<int> task = BaseStream.ReadAsync(
                        buffer, offset, count, _source.Token);
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
                result = BaseStream.Read(buffer, offset, count);
            }

            return result;
        }

        public override long Seek(long offset, SeekOrigin origin) => BaseStream.Seek(offset, origin);

        public override void SetLength(long value) => BaseStream.SetLength(value);

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (BaseStream.CanWrite && !BaseStream.CanTimeout)
            {
                try
                {
                    _source.CancelAfter(_readTimeout);
                    Task task = BaseStream.WriteAsync(buffer, offset, count, _source.Token);
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
                BaseStream.Write(buffer, offset, count);
            }
        }
    }
}
