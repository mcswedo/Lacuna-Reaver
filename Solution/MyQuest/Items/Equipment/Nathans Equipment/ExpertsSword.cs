using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class ExpertsSword : Equipment
    {
        public ExpertsSword()
        {
            DisplayName = Strings.ZA149;
            Description = Strings.ZA150 + "\n(" + Strings.ZA079 + " +20)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 20.0f
                }
            };

            LoadContent("Sword");
        }
    }
}
