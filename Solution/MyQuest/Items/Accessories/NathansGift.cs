using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class NathansGift : Accessory
    {
        public NathansGift()
        {
            DisplayName = Strings.ZA103;
            Description = Strings.ZA104 + "\n(" + Strings.ZA079 + " +4, " + Strings.ZA084 + " +4, " + Strings.ZA080 + " +4)";
            DropChance = 0.03f;

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 4.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Defense,
                    ModifierValue = 4.0f
                },

                 new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Agility,
                    ModifierValue = 4.0f
                }
            };

            LoadContent("NathansGift");
        }
    }
}