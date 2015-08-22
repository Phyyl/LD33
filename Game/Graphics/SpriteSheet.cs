using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyylsGameLibrary.Graphics
{
    public class SpriteSheet
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int TilesX { get { return Texture.Width / Width; } }
        public int TilesY { get { return Texture.Height / Height; } }
        public Texture2D Texture { get; set; }

        public SpriteSheet(Texture2D texture, int width, int height)
        {
            Width = width;
            Height = height;
            Texture = texture;
        }

        public SubTexture this[int x, int y]
        {
            get
            {
                return new SubTexture(Texture, x * Width, y * Height, Width, Height);
            }
        }

        public void Render(int x, int y, Texture2DRenderingOptions options)
        {
            options.SubRegion = new RectangleF(x * Width, y * Height, Width, Height);
            Texture.Render(options);
        }

        public void Dispose()
        {
            Texture.Dispose();
        }
    }
}
