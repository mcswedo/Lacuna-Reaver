using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PlainSword : Equipment
    {
        public PlainSword()
        {
            DisplayName = Strings.ZA145;
            Description = Strings.ZA146 + "\n(" + Strings.ZA079 + " +6)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 6.0f
                }
            };

            LoadContent("Sword");
        }
    }
}
