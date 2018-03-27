using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
