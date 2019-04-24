using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Toolbox.IO.Extensions;

namespace Toolbox.IO
{
    /// <summary>
    /// Seekable FTP File Stream is a Stream wrapper around a WebRequest creating a seekable network stream.
    /// This is performed by reconnecting to the FTP when a negative position change is requested.
    /// </summary>
    public class SeekableFtpFileStream : Stream, IDisposable
    {
        // Request URI saved to create new requests
        private readonly Uri? _requestUri;
        // Length of file requested, used for boundary when seeking
        private readonly long _length;

        // Current position, used to move cursor to the right position
        private long _position = 0;
        // Pointing to the current reading position on the stream
        private long _cursor = 0;

        // Request field for cloning the request when creating new request
        private readonly FtpWebRequest? _request = null;
        // Response field to allow closing connections
        private FtpWebResponse? _response = null;

        private bool _isDisposed = false;

        // Response stream field for reading
        private Stream? _stream;

        /// <summary>
        /// Create instance with basic WebRequest from URI string
        /// </summary>
        /// <param name="requestUriString">Address for requests</param>
        public SeekableFtpFileStream(string requestUriString) : this(new Uri(requestUriString)) { }

        /// <summary>
        /// Create instance with basic WebRequest from URI
        /// </summary>
        /// <param name="requestUri">Address for requests</param>
        public SeekableFtpFileStream(Uri requestUri)
        {
            _requestUri = requestUri;

            OpenConnection();
            // Get filesize from FTP
            _length = _response?.ContentLength ?? throw new ArgumentNullException(nameof(_response));
            CloseConnection();
        }

        /// <summary>
        /// Create instance from WebRequest
        /// </summary>
        /// <param name="request">Custom FTP WebRequest</param>
        public SeekableFtpFileStream(FtpWebRequest request)
        {
            // Assign request to field for later cloning
            _request = request;

            OpenConnection();
            // Get filesize from FTP
            _length = _response?.ContentLength ?? throw new ArgumentNullException(nameof(_response));
            CloseConnection();
        }

        /// <summary>
        /// Open a new connection to FTP
        /// </summary>
        /// <param name="offset">Offset on file stream</param>
        private void OpenConnection(long offset = 0)
        {
            // Close any existing connections to FTP
            CloseConnection();
            Connect(offset);

            // Set cursor to offset
            _cursor = offset;
        }

        /// <summary>
        /// Creating a new FtpWebRequest.
        /// Clones or creates request.
        /// </summary>
        /// <returns>FtpWebRequest</returns>
        private FtpWebRequest CreateRequest()
        {
            FtpWebRequest request;

            // If _request is set, clone the request
            // Else create new request
            if (_request != null)
            {
                request = _request.Clone();
            }
            else
            {
                request = (FtpWebRequest)WebRequest.Create(_requestUri);
            }

            return request;
        }

        /// <summary>
        /// Create new request with offset
        /// </summary>
        /// <param name="offset">Offset on file stream</param>
        private void Connect(long offset = 0)
        {
            FtpWebRequest request = CreateRequest();
            Connect(request, offset);
        }

        /// <summary>
        /// Connect to request with offset
        /// </summary>
        /// <exception cref="FileNotFoundException">If unable to open stream</exception>
        /// <param name="request">FtpWebRequest for file download</param>
        /// <param name="offset">Offset on file stream</param>
        private void Connect(FtpWebRequest request, long offset = 0)
        {
            request.ContentOffset = offset;
            _response = (FtpWebResponse)request.GetResponse();

            // If unexpected status code, throw exception
            if (_response.StatusCode != FtpStatusCode.OpeningData)
            {
                throw new FileNotFoundException("Unable to open data stream");
            }

            _stream = _response.GetResponseStream();
        }

        /// <summary>
        /// Close, dispose and nullify _stream and _response
        /// </summary>
        private void CloseConnection()
        {
            // Clean up response if exists
            if (_response != null)
            {
                _response.Dispose();
                _response = null;
            }

            // Clean up stream if exists
            if (_stream != null)
            {
                _stream.Dispose();
                _stream = null;
            }
        }

        /// <summary>
        /// If position is smaller than length, stream can read.
        /// </summary>
        public override bool CanRead => (Position < Length) && !_isDisposed;

        /// <summary>
        /// Always true
        /// </summary>
        public override bool CanSeek => true;

        /// <summary>
        /// Always false
        /// </summary>
        public override bool CanWrite => false;

        /// <summary>
        /// Gets length of file
        /// </summary>
        public override long Length => _length;


        /// <summary>
        /// Gets stream position
        /// </summary>
        public override long Position
        {
            get => _position;
            set => _position = value;
        }

        /// <summary>
        /// Always throws NotSupportedException
        /// </summary>
        public override void Flush() => throw new NotSupportedException();

        /// <summary>
        /// Reads from stream to buffer
        /// </summary>
        /// <param name="buffer">Byte[] to contain read bytes</param>
        /// <param name="offset">Offset from position</param>
        /// <param name="count">Bytes to read</param>
        /// <returns>Bytes read count</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            // If _cursor is further ahead on stream than request _position or _stream is null
            if (IsCursorAhead || _stream == null)
            {
                // Open new connection
                OpenConnection(_position);
            }
            // If _cursor is behind _position, read till position
            else if (IsCursorBehind)
            {
                // Move _curstor till _position
                for (; IsCursorBehind; _cursor++)
                {
                    // Skip bytes
                    _stream.ReadByte();
                }
            }

            // Read from _stream
            int bytesRead = _stream?.Read(buffer, offset, count) ?? throw new ArgumentNullException(nameof(_stream));

            // Move _cursor position
            _cursor += bytesRead;
            // Set _position to cursor position
            _position = _cursor;

            return bytesRead;
        }

        private bool IsCursorBehind => _cursor < _position;

        private bool IsCursorAhead => _cursor > _position;

        /// <summary>
        /// Seeks for a position on the stream
        /// </summary>
        /// <exception cref="ArgumentException">Thrown on seeking before or after stream</exception>
        /// <param name="offset">Offset from origin</param>
        /// <param name="origin">Seeking from here</param>
        /// <returns>Position on stream</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    if (offset < 0)
                    {
                        throw new ArgumentException("Cannot seek before beginning.");
                    }
                    if (offset > _length)
                    {
                        throw new ArgumentException("Cannot seek after ending.");
                    }
                    // Set position to offset
                    _position = offset;
                    break;
                case SeekOrigin.End:
                    if (offset > 0)
                    {
                        throw new ArgumentException("Cannot seek after ending.");
                    }
                    if (offset < _length * -1)
                    {
                        throw new ArgumentException("Cannot seek before beginning.");
                    }
                    // Add offset to file _length
                    _position = _length + offset;
                    break;
                default:
                    if (_position + offset < 0)
                    {
                        throw new ArgumentException("Cannot seek before beginning.");
                    }
                    if (_position + offset > _length)
                    {
                        throw new ArgumentException("Cannot seek after ending.");
                    }
                    // Add offset to current _position
                    _position += offset;
                    break;
            }

            return _position;
        }

        /// <summary>
        /// Always throws NotSupportedException
        /// </summary>
        /// <param name="value"></param>
        public override void SetLength(long value) => throw new NotSupportedException();

        /// <summary>
        /// Always throws NotSupportedException
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

        /// <summary>
        /// Closes connection to FTP
        /// </summary>
        public new void Dispose()
        {
            CloseConnection();
            _isDisposed = true;
        }
    }
}
