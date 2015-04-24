using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class RingOfTheSages : Accessory
    {
        public RingOfTheSages()
        {
            DisplayName = Strings.ZA089;
            Description = Strings.ZA090 + "\n(" + Strings.ZA081 + " +2, " + Strings.ZA082 + " +2)";
            DropChance = 0.01f;

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Intelligence,
                    ModifierValue = 2.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.WillPower,
                    ModifierValue = 2.0f
                }
            };

            LoadContent("RingOfTheSages");

//            Graphic = content.Load<Texture2D>(ContentPath.ToItemIcons + "RingOfTheSages");
        }
    }
}