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
        public Vector2 Position;
        public Map Map { get; set; }

        public abstract void Update(float delta);
        public abstract void Render(float delta);
    }
}
