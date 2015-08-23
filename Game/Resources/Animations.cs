using Game.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Resources
{
    public static class Animations
    {
        public static Animation Passive { get; private set; }
        public static Animation Monster { get; private set; }

        public static void Load()
        {
            Passive = new Animation(SpriteSheets.Monster, 0.5f, 0, 1, 2, 1);
            Monster = new Animation(SpriteSheets.Monster, 0.5f, 0, 1, 2, 1);
        }
    }
}
