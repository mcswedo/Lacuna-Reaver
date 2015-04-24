using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PlainBook : Equipment
    {
        public PlainBook()
        {
            DisplayName = Strings.ZA141;
            Description = Strings.ZA142 + "\n(" + Strings.ZA079 + " +3, " + Strings.ZA081 + " +12)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 3.0f
                },
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Intelligence,
                    ModifierValue = 12.0f
                }
            };

            LoadContent("Sword");
        }
    }
}
