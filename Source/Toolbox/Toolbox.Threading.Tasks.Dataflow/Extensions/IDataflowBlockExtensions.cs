using System;
using System.Threading.Tasks.Dataflow;

namespace Toolbox.Threading.Tasks.Dataflow.Extensions
{
    public static class IDataflowBlockExtensions
    {
        public static void LinkTo<T>(this IDataflowBlock source, ITargetBlock<T> target, DataflowLinkOptions options)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            
            source.Completion.ContinueWith(task =>
            {
                if (options.PropagateCompletion)
                {
                    target.Complete();
                }
            });
        }
    }
}
