﻿using Game.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class NPC : LivingEntity
    {
        public float FOV { get; private set; }

        public override bool Alive { get; protected set; }
        public override float Angle { get; protected set; }

        public override void Update(float delta)
        {

        }

        public override void Render(float delta)
        {
            SpriteSheets.Passive1.Render(0, 0, Position, SpriteSheets.Passive1.Size / 2, Angle);
        }
    }
}
