using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class TrainingSword : Equipment
    {
        public TrainingSword()
        {
            DisplayName = Strings.ZA720;
            Description = Strings.ZA721 + "\n(" + Strings.ZA079 + " +6)";

            Modifiers = new List<StatModifier>()
            {
                new StatModifier()
                {
                    TargetStat = MyQuest.TargetStat.Strength,
                    ModifierValue = 6.0f
                }
            };

            LoadContent("Sword");
        }
    }
}
