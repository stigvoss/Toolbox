using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Toolbox.Xml.Extensions
{
    public static class XmlNodeExtensions
    {
        public static T GetElementContentAs<T>(this XmlNode node, string xpath, XmlNamespaceManager? nsmgr = null)
        {
            XmlNode element;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            T result = default;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            if (nsmgr != null)
            {
                element = node.SelectSingleNode(xpath, nsmgr);
            }
            else
            {
                element = node.SelectSingleNode(xpath);
            }


            if (element != null)
            {
                result = element.GetElementContentAs<T>();
            }

#pragma warning disable CS8603 // Possible null reference return.
            return result;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public static T GetElementContentAs<T>(this XmlNode node)
        {
            string value = node.InnerText;
            T result = (T)Convert.ChangeType(value, typeof(T));
            return result;
        }
    }
}
