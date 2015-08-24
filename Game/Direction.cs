using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public enum Direction : int
    {
        Right = 0,
        Down = 90,
        Left = 180,
        Up = 270
    }

    public static class DirectionExtensions
    {
        public static Vector2 GetDirectionVector(this Direction direction)
        {
            float angle = MathHelper.DegreesToRadians((int)direction);
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
    }
}
