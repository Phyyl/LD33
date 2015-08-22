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

        public Map()
        {
            entities = new List<Entity>();
        }

        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
            entity.Map = this;
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
        }
    }
}
