using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Graphics
{
    public class Texture
    {
        private static readonly float[] BASE_COORDS = new float[] { 0, 0, 1, 0, 1, 1, 0, 1 };

        private int textureID;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Vector2 Size { get { return new Vector2(Width, Height); } }

        public Texture(string fileName, TextureMinFilter minFilter = TextureMinFilter.Linear, TextureMagFilter magFilter = TextureMagFilter.Nearest, TextureWrapMode wrapS = TextureWrapMode.ClampToBorder, TextureWrapMode wrapT = TextureWrapMode.ClampToBorder)
            : this(new Bitmap(fileName), minFilter, magFilter, wrapS, wrapT)
        {

        }

        public Texture(Bitmap bitmap, TextureMinFilter minFilter = TextureMinFilter.Linear, TextureMagFilter magFilter = TextureMagFilter.Nearest, TextureWrapMode wrapS = TextureWrapMode.ClampToBorder, TextureWrapMode wrapT = TextureWrapMode.ClampToBorder)
        {
            int textureID;
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out textureID);
            GL.BindTexture(TextureTarget.Texture2D, textureID);
            GL.Ext.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)minFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)magFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)wrapS);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)wrapT);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            this.textureID = textureID;
            Width = bitmap.Width;
            Height = bitmap.Height;
        }

        public virtual void Render(Vector2 position = default(Vector2), Vector2 origin = default(Vector2), float angle = default(float), Color? color = null, TextureFlip textureFlip = default(TextureFlip), RectangleF subRegion = default(RectangleF), float scale = 1, float zIndex = 0)
        {
            GL.PushMatrix();
            {
                GL.BindTexture(TextureTarget.Texture2D, textureID);

                Vector2 renderSize = subRegion.IsEmpty ? Size : new Vector2(subRegion.Width, subRegion.Height);

                GL.Scale(scale, scale, 1);
                GL.Translate(new Vector3(position));
                GL.Rotate(angle, 0, 0, 1);
                float[] texture_points = BASE_COORDS;
                switch (textureFlip)
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
                GL.Translate(new Vector3(-origin));
                GL.Color4(color ?? Color.White);

                GL.Enable(EnableCap.Texture2D);
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                GL.EnableClientState(ArrayCap.VertexArray);
                GL.EnableClientState(ArrayCap.TextureCoordArray);
                {
                    float[] textureCoords = BASE_COORDS;

                    if (!subRegion.IsEmpty)
                    {
                        float x1 = subRegion.Left / Width;
                        float x2 = subRegion.Right / Width;
                        float y1 = subRegion.Top / Height;
                        float y2 = subRegion.Bottom / Height;

                        textureCoords = new float[] { x1, y1, x2, y1, x2, y2, x1, y2 };
                    }

                    GL.VertexPointer(2, VertexPointerType.Float, 0, BASE_COORDS);
                    GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, textureCoords);
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
