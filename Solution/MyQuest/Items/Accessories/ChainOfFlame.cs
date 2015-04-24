using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class ChainOfFlame : Accessory
    {
        public ChainOfFlame()
        {
            DisplayName = Strings.ZA115;
            Description = Strings.ZA116 + "\n(" + Strings.ZA079 + " +10, " + Strings.ZA082 + " +12, " + Strings.ZA085 + " +25)";
            DropChance = 0.01f;

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 10.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.WillPower,
                    ModifierValue = 12.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.MaxEnergy,
                    ModifierValue = 25.0f
                }
            };

            LoadContent("ChainOfFlame");
        }
    }
}