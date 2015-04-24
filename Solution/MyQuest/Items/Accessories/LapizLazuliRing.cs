using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class LapizLazuliRing : Accessory
    {
        public LapizLazuliRing()
        {   
            DisplayName = Strings.ZA093;
            Description = Strings.ZA094 + "\n(" + Strings.ZA081 + " +4, " + Strings.ZA082 + " +5)";
            DropChance = 0.01f;

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Intelligence,
                    ModifierValue = 4.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.WillPower,
                    ModifierValue = 5.0f
                }
            };

            LoadContent("LapizLazuliRing");

//            Graphic = content.Load<Texture2D>(ContentPath.ToItemIcons + "LapizLazuliRing");
        }
    }
}
