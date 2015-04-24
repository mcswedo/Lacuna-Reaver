using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{
    public class SidDoubleStrike : DoubleStrike
    {
        public SidDoubleStrike()
        {
            DrawOffset = new Vector2(-125, -25);
            SpCost = 3;
        }
    }
}
