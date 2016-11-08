using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Extensions;

namespace Toolbox.IO
{
    /// <summary>
    /// 
    /// </summary>
    public class SeekableFtpFileStream : Stream, IDisposable
    {
        private readonly Uri _requestUri;
        private readonly long _length;

        private long _position = 0;
        private long _cursor = 0;

        FtpWebRequest _request = null;
        FtpWebResponse _response = null;

        private Stream _stream;

        public SeekableFtpFileStream(string requestUriString) : this(new Uri(requestUriString)) { }

        public SeekableFtpFileStream(Uri requestUri)
        {
            _requestUri = requestUri;

            OpenConnection();
            _length = _response.ContentLength;
            CloseConnection();
        }

        public SeekableFtpFileStream(FtpWebRequest request)
        {
            _request = request;
            _requestUri = request.RequestUri;

            OpenConnection();
            _length = _response.ContentLength;
            CloseConnection();
        }

        private void OpenConnection(long offset = 0)
        {
            CloseConnection();
            Connect(offset);

            _cursor = offset;
        }

        private void Connect(long offset = 0)
        {
            FtpWebRequest request;

            if (_request != null)
            {
                request = _request.Clone();
            }
            else
            {
                request = (FtpWebRequest)WebRequest.Create(_requestUri);
            }

            Connect(request, offset);
        }

        private void Connect(FtpWebRequest request, long offset = 0)
        {
            request.ContentOffset = offset;
            _response = (FtpWebResponse)request.GetResponse();
            _stream = _response.GetResponseStream();
        }

        private void CloseConnection()
        {
            if (_response != null)
            {
                _response.Close();
                _response.Dispose();
            }

            if (_stream != null)
            {
                _stream.Dispose();
                _stream = null;
            }
        }

        public override bool CanRead
        {
            get
            {
                return (Position < Length);
            }
        }

        public override bool CanSeek
        {
            get
            {
                return true;
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
            get { return _length; }
        }

        public override long Position
        {
            get
            {
                return _position;
            }

            set
            {
                _position = value;
            }
        }

        public override void Flush()
        {
            throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_cursor > _position || _stream == null)
            {
                OpenConnection(_position);
            }
            else if (_cursor < _position)
            {
                for (; _cursor < _position; _cursor++)
                {
                    _stream.ReadByte();
                }
            }

            int bytesRead = _stream.Read(buffer, offset, count);

            _cursor += bytesRead;
            _position = _cursor;

            return bytesRead;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    if (offset < 0)
                        throw new ArgumentException("Cannot seek before beginning.");
                    if (offset > _length)
                        throw new ArgumentException("Cannot seek after ending.");
                    _position = offset;
                    break;
                case SeekOrigin.End:
                    if (offset > 0)
                        throw new ArgumentException("Cannot seek after ending.");
                    if (offset < _length * -1)
                        throw new ArgumentException("Cannot seek before beginning.");
                    _position = _length + offset;
                    break;
                default:
                    if (_position + offset < 0)
                        throw new ArgumentException("Cannot seek before beginning.");
                    if (_position + offset > _length)
                        throw new ArgumentException("Cannot seek after ending.");
                    _position = _position + offset;
                    break;
            }

            return _position;
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public new void Dispose()
        {
            CloseConnection();
        }
    }
}
