using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandboxThingy.Character
{
    public interface IMoveable
    {
        int Speed { get; set; }
        void MoveXPlus();
        void MoveXMinus();
        void MoveYPlus();
        void MoveYMinus();
    }
}
