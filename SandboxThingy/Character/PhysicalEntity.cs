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
    public abstract class PhysicalEntity : Entity, ICollidable
    {
        protected Rectangle collisionArea;

        public Rectangle CollisionArea => collisionArea;

        public PhysicalEntity(Rectangle collisionArea, Texture2D texture, uint id = 0)
            : base(collisionArea.Location, texture, id) => this.collisionArea = collisionArea;
        public PhysicalEntity(Point location, int width, int height, Texture2D texture, uint id = 0)
            : base(location, texture, id) => collisionArea = new Rectangle(location, new Point(width, height));
        public PhysicalEntity(Point location, Point size, Texture2D texture, uint id = 0)
            : this(new Rectangle(location, size), texture, id) { }
        public PhysicalEntity(int x, int y, int width, int height, Texture2D texture, uint id = 0)
            : this(new Rectangle(x, y, width, height), texture, id) { }

        public bool CollidesWith(Rectangle otherCollisionArea) => collisionArea.Intersects(otherCollisionArea);
        public bool CollidesWith(params Rectangle[] otherCollisionAreas) => otherCollisionAreas.Any(colArea => colArea.Intersects(collisionArea));
    }
}
