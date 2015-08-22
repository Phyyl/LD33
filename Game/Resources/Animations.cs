using PhyylsGameLibrary.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Resources
{
    public static class Animations
    {
        public static Animation Passive1 { get; private set; }
        public static Animation Monster1 { get; private set; }

        public static void LoadSpriteSheets()
        {
            Passive1 = new Animation(SpriteSheets.Monster1, 0.5f, 0, 1, 2, 1);
            Monster1 = new Animation(SpriteSheets.Monster1, 0.5f, 0, 1, 2, 1);
        }
    }
}
