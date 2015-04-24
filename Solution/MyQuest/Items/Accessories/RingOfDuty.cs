using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class RingOfDuty : Accessory
    {
        public RingOfDuty(ContentManager content)
        {
            DisplayName = "Ring of Duty";
            Description = "This ring grants its wearer strength and defense. (Str +1, Def +3)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 1.0f
                },
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Defense,
                    ModifierValue = 3.0f
                }
            };

            Graphic = content.Load<Texture2D>(ContentPath.ToItemIcons + "RingOfStrength");
        }
    }
}