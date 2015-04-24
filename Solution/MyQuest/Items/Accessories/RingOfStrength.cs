using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class RingOfStrength : Accessory
    {
        public RingOfStrength(ContentManager content)
        {
            DisplayName = "Ring of Strength";
            Description = "This ring grants its wearer great strength. (Str +4)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 4.0f
                }
            };

            Graphic = content.Load<Texture2D>(ContentPath.ToItemIcons + "RingOfStrength");
        }
    }
}