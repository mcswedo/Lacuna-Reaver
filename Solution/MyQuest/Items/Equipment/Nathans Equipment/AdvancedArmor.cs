using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class AdvancedArmor : Equipment
    {
        public AdvancedArmor()
        {
            DisplayName = Strings.ZA153;
            Description = Strings.ZA154 + "\n(" + Strings.ZA084 + " +15, " + Strings.ZA086 + " +2)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Defense,
                    ModifierValue = 30.0f
                },
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Stamina,
                    ModifierValue = 2.0f
                }
            };

            LoadContent("Armor");
        }
    }
}
