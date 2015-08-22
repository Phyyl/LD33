using Game.Resources;
using Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Map
    {
        private List<Entity> entities;

        private World world;

        public Map(World world)
        {
            entities = new List<Entity>();
            this.world = world;
        }

        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
            entity.Map = this;
        }

        public void RemoveEntity(Entity entity)
        {
            entities.Remove(entity);
        }

        public void Update(float delta)
        {
            foreach (var entity in entities)
            {
                entity.Update(delta);
            }
        }

        public void Render(float delta)
        {
            foreach (var entity in entities)
            {
                entity.Render(delta);
            }
            
            Vector2 groundTextureSize = Textures.Ground.Size;

            for (int x = -10; x <= 10; x++)
            {
                for (int y = -10; y <= 10; y++)
                {
                    Textures.Ground.Render(new Vector2(x * groundTextureSize.X, y * groundTextureSize.Y), zIndex: 0.5f);
                }
            }
        }
    }
}
