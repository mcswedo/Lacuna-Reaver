using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class AdvancedSword : Equipment
    {
        public AdvancedSword()
        {
            DisplayName = Strings.ZA147;
            Description = Strings.ZA148 + "\n(" + Strings.ZA079 + " +15)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 15.0f
                }
            };

            LoadContent("Sword");
        }
    }
}
