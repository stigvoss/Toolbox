using System;
using System.Xml;

namespace Toolbox.Xml.Extensions
{
    public static class XmlReaderExtensions
    {
        public static DateTimeOffset ReadElementContentAsDateTimeOffset(this XmlReader reader)
        {
            var content = reader.ReadElementContentAsString();

            if (!DateTimeOffset.TryParse(content, out DateTimeOffset dateTime))
            {
                throw new InvalidCastException();
            }

            return dateTime;
        }
    }
}
