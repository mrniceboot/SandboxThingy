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
    public abstract class MovingEntity : Entity, IMoveable
    {
        public int Speed { get; set; }

        public void MoveXMinus() => location.X -= Speed;
        public void MoveXPlus() => location.X +=  Speed;
        public void MoveYMinus() => location.Y -= Speed;
        public void MoveYPlus() => location.Y +=  Speed;

        public MovingEntity(Point location, int speed, Texture2D texture, uint id) : base(location, texture, id = 0) => Speed = speed;
        public MovingEntity(int x, int y, int speed, Texture2D texture, uint id) : base(x, y, texture, id = 0) => Speed = speed;
    }
}
