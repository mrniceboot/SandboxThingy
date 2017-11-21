using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SandboxThingy.Character
{
    interface ICollidable
    {
        Rectangle CollisionArea { get; }
        bool CollidesWith(Rectangle otherCollisionArea);
        bool CollidesWith(params Rectangle[] otherCollisionAreas);
        //TODO: Projected course
        //bool IsGoingToCollideWith();
    }
}
