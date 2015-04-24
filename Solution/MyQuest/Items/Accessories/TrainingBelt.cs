using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class TrainingBelt : Accessory
    {
        public TrainingBelt()
        {
            DisplayName = Strings.ZA131;
            //Description = Strings.ZA132 + "\n(" + Strings.ZA086 + " +1, " + Strings.ZA079 + " +5, " + Strings.ZA081 + " +5, "  + Strings.ZA084 + " +5, " + Strings.ZA082 + " +5, " + Strings.ZA080 + " +5, "
            //     + Strings.ZA083 + " +200, " + Strings.ZA085 + " +200, )"; //All stats + 5, HP and Mana + 200, Stam. + 1
            Description = Strings.ZA132 + "\n(" + Strings.ZA614 + ")";
            DropChance = 0.01f;

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Stamina,
                    ModifierValue = 1.0f
                },
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 5f
                },
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Defense,
                    ModifierValue = 5f
                },
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Intelligence,
                    ModifierValue = 5f
                },
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.WillPower,
                    ModifierValue = 5f
                },
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Agility,
                    ModifierValue = 5f
                },
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.MaxHealth,
                    ModifierValue = 200f
                },
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.MaxEnergy,
                    ModifierValue = 200f
                },

            };

            LoadContent("TrainingBelt");
        }
    }
}