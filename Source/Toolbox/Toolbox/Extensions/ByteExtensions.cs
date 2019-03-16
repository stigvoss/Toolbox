using System;
using System.Collections.Generic;
using System.Text;

namespace Toolbox.Extensions
{
    public static class ByteExtensions
    {
        public static string ToHex(this byte[] bytes)
        {
            var hexidecimal = BitConverter.ToString(bytes);
            return hexidecimal.Replace("-", "").ToLower();
        }

        public static byte[] FromHex(this string hexString)
        {
            var bytes = new byte[hexString.Length / 2];

            var startIndex = 0;

            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException("Not a hexidecimal string.", hexString);
            }

            if (hexString[0] == '0' && hexString[1] == 'x')
            {
                startIndex = 2;
            }

            for (var i = startIndex; i < hexString.Length; i += 2)
            {
                var byteString = hexString.Substring(i, 2);
                bytes[i / 2] = Convert.ToByte(byteString, 16);
            }

            return bytes;
        }
    }
}
