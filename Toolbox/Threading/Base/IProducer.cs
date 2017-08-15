using System.Collections.Concurrent;

namespace Toolbox.Threading.Base
{
    public interface IProducer<TOutput>
    {
        BlockingCollection<TOutput> Output { get; }

        TBlock Then<TBlock>(BlockArgs args) where TBlock : Block, IConsumer<TOutput>, new();
    }
}