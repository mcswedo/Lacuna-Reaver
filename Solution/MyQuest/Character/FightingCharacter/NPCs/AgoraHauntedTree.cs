using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class AgoraHauntedTree : HauntedTree
    {
        public AgoraHauntedTree()
        {
            Name = "Cursed Tree";
            BehaviorType = AIType.Knowledgeable;
            HitNoiseSoundCue = AudioCues.Thunk;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.MonsterHit;
            FighterStats = new FighterStats
            {
                BaseMaxHealth = 20340,
                Health = 20000,
                BaseMaxEnergy = 1000,
                Energy = 1000,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 199,
                BaseDefense = 213,
                BaseIntelligence = 80,
                BaseWillpower = 128,
                BaseAgility = 97,
                Level = 23,
                Experience = 3300,
                Gold = 97,
            };
            ItemsDropped = new List<string>()
            {
            };
        }
    }
}
