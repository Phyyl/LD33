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
        private Monster monster;

        public World()
        {
            maps = new List<Map>();
            monster = new Monster();

            Map map = new Map();
            map.AddEntity(monster);
            map.AddEntity(new NPC { Position = new Vector2(0, 0) });
        }

        public void Update(float delta)
        {
            monster?.Map?.Update(delta);
        }

        public void Render(float delta)
        {
            GL.Translate(Game.Instance.WindowSize.X / 2 - monster.Position.X - SpriteSheets.Monster1.Size.X / 2,
                         Game.Instance.WindowSize.Y / 2 - monster.Position.Y - SpriteSheets.Monster1.Size.Y / 2, 0);
            monster?.Map?.Render(delta);
        }
    }
}
