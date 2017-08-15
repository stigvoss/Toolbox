using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Threading
{
    public class BlockArgs
    {
        private const int DEFAULT_THREAD_COUNT = 1;
        private const int DEFAULT_BUFFER_CAPACITY = int.MaxValue;

        private int _threadCount = DEFAULT_THREAD_COUNT;
        private int _bufferCapacity = DEFAULT_THREAD_COUNT;

        public int BufferCapacity
        {
            get { return _bufferCapacity; }
            set { _bufferCapacity = value; }
        }

        public int ThreadCount
        {
            get { return _threadCount; }
            set { _threadCount = value; }
        }
    }
}
