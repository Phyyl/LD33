using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;

namespace PhyylsGameLibrary.Graphics
{
    public class Texture2D
    {
        private static readonly float[] VERTEX_COODRS = new float[] { 0, 0, 1, 0, 1, 1, 0, 1 };

        private int textureID;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Vector2 Size { get { return new Vector2(Width, Height); } }

        public Texture2D(string fileName) : this(new Bitmap(fileName), new Texture2DLoadingOptions()) { }
        public Texture2D(string fileName, Texture2DLoadingOptions options) : this(new Bitmap(fileName), options) { }
        private Texture2D(Bitmap bitmap, Texture2DLoadingOptions options)
        {
            int textureID;
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out textureID);
            GL.BindTexture(TextureTarget.Texture2D, textureID);
            GL.Ext.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)options.MinFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)options.MagFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)options.WrapS);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)options.WrapT);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            this.textureID = textureID;
            Width = bitmap.Width;
            Height = bitmap.Height;
        }

        public virtual void Render(Texture2DRenderingOptions options)
        {
            GL.PushMatrix();
            {
                GL.BindTexture(TextureTarget.Texture2D, textureID);

                Vector2 renderSize = options.SubRegion.IsEmpty ? Size : new Vector2(options.SubRegion.Width, options.SubRegion.Height);

                GL.Scale(options.Scale, options.Scale, 1);
                GL.Translate(new Vector3(options.Position));
                GL.Rotate(options.AngleDegrees, 0, 0, 1);
                float[] texture_points = VERTEX_COODRS;
                switch (options.TextureFlip)
                {
                    case TextureFlip.Vertical:
                        GL.Translate(0, renderSize.Y / 2, 0);
                        GL.Rotate(180, 1, 0, 0);
                        GL.Translate(0, -renderSize.Y / 2, 0);
                        break;
                    case TextureFlip.Horizontal:
                        GL.Translate(renderSize.X / 2, 0, 0);
                        GL.Rotate(180, 0, 1, 0);
                        GL.Translate(-renderSize.X / 2, 0, 0);
                        break;
                    case TextureFlip.Both:
                        GL.Translate(renderSize.X / 2, renderSize.Y / 2, 0);
                        GL.Rotate(180, 1, 0, 0);
                        GL.Rotate(180, 0, 1, 0);
                        GL.Translate(-renderSize.X / 2, -renderSize.Y / 2, 0);
                        break;
                }
                GL.Translate(new Vector3(-options.Origin));
                GL.Color4(options.Color);

                GL.Enable(EnableCap.Texture2D);
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                GL.EnableClientState(ArrayCap.VertexArray);
                GL.EnableClientState(ArrayCap.TextureCoordArray);
                {
                    GL.VertexPointer(2, VertexPointerType.Float, 0, VERTEX_COODRS);
                    GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, options.CreateTexCoordBuffer(this));
                    GL.Scale(new Vector3(renderSize));
                    GL.DrawArrays(PrimitiveType.Quads, 0, 4);
                }
                GL.DisableClientState(ArrayCap.TextureCoordArray);
                GL.DisableClientState(ArrayCap.VertexArray);
                GL.Disable(EnableCap.Blend);
                GL.Disable(EnableCap.Texture2D);

            }
            GL.PopMatrix();

            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Dispose()
        {
            GL.DeleteTexture(textureID);
        }
    }
}
