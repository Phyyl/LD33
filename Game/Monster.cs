using Game.Graphics;
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

        ParticleEmitter bloodEmitter = new ParticleEmitter(new Vector2(), Color.DarkRed, 20, 4);

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
        private Animation animation;

        public Monster(Vector2 position)
            : base(position)
        {
            animation = Animations.Monster.CreateAnimation();
        }

        public override void Update(float delta)
        {
            if (Alive)
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

                bool running = false;

                if (shift && !regeneratingStamina)
                {
                    if (running = (Stamina > 0))
                    {
                        Stamina -= delta;
                    }
                }
                else
                {
                    Stamina += delta * (Moving ? 0.5f : 1);
                }

                Move(movement.NormalizedSafe() * delta * SPEED * (running ? 2 : 1));

                if (Moving)
                {
                    animation.Update(delta);
                    if (running)
                    {
                        animation.Update(delta);
                    }
                }
                else
                {
                    animation.Reset();
                }

                foreach (var entity in Map.Entities)
                {
                    NPC npc = entity as NPC;

                    if (npc != null && npc.Alive && npc.CollisionBox.IntersectsWith(CollisionBox))
                    {
                        npc.HP = 0;
                        bloodEmitter.Position = npc.Position;
                        bloodEmitter.Emit(25);
                    }
                }
            }
        }

        public override void Render(float delta)
        {
            if (Alive)
            {
                if (Moving)
                {
                    animation.Render(Position, Size / 2, Angle);
                }
                else
                {
                    SpriteSheets.Monster.Render(0, 0, Position, Size / 2, Angle);
                }
            }
            else
            {
                Textures.DeadMonster.Render(Position, Textures.DeadMonster.Size / 2, Angle, zIndex: -0.4f);
            }
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

                GL.Translate(Textures.Frame.Size.X / 2, Textures.Frame.Size.Y / 2 - 11, 0);
                GL.Begin(PrimitiveType.Quads);
                {
                    GL.Color3(Color.Black);
                    GL.Vertex2(0, 0);
                    GL.Vertex2(100, 0);
                    GL.Vertex2(100, 9);
                    GL.Vertex2(0, 9);

                    GL.Color3(regeneratingStamina ? Color.Orange : Color.Green);
                    GL.Vertex2(0, 1);
                    GL.Vertex2(99 * (stamina / MAX_STAMINA), 1);
                    GL.Vertex2(99 * (stamina / MAX_STAMINA), 4);
                    GL.Vertex2(0, 4);

                    GL.Color3(Color.Red);
                    GL.Vertex2(0, 5);
                    GL.Vertex2(99 * (HP / MaxHP), 5);
                    GL.Vertex2(99 * (HP / MaxHP), 8);
                    GL.Vertex2(0, 8);
                }
                GL.End();
            }
            GL.PopMatrix();
        }
    }
}
