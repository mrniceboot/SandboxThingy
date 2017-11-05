using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Input = Microsoft.Xna.Framework.Input;

namespace GameLib.UI_Elements
{
    class Button
    {
        Action onPressed;
        Rectangle rect;

        public void Update()
        {
            var mouseState = Input.Mouse.GetState();
            if (mouseState.LeftButton == Input.ButtonState.Pressed && rect.Contains(mouseState.Position)) {
                onPressed();
            }
        }

        public Button(Rectangle rect, Action onPressed)
        {
            this.rect = rect;
            this.onPressed = onPressed;
        }

    }
}
