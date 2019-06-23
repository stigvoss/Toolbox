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

        public static async Task ParallelForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, Task> action)
        {
            await enumerable.ParallelForEachAsync(action, Environment.ProcessorCount);
        }

        public static async Task ParallelForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, Task> action, int threadCount)
        {
            using var semaphore = new Semaphore(threadCount, threadCount);

            var tasks = enumerable.Select(async item =>
            {
                semaphore.WaitOne();

                try
                {
                    await action(item);
                }
                finally
                {
                    semaphore.Release();
                }
            });

            await Task.WhenAll(tasks);
        }
    }
}
