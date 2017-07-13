using System;
using System.Net;
using Toolbox.Extensions;
using System.IO;
using Toolbox.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ToolboxTests
{
    [TestClass]
    public class FtpWebRequestExtensionsTest
    {
        const string URL_FILE = "ftp://ftp.eenet.ee/pub/mariadb/mariadb-10.1.19/winx64-packages/mariadb-10.1.19-winx64.zip";
        const long URL_FILE_LENGTH = 333630731;

        [TestMethod]
        public void CloneTest()
        {
            string url = "ftp://ftp.contoso.com/";
            string username = "admin";
            string password = "password";
            bool enableSsl = false;
            string headerName = "Accept-Range";
            string headerValue = "bytes";

            FtpWebRequest original = (FtpWebRequest)WebRequest.Create(url);
            original.Credentials = new NetworkCredential(username, password);
            original.EnableSsl = enableSsl;
            original.Headers.Add(headerName, headerValue);

            FtpWebRequest clone = original.Clone();

            NetworkCredential cloneCredentials = (NetworkCredential)clone.Credentials;

            Assert.AreEqual(url, clone.RequestUri.AbsoluteUri);
            Assert.AreEqual(username, cloneCredentials.UserName);
            Assert.AreEqual(password, cloneCredentials.Password);
            Assert.AreEqual(enableSsl, clone.EnableSsl);
            Assert.AreEqual(1, clone.Headers.Count);
        }

        [TestMethod]
        public void GetSeekableResponseStreamTest()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(URL_FILE);

            request.Credentials = new NetworkCredential("anonymous", "tests@toolbox.org");
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            Stream stream = request.GetSeekableFileStream();

            Assert.IsInstanceOfType(stream, typeof(SeekableFtpFileStream));
        }
    }
}
