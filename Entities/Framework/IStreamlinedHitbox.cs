using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.ObjectModel;

namespace SandboxThingy.Entity.Framework
{
    interface IStreamlinedHitbox
    {
        ReadOnlyCollection<Rectangle> Hitbox { get; }
        bool CollidesWith<T>(T other) where T : IEnumerable<Rectangle>;
        bool CollidesWith(Hitbox other);
        bool CollidesWithMultiple<T>(T other) where T : IEnumerable<Hitbox>, IEnumerable<IEnumerable<Rectangle>>;
        void StreamlineHitboxes();
    }
}
