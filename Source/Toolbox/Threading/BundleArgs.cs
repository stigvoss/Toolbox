using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Threading
{
    public class BundleArgs : BlockArgs
    {
        public static BundleArgs Create(int size = 1000)
        {
            return new BundleArgs
            {
                Size = size
            };
        }

        public int Size { get; set; }
    }
}
