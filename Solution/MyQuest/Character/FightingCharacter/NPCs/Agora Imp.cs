using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class AgoraImp : Imp
    {
 

        public AgoraImp()
        {

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 5500,
                Health = 5500,
                BaseMaxEnergy = 2500,
                Energy = 2500,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 95,
                BaseDefense =  90,
                BaseIntelligence = 100,
                BaseWillpower = 90,
                BaseAgility = 100,
                Level = 18,
                Experience = 2200,
                Gold = 2500,
            };

        }
    }
}
