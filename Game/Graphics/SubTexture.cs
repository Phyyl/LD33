using PhyylsGameLibrary.Collisions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyylsGameLibrary.Graphics
{
    public class SubTexture
    {
        public Texture2D Texture { get; set; }
        public Rect SubRegion { get; set; }

        public SubTexture(Texture2D texture, float x, float y, float width, float height)
        {
            Texture = texture;
            SubRegion = new Rect(x, y, width, height);
        }

        public void Render(Texture2DRenderingOptions options)
        {
            options.SubRegion = SubRegion;
            Texture.Render(options);
        }
    }
}
