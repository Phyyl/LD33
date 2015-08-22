using Game.Resources;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
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

            Map map = new Map(this);
            map.AddEntity(Player);
            map.AddEntity(new NPC { Position = new Vector2(0, 0) });
        }

        public void Update(float delta)
        {
            Player?.Map?.Update(delta);
        }

        public void Render(float delta)
        {
            GL.Translate(Game.Instance.WindowSize.X / 2 - Player.Position.X - SpriteSheets.Monster.Size.X / 2,
                         Game.Instance.WindowSize.Y / 2 - Player.Position.Y - SpriteSheets.Monster.Size.Y / 2, 0);
            Player?.Map?.Render(delta);
        }
    }
}
