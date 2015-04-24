using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class ExpertsScythe : Equipment
    {
        public ExpertsScythe()
        {
            DisplayName = Strings.ZA620;
            Description = Strings.ZA162 + "\n(" + Strings.ZA079 + " +24, " + Strings.ZA081 + " +10)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 24.0f
                },
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Intelligence,
                    ModifierValue = 10.0f
                }
            };

            LoadContent("Sword");
        }
    }
}
