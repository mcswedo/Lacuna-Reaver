using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class BattleMageRing : Accessory
    {
        public BattleMageRing()
        {   
            DisplayName = Strings.ZA125;
            Description = Strings.ZA126 + "\n(" + Strings.ZA081 + " +20, " + Strings.ZA082 + " +10, " + Strings.ZA083 + " +200)";
            DropChance = 0.01f;

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Intelligence,
                    ModifierValue = 20.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.WillPower,
                    ModifierValue = 10.0f
                },

                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.MaxHealth,
                    ModifierValue = 200.0f
                }
            };

            LoadContent("BattleMageRing");
        }
    }
}
