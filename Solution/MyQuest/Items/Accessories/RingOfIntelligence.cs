using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class RingOfIntelligence : Accessory
    {
        public RingOfIntelligence(ContentManager content)
        {
            DisplayName = "Ring of Intelligence";
            Description = "This ring grants its wearer a boost to magical power. (Int +4)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Intelligence,
                    ModifierValue = 4.0f
                }
            };

            Graphic = content.Load<Texture2D>(ContentPath.ToItemIcons + "RingOfStrength");
        }
    }
}