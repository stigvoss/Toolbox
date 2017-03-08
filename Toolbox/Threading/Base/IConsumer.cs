using System.Collections.Concurrent;

namespace Toolbox.Threading.Base
{
    public interface IConsumer<TInput>
    {
        BlockingCollection<TInput> Source { get; set; }
    }
}