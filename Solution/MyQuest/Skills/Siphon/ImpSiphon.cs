using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class ImpSiphon : Siphon
    {
        public ImpSiphon()
            : base()
        {
            Name = Strings.ZA478;
            Description = Strings.ZA480;

            MpCost = 500;
            SpCost = 6;

            SpellPower = 15.0f;

            DrawOffset = new Vector2(-50, -50);
        }
    }
}
