using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Collections.Generic
{
    public class IndexedEnumerable<T> : IEnumerable<(T element, int index)>
    {
        private readonly IEnumerable<T> _collection;

        public IndexedEnumerable(IEnumerable<T> collection)
        {
            _collection = collection;
        }

        public IEnumerator<(T element, int index)> GetEnumerator() => new IndexedEnumerator<T>(_collection);

        IEnumerator IEnumerable.GetEnumerator() => new IndexedEnumerator<T>(_collection);
    }
}
