using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.UI_Elements
{
    class Text
    {
        string text;
        Rectangle rect;
        Color color;
        SpriteFont font;
        
        public void Draw(SpriteBatch sb) => sb.DrawString(font, text, rect.Location.ToVector2(), Color.White);

        public Text(string text, SpriteFont font, Rectangle? rect = null) : this(text, font, Color.Black, rect) { }
        public Text(string text, SpriteFont font, Color color, Rectangle ? rect = null)
        {
            this.text = text;
            this.font = font;
            this.color = color;

            if (rect == null)
                return;

            this.rect = rect.Value;
            var stringSize = font.MeasureString(text);

            if (this.rect.Height < stringSize.Y) {
                //implementLater
                throw new Exception("String to tall");
            }

            if (this.rect.Width < stringSize.X) {
                //implementLater
                throw new Exception("String to tall");
            }
        }

    }
}
