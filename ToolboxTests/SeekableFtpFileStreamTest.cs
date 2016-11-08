using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Toolbox.IO;
using System.Net;
using System.Linq;

namespace ToolboxTests
{
    [TestClass]
    public class SeekableFtpFileStreamTest
    {
        const string URL_FILE = "ftp://ftp.swfwmd.state.fl.us/pub/out/TPolk.zip";
        const long URL_FILE_LENGTH = 789556976;

        SeekableFtpFileStream _stream;

        [TestInitialize]
        public void Initialize()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.swfwmd.state.fl.us/pub/out/TPolk.zip");

            request.Credentials = new NetworkCredential("anonymous", "tests@toolbox.org");
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            _stream = new SeekableFtpFileStream(request);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _stream.Dispose();
        }

        [TestMethod]
        public void SeekBeginTest()
        {
            long target = 8;
            long expected = 8;
            long position = 0;

            position = _stream.Seek(target, SeekOrigin.Begin);

            Assert.AreEqual(expected, position);
        }

        [TestMethod]
        public void SeekCurrentTest()
        {
            long target = 8;
            long expected = 8;
            long position = 0;

            position = _stream.Seek(target, SeekOrigin.Current);

            Assert.AreEqual(expected, position);
        }

        [TestMethod]
        public void SeekEndTest()
        {
            long target = -8;
            long expected = URL_FILE_LENGTH - 8;
            long position = 0;

            position = _stream.Seek(target, SeekOrigin.End);

            Assert.AreEqual(expected, position);
        }

        [TestMethod]
        public void ReadBeginTest()
        {
            byte[] expectedBytes = new byte[] { 80, 75, 3, 4, 20, 0, 0, 0 };
            int expectedBytesRead = 8;

            byte[] buffer = new byte[8];
            int bytesRead;

            bytesRead = _stream.Read(buffer, 0, buffer.Length);

            Assert.IsTrue(buffer.SequenceEqual(expectedBytes));
            Assert.AreEqual(bytesRead, expectedBytesRead);
        }

        [TestMethod]
        public void ReadCurrentTest()
        {
            byte[] expectedBytes = new byte[] { 0, 0, 184, 170, 15, 47, 0, 0 };
            int expectedBytesRead = 8;

            byte[] buffer = new byte[8];
            int bytesRead;

            _stream.Seek(-16, SeekOrigin.End);
            _stream.Seek(8, SeekOrigin.Current);

            bytesRead = _stream.Read(buffer, 0, buffer.Length);

            Assert.IsTrue(buffer.SequenceEqual(expectedBytes));
            Assert.AreEqual(bytesRead, expectedBytesRead);
        }

        [TestMethod]
        public void ReadEndTest()
        {
            byte[] expectedBytes = new byte[] { 0, 0, 184, 170, 15, 47, 0, 0 };
            int expectedBytesRead = 8;

            long target = -8;

            byte[] buffer = new byte[8];
            int bytesRead;

            _stream.Seek(target, SeekOrigin.End);

            bytesRead = _stream.Read(buffer, 0, buffer.Length);

            Assert.IsTrue(buffer.SequenceEqual(expectedBytes));
            Assert.AreEqual(bytesRead, expectedBytesRead);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SeekNegativeBeginTest()
        {
            long target = -8;

            _stream.Seek(target, SeekOrigin.Begin);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SeekPositiveEndTest()
        {
            long target = 8;

            _stream.Seek(target, SeekOrigin.End);
        }

        [TestMethod]
        public void CanReadAtStartTest()
        {
            Assert.IsTrue(_stream.CanRead);
        }

        [TestMethod]
        public void CanReadAtEndTest()
        {
            _stream.Seek(0, SeekOrigin.End);
            Assert.IsFalse(_stream.CanRead);
        }

        [TestMethod, ExpectedException(typeof(NotSupportedException))]
        public void SetLengthTest()
        {
            long length = 100;

            _stream.SetLength(length);
        }
        
        [TestMethod, ExpectedException(typeof(NotSupportedException))]
        public void WriteTest()
        {
            byte[] buffer = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            _stream.Write(buffer, 0, buffer.Length);
        }

        [TestMethod]
        public void CanWriteTest()
        {
            Assert.IsFalse(_stream.CanWrite);
        }

        [TestMethod]
        public void CanSeekTest()
        {
            Assert.IsTrue(_stream.CanSeek);
        }

        [TestMethod, ExpectedException(typeof(NotSupportedException))]
        public void FlushTest()
        {
            _stream.Flush();
        }

        [TestMethod]
        public void LengthTest()
        {
            Assert.AreEqual(URL_FILE_LENGTH, _stream.Length);
        }

        [TestMethod]
        public void PositionTest()
        {
            int expected = 8;

            _stream.Seek(8, SeekOrigin.Begin);

            Assert.AreEqual(expected, _stream.Position);
        }

        [TestMethod]
        public void ReadByteTest()
        {
            int expectedPosition = 1;
            int expectedByte = 80;

            int readByte = _stream.ReadByte();

            Assert.AreEqual(expectedPosition, _stream.Position);
            Assert.AreEqual(expectedByte, 80);
        }
    }
}
