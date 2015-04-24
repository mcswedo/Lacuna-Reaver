using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class EarthenGloves : Accessory
    {
        public EarthenGloves()
        {
            DisplayName = Strings.ZA117;
            Description = Strings.ZA118 + "\n(" + Strings.ZA079 + " +13, " + Strings.ZA084 + " +7, " + Strings.ZA082 + " +7)";
            DropChance = 0.01f;

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 13.0f
                },
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Defense,
                    ModifierValue = 7.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.WillPower,
                    ModifierValue = 7.0f
                }
            };

            LoadContent("EarthenGloves");
        }
    }
}