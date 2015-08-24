using Game.Particles;
using Game.Resources;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public partial class World
    {
        public List<Map> Maps { get; private set; }
        public Monster Monster { get; private set; }
        
        public World()
        {
            Maps = new List<Map>();

            CreateMaps();
            
            Monster = new Monster(new Vector2(0, 0));

            Maps[0].AddEntity(Monster);
        }

        public void Update(float delta)
        {
            Monster?.Map?.Update(delta);
        }

        public void Render(float delta)
        {
            GL.Translate(Game.Instance.WindowSize.X / 2 - Monster.Position.X, Game.Instance.WindowSize.Y / 2 - Monster.Position.Y, 0);
            Monster?.Map?.Render(delta);
        }
    }
}
