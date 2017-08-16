using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Collections
{
    public class IndexedEnumerable<T> : IEnumerable<(T element, int index)>
    {
        private readonly IEnumerable<T> _collection;

        public IndexedEnumerable(IEnumerable<T> collection)
        {
            _collection = collection;
        }

        public IEnumerator<(T element, int index)> GetEnumerator()
        {
            return new IndexedEnumerator<T>(_collection);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new IndexedEnumerator<T>(_collection);
        }
    }
}
