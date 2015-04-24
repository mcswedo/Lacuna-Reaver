using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class AgoraGhost : Ghost
    {
        public AgoraGhost()
        {
            Name = "Ghost";
            BehaviorType = AIType.Knowledgeable;
            HitNoiseSoundCue = AudioCues.Swoosh;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.MonsterHit;
            FighterStats = new FighterStats
            {
                BaseMaxHealth = 18550,
                Health = 18550,
                BaseMaxEnergy = 5000,
                Energy = 5000,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 183,
                BaseDefense = 168,
                BaseIntelligence = 151,
                BaseWillpower = 127,
                BaseAgility = 269,
                Level = 24,
                Experience = 3575,
                Gold = 99,
            };

            ItemsDropped = new List<string>()
            {
            };
        }
    }
}
