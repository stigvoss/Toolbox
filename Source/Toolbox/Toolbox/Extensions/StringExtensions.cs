using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Extensions
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string text)
        {
            if (text is null)
            {
                return null;
            }

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
        }

        public static byte[] ToBytes(this string value, Encoding encoding = null)
        {
            if (value is null)
            {
                return null;
            }

            if (encoding is null)
            {
                encoding = Encoding.UTF8;
            }

            return encoding.GetBytes(value);
        }
    }
}
