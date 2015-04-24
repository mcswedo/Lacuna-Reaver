using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class AgoraElderMantis : ElderMantis
    {
        public AgoraElderMantis()
        {
            FighterStats = new FighterStats
            {
                BaseMaxHealth = 21000,
                Health = 21000,
                BaseMaxEnergy = 1500,
                Energy = 1500,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 120,
                BaseDefense = 182,
                BaseIntelligence = 223,
                BaseWillpower = 197,
                BaseAgility = 172,
                Level = 25,
                Experience = 3750,
                Gold = 103,
            };

            ItemsDropped = new List<string>()
            {
            };
        }
    }
}
