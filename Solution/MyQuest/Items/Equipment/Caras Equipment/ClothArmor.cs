using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class ClothArmor : Equipment
    {
        public ClothArmor()
        {
            DisplayName = Strings.ZA135;
            Description = Strings.ZA136 + "\n(" + Strings.ZA084 + " +8)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Defense,
                    ModifierValue = 8.0f
                }
            };

            LoadContent("Armor");
        }
    }
}
