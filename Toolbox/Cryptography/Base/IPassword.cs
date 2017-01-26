using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Cryptography.Base
{
    public interface IPassword<T>
    {
        T Content { get; set; }
    }
}
