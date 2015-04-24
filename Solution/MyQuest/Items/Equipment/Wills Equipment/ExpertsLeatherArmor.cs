using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class ExpertsLeatherArmor : Equipment
    {
        public ExpertsLeatherArmor()
        {
            DisplayName = Strings.ZA621;
            Description = Strings.ZA168 + "\n(" + Strings.ZA084 + " +36, " + Strings.ZA080 + " +9 " + Strings.ZA086 + " +5)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Defense,
                    ModifierValue = 36.0f
                },
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Agility,
                    ModifierValue = 9.0f
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
