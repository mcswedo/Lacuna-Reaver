using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class ExpertsBook : Equipment
    {
        public ExpertsBook()
        {
            DisplayName = Strings.ZA139;
            Description = Strings.ZA140 + "\n(" + Strings.ZA079 + " +10, " + Strings.ZA081 + " +36)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 10.0f
                },
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Intelligence,
                    ModifierValue = 36.0f
                }
            };

            LoadContent("Sword");
        }
    }
}
