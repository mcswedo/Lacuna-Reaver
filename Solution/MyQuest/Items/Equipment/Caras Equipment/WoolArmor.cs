using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class WoolArmor : Equipment
    {
        public WoolArmor()
        {
            DisplayName = Strings.ZA143;
            Description = Strings.ZA144 + "\n(" + Strings.ZA084 + " +24, " + Strings.ZA082 + " +8 " + Strings.ZA086 + " +2)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Defense,
                    ModifierValue = 24.0f
                },
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.WillPower,
                    ModifierValue = 8.0f
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
