using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Toolbox.Collections.Concurrent;

namespace Toolbox.Collections.Generic.Extensions
{
    public static class IEnumerableExtensions
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> collection)
        {
            return new HashSet<T>(collection);
        }

        public static HashSet<TResult> ToHashSet<TSource, TResult>(
            this IEnumerable<TSource> collection,
            Func<TSource, TResult> selector)
        {
            return new HashSet<TResult>(collection.Select(selector));
        }

        public static ConcurrentHashSet<T> ToConcurrentHashSet<T>(this IEnumerable<T> collection)
        {
            return new ConcurrentHashSet<T>(new HashSet<T>(collection));
        }

        public static ConcurrentHashSet<TResult> ToConcurrentHashSet<TSource, TResult>(
            this IEnumerable<TSource> collection,
            Func<TSource, TResult> selector)
        {
            return new ConcurrentHashSet<TResult>(new HashSet<TResult>(collection.Select(selector)));
        }

        public static IndexedEnumerable<TResult> AsIndexedEnumerable<TResult>(this IEnumerable<TResult> collection)
        {
            return new IndexedEnumerable<TResult>(collection);
        }

        public static IEnumerable<Task> ParallelForEachAsync<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            return enumerable.ParallelForEachAsync(action, Environment.ProcessorCount);
        }

        public static IEnumerable<Task> ParallelForEachAsync<T>(this IEnumerable<T> enumerable, Action<T> action, int processorCount)
        {
            var semaphore = new SemaphoreSlim(processorCount);

            return enumerable.Select(async item =>
            {
                await semaphore.WaitAsync();

                await Task.Run(() => action(item));

                semaphore.Release();
            });
        }
    }
}
