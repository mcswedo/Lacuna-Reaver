using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class AgoraCaveCrab : CaveCrab
    {
        public AgoraCaveCrab()
        {
            FighterStats = new FighterStats
            {
                BaseMaxHealth = 6500,
                Health = 6500,
                BaseMaxEnergy = 1000,
                Energy = 1000,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 100,
                BaseDefense = 130,
                BaseIntelligence = 80,
                BaseWillpower = 70,
                BaseAgility = 60,
                Level = 18,
                Experience = 2200,
                Gold = 45,
            };
        }
    }
}
