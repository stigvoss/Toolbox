using System;
using System.IO;

namespace Toolbox.Extensions
{
    public static class UriExtensions
    {
        public static DriveInfo GetDriveInfo(this Uri uri)
        {
            return new DriveInfo(Path.GetPathRoot(uri.LocalPath));
        }

        public static DirectoryInfo GetDirectoryInfo(this Uri uri)
        {
            if (uri.IsFile)
            {
                throw new ArgumentException("Uri is not a directory");
            }

            return new DirectoryInfo(uri.LocalPath);
        }
    }
}
