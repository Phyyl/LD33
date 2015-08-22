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
        public Vector2 Size { get; private set; }
        public int TilesX { get { return (int)(Texture.Width / Size.X); } }
        public int TilesY { get { return (int)(Texture.Height / Size.Y); } }
        public Texture Texture { get; set; }

        public SpriteSheet(Texture texture, int width, int height)
        {
            Size = new Vector2(width, height);
            Texture = texture;
        }

        public SubTexture this[int x, int y]
        {
            get
            {
                return new SubTexture(Texture, x * Size.X, y * Size.Y, Size.X, Size.Y);
            }
        }

        public void Render(int x, int y, Vector2 position = default(Vector2), Vector2 origin = default(Vector2), float angle = default(float), Color? color = null, TextureFlip textureFlip = default(TextureFlip), RectangleF subRegion = default(RectangleF), float scale = 1, float zIndex = 0)
        {
            RectangleF newSubRegion = new RectangleF(x * Size.X, y * Size.Y, Size.X, Size.Y);

            if (!subRegion.IsEmpty)
            {
                newSubRegion.X += subRegion.X;
                newSubRegion.Y += subRegion.Y;

                newSubRegion.Width = Math.Min(subRegion.Width, Size.X);
                newSubRegion.Height = Math.Min(subRegion.Height, Size.Y);
            }

            Texture.Render(position, origin, angle, color, textureFlip, newSubRegion, scale, zIndex);
        }

        public void Dispose()
        {
            Texture.Dispose();
        }
    }
}
