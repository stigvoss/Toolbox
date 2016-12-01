using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Extensions
{
    public static class UriExtensions
    {
        public static DriveInfo GetDriveInfo(this Uri uri)
        {
            string root = Path.GetPathRoot(uri.LocalPath);
            return new DriveInfo(root);
        }

        public static DirectoryInfo GetDirectoryInfo(this Uri uri)
        {
            if (uri.IsFile)
                throw new ArgumentException("Uri is not a directory");

            return new DirectoryInfo(uri.LocalPath);
        }
    }
}
