using PhyylsGameLibrary.Graphics;
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

        public static Texture2D Passive1 { get; private set; }
        public static Texture2D Monster1 { get; private set; }

        public static void Load()
        {
            Passive1 = LoadTexture("passive1.png");
            Monster1 = LoadTexture("monster1.png");
        }

        private static Texture2D LoadTexture(string fileName)
        {
            return new Texture2D(DIRECTORY + fileName);
        }
    }
}
