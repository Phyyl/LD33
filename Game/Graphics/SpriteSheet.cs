using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class SpriteSheet
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int TilesX { get { return Texture.Width / Width; } }
        public int TilesY { get { return Texture.Height / Height; } }
        public Texture Texture { get; set; }

        public SpriteSheet(Texture texture, int width, int height)
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

        public void Render(int x, int y, Vector2 position = default(Vector2), Vector2 origin = default(Vector2), float radians = default(float), Color? color = null, TextureFlip textureFlip = default(TextureFlip), RectangleF subRegion = default(RectangleF), float scale = 1)
        {
            RectangleF newSubRegion = new RectangleF(x * Width, y * Height, Width, Height);

            if (!subRegion.IsEmpty)
            {
                newSubRegion.X += subRegion.X;
                newSubRegion.Y += subRegion.Y;

                newSubRegion.Width = Math.Min(subRegion.Width, Width);
                newSubRegion.Height = Math.Min(subRegion.Height, Height);
            }

            Texture.Render(position, origin, radians, color, textureFlip, newSubRegion, scale);
        }

        public void Dispose()
        {
            Texture.Dispose();
        }
    }
}
