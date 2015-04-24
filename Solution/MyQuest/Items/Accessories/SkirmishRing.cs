using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class SkirmishRing : Accessory
    {
        public SkirmishRing()
        {
            DisplayName = Strings.ZA107;
            Description = Strings.ZA108 + "\n(" + Strings.ZA079 + " +8, " + Strings.ZA080 + " +7, " + Strings.ZA085 + " +15)";
            DropChance = 0.01f;

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 8.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Agility,
                    ModifierValue = 7.0f
                },
                
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.MaxEnergy,
                    ModifierValue = 15.0f
                }
            };

            LoadContent("SkirmishRing");
        }
    }
}