using Game.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Game
{
    public class NPC : LivingEntity
    {
        private const float SPEED = 20;

        private static readonly Random random = new Random();

        public float FOV { get; private set; }
        
        public override Vector2 Size => SpriteSheets.Passive.Size;

        public override int MaxHP => 20;

        private float walkTime = 0;
        private float walkAngle = 0;

        public override void Update(float delta)
        {
            if (walkTime < 0)
            {
                walkTime = (float)(random.NextDouble()) * 3;
                walkAngle = (float)(random.NextDouble() * 360);
            }

            walkTime -= delta;

            Move(new Vector2((float)MathHelper.DegreesToRadians(Math.Cos(walkAngle)), (float)MathHelper.DegreesToRadians(Math.Sin(walkAngle))) * SPEED);
        }

        public override void Render(float delta)
        {
            SpriteSheets.Passive.Render(0, 0, Position, SpriteSheets.Passive.Size / 2, Angle);
        }
    }
}
