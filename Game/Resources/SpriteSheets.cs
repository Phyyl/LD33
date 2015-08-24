using Game.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Resources
{
    public static class SpriteSheets
    {
        public static SpriteSheet NPC { get; private set; }
        public static SpriteSheet Monster { get; private set; }

        public static void Load()
        {
            NPC = new SpriteSheet(Textures.NPC, 16, 14);
            Monster = new SpriteSheet(Textures.Monster, 16, 14);
        }
    }
}
