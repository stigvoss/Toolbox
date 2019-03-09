using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Toolbox.IO.Extensions
{
    public static class StreamReaderExtensions
    {
        public static bool ReadLine(this StreamReader reader, out string line)
        {
            line = reader.ReadLine();

            return line != null;
        }
    }
}
