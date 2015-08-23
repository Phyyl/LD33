using Game.Resources;
using Game.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public partial class Game
    {
        public static float SCALE = 3;

        private World world;

        public void Load()
        {
            Textures.Load();
            SpriteSheets.Load();
            Animations.Load();

            world = new World();
        }

        public void Resize(int width, int height)
        {

        }

        public void Update(float delta)
        {
            world.Update(delta);
        }

        public void Render(float delta)
        {
            GL.PushMatrix();
            {
                GL.Scale(SCALE, SCALE, 1);
                world.Render(delta);
            }
            GL.PopMatrix();
        }
    }
}
