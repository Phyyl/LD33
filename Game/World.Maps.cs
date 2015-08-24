using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public partial class World
    {
        private void CreateMaps()
        {
            Maps.Add(CreateMap1());
        }

        private Map CreateMap1()
        {
            Map map = new Map(this, new System.Drawing.RectangleF());

            Door door1 = new Door(new Vector2(0, 50), Direction.Up);
            Door door2 = new Door(new Vector2(0, 200), Direction.Down);

            door1.Destination = door2;
            door2.Destination = door1;

            map.AddEntity(new NPC(new Vector2(50, 0)));

            map.AddEntity(door1);
            map.AddEntity(door2);

            return map;
        }
    }
}
