using OpenTK;
using System;
using System.Collections.Generic;
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
    }
}
