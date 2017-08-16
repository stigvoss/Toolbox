using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Extensions
{
    public static class BlockingCollectionExtensions
    {
        public static void AddRange<T>(this BlockingCollection<T> target, IEnumerable<T> collection)
        {
            foreach (T obj in collection)
            {
                target.Add(obj);
            }
        }
    }
}
