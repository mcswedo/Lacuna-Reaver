using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class SpikedChain : Accessory
    {
        public SpikedChain()
        {
            DisplayName = Strings.ZA113;
            Description = Strings.ZA114 + "\n(" + Strings.ZA079 + " +8, " + Strings.ZA084 + " +3, " + Strings.ZA082 + " +4)";
            DropChance = 0.01f;

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 8.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Defense,
                    ModifierValue = 3.0f
                },
                
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.WillPower,
                    ModifierValue = 4.0f
                }
            };

            LoadContent("SpikedChain");
        }
    }
}