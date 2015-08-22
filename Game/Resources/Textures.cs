using Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Resources
{
    public static class Textures
    {
        private const string DIRECTORY = "Resources/Textures/";

        public static Texture Passive { get; private set; }
        public static Texture Monster { get; private set; }
        public static Texture Ground { get; private set; }
        public static Texture MonsterFace { get; private set; }
        public static Texture Frame { get; private set; }
        public static Texture Button { get; private set; }
        public static Texture Crate16x16 { get; private set; }
        public static Texture Crate32x32 { get; private set; }
        public static Texture Crate64x64 { get; private set; }
		public static Texture Table { get; private set; }

        public static void Load()
        {
            Passive = LoadTexture("passive.png");
            Monster = LoadTexture("monster.png");
            Ground = LoadTexture("ground.png");
            Button = LoadTexture("button.png");
            MonsterFace = LoadTexture("monsterface.png");
            Frame = LoadTexture("frame.png");
            Crate16x16 = LoadTexture("crate16x16.png");
            Crate32x32 = LoadTexture("crate32x32.png");
            Crate64x64 = LoadTexture("crate64x64.png");
			Table = LoadTexture("table.jpg");
        }

        private static Texture LoadTexture(string fileName)
        {
            return new Texture(DIRECTORY + fileName);
        }
    }
}
