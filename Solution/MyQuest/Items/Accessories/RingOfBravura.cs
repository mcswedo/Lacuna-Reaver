using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class RingOfBravura : Accessory
    {
        public RingOfBravura()
        {
            DisplayName = Strings.ZA077;
            Description = Strings.ZA078 + "\n(" + Strings.ZA079 + " +3, " + Strings.ZA083 + " +10)";
            DropChance = 0.01f;

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 3.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.MaxHealth,
                    ModifierValue = 10.0f
                }
            };

            LoadContent("RingOfBravura");

//            Graphic = content.Load<Texture2D>(ContentPath.ToItemIcons + "RingOfBravura");
        }
    }
}