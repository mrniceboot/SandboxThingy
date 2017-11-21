using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandboxThingy.Entity.Framework
{
    interface IJumpable
    {
        float JumpHeight { get; set; }
        void Jump();
    }
}
