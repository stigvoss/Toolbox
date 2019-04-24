using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Toolbox.Collections.Concurrent.Extensions
{
    public static class BlockingCollectionExtensions
    {
        public static void AddRange<T>(this BlockingCollection<T> target, IEnumerable<T> collection)
        {
            foreach (var obj in collection)
            {
                target.Add(obj);
            }
        }
    }
}
