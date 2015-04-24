using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PearlBand : Accessory
    {
        public PearlBand()
        {
            DisplayName = Strings.ZA095;
            Description = Strings.ZA096 + "\n(" + Strings.ZA081 + " +5, " + Strings.ZA084 + " +4)";
            DropChance = 0.01f;

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Intelligence,
                    ModifierValue = 5.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Defense,
                    ModifierValue = 4.0f
                }
            };

            LoadContent("PearlBand");

           // Graphic = content.Load<Texture2D>(ContentPath.ToItemIcons + "PearlBand");
        }
    }
}