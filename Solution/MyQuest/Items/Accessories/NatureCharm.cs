using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class NatureCharm : Accessory
    {
        public NatureCharm()
        {
            DisplayName = Strings.ZA119;
            Description = Strings.ZA120 + "\n(" + Strings.ZA079 + " +12, " + Strings.ZA080 + " +10, " + Strings.ZA083 + " +150)";
            DropChance = 0.01f;

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 12.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Agility,
                    ModifierValue = 10.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.MaxHealth,
                    ModifierValue = 150.0f
                }
            };

            LoadContent("NatureCharm");
        }
    }
}