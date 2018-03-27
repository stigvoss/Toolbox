using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Collections;
using Toolbox.Collections.Concurrent;
using Toolbox.Collections.Generic;

namespace Toolbox.Collections.Generic.Extensions
{
    public static class IEnumerableExtensions
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> collection)
        {
            return new HashSet<T>(collection);
        }

        public static HashSet<TResult> ToHashSet<TSource, TResult>(this IEnumerable<TSource> collection, Func<TSource, TResult> selector = null)
        {
            return new HashSet<TResult>(collection.Select(selector));
        }
        public static ConcurrentHashSet<T> ToConcurrentHashSet<T>(this IEnumerable<T> collection)
        {
            return new ConcurrentHashSet<T>(new HashSet<T>(collection));
        }

        public static ConcurrentHashSet<TResult> ToConcurrentHashSet<TSource, TResult>(this IEnumerable<TSource> collection, Func<TSource, TResult> selector = null)
        {
            return new ConcurrentHashSet<TResult>(new HashSet<TResult>(collection.Select(selector)));
        }

        public static IndexedEnumerable<TResult> AsIndexedEnumerable<TResult>(this IEnumerable<TResult> collection)
        {
            return new IndexedEnumerable<TResult>(collection);
        }
    }
}
