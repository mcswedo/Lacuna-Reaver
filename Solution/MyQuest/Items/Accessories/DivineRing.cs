using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class DivineRing : Accessory
    {
        public DivineRing()
        {
            DisplayName = Strings.ZA111;
            Description = Strings.ZA112 + "\n(" + Strings.ZA081 + " +8, " + Strings.ZA085 + " +15, " + Strings.ZA080 + " +5)";
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
                    TargetStat = MyQuest.TargetStat.MaxEnergy,
                    ModifierValue = 15.0f
                },

                 new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Agility,
                    ModifierValue = 5.0f
                }
            };

            LoadContent("DivineRing");
        }
    }
}