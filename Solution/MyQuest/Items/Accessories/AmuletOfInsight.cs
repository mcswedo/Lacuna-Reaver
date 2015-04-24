using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class AmuletOfInsight : Accessory
    {
        public AmuletOfInsight()
        {
            DisplayName = Strings.ZA268;
            Description = Strings.ZA269 + "\n(" + Strings.ZA079 + " +5, " + Strings.ZA080 + " +5, " + Strings.ZA081 + " +5)";
            DropChance = 0.015f;

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 5.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Agility,
                    ModifierValue = 5.0f
                },

                 new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Intelligence,
                    ModifierValue = 5.0f
                }
            };

            LoadContent("ObsidianCharm");
        }
    }
}