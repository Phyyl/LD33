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
    public class ParticleEmitter
    {
        private static readonly Random random = new Random();

        public Vector2 Position;

        private List<Particle> particles;
        private Color color;
        private float radius;
        private float decayTime;
        private float amount;
        private float flowTime;

        public ParticleEmitter(Vector2 position, Color color, float radius, float decayTime, float amount = 200)
        {
            Position = position;

            this.color = color;
            this.radius = radius;
            this.decayTime = decayTime;
            this.amount = amount;

            particles = new List<Particle>();
        }

        public void Update(float delta)
        {
            if (flowTime > 0)
            {
                for (int i = 0, m = (int)(amount * delta); i < m; i++)
                {
                    particles.Add(new Particle(decayTime, Position, new Vector2((float)random.NextDouble() - 0.5f, (float)random.NextDouble() - 0.5f).Normalized() * radius / decayTime, color, (float)(random.NextDouble() / 4 + 0.75)));
                }

                if (flowTime < 0)
                {
                    flowTime = 0;
                }
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

        public void Emit(float flowTime)
        {
            this.flowTime = flowTime;
        }

        public void Render(float delta, float zIndex = 0)
        {
            GL.PointSize(Game.SCALE);
            GL.Begin(PrimitiveType.Points);
            foreach (var particle in particles)
            {
                particle.Render(delta, zIndex);
            }
            GL.End();
        }
    }
}
