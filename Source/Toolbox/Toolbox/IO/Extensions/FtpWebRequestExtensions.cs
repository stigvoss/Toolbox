using System;
using System.Net;
using System.Reflection;

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
            var type = typeof(FtpWebRequest);

            var clone = (FtpWebRequest)WebRequest.Create(request.RequestUri);

            foreach (var property in type.GetProperties())
            {
                if (property.CanRead && property.CanWrite)
                {
                    try
                    {
                        var originalValue = property.GetValue(request);
                        var cloneValue = property.GetValue(clone);

                        if (originalValue is object && !originalValue.Equals(cloneValue))
                        {
                            property.SetValue(clone, originalValue);
                        }
                    }
                    catch (TargetInvocationException ex)
                    {
                        var exceptionType = ex.InnerException.GetType();
                        if (exceptionType != typeof(NotSupportedException) && exceptionType != typeof(NotImplementedException))
                        {
                            throw ex;
                        }
                    }
                }
            }

            return clone;
        }
    }
}
