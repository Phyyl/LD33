using System;
using OpenTK;
using Game.Graphics;
using Game.Resources;
using System.Drawing;

namespace Game
{
	public class Door : Prop
	{
		private Door destination;
		private Vector2 spawnCoordinates;
		private const float ACTION_BOX_INFLATION = 1;

        public override RectangleF CollisionBox 
        {
            get{
                RectangleF box = base.CollisionBox;

                if ((int)PropAngle % 180 == 0)
                {
                    box.Inflate(-3, 0);
                }
                else
                {
                    box.Inflate(0, -3);
                }
                return box;
            }
        }

		public Door Destination
		{
			get { return destination; }
			set { destination = value; }
		}

		public Vector2 SpawnCoordinate
		{
			get
			{ 
				return spawnCoordinates;
			}
			set { spawnCoordinates = value; }
		}

		public Door(Door destination = null, PropAngle angle = default(PropAngle))
			: base(Textures.Door, angle)
		{
			this.destination = destination;
		}

		public override void Update(float delta)
		{
			base.Update(delta);

			RectangleF doorActionBox = this.CollisionBox;

			RectangleF playerHitBox = Map.World.Player.CollisionBox;


			if ((int)PropAngle % 180 == 0)
			{
				doorActionBox.Inflate(ACTION_BOX_INFLATION, 0);
			}
			else
			{
				doorActionBox.Inflate(0, ACTION_BOX_INFLATION);
			}
						  
			if (doorActionBox.IntersectsWith(playerHitBox))
			{
				Destination.Map.AddEntity(Map.World.Player);

				if ((int)PropAngle % 180 == 0)
				{
					Map.World.Player.Position = new Vector2(Destination.Position.X - Destination.Size.X / 2 - Map.World.Player.Size.X / 2 - ACTION_BOX_INFLATION, Destination.Position.Y);
				}
				else
				{
					Map.World.Player.Position = new Vector2(Destination.Position.X, Destination.Position.Y - Destination.Size.Y / 2 - Map.World.Player.Size.Y / 2 - ACTION_BOX_INFLATION);
				}
			}
		}
	}
}
