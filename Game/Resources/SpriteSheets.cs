using Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Resources
{
    public static class SpriteSheets
    {
        public static SpriteSheet Passive1 { get; private set; }
        public static SpriteSheet Monster1 { get; private set; }

        public static void Load()
        {
            Passive1 = new SpriteSheet(Textures.Passive1, 16, 16);
            Monster1 = new SpriteSheet(Textures.Monster1, 16, 16);
        }
    }
}
