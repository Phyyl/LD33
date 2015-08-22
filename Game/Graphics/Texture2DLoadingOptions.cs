using OpenTK.Graphics.OpenGL;

namespace PhyylsGameLibrary.Graphics
{
    /// <summary>
    /// Represents the Texture2D loading options
    /// </summary>
    public class Texture2DLoadingOptions
    {
        public TextureMinFilter MinFilter { get; set; }
        public TextureMagFilter MagFilter { get; set; }

        public TextureWrapMode WrapS { get; set; }
        public TextureWrapMode WrapT { get; set; }

        public Texture2DLoadingOptions()
        {
            MinFilter = TextureMinFilter.Linear;
            MagFilter = TextureMagFilter.Nearest;
            WrapS = TextureWrapMode.ClampToBorder;
            WrapT = TextureWrapMode.ClampToBorder;
        }
    }
}
