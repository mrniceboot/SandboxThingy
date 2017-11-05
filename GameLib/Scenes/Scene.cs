using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Scenes
{

    abstract class Scene
    {
        protected ContentManager contentManager;
        
        public abstract void Update();
        public abstract void Draw();

        protected void Initialize()
        {

        }
        protected abstract void Unload();

        /*
        public void SwitchTo(Scene To)
        {
            Unload();
            To.Initialize(contentManager.ServiceProvider);
        }
        */
    }
}
