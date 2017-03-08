using Toolbox.Threading.Exception;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Threading.Base
{
    public abstract class EndBlock<TInput> : Block, IConsumer<TInput>
    {
        private BlockingCollection<TInput> _in = new BlockingCollection<TInput>();
        
        public BlockingCollection<TInput> Source
        {
            get { return _in; }
            set { _in = value; }
        }

        public override void Execute(BlockArgs args)
        {
            foreach (TInput item in Source.GetConsumingEnumerable())
            {
                try
                {
                    Process(item);
                }
                catch (PipelineProcessingException) { }

                if (Token.IsCancellationRequested)
                {
                    break;
                }
            }
        }

        public override void Done()
        {
        }

        protected abstract void Process(TInput item);
    }
}
