using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class MathUtil
    {
        public static Vector2 DegreesToNormalVector(this float f)
        {
            return MathHelper.DegreesToRadians(f).RadiansToNormalVector();
        }

        public static Vector2 RadiansToNormalVector(this float f)
        {
            return new Vector2((float)Math.Cos(f), (float)Math.Sin(f)).Normalized();
        }

        public static bool PointFrustrumCollision(Vector2 position, Vector2 point, float angle, float fov)
        {
            Vector2 diff = (point - position).Normalized();
            float rel = Mod((float)MathHelper.RadiansToDegrees(Math.Atan2(diff.Y, diff.X)), 360);
            float dist = Math.Abs(angle - rel);
            if (dist > 180)
            {
                dist = Mod(-dist, 360);
            }
            return dist < fov / 2;
        }

        public static float Mod(float a, float b)
        {
            return ((a % b) + b) % b;
        }

        public static int Mod(int a, int b)
        {
            return ((a % b) + b) % b;
        }

        public static Vector2 NormalizedSafe(this Vector2 vector)
        {
            if (vector.Length == 0)
            {
                return vector;
            }
            else
            {
                return vector.Normalized();
            }
        }

        public static bool IntersectsRay(this RectangleF rectangle, Vector2 start, Vector2 end)
        {
            Vector2 diff = end - start;

            if (diff.Length == 0)
            {
                return false;
            }

            if (diff.X == 0)
            {
                return start.X > rectangle.Left && start.X < rectangle.Right;
            }
            else if (diff.Y == 0)
            {
                return start.Y > rectangle.Top && start.Y < rectangle.Bottom;
            }
            else
            {
                float yLeft;
                float yRight;
                float xTop;
                float xBottom;

                float xa = diff.Y / diff.X;
                float ya = diff.X / diff.Y;
                float xb = start.Y - (diff.Y * start.X);
                float yb = start.X - (diff.X * start.Y);

                yLeft = -xb / (xa * rectangle.Left);
                yRight = -xb / (xa * rectangle.Left);
                xTop = -yb / (ya * rectangle.Top);
                xBottom = -yb / (ya * rectangle.Bottom);

                return (yLeft > rectangle.Top && yLeft < rectangle.Bottom) ||
                    (yRight > rectangle.Top && yRight < rectangle.Bottom) ||
                    (xTop > rectangle.Left && xTop < rectangle.Right) ||
                    (xBottom > rectangle.Left && xBottom < rectangle.Right);
            }
        }
    }
}
