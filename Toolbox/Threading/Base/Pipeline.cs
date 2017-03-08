using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Toolbox.Threading.Base
{
    public class Pipeline
    {
        private TaskFactory _factory;

        private List<Block> _blocks = new List<Block>();

        private List<Task> _tasks = null;

        private CancellationTokenSource _tokenSource;

        private bool _isRunning = false;
        private bool _isCancelled = false;

        private CancellationTokenSource TokenSource
        {
            get { return _tokenSource; }
        }

        internal CancellationToken Token
        {
            get { return _tokenSource.Token; }
        }

        public bool IsRunning
        {
            get { return _isRunning; }
        }

        public bool IsCancelled
        {
            get { return _isCancelled; }
        }

        public void Cancel()
        {
            _isCancelled = true;
            TokenSource.Cancel();

            Task.WaitAll(this);
            _isRunning = false;
        }

        internal List<Task> Tasks
        {
            get { return _tasks; }
        }

        private Pipeline()
        {
            _tokenSource = new CancellationTokenSource();
            _factory = new TaskFactory(_tokenSource.Token);
        }

        internal void Add(Block block)
        {
            if(_isRunning)
            {
                throw new InvalidOperationException("Pipeline is already running.");
            }

            _blocks.Add(block);
        }

        public Pipeline Start()
        {
            if(_isRunning)
            {
                throw new InvalidOperationException("Pipeline is already running");
            }

            _tasks = new List<Task>();

            foreach (Block block in _blocks)
            {
                BlockArgs config = block.Arguments;

                for (int i = 0; i < config.ThreadCount; i++)
                {
                    Task task = Task.Factory.StartNew(() => block.Run(), TaskCreationOptions.LongRunning);
                    _tasks.Add(task);
                }
            }

            _isRunning = true;

            return this;
        }

        public static TBlock First<TBlock>(int threadCount = 1, int bufferCapacity = int.MaxValue, BlockArgs args = null) where TBlock : Block, new()
        {
            if (args == null)
            {
                args = new BlockArgs();
            }

            args.ThreadCount = threadCount;
            args.BufferCapacity = bufferCapacity;

            return First<TBlock>(args);
        }

        public static TBlock First<TBlock>(BlockArgs args) where TBlock : Block, new()
        {
            TBlock block = new TBlock();
            Pipeline pipeline = new Pipeline();

            block.Pipeline = pipeline;
            block.Arguments = args;

            pipeline.Add(block);

            return block;
        }

        public static implicit operator Task[] (Pipeline pipeline)
        {
            return pipeline.Tasks.ToArray();
        }
    }
}