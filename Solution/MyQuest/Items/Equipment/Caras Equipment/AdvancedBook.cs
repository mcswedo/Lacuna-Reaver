using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class AdvancedBook : Equipment
    {
        public AdvancedBook()
        {
            DisplayName = Strings.ZA133;
            Description = Strings.ZA134 + "\n(" + Strings.ZA079 + " +6, " + Strings.ZA081 + " +24)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 6.0f
                },
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Intelligence,
                    ModifierValue = 24.0f
                }
            };

            LoadContent("Sword");
        }
    }
}
