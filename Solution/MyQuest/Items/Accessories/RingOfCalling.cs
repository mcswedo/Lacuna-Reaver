using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class RingOfCalling : Accessory
    {
        public RingOfCalling()
        {
            DisplayName = Strings.ZA091;
            Description = Strings.ZA092 + "\n(" + Strings.ZA081 + " +2, " + Strings.ZA080 + " +2)";
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
                    TargetStat = MyQuest.TargetStat.Agility,
                    ModifierValue = 2.0f
                }
            };

            LoadContent("RingOfCalling");

//            Graphic = content.Load<Texture2D>(ContentPath.ToItemIcons + "RingOfCalling");
        }
    }
}