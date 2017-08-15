using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Toolbox.Threading.Base
{
    public interface IBlock
    {
        Pipeline Pipeline { get; set; }

        void Initialize(BlockArgs args);
        
        void Execute();

        void Done();
    }
}
