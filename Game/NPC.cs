using Game.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK.Input;
using Game.Graphics;

namespace Game
{
    public class NPC : LivingEntity
    {
        private const float SPEED = 20;

        private static readonly Random random = new Random();

        public float FOV { get; private set; }
        public bool Alerted { get; private set; }

        public override Vector2 Size => SpriteSheets.NPC.Size;
        public override int MaxHP => 20;

        private float walkTime = 0;
        private float walkAngle = 0;

        private Animation walkAnimation;
        private Animation alertedAnimation;

        public NPC(Vector2 position, float fov = 60)
            : base(position)
        {
            FOV = fov;
            walkAnimation = Animations.NPC.CreateAnimation();
            alertedAnimation = Animations.NPCAlerted.CreateAnimation();
        }

        public override void Update(float delta)
        {
            if (Alive)
            {
                Alerted = SeesMonster();

                if (Alerted && Map.World.Monster.Alive)
                {
                    Vector2 diff = Map.World.Monster.Position - Position;

                    Move(diff.NormalizedSafe() * SPEED * delta);
                }
                else
                {
                    if (walkTime < 0)
                    {
                        walkTime = (float)(random.NextDouble()) * 3;
                        walkAngle = (float)(random.NextDouble() * 360);
                    }

                    walkTime -= delta;

                    Move(walkAngle.DegreesToNormalVector() * SPEED * delta);
                }

                if (Moving)
                {
                    walkAnimation.Update(delta);
                    alertedAnimation.Update(delta);
                }
                else
                {
                    walkAnimation.Reset();
                    alertedAnimation.Reset();
                }
            }
        }

        public override void Render(float delta)
        {
            if (Alive)
            {
                if (Moving)
                {
                    (Alerted ? alertedAnimation : walkAnimation).Render(Position, SpriteSheets.NPC.Size / 2, Angle);
                }
                else
                {
                    SpriteSheets.NPC.Render(0, Alerted ? 1 : 0, Position, SpriteSheets.NPC.Size / 2, Angle);
                }
            }
            else
            {
                Textures.DeadNPC.Render(Position, Textures.DeadNPC.Size / 2, Angle, zIndex: -0.4f);
            }
        }

        public void RenderFOV()
        {
            int r = 0;
            int g = 0;
            int b = 0;

            if (Alerted)
            {
                r = 255;
            }
            else
            {
                b = 255;
            }

            GL.Begin(PrimitiveType.Triangles);
            {
                GL.Color4(Color.FromArgb(127, r, g, b));
                GL.Vertex2(Position);
                GL.Color4(Color.FromArgb(0, r, g, b));
                GL.Vertex2(Position + (Angle - FOV / 2).DegreesToNormalVector() * 200);
                GL.Vertex2(Position + (Angle + FOV / 2).DegreesToNormalVector() * 200);
            }
            GL.End();
        }

        private bool SeesMonster()
        {
            if (Map.World.Monster.CollisionBox.IntersectsWith(CollisionBox))
            {
                return true;
            }
            else if (MathUtil.PointFrustrumCollision(Position, Map.World.Monster.Position, Angle, FOV))
            {
                /*
                foreach (var entity in Map.Entities)
                {
                    Prop prop = entity as Prop;
                    if (prop != null && !prop.CanSeeThrough)
                    {
                        if (prop.CollisionBox.IntersectsRay(Position, Map.World.Monster.Position))
                        {
                            return false;
                        }
                    }
                }
                */
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
