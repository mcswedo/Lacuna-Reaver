using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class BattleThiefRing : Accessory
    {
        public BattleThiefRing()
        {   
            DisplayName = Strings.ZA129;
            Description = Strings.ZA130 + "\n(" + Strings.ZA079 + " +15, " + Strings.ZA080 + " +15, " + Strings.ZA083 + " +250)";
            DropChance = 0.01f;

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 15.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Agility,
                    ModifierValue = 15.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.MaxHealth,
                    ModifierValue = 250.0f
                }
            };

            LoadContent("BattleThiefRing");
        }
    }
}
