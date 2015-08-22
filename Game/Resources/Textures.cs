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
        public static Texture Button { get; private set; }
        public static Texture MonsterFace { get; private set; }
        public static Texture Frame { get; private set; }

        public static void Load()
        {
            Passive = LoadTexture("passive.png");
            Monster = LoadTexture("monster.png");
            Ground = LoadTexture("ground.png");
            Button = LoadTexture("button.png");
            MonsterFace = LoadTexture("monsterface.png");
            Frame = LoadTexture("frame.png");
        }

        private static Texture LoadTexture(string fileName)
        {
            return new Texture(DIRECTORY + fileName);
        }
    }
}
