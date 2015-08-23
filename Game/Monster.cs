using Game.Particles;
using Game.Resources;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Monster : LivingEntity
    {
        private const float SPEED = 50;
        
        public override Vector2 Size => SpriteSheets.Monster.Size;
        public override int MaxHP => 100;

        public override void Update(float delta)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            bool left = keyboardState.IsKeyDown(Key.Left);
            bool right = keyboardState.IsKeyDown(Key.Right);
            bool up = keyboardState.IsKeyDown(Key.Up);
            bool down = keyboardState.IsKeyDown(Key.Down);

            Vector2 movement = new Vector2();

            if (left ^ right)
            {
                movement.X += left ? -1 : 1;
            }

            if (up ^ down)
            {
                movement.Y += up ? -1 : 1;
            }

            Move(movement * delta * SPEED);
        }

        public override void Render(float delta)
        {
            SpriteSheets.Monster.Render(0, 0, Position, Size / 2, Angle);
            RenderGUI();
        }

        private void RenderGUI()
        {
            GL.PushMatrix();
            {
                GL.LoadIdentity();
                GL.Scale(Game.SCALE, Game.SCALE, 1);
                GL.Translate(Size.X, Game.Instance.WindowSize.Y - Size.Y, -0.1f);
                Textures.Frame.Render(origin: Textures.Frame.Size / 2, color: Color.DarkGreen);
                Textures.MonsterFace.Render(origin: Textures.MonsterFace.Size / 2);
            }
            GL.PopMatrix();
        }
    }
}
