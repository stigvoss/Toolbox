using System.IO;

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
