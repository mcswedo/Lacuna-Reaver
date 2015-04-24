using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class RingOfAgility : Accessory
    {
        public RingOfAgility(ContentManager content)
        {
            DisplayName = "Ring of Agility";
            Description = "This ring grants its wearer a boost to agility. (Agi +4)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Agility,
                    ModifierValue = 4.0f
                }
            };

            Graphic = content.Load<Texture2D>(ContentPath.ToItemIcons + "RingOfStrength");
        }
    }
}