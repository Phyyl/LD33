using Game.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Particles
{
    public class Particle
    {
        private float decayTime;
        private float life;
        private Vector2 position;
        private Vector2 movement;
        private Color color;

        public bool Alive => life > 0;

        public Particle(float decayTime, Vector2 position, Vector2 movement, Color color)
        {
            this.decayTime = decayTime;
            this.position = position;
            this.movement = movement;
            this.color = color;

            life = decayTime;
        }

        public void Update(float delta)
        {
            if (Alive)
            {
                position += movement * delta;
                life -= delta;
            }
        }

        public void Render(float delta, float zIndex = 0)
        {
            if (Alive)
            {
                float percent = life / decayTime;
                GL.Color4(color.R, color.G, color.B, (byte)(percent * 255));
                GL.Vertex3(position.X, position.Y, zIndex);
            }
        }
    }
}
