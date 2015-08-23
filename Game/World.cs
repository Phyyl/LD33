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
    public class World
    {
        private List<Map> maps;
        public Monster Player;

        public World()
        {
            maps = new List<Map>();
            Player = new Monster();

            Map map = new Map(this, new RectangleF(0, 0, 1000, 1000));
            maps.Add(map);
            map.AddEntity(Player);
            for (int i = 0; i < 10; i++)
            {
                map.AddEntity(new NPC { Position = new Vector2(0, 0) });
            }
        }

        public void Update(float delta)
        {
            Player?.Map?.Update(delta);
        }

        public void Render(float delta)
        {
            GL.Translate(Game.Instance.WindowSize.X / 2 - Player.Position.X,
                         Game.Instance.WindowSize.Y / 2 - Player.Position.Y, 0);
            Player?.Map?.Render(delta);
        }
    }
}
