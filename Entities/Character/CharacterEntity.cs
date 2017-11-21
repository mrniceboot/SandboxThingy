using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SandboxThingy.Entity.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SandboxThingy.Entity.Character
{
    public class CharacterEntity : Framework.Entity, IMoveable, ICollidable, IEntity
    {
        //TODO: Be able to add custom attributes and events

        public Hitbox CharacterHitbox { get; }
        public Vector2 Speed { get; set; }
        public Rectangle CollisionArea => (CharacterHitbox as ICollidable).CollisionArea;

        public CharacterEntity(Texture2D charTexture, Vector2 speed, uint id) : base(new Point(-5000), charTexture, id)
        {
            Speed = speed;
            CharacterHitbox = Hitbox.StreamlinedHitbox(charTexture);
        }
        public CharacterEntity(Texture2D charTexture, Vector2 speed, uint id, Point startLocation) : base(startLocation, charTexture, id)
        {
            Speed = speed;
            CharacterHitbox = Hitbox.StreamlinedHitbox(charTexture);
        }

        /// <summary>
        /// Moves the CharacterEntity according to the field <see cref="Speed"/>
        /// </summary>
        public void Move()
        {
            location.X += (int)Speed.X;
            location.Y += (int)Speed.Y;
        }

        public bool CollidesWith(Rectangle otherCollisionArea) => CharacterHitbox.CollidesWith(otherCollisionArea);
        public bool CollidesWith<T>(T otherCollisionAreas) where T : IEnumerable<Rectangle> => CharacterHitbox.CollidesWith(otherCollisionAreas);
        public bool CollidesWith(params Rectangle[] otherCollisionAreas) => CharacterHitbox.CollidesWith(otherCollisionAreas: otherCollisionAreas);

        //TODO: Add update logic
        public override void Update()
        {
            base.Update();
        }
        //TODO: Add draw functionality
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }

    }
}
