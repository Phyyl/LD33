using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Graphics
{
    public class AnimationDescriptor
    {
        private SpriteSheet spriteSheet;
        private int[] frames;
        private float interval;

        public AnimationDescriptor(SpriteSheet spriteSheet, float interval, params int[] frames)
        {
            this.spriteSheet = spriteSheet;
            this.frames = frames ?? GenerateFrames(spriteSheet.TilesX * spriteSheet.TilesY);
            this.interval = interval;
        }

        public Animation CreateAnimation()
        {
            return new Animation(spriteSheet, interval, frames);
        }

        private static int[] GenerateFrames(int length)
        {
            int[] result = new int[length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = i;
            }

            return result;
        }
    }
}
