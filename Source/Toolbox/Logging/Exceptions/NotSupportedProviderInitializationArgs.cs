using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Logging.Exceptions
{
    public class UnsupportedProviderInitializationArgs : ArgumentException
    {
        public UnsupportedProviderInitializationArgs(string message) : base(message)
        {
        }
    }
}
