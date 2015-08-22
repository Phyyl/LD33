using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public abstract class Entity
    {
        private Map map;
        
        public Vector2 Position;
        public Map Map
        {
            get { return map; }
            set
            {
                map?.RemoveEntity(this);
                map = value;
            }
        }

        public abstract Vector2 Size { get; }

        public abstract void Update(float delta);
        public abstract void Render(float delta);
    }
}
