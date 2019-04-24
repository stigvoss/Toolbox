using System.IO;

namespace Toolbox.IO.Extensions
{
    public static class DirectoryInfoExtensions
    {
        public static DriveInfo GetDriveInfo(this DirectoryInfo directory)
        {
            return new DriveInfo(directory.Root.FullName);
        }
    }
}
