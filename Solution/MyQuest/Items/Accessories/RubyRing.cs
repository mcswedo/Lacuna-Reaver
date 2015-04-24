using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class RubyRing : Accessory
    {
        public RubyRing()
        {
            DisplayName = Strings.ZA101;
            Description = Strings.ZA102 + "\n(" + Strings.ZA079 + " +5, " + Strings.ZA085 + " +15)";
            DropChance = 0.01f;

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 5.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.MaxEnergy,
                    ModifierValue = 15.0f
                }
            };

            LoadContent("RubyRing");
        }
    }
}