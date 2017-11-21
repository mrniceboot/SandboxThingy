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
    public abstract class Entity : IEntity
    {
        protected Point location;
        protected Texture2D texture;
        protected uint id;

        public virtual Point Location { get => location; private set => location = value; }
        public virtual Texture2D Texture { get => texture; private set => texture = value; }
        public uint ID { get => id; }

        public Entity(Point location, Texture2D texture, uint id = 0)
        {
            this.location = location;
            this.texture = texture;
            this.id = id;
        }
        public Entity(int x, int y, Texture2D texture, uint id = 0) : this(new Point(x, y), texture, id) { }

        public virtual void Update() { }
        public virtual void Draw(SpriteBatch sb) => sb.Draw(texture, location.ToVector2(), Color.White);
    }
}
