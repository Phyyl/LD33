using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public abstract class LivingEntity : Entity
    {
        public abstract bool Alive { get; protected set; }
        public abstract float Angle { get; protected set; }
    }
}
