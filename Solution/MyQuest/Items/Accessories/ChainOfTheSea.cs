using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class ChainOfTheSea : Accessory
    {
        public ChainOfTheSea()
        {
            DisplayName = Strings.ZA121;
            Description = Strings.ZA122 + "\n(" + Strings.ZA081 + " +11, " + Strings.ZA080 + " +8, " + Strings.ZA079 + " +5)";
            DropChance = 0.01f;

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Intelligence,
                    ModifierValue = 11.0f
                },
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Agility,
                    ModifierValue = 8.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 5.0f
                }
            };

            LoadContent("ChainOfTheSea");
        }
    }
}