using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class WitchDoctor : NPCFightingCharacter
    {
        #region Static FrameAnimations
        
        static readonly Vector2 commonDrawOffset = new Vector2(-85, -105);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.195,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(600, 0 ,200, 200),
                new Rectangle(800, 0, 200, 200),
                new Rectangle(1000, 0, 200, 200),
                new Rectangle(1200, 0, 200, 200),
                new Rectangle(1400, 0 ,200, 200),
                new Rectangle(1600, 0, 200, 200),
                new Rectangle(1800, 0, 200, 200),

                new Rectangle(0, 200, 200, 200),
                new Rectangle(200, 200, 200, 200),
                new Rectangle(400, 200, 200, 200),
                new Rectangle(600, 200 ,200, 200),
                new Rectangle(800, 200, 200, 200),
                new Rectangle(1000,200, 200, 200),
                new Rectangle(1200, 200, 200, 200),
                new Rectangle(1400, 200 ,200, 200),
                new Rectangle(1600, 200, 200, 200),
                new Rectangle(1800, 200, 200, 200),
            }
        };

        static readonly FrameAnimation DashFrames = new FrameAnimation
        {
            FrameDelay = 0.75,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 200, 200, 200),
            }
        };

        static readonly FrameAnimation AttackFrames = new FrameAnimation
        {
            FrameDelay = 0.195,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(600, 0 ,200, 200),
                new Rectangle(800, 0, 200, 200),
                new Rectangle(1000, 0, 200, 200),
                new Rectangle(1200, 0, 200, 200),
                new Rectangle(1400, 0 ,200, 200),
                new Rectangle(1600, 0, 200, 200),
                new Rectangle(1800, 0, 200, 200),

                new Rectangle(0, 200, 200, 200),
                new Rectangle(200, 200, 200, 200),
                new Rectangle(400, 200, 200, 200),
            }
        };

        static readonly FrameAnimation WeaknessFrames = new FrameAnimation
        {
            FrameDelay = 0.125,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(600, 0 ,200, 200),
                new Rectangle(800, 0, 200, 200),
                new Rectangle(1000, 0, 200, 200),
                new Rectangle(1200, 0, 200, 200),
                new Rectangle(1400, 0 ,200, 200),
                new Rectangle(1600, 0, 200, 200),
                new Rectangle(1800, 0, 200, 200),

                new Rectangle(0, 200, 200, 200),

            }
        };

        #endregion

        #region Constructor

        public WitchDoctor()
        {
            Name = "Witch Doctor";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "WitchDoctorPoisonStrike";
            HitNoiseSoundCue = AudioCues.Flesh;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.MonsterHit;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 3500,
                Health = 3500,
                BaseMaxEnergy = 500,
                Energy = 500,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 110,
                BaseDefense = 104,
                BaseIntelligence = 88,
                BaseWillpower = 94,
                BaseAgility = 140,
                Level = 14,
                Experience = 1120,
                Gold = 52,
            };

            //ModifiedStats = new FighterStats();
            
            //offsets
            damageMessageOffset = new Vector2(0, -110);
            defenseShieldOffset = new Vector2(-100, -20);
            statusEffectMessageOffset = new Vector2(-130, -35);
            pointerOffset = new Vector2(-115, 61);
            projectileOriginOffset = new Vector2(45, -25);
            BehaviorType = AIType.Knowledgeable; //bug. Is this still a bug? -Kyle revision 1556

            CombatAnimation DashAnimation = new CombatAnimation()
            {
                Name = "Dash",
                TextureName = "nathan_stick_animations",
                Animation = DashFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation AttackAnimation = new CombatAnimation()
            {
                Name = "Attack",
                TextureName = "witchdoctor_attack",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = new Vector2(-98, -103)
            };


            CombatAnimation WeaknessAnimation = new CombatAnimation()
            {
                Name = "Weakness",
                TextureName = "witch_doctor_curse",
                Animation = WeaknessFrames,
                Loop = false,
                DrawOffset = new Vector2(-98, -103)
            };

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "witchdoctor_idle",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            ItemsDropped = new List<string>()
            {
                "SkirmishRing",
                "SpikedChain"
            };

            SkillNames = new List<string>()
            {
               "WitchDoctorPoisonStrike",
                "WitchDoctorWeakness",
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
            AddCombatAnimation(WeaknessAnimation);
        }

        #endregion
    }
}
