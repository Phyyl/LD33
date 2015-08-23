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

namespace Game
{
    public class NPC : LivingEntity
    {
        private const float SPEED = 20;

        private static readonly Random random = new Random();

        public float FOV { get; set; }

        public override Vector2 Size => SpriteSheets.Passive.Size;
        public override int MaxHP => 20;

        private float walkTime = 0;
        private float walkAngle = 0;

        public NPC(float fov = 60)
        {
            FOV = fov;
        }

        public override void Update(float delta)
        {
            if (walkTime < 0)
            {
                walkTime = (float)(random.NextDouble()) * 3;
                walkAngle = (float)(random.NextDouble() * 360);
            }

            walkTime -= delta;

            Move(walkAngle.DegreesToNormalVector() * SPEED * delta);
        }

        public override void Render(float delta)
        {
            int r = 0;
            int g = 0;
            int b = 0;

            if (MathUtil.PointFrustrumCollision(Position, Map.World.Player.Position, Angle, FOV))
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

            SpriteSheets.Passive.Render(0, 0, Position, SpriteSheets.Passive.Size / 2, Angle);
        }
    }
}
