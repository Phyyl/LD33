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
    public class ParticleEmitter
    {
        private static readonly Random random = new Random();

        public Vector2 Position;

        private List<Particle> particles;
        private Texture texture;
        private Color color;
        private float radius;
        private float decayTime;
        private int count;
        private float amount;

        public ParticleEmitter(Texture texture, Vector2 position, Color color, float radius, float decayTime, float amount)
        {
            Position = position;

            this.color = color;
            this.texture = texture;
            this.radius = radius;
            this.decayTime = decayTime;
            this.amount = amount;

            particles = new List<Particle>();
        }

        public void Update(float delta)
        {
            for (int i = 0, m = (int)(amount * delta); i < m; i++)
            {
                particles.Add(new Particle(texture, decayTime, Position, new Vector2((float)random.NextDouble() - 0.5f, (float)random.NextDouble() - 0.5f).Normalized() * radius / decayTime, color));
            }

            for (int i = particles.Count - 1; i >= 0; i--)
            {
                Particle particle = particles[i];

                if (!particle.Alive)
                {
                    particles.Remove(particle);
                }
                else
                {
                    particle.Update(delta);
                }
            }
        }

        public void Render(float delta)
        {
            foreach (var particle in particles)
            {
                particle.Render(delta);
            }
        }
    }
}
