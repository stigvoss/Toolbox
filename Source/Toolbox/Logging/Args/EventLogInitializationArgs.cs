using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Logging.Base;

namespace Toolbox.Logging.Args
{
    public class EventLogInitializationArgs : IProviderInitializationArgs
    {
        public string Source { get; set; }
        public string Log { get; set; }
    }
}
