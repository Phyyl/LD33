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
        public static SpriteSheet Passive { get; private set; }
        public static SpriteSheet Monster { get; private set; }

        public static void Load()
        {
            Passive = new SpriteSheet(Textures.Passive, 16, 16);
            Monster = new SpriteSheet(Textures.Monster, 16, 16);
        }
    }
}
