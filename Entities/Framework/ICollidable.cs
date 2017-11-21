using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SandboxThingy.Entity.Framework
{
    interface ICollidable
    {
        Rectangle CollisionArea { get; }
        bool CollidesWith(Rectangle otherCollisionArea);
        bool CollidesWith<T>(T otherCollisionAreas) where T : IEnumerable<Rectangle>;
        bool CollidesWith(params Rectangle[] otherCollisionAreas);
        //TODO: Projected course
        //bool IsGoingToCollideWith();
    }
}
