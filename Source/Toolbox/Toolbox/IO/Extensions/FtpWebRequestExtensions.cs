using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Toolbox.IO;

namespace Toolbox.IO.Extensions
{
    public static class FtpWebRequestExtensions
    {
        public static SeekableFtpFileStream GetSeekableFileStream(this FtpWebRequest request)
        {
            return new SeekableFtpFileStream(request);
        }

        public static FtpWebRequest Clone(this FtpWebRequest request)
        {
            Type type = typeof(FtpWebRequest);

            FtpWebRequest clone = (FtpWebRequest)WebRequest.Create(request.RequestUri);

            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.CanRead && property.CanWrite)
                {
                    try
                    {
                        object originalValue = property.GetValue(request);
                        object cloneValue = property.GetValue(clone);

                        if (originalValue != null && !originalValue.Equals(cloneValue))
                        {
                            property.SetValue(clone, originalValue);
                        }
                    }
                    catch (TargetInvocationException ex)
                    {
                        Type exceptionType = ex.InnerException.GetType();
                        if (exceptionType != typeof(NotSupportedException) && exceptionType != typeof(NotImplementedException))
                            throw ex;
                    }
                }
            }

            return clone;
        }
    }
}
