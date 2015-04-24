using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class AgoraDemon3 : NPCFightingCharacter
    {
        #region Static FrameAnimations

        static readonly Vector2 commonDrawOffset = new Vector2(-100, -100);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.195,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(600, 0, 200, 200),
                new Rectangle(800, 0, 200, 200),
                new Rectangle(1000, 0, 200, 200),
                new Rectangle(1200, 0, 200, 200),
                new Rectangle(1400, 0, 200, 200)
         //       new Rectangle(1600, 0, 200, 200),
          //      new Rectangle(1800, 0, 200, 200)

            }
        };

        static readonly FrameAnimation DashFrames = new FrameAnimation
        {
            FrameDelay = 0.75,
            Frames = new List<Rectangle>
            {
                new Rectangle(800, 0, 200, 200),
            }
        };

        static readonly FrameAnimation AttackFrames = new FrameAnimation
        {
            FrameDelay = 0.075,
            Frames = new List<Rectangle>
            {
               new Rectangle(0, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(600, 0, 200, 200),
                new Rectangle(800, 0, 200, 200),
                new Rectangle(1000, 0, 200, 200),
                new Rectangle(1200, 0, 200, 200),
                new Rectangle(1400, 0, 200, 200),
                new Rectangle(1600, 0, 200, 200),
                new Rectangle(1800, 0, 200, 200),

                new Rectangle(0,   200, 200, 200),
                new Rectangle(200, 200, 200, 200),
                new Rectangle(400, 200, 200, 200),
                new Rectangle(600, 200, 200, 200),
                new Rectangle(800, 200, 200, 200),
                new Rectangle(1000, 200, 200, 200),
                new Rectangle(1200, 200, 200, 200),
                new Rectangle(1400, 200, 200, 200),
                new Rectangle(1600, 200, 200, 200),
                new Rectangle(1800, 200, 200, 200)
            }
        };
  

        #endregion

        #region Constructor

        public AgoraDemon3()
        {
            Name = "Cursed Knight";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "Demon3Attack";
            BehaviorType = AIType.Knowledgeable;
            HitNoiseSoundCue = AudioCues.Stab;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.Steel;


            FighterStats = new FighterStats
            {
                BaseMaxHealth = 17050,
                Health = 17050,
                BaseMaxEnergy = 5000,
                Energy = 5000,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 170,
                BaseDefense = 220,
                BaseIntelligence = 110,
                BaseWillpower = 120,
                BaseAgility = 90,
                Level = 22,
                Experience = 2560,
                Gold = 95,
            };
 

            //offsets
            damageMessageOffset = new Vector2(25, -80);
            defenseShieldOffset = new Vector2(0, 0);
            statusEffectMessageOffset = new Vector2(-125, -50);
            pointerOffset = new Vector2(-100, 85);

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "cursed_knight_idle",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

           CombatAnimation DashAnimation = new CombatAnimation()
            {
                Name = "Dash",
                TextureName = "cursed_knight_idle",
                Animation = DashFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation AttackAnimation = new CombatAnimation()
            {
                Name = "Attack",
                TextureName = "cursed_knight_attack",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };
   

            ItemsDropped = new List<string>()
            {
                "SpikedChain"
            };

            SkillNames = new List<string>()
            {
                "Demon3Attack",
                "Demon3Lightning",
            };

            CombatAnimations = new Dictionary<string, CombatAnimation>()
            {
                { "Idle", IdleAnimation },
                { "Dash", DashAnimation },
                { "Attack", AttackAnimation },
            };
        }

        #endregion
    }
}
