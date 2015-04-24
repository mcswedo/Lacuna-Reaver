using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class RustyScythe : Equipment
    {
        public RustyScythe()
        {
            DisplayName = Strings.ZA510;
            Description = Strings.ZA509;

            Modifiers = new List<StatModifier>();

            LoadContent("Sword");
        }
    }
}
