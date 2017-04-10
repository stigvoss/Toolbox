using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Threading.Base
{
    public abstract class InitialBlock<TOutput> : Block, IProducer<TOutput>
    {
        private BlockingCollection<TOutput> _out;

        public InitialBlock()
        {
            int capacity = Arguments.BufferCapacity;
            _out = new BlockingCollection<TOutput>(capacity);
        }

        public BlockingCollection<TOutput> Output
        {
            get { return _out; }
        }

        public override void Initialize(BlockArgs args)
        {
        }

        public override void Execute()
        {
            Initialize(Arguments);
            Execute(Output);
            Done();
        }

        public abstract void Execute(BlockingCollection<TOutput> output);

        public override void Done()
        {
            _out.CompleteAdding();
        }

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

        public Pipeline Finally<TBlock>(int threadCount = 1, BlockArgs args = null) where TBlock : EndBlock<TOutput>, new()
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

        public Pipeline Finally<TBlock>(BlockArgs args) where TBlock : EndBlock<TOutput>, new()
        {
            TBlock block = new TBlock()
            {
                Pipeline = Pipeline,
                Source = Output,
                Arguments = args
            };
            Pipeline.Add(block);

            Pipeline.Start();

            return this;
        }
    }
}
