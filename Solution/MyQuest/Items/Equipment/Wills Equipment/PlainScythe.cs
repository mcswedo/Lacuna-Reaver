using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PlainScythe : Equipment
    {
        public PlainScythe()
        {
            DisplayName = Strings.ZA157;
            Description = Strings.ZA158 + "\n(" + Strings.ZA079 + " +8, " + Strings.ZA081 + " +2)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 8.0f
                },
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Intelligence,
                    ModifierValue = 2.0f
                }
            };

            LoadContent("Sword");
        }
    }
}
