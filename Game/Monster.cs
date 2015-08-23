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
        private const int MAX_STAMINA = 3;

        public override Vector2 Size => SpriteSheets.Monster.Size;
        public override int MaxHP => 100;

        private float stamina;
        public float Stamina
        {
            get { return stamina; }
            set
            {
                stamina = MathHelper.Clamp(value, 0, MAX_STAMINA);

                if (stamina == 0)
                {
                    regeneratingStamina = true;
                }
                else if (stamina == MAX_STAMINA)
                {
                    regeneratingStamina = false;
                }
            }
        }
        private bool regeneratingStamina;

        public override void Update(float delta)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            bool left = keyboardState.IsKeyDown(Key.Left);
            bool right = keyboardState.IsKeyDown(Key.Right);
            bool up = keyboardState.IsKeyDown(Key.Up);
            bool down = keyboardState.IsKeyDown(Key.Down);
            bool shift = keyboardState.IsKeyDown(Key.LShift);

            Vector2 movement = new Vector2();

            if (left ^ right)
            {
                movement.X += left ? -1 : 1;
            }

            if (up ^ down)
            {
                movement.Y += up ? -1 : 1;
            }

            bool run = false;

            if (shift && !regeneratingStamina)
            {
                if (run = (Stamina > 0))
                {
                    Stamina -= delta;
                }
            }
            else
            {
                Stamina += delta;
            }

            Move(movement * delta * SPEED * (run ? 2 : 1));
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
                GL.Translate(Size.X, Game.Instance.WindowSize.Y - Size.Y, 0.1f);
                Textures.Frame.Render(origin: Textures.Frame.Size / 2, color: Color.DarkGreen);
                Textures.MonsterFace.Render(origin: Textures.MonsterFace.Size / 2);

                GL.Translate(Textures.Frame.Size.X / 2, -4, 0);
                GL.Begin(PrimitiveType.Quads);
                {
                    GL.Color3(Color.Black);
                    GL.Vertex2(0, 0);
                    GL.Vertex2(100, 0);
                    GL.Vertex2(100, 8);
                    GL.Vertex2(0, 8);

                    GL.Color3(regeneratingStamina ? Color.Orange : Color.Green);
                    GL.Vertex2(0, 1);
                    GL.Vertex2(99 * (stamina / MAX_STAMINA), 1);
                    GL.Vertex2(99 * (stamina / MAX_STAMINA), 7);
                    GL.Vertex2(0, 7);
                }
                GL.End();
            }
            GL.PopMatrix();
        }
    }
}
