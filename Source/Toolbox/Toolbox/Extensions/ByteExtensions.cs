using System;
using System.Collections.Generic;
using System.Text;

namespace Toolbox.Extensions
{
    public static class ByteExtensions
    {
        public static string ToHexString(this byte[] bytes)
        {
            var hexidecimal = BitConverter.ToString(bytes);
            return hexidecimal.Replace("-", "").ToLower();
        }
    }
}
