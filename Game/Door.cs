using System;
using OpenTK;
using Game.Graphics;
using Game.Resources;
using System.Drawing;

namespace Game
{
    public class Door : Prop
    {
        private const float DOOR_SIZE_DEFLATION = 6;

        public Door Destination { get; set; }

        private float enterTimeout;

        private bool canEnter => enterTimeout == 0;

        public Door(Vector2 position, Direction propAngle)
            : base(position, Textures.Door, propAngle)
        {

        }

        public override void Update(float delta)
        {
            base.Update(delta);

            if (!canEnter)
            {
                enterTimeout -= delta;

                if (enterTimeout < 0)
                {
                    enterTimeout = 0;
                }
            }
        }

        public override void OnCollision(Entity entity, Direction direction)
        {
            if (((int)direction + (int)PropAngle) % 180 == 0 && Destination != null && canEnter)
            {
                Direction exitDirection = (Direction)(MathUtil.Mod((int)direction + ((int)Destination.PropAngle - (int)PropAngle), 360));
                Destination.Exit(entity, exitDirection);
            }
        }

        private void Exit(Entity entity, Direction direction)
        {
            enterTimeout = 1;

            if (entity.Map != Map)
            {
                Map.AddEntity(entity);
            }

            entity.Position = Position + direction.GetDirectionVector() * (Size / 2 + entity.Size / 2);
        }
    }
}
