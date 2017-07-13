using Toolbox.Threading.Exception;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Threading.Base
{
    public abstract class IntermediateBlock<TInput, TOutput> : Block, IProducer<TOutput>, IConsumer<TInput>
    {
        private BlockingCollection<TOutput> _out = new BlockingCollection<TOutput>();
        private BlockingCollection<TInput> _in = new BlockingCollection<TInput>();

        public BlockingCollection<TOutput> Output
        {
            get { return _out; }
        }

        public BlockingCollection<TInput> Source
        {
            get { return _in; }
            set { _in = value; }
        }

        public override void Execute()
        {
            foreach (TInput item in Source.GetConsumingEnumerable())
            {
                try
                {
                    TOutput handled = Process(item);
                    if(handled != null)
                    {
                        _out.Add(handled);
                    }
                }
                catch (PipelineProcessingException) { }

                if (Token.IsCancellationRequested)
                {
                    break;
                }
            }
        }

        public override void Initialize(BlockArgs args)
        {
        }

        public override void Done()
        {
            _out.CompleteAdding();
        }

        protected abstract TOutput Process(TInput item);

        public TBlock Then<TBlock>(int threadCount = 1, int bufferCapacity = int.MaxValue, BlockArgs args = null) where TBlock : Block, IConsumer<TOutput>, new()
        {
            if (args == null)
            {
                args = new BlockArgs();
            }

            args.ThreadCount = threadCount;
            args.BufferCapacity = bufferCapacity;

            return Then<TBlock>(args);
        }

        public Pipeline Finally<TBlock>(int threadCount = 1, BlockArgs args = null) where TBlock : FinalBlock<TOutput>, new()
        {
            if (args == null)
            {
                args = new BlockArgs();
            }

            args.ThreadCount = threadCount;

            return Finally<TBlock>(args);
        }

        public TBlock Then<TBlock>(BlockArgs args) where TBlock : Block, IConsumer<TOutput>, new()
        {
            TBlock block = new TBlock()
            {
                Pipeline = Pipeline,
                Source = Output,
                Arguments = args
            };
            Pipeline.Add(block);

            return block;
        }

        public Pipeline Finally<TBlock>(BlockArgs args) where TBlock : FinalBlock<TOutput>, new()
        {
            TBlock block = new TBlock()
            {
                Pipeline = Pipeline,
                Source = Output,
                Arguments = args
            };
            Pipeline.Add(block);

            Pipeline.Start();

            return block;
        }
    }
}
