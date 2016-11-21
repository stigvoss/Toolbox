using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Extensions
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
    }
}
