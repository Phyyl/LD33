using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyylsGameLibrary.Graphics
{
    public class Texture2DRenderingOptions : RenderingOptions
    {
        private static readonly float[] TEXTURE_COORDS = new float[] { 0, 0, 1, 0, 1, 1, 0, 1 };
        public TextureFlip TextureFlip { get; set; }
        public RectangleF SubRegion { get; set; }

        internal float[] CreateTexCoordBuffer(Texture2D texture)
        {
            if (SubRegion.IsEmpty)
            {
                return TEXTURE_COORDS;
            }
            else
            {
                float x1 = SubRegion.Left /  (float)texture.Width;
                float x2 = SubRegion.Right / (float)texture.Width;
                float y1 = SubRegion.Top / (float)texture.Height;
                float y2 = SubRegion.Bottom / (float)texture.Height;

                return new float[] { x1, y1, x2, y1, x2, y2, x1, y2 };
            }
        }
    }
}
