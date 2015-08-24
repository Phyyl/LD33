using Game.Graphics;
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

        public static Texture NPC { get; private set; }
        public static Texture Monster { get; private set; }
        public static Texture Ground { get; private set; }
        public static Texture MonsterFace { get; private set; }
        public static Texture Frame { get; private set; }
        public static Texture Button { get; private set; }
        public static Texture Crate16x16 { get; private set; }
        public static Texture Crate32x32 { get; private set; }
        public static Texture Crate64x64 { get; private set; }
        public static Texture Table { get; private set; }
        public static Texture ComputerDesk { get; private set; }
        public static Texture CompanionCube { get; private set; }
        public static Texture Key { get; private set; }
        public static Texture Door { get; private set; }
        public static Texture TrapDoor { get; private set; }
        public static Texture Chest { get; private set; }
        public static Texture Wall { get; private set; }
        public static Texture WallCorner { get; private set; }
        public static Texture DeadMonster { get; private set; }
        public static Texture DeadNPC       { get; private set; }

        public static void Load()
        {
            NPC = LoadTexture("npc.png");
            Monster = LoadTexture("monster.png");
            Ground = LoadTexture("ground.png");
            Button = LoadTexture("button.png");
            MonsterFace = LoadTexture("monsterface.png");
            Frame = LoadTexture("frame.png");
            Crate16x16 = LoadTexture("crate16x16.png");
            Crate32x32 = LoadTexture("crate32x32.png");
            Crate64x64 = LoadTexture("crate64x64.png");
            Table = LoadTexture("table.png");
            ComputerDesk = LoadTexture("computer_desk.png");
            CompanionCube = LoadTexture("companion_cube.png");
            Key = LoadTexture("key.png");
            Door = LoadTexture("door.png");
            TrapDoor = LoadTexture("trapdoor.png");
            Chest = LoadTexture("chest.png");
            Wall = LoadTexture("wall.png");
            WallCorner = LoadTexture("wallcorner.png");
            DeadMonster  = LoadTexture("deadmonster.png");
            DeadNPC = LoadTexture("deadnpc.png");
        }

        private static Texture LoadTexture(string fileName)
        {
            return new Texture(DIRECTORY + fileName);
        }
    }
}
