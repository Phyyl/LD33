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
        public static AnimationDescriptor Monster { get; private set; }
        public static AnimationDescriptor NPC { get; private set; }
        public static AnimationDescriptor NPCAlerted { get; private set; }

        public static void Load()
        {
            Monster = new AnimationDescriptor(SpriteSheets.Monster, 0.5f, 1, 2);
            NPC = new AnimationDescriptor(SpriteSheets.NPC, 0.5f, 1, 2);
            NPCAlerted = new AnimationDescriptor(SpriteSheets.NPC, 0.5f, 4, 5);
        }
    }
}
