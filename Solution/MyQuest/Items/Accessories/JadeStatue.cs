using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class JadeStatue : Accessory
    {
        public JadeStatue()
        {
            DisplayName = Strings.ZA097;
            Description = Strings.ZA098 + "\n(" + Strings.ZA079 + " +4, " + Strings.ZA082 + " +5)";
            DropChance = 0.01f;

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 4.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.WillPower,
                    ModifierValue = 5.0f
                }
            };

            LoadContent("JadeStatue");
        }
    }
}