using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class ExpertsClothArmor : Equipment
    {
        public ExpertsClothArmor()
        {
            DisplayName = Strings.ZA622;
            Description = Strings.ZA138 + "\n(" + Strings.ZA084 + " +28, " + Strings.ZA082 + " +12 " + Strings.ZA086 + " +5)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Defense,
                    ModifierValue = 28.0f
                },
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.WillPower,
                    ModifierValue = 12.0f
                },
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Stamina,
                    ModifierValue = 5.0f
                }
            };

            LoadContent("Armor");
        }
    }
}
