using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading.Base;

namespace Toolbox.Threading
{
    public class Bundle<T> : LinkBlock<T, IEnumerable<T>>
    {
        List<T> _bundle = null;

        private const int SIZE = 10000;

        protected override IEnumerable<T> Process(T item)
        {
            List<T> result = null;

            if (_bundle == null)
            {
                _bundle = new List<T>();
            }

            _bundle.Add(item);

            if(_bundle.Count == SIZE)
            {
                result = _bundle;
                _bundle = null;
            }

            return result;
        }
    }
}
