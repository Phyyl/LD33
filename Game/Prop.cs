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

        public override RectangleF CollisionBox => Angle % 180 == 0 ? base.CollisionBox : new RectangleF(Position.X - Size.Y / 2, Position.Y - Size.X / 2, Size.Y, Size.X);

        private Texture texture;

        public Prop(Texture texture, PropAngle angle = default(PropAngle))
        {
            this.texture = texture;
            Angle = (int)angle;
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
