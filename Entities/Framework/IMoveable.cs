using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandboxThingy.Entity.Framework
{
    public interface IMoveable
    {
        Vector2 Speed { get; set; }

        void Move();
    }
}
