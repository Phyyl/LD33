using Game.Resources;
using OpenTK;
using Game.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Map
    {
        public List<Entity> Entities { get; private set; }


        public World World { get; private set; }

        public Map(World world, RectangleF bounds)
        {
            Entities = new List<Entity>();
            World = world;
        }

        public void AddEntity(Entity entity)
        {
            Entities.Add(entity);
            entity.Map = this;
        }

        public void RemoveEntity(Entity entity)
        {
            Entities.Remove(entity);
        }

        public void Update(float delta)
        {
            for (int i = 0; i < Entities.Count; i++)
            {
                Entities[i].Update(delta);
            }
        }

        public void Render(float delta)
        {
            Vector2 groundTextureSize = Textures.Ground.Size;

            for (int x = -10; x <= 10; x++)
            {
                for (int y = -10; y <= 10; y++)
                {
                    Textures.Ground.Render(new Vector2(x * groundTextureSize.X, y * groundTextureSize.Y), zIndex: -0.5f);
                }
            }

            for (int i = 0; i < Entities.Count; i++)
            {
                Entities[i].Render(delta);
            }
        }
    }
}
