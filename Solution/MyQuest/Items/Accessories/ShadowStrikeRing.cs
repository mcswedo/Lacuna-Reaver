using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class ShadowStrikeRing : Accessory
    {
        public ShadowStrikeRing()
        {   
            DisplayName = Strings.ZA109;
            Description = Strings.ZA110 + "\n(" + Strings.ZA081 + " +8, " + Strings.ZA080 + " +5, " + Strings.ZA084 + " +2)";
            DropChance = 0.01f;

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Intelligence,
                    ModifierValue = 8.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Agility,
                    ModifierValue = 5.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Defense,
                    ModifierValue = 2.0f
                }
            };

            LoadContent("ShadowStrikeRing");
        }
    }
}
