using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class AgoraFireFlyder : FireFlyder
    {
        public AgoraFireFlyder()
        {
            FighterStats = new FighterStats
            {
                BaseMaxHealth = 6000,
                Health = 6000,
                BaseMaxEnergy = 1000,
                Energy = 1000,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 150,
                BaseDefense = 80,
                BaseIntelligence = 88,
                BaseWillpower = 130,
                BaseAgility = 110,
                Level = 20,
                Experience = 2500,
                Gold = 250,
            };
        }
    }
}
