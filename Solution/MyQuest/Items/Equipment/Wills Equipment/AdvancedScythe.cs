using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class AdvancedScythe : Equipment
    {
        public AdvancedScythe()
        {
            DisplayName = Strings.ZA159;
            Description = Strings.ZA160 + "\n(" + Strings.ZA079 + " +16, " + Strings.ZA081 + " +6)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 16.0f
                },
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Intelligence,
                    ModifierValue = 6.0f
                }
            };

            LoadContent("Sword");
        }
    }
}
