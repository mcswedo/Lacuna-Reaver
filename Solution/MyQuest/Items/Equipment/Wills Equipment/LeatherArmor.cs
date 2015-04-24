using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class LeatherArmor : Equipment
    {
        public LeatherArmor()
        {
            DisplayName = Strings.ZA163;
            Description = Strings.ZA164 + "\n(" + Strings.ZA084 + " +12, " + Strings.ZA080 + " +3)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Defense,
                    ModifierValue = 12.0f
                },
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Agility,
                    ModifierValue = 3.0f
                }
                
            };

            LoadContent("Armor");
        }
    }
}
