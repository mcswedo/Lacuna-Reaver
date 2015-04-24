using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class ObsidianCharm : Accessory
    {
        public ObsidianCharm()
        {
            DisplayName = Strings.ZA099;
            Description = Strings.ZA100 + "\n(" + Strings.ZA079 + " +3, " + Strings.ZA080 + " +6)";
            DropChance = 0.01f;

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 3.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Agility,
                    ModifierValue = 6.0f
                }
            };

            LoadContent("ObsidianCharm");
        }
    }
}