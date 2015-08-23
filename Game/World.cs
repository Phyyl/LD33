using Game.Particles;
using Game.Resources;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class World
    {
        private List<Map> maps;
        public Monster Player;

		public IEnumerable<Map> Maps 
		{ 
			get
			{
				foreach (var map in maps)
				{
					yield return map;
				}
			}
		}

        ParticleEmitter emitter = new ParticleEmitter(new Vector2(0, 0), Color.DarkRed, 25, 3, 500);

        public World()
        {
            maps = new List<Map>();
            Player = new Monster();



            Map map = new Map(this, new RectangleF(0, 0, 1000, 1000));
            maps.Add(map);
            map.AddEntity(Player);

            for (int i = 0; i < 8; i++)
            {
                map.AddEntity(new NPC());
            }

			Map map2 = new Map(this, new RectangleF(0, 0, 1000, 1000));
			maps.Add(map2);
			map2.AddEntity(new Prop(Textures.ComputerDesk, PropAngle.Angle0) { Position = new Vector2(100, 100) });
			map2.AddEntity(new Prop(Textures.ComputerDesk, PropAngle.Angle180) { Position = new Vector2(-200, 100) });
			map2.AddEntity(new Prop(Textures.ComputerDesk, PropAngle.Angle90) { Position = new Vector2(380, 100) });
			map2.AddEntity(new Prop(Textures.ComputerDesk, PropAngle.Angle270) { Position = new Vector2(444, -240) });

			Door portemap1 = new Door (){ Position = new Vector2 (-230, -130) };
			map2.AddEntity (portemap1);

			Door portemap2 = new Door (){ Position = new Vector2 (-100, -100) };

			portemap1.Destination = portemap2;
			portemap2.Destination = portemap1;

            map.AddEntity(new Prop(Textures.ComputerDesk, PropAngle.Angle0) { Position = new Vector2(100, -100) });
            map.AddEntity(new Prop(Textures.ComputerDesk, PropAngle.Angle180) { Position = new Vector2(100, -200) });
            map.AddEntity(new Prop(Textures.ComputerDesk, PropAngle.Angle90) { Position = new Vector2(100, -300) });
            map.AddEntity(new Prop(Textures.ComputerDesk, PropAngle.Angle270) { Position = new Vector2(100, 400) });
            map.AddEntity(new Prop(Textures.Wall, PropAngle.Angle0) { Position = new Vector2(-100, -129) });
			map.AddEntity (portemap2);
           
        }

        public void Update(float delta)
        {
            Player?.Map?.Update(delta);
            emitter.Update(delta);
        }

        public void Render(float delta)
        {
            GL.Translate(Game.Instance.WindowSize.X / 2 - Player.Position.X, Game.Instance.WindowSize.Y / 2 - Player.Position.Y, 0);
            Player?.Map?.Render(delta);
            //emitter.Render(delta, zIndex: 0.1f);
        }
    }
}
