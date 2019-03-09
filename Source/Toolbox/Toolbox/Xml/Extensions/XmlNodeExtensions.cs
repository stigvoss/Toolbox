﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Toolbox.Xml.Extensions
{
    public static class XmlNodeExtensions
    {
        public static T GetElementContentAs<T>(this XmlNode node, string xpath, XmlNamespaceManager nsmgr = null)
        {
            XmlNode element;
            T result = default;

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

            return result;
        }

        public static T GetElementContentAs<T>(this XmlNode node)
        {
            string value = node.InnerText;
            T result = (T)Convert.ChangeType(value, typeof(T));
            return result;
        }
    }
}
