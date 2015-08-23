using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public abstract class LivingEntity : Entity
    {
        private int hp;

        public override bool Solid => false;

        public abstract int MaxHP { get; }

        public int HP
        {
            get { return hp; }
            set
            {
                hp = MathHelper.Clamp(value, 0, MaxHP);
            }
        }

        public bool Alive => HP > 0;
    }
}
