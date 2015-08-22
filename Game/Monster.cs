using Game.Resources;
using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Monster : LivingEntity
    {
        private const float SPEED = 50;

        public override bool Alive { get; protected set; }
        public override float Angle { get; protected set; }

        private KeyboardState previousKeyboardState = Keyboard.GetState();

        public override void Update(float delta)
        {
            KeyboardState keyboardState = Keyboard.GetState();


            bool left = keyboardState.IsKeyDown(Key.Left);
            bool right = keyboardState.IsKeyDown(Key.Right);
            bool up = keyboardState.IsKeyDown(Key.Up);
            bool down = keyboardState.IsKeyDown(Key.Down);
            
            Vector2 movement = new Vector2();

            if (left ^ right)
            {
                movement.X += left ? -1 : 1;
            }

            if (up ^ down)
            {
                movement.Y += up ? -1 : 1;
            }

            if (movement.Length != 0)
            {
                Angle = (float)(Math.Atan2(movement.Y, movement.X) / Math.PI * 180) + 90;

                Position += movement.Normalized() * SPEED * delta;
            }

            previousKeyboardState = keyboardState;
        }

        public override void Render(float delta)
        {
            SpriteSheets.Monster1.Render(0, 0, Position, SpriteSheets.Monster1.Size / 2, Angle);
        }
    }
}
