using System;

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
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException("Not a hexidecimal string.", hexString);
            }

            if (hexString.StartsWith("0x"))
            {
                hexString = hexString.Substring(2);
            }

            var bytes = new byte[hexString.Length / 2];

            for (var i = 0; i < hexString.Length; i += 2)
            {
                var byteString = hexString.Substring(i, 2);
                bytes[i / 2] = Convert.ToByte(byteString, 16);
            }

            return bytes;
        }
    }
}
