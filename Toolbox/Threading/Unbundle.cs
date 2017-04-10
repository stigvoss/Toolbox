using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading.Base;

namespace Toolbox.Threading
{
    public class Unbundle<T> : LinkBlock<IEnumerable<T>, T>
    {

        protected override T Process(IEnumerable<T> items)
        {
            foreach(T item in items)
            {
                Output.Add(item);
            }

            return default(T);
        }
    }
}
