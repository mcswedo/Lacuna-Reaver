using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class BattleLordsRing : Accessory
    {
        public BattleLordsRing()
        {
            DisplayName = Strings.ZA127;
            Description = Strings.ZA128 + "\n(" + Strings.ZA079 + " +20, " + Strings.ZA084 + " +10, " + Strings.ZA083 + " +350)";

            DropChance = 0.01f;

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 20.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Defense,
                    ModifierValue = 10.0f
                },
                
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.MaxHealth,
                    ModifierValue = 350.0f
                }
            };

            LoadContent("BattleLordsRing");
        }
    }
}