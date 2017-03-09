using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Threading.Collection
{
    public class ConcurrentHashSet<T> : ISet<T>
    {
        HashSet<T> _hashSet = new HashSet<T>();

        public ConcurrentHashSet(HashSet<T> hashSet)
        {
            _hashSet = hashSet;
        }

        public int Count
        {
            get
            {
                return _hashSet.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool Add(T item)
        {
            lock (_hashSet)
            {
                return _hashSet.Add(item);
            }
        }

        public void Clear()
        {
            lock (_hashSet)
            {
                _hashSet.Clear();
            }
        }

        public bool Contains(T item)
        {
            lock (_hashSet)
            {
                return _hashSet.Contains(item);
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (_hashSet)
            {
                _hashSet.CopyTo(array, arrayIndex);
            }
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            lock (_hashSet)
            {
                _hashSet.ExceptWith(other);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _hashSet.GetEnumerator();
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            lock (_hashSet)
            {
                _hashSet.IntersectWith(other);
            }
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            lock (_hashSet)
            {
                return _hashSet.IsProperSubsetOf(other);
            }
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            lock (_hashSet)
            {
                return _hashSet.IsProperSupersetOf(other);
            }
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            lock (_hashSet)
            {
                return _hashSet.IsSubsetOf(other);
            }
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            lock (_hashSet)
            {
                return _hashSet.IsSupersetOf(other);
            }
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            lock (_hashSet)
            {
                return _hashSet.Overlaps(other);
            }
        }

        public bool Remove(T item)
        {
            lock (_hashSet)
            {
                return _hashSet.Remove(item);
            }
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            lock (_hashSet)
            {
                return _hashSet.SetEquals(other);
            }
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            lock (_hashSet)
            {
                _hashSet.SymmetricExceptWith(other);
            }
        }

        public void UnionWith(IEnumerable<T> other)
        {
            lock (_hashSet)
            {
                _hashSet.UnionWith(other);
            }
        }

        void ICollection<T>.Add(T item)
        {
            lock (_hashSet)
            {
                _hashSet.Add(item);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _hashSet.GetEnumerator();
        }
    }
}
