using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class RawhideLeatherArmor : Equipment
    {
        public RawhideLeatherArmor()
        {
            DisplayName = Strings.ZA165;
            Description = Strings.ZA166 + "\n(" + Strings.ZA084 + " +15, " + Strings.ZA080 + " +6 " + Strings.ZA086 + " +2)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Defense,
                    ModifierValue = 24.0f
                },
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Agility,
                    ModifierValue = 6.0f
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
