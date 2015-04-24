using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class AgoraTohmey : Tohmey
    {
        public AgoraTohmey()
        {
            BehaviorType = AIType.Simple;
            HitNoiseSoundCue = AudioCues.Flesh;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.MonsterHit;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 17500,
                Health = 17500,
                BaseMaxEnergy = 2500,
                Energy = 2500,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 192,
                BaseDefense =  153,
                BaseIntelligence = 38,
                BaseWillpower = 118,
                BaseAgility = 164,
                Level = 24,
                Experience = 3450,
                Gold = 93,
            };
            ItemsDropped = new List<string>()
            {
            };
        }
    }
}
