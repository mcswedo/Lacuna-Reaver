using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class RingOfJustice : Accessory
    {
        public RingOfJustice()
        {
            DisplayName = Strings.ZA075;
            Description = Strings.ZA076 + "\n(" + Strings.StatusScreen_Str + " +2, " + Strings.StatusScreen_Agil +"  +2)";
            DropChance = 0.01f;

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 2.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Agility,
                    ModifierValue = 2.0f
                }
            };

            LoadContent("RingOfJustice");
//            Graphic = content.Load<Texture2D>(ContentPath.ToItemIcons + "RingOfJustice");
        }
    }
}