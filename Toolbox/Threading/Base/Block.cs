using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Toolbox.Threading.Base
{
    public abstract class Block : IBlock
    {
        private Pipeline _pipeline = null;
        private CancellationToken _token;

        private int _threadsDone = 0;

        private BlockArgs _args = new BlockArgs();

        internal BlockArgs Arguments
        {
            get { return _args; }
            set { _args = value; }
        }

        public Pipeline Pipeline
        {
            get { return _pipeline; }
            set { _pipeline = value; }
        }

        protected CancellationToken Token
        {
            get { return _pipeline.Token; }
        }

        public Pipeline Start()
        {
            Pipeline.Start();

            return Pipeline;
        }

        public abstract void Execute(BlockArgs args);

        public void Run()
        {
            try
            {
                Initialize();
                Execute(_args);
            }
            finally
            {
                Interlocked.Increment(ref _threadsDone);
                if(_threadsDone == Arguments.ThreadCount)
                {
                    Done();
                }
            }
        }

        public abstract void Done();

        public abstract void Initialize();

        public static implicit operator Pipeline(Block block)
        {
            return block.Pipeline;
        }
    }
}
