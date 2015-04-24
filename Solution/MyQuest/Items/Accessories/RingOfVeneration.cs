using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class RingOfVeneration : Accessory
    {
        public RingOfVeneration()
        {
            DisplayName = Strings.ZA087;
            Description = Strings.ZA088 + "\n(" + Strings.ZA079 + " +1, " + Strings.ZA084 + " +3)";
            DropChance = 0.01f;

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

            LoadContent("RingOfVeneration");

//            Graphic = content.Load<Texture2D>(ContentPath.ToItemIcons + "RingOfVeneration");
        }
    }
}