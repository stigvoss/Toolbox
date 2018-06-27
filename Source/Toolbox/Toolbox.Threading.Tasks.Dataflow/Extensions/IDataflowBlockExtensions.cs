using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace Toolbox.Threading.Tasks.Dataflow.Extensions
{
    public static class IDataflowBlockExtensions
    {
        public static void LinkTo<T>(this IDataflowBlock source, ITargetBlock<T> target, DataflowLinkOptions options)
        {
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
