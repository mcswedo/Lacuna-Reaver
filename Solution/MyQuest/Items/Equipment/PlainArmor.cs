using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PlainArmor : Equipment
    {
        public PlainArmor()
        {
            DisplayName = Strings.ZA514;
            Description = Strings.ZA515 + "\n";

            Modifiers = new List<StatModifier>();

            LoadContent("Armor");
        }
    }
}
