using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class RingOfHealth : Accessory
    {
        public RingOfHealth(ContentManager content)
        {
            DisplayName = "Ring of Health";
            Description = "This ring grants its wearer a boost to health. (HP +35)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.MaxHealth,
                    ModifierValue = 35.0f
                },
            };

            Graphic = content.Load<Texture2D>(ContentPath.ToItemIcons + "RingOfStrength");
        }
    }
}