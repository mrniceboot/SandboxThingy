using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SandboxThingy.Entity.Framework
{
    public struct Hitbox : IStreamlinedHitbox, ICollidable
    {
        private LinkedList<Rectangle> hitbox;
        private Rectangle generalArea;
        private bool generelized;

        ReadOnlyCollection<Rectangle> IStreamlinedHitbox.Hitbox => new ReadOnlyCollection<Rectangle>(hitbox as IList<Rectangle>);
        Rectangle ICollidable.CollisionArea => generalArea;

        public Hitbox(Rectangle generalHitbox)
        {
            generalArea = generalHitbox;
            hitbox = new LinkedList<Rectangle>();
            hitbox.AddLast(generalHitbox);
            generelized = false;
        }
        public Hitbox(IEnumerable<Rectangle> hitboxList)
        {
            generalArea = hitboxList.First();
            hitbox = new LinkedList<Rectangle>();
            foreach (var item in hitboxList)
                hitbox.AddLast(item);
            generelized = true;
            Generalize();
        }
        public Hitbox(params Rectangle[] hitboxParts) : this(hitboxList: hitboxParts)
        { }

        private void Generalize()
        {
            foreach (var item in hitbox)
            {
                if (item.X < generalArea.X)
                    generalArea.X = item.X;
                if (item.Y < generalArea.Y)
                    generalArea.Y = item.Y;
            }
            foreach (var item in hitbox)
            {
                if (item.X - generalArea.X + generalArea.Width > generalArea.Width)
                    generalArea.Width += item.X - generalArea.X;
                if (item.Y - generalArea.Y + generalArea.Height > generalArea.Height)
                    generalArea.Height += item.Y - generalArea.Y;
            }
        }

        /// <summary>
        /// Returns true if this hitbox collides(intersects) with another collision area, otherwise it will return false.
        /// </summary>
        /// <typeparam name="T">Any type of list consisting of Rectangle(s), hitbox is a valid substitution.</typeparam>
        /// <param name="otherCollisionArea">A list of rectangles.</param>
        /// <returns></returns>
        public bool CollidesWith<T>(T otherCollisionArea) where T : IEnumerable<Rectangle>
        {
            foreach (var otherItem in otherCollisionArea)
            {
                foreach (var item in hitbox)
                {
                    if (item.Intersects(otherItem))
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Returns true if this hitbox collides(intersects) with another collision area, otherwise it will return false.
        /// </summary>
        /// <param name="otherCollisionArea">A rectangle that represents a possible collision area.</param>
        /// <returns></returns>
        public bool CollidesWith(Rectangle otherCollisionArea) => hitbox.Any(area => area.Intersects(otherCollisionArea));
        /// <summary>
        /// Returns true if this hitbox collides(intersects) with another collision area, otherwise it will return false.
        /// </summary>
        /// <param name="other">Another hitbox. Self explanatory.</param>
        /// <returns></returns>
        public bool CollidesWith(Hitbox other) => CollidesWith((Rectangle)other);
        /// <summary>
        /// Returns true if this hitbox collides(intersects) with another collision area, otherwise it will return false.
        /// </summary>
        /// <param name="otherCollisionAreas">Multiple rectangles that represent multple collision areas, or parts of one to describe a complex figure.</param>
        /// <returns></returns>
        public bool CollidesWith(params Rectangle[] otherCollisionAreas)
        {
            foreach (var otherItem in otherCollisionAreas)
            {
                foreach (var item in hitbox)
                {
                    if (item.Intersects(otherItem))
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Returns true if this hitbox collides(intersects) with another collision area, otherwise it will return false.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="others">A data collection that represents several collision areas.</param>
        /// <returns></returns>
        public bool CollidesWithMultiple<T>(T others) where T : IEnumerable<Hitbox>, IEnumerable<IEnumerable<Rectangle>>
        {
            foreach (var item in others as IEnumerable<IEnumerable<Rectangle>>)
                if (CollidesWith(item))
                    return true;
            return false;
        }

        public void Add(Rectangle area) => hitbox.AddLast(area);

        /// <summary>
        /// Returns a streamlined hitbox for a specific texture, ignoring white and transparent (from Color).
        /// See <see cref="StreamlineHitboxes"/> for definition of streamlined.
        /// </summary>
        /// <param name="texture">The texture which the hitbox is based upon.</param>
        public static Hitbox StreamlinedHitbox(Texture2D texture) => StreamlinedHitbox(texture, Color.Transparent, Color.White);
        /// <summary>
        /// Returns a streamlined hibox for a specific texture.
        /// See <see cref="StreamlineHitboxes"/> for definition of streamlined.
        /// </summary>
        /// <param name="texture">The texture which the hitbox is based upon.</param>
        /// <param name="backgroundColors">The colors that should be ignored when creating the hitbox</param>
        /// <returns></returns>
        public static Hitbox StreamlinedHitbox(Texture2D texture, params Color[] backgroundColors)
        {
            var rowlist = new LinkedList<LinkedList<Rectangle>>();
            int width = texture.Width;
            Color[] colorData = null;
            texture.GetData(colorData);
            bool IsOnlyBGC()
            {
                foreach (var color in colorData)
                {
                    foreach (var bgc in backgroundColors)
                    {
                        if (color != bgc)
                            return false;
                    }
                }
                return true;
            }

            if (IsOnlyBGC())
                return new Hitbox(texture.Bounds);
            //Dissolve texture into smaller boxes
            for (int i = 0, y = 0, x = i % width; i < colorData.Length; i++)
            {
                if (backgroundColors.Any(color => color == colorData[i]))
                    continue;
                if (i % width == 0)
                {
                    rowlist.AddLast(new LinkedList<Rectangle>());
                    y++;
                }
                rowlist.Last.Value.AddLast(new Rectangle(x, y, 1, 1));
            }
            //Assimilate 'pixels' into rows
            for (int y = 0; y < rowlist.Count; y++)
            {
                var columlist = rowlist.ElementAt(y);
                var row = columlist.First.Value;
                row.Width = columlist.Count;
                rowlist.ElementAt(y).First.Value = row;
                for (int x = 1; x < columlist.Count; x++)
                    columlist.Remove(columlist.ElementAt(x));
            }
            //Assimilate rows into bigger boxes
            for (int y = 0, combinedCount = 0; y < rowlist.Count - combinedCount; y++)
            {
                if (rowlist.ElementAt(y).First.Value.Width == rowlist.ElementAt(y + 1).First.Value.Width)
                {
                    var thing = rowlist.ElementAt(y).First.Value;
                    thing.Height += 1;
                    rowlist.ElementAt(y).First.Value = thing;
                    y--;
                    combinedCount++;
                }
            }
            var returnlist = new LinkedList<Rectangle>();
            for (int i = 0; i < rowlist.Count; i++)
                returnlist.AddLast(rowlist.ElementAt(i).First.Value);
            return new Hitbox(returnlist);
        }

        /// <summary>
        /// Streamlines this hitbox, making it have the least amount of areas possible and as large areas as possible.
        /// </summary>
        public void StreamlineHitboxes()
        {
            if (!generelized)
                Generalize();
            var pixels = new HashSet<Rectangle>();

            //Dissolve hitboxes into 'pixels' (small boxesf)
            foreach (var item in hitbox.OrderBy(rect => rect.Y))
            {
                for (int y = item.Y; y < y + item.Height; y++)
                {
                    for (int x = item.X; x < x + item.Width; x++)
                        pixels.Add(new Rectangle(x, y, 1, 1));
                }
            }
            var pixelList = pixels.ToList();

            //Assimilate 'pixels' into rows
            for (int y = pixelList.OrderBy(rect => rect.Y).First().Y; y < pixels.Count; y++)
            {
                IEnumerable<Rectangle> rowOfPixels = pixelList.Where(rect => rect.Y == y);
                Rectangle onerow = rowOfPixels.First();
                int index = pixelList.IndexOf(onerow);
                onerow.Width = rowOfPixels.Count();
                pixelList[index] = onerow;
                for (int i = index + 1, thing = index + onerow.Width - 1; i < thing; thing--)
                    pixelList.RemoveAt(i);
            }
            //Assimilate rows into bigger boxes
            for (int y = 0, combinedCount = 0; y < pixels.Count - combinedCount; y++)
            {
                if (pixels.ElementAt(y).Width == pixels.ElementAt(y + 1).Width)
                {
                    var thing = pixelList[y];
                    thing.Height += 1;
                    pixelList[y] = thing;
                    y--;
                    combinedCount++;
                }
            }
        }

        public static implicit operator LinkedList<Rectangle>(Hitbox hb) => hb.hitbox;
        public static explicit operator Hitbox(LinkedList<Rectangle> llr) => new Hitbox(hitboxList: llr);
        public static explicit operator Rectangle(Hitbox hb) => hb.generalArea;
    }
}
