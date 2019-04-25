﻿using System.Collections;
using System.Collections.Generic;

namespace Toolbox.Collections.Generic
{
    public class IndexedEnumerator<T> : IEnumerator<(T element, int index)>
    {
        private readonly IEnumerator<T> _enumerator;
        private int _index = -1;

        public IndexedEnumerator(IEnumerable<T> collection)
        {
            _enumerator = collection.GetEnumerator();
        }

        public IndexedEnumerator(IEnumerator<T> enumerator)
        {
            _enumerator = enumerator;
        }

        public (T element, int index) Current => (_enumerator.Current, _index);

        object IEnumerator.Current => Current;

        public void Dispose() => _enumerator.Dispose();

        public bool MoveNext()
        {
            var result = _enumerator.MoveNext();

            if (result)
            {
                _index++;
            }

            return result;
        }

        public void Reset()
        {
            _enumerator.Reset();
            _index = -1;
        }
    }
}
