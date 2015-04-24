using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class Armor : Equipment
    {
        public Armor()
        {
            DisplayName = Strings.ZA151;
            Description = Strings.ZA152 + "\n(" + Strings.ZA084 + " +15)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Defense,
                    ModifierValue = 15.0f
                }
            };

            LoadContent("Armor");
        }
    }
}
