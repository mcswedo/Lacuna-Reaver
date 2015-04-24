using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class ExpertsArmor : Equipment
    {
        public ExpertsArmor()
        {
            DisplayName = Strings.ZA155;
            Description = Strings.ZA156 + "\n(" + Strings.ZA084 + " +35, " + Strings.ZA086 + " +5)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Defense,
                    ModifierValue = 35.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Stamina,
                    ModifierValue = 5.0f
                }
            };

            LoadContent("Armor");
        }
    }
}
