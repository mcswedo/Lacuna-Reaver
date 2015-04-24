using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class ChainOfSkies : Accessory
    {
        public ChainOfSkies()
        {
            DisplayName = Strings.ZA123;
            Description = Strings.ZA124 + "\n(" + Strings.ZA081 + " +13, " + Strings.ZA084 + " +9, " + Strings.ZA085 + " +5)";
            DropChance = 0.01f;

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Intelligence,
                    ModifierValue = 13.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.WillPower,
                    ModifierValue = 9.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Defense,
                    ModifierValue = 5.0f
                }
            };

            LoadContent("ChainOfSkies");
        }
    }
}