using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public partial class Game
    {
        public static Game Instance;

        private GameWindow Window;

        public Vector2 WindowSize => new Vector2(Window.ClientSize.Width / SCALE, Window.ClientSize.Height / SCALE);

        public void Run()
        {
            Instance = this;

            Window = new GameWindow();

            Window.Load += Window_Load;
            Window.Resize += Window_Resize;
            Window.UpdateFrame += Window_UpdateFrame;
            Window.RenderFrame += Window_RenderFrame;

            Window.Run(60);
        }

        private void Window_Load(object sender, EventArgs e)
        {
            Load();
        }

        private void Window_Resize(object sender, EventArgs e)
        {
            Matrix4 projectionMatrix = Matrix4.CreateOrthographicOffCenter(0, Window.ClientSize.Width, Window.ClientSize.Height, 0, 1, -1);

            GL.Viewport(0, 0, Window.ClientSize.Width, Window.ClientSize.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.LoadMatrix(ref projectionMatrix);
            GL.MatrixMode(MatrixMode.Modelview);

            Resize(Window.ClientSize.Width, Window.ClientSize.Height);
        }

        private void Window_UpdateFrame(object sender, FrameEventArgs e)
        {
            Update((float)e.Time);
        }

        private void Window_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            Render((float)e.Time);
            Window.SwapBuffers();
        }

        static void Main(string[] args)
        {
            new Game().Run();
        }
    }
}
