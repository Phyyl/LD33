using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Game.Graphics;
using System.Drawing;

namespace Game
{
    public class Prop : Entity
    {
        public override Vector2 Size => texture.Size;
        public override bool Solid => true;

        public override RectangleF CollisionBox => Horizontal ? base.CollisionBox : new RectangleF(Position.X - Size.Y / 2, Position.Y - Size.X / 2, Size.Y, Size.X);

        private Texture texture;

        protected bool Horizontal => (int)PropAngle % 180 == 0;

        private Direction propAngle;
        public Direction PropAngle
        {
            get { return propAngle; }
            set
            {
                propAngle = value;
                Angle = (int)value;
            }
        }

        public bool CanSeeThrough { get; set; }

        public Prop(Vector2 position, Texture texture, Direction angle, bool canSeeThrough = false)
            : base(position)
        {
            this.texture = texture;

            CanSeeThrough = canSeeThrough;
            PropAngle = angle;

        }

        public override void Update(float delta)
        {

        }

        public override void Render(float delta)
        {
            texture.Render(Position, Size / 2, Angle);
        }
    }
}
