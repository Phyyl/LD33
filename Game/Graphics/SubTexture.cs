using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class SubTexture
    {
        public Texture Texture { get; set; }
        public RectangleF SubRegion { get; set; }

        public SubTexture(Texture texture, float x, float y, float width, float height)
        {
            Texture = texture;
            SubRegion = new RectangleF(x, y, width, height);
        }

        public void Render(Vector2 position = default(Vector2), Vector2 origin = default(Vector2), float angle = default(float), Color? color = null, TextureFlip textureFlip = default(TextureFlip), RectangleF subRegion = default(RectangleF), float scale = 1, float zIndex = 0)
        {
            RectangleF newSubRegion = SubRegion;

            if (!subRegion.IsEmpty)
            {
                newSubRegion.X += subRegion.X;
                newSubRegion.Y += subRegion.Y;

                newSubRegion.Width = Math.Min(subRegion.Width, SubRegion.Width);
                newSubRegion.Height = Math.Min(subRegion.Height, SubRegion.Height);
            }

            Texture.Render(position, origin, angle, color, textureFlip, newSubRegion, scale);
        }
    }
}
