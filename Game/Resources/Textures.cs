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

        public static Texture Passive1 { get; private set; }
        public static Texture Monster1 { get; private set; }
        public static Texture Ground { get; private set; }
        public static Texture Button { get; private set; }

        public static void Load()
        {
            Passive1 = LoadTexture("passive1.png");
            Monster1 = LoadTexture("monster1.png");
            Ground =   LoadTexture("ground.png");
            Button =   LoadTexture("button.png");
        }

        private static Texture LoadTexture(string fileName)
        {
            return new Texture(DIRECTORY + fileName);
        }
    }
}
