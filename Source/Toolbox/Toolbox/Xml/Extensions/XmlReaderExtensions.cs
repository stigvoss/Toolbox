using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Toolbox.Xml.Extensions
{
    public static class XmlReaderExtensions
    {
        public static DateTimeOffset ReadElementContentAsDateTimeOffset(this XmlReader reader)
        {
            DateTimeOffset dateTime;
            string content = reader.ReadElementContentAsString();

            if(!DateTimeOffset.TryParse(content, out dateTime))
            {
                throw new InvalidCastException();
            }

            return dateTime;
        }
    }
}
