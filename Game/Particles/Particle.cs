using Game.Graphics;
using OpenTK;
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
        private Texture texture;
        private float decayTime;
        private float life;
        private Vector2 position;
        private Vector2 movement;
        private Color color;

        public bool Alive => life > 0;

        public Particle(Texture texture, float decayTime, Vector2 position, Vector2 movement, Color color)
        {
            this.texture = texture;
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

        public void Render(float delta)
        {
            if (Alive)
            {
                float percent = life / decayTime;
                texture.Render(position, texture.Size / 2, color: Color.FromArgb((int)(percent * 255), color.R, color.G, color.B));
            }
        }
    }
}
