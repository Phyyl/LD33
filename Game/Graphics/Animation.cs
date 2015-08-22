using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyylsGameLibrary.Graphics
{
    public struct Animation
    {
        private SpriteSheet spriteSheet;
        private int[] frames;
        private float interval;
        private int currentFrame;
        private float currentTime;

        public Animation(SpriteSheet spriteSheet, float interval, params int[] frames)
        {
            this.spriteSheet = spriteSheet;
            this.frames = frames;
            this.interval = interval;
            currentFrame = 0;
            currentTime = 0;
        }

        public void Reset()
        {
            currentFrame = 0;
            currentTime = 0;
        }

        public void Update(float delta)
        {
            currentTime += delta;
            while (currentTime >= interval)
            {
                currentTime -= interval;
                currentFrame++;
                currentFrame %= frames.Length;
            }
        }

        public void Render(Texture2DRenderingOptions options)
        {
            int tileY = frames[currentFrame] / spriteSheet.TilesX;
            int tileX = frames[currentFrame] - tileY * spriteSheet.TilesX;
            spriteSheet.Render(tileX, tileY, options);
        }

        public void Dispose()
        {
            spriteSheet.Dispose();
        }
    }
}
