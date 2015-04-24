using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class BoggimusTadpole: NPCFightingCharacter
    {
        #region Static FrameAnimations

        static readonly Vector2 commonDrawOffset = new Vector2(-65, -90);

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
                new Rectangle(1400, 0, 200, 200),
                new Rectangle(1600, 0, 200, 200),
                new Rectangle(1800, 0, 200, 200),
 
                new Rectangle(0, 200, 200, 200),
                new Rectangle(200, 200, 200, 200),
                new Rectangle(400, 200, 200, 200),
                new Rectangle(600, 200, 200, 200),
                new Rectangle(800, 200, 200, 200),
                new Rectangle(1000, 200, 200, 200),
                new Rectangle(1200, 200, 200, 200),
                new Rectangle(1400, 200, 200, 200),
                new Rectangle(1600, 200, 200, 200),
                new Rectangle(1800, 200, 200, 200),

                new Rectangle(0, 400, 200, 200),
                new Rectangle(200, 400, 200, 200),
                new Rectangle(400, 400, 200, 200),
                new Rectangle(600, 400, 200, 200),
                new Rectangle(800, 400, 200, 200),
                new Rectangle(1000, 400, 200, 200),
                new Rectangle(1200, 400, 200, 200),
                new Rectangle(1400, 400, 200, 200),
                new Rectangle(1600, 400, 200, 200),
                new Rectangle(1800, 400, 200, 200),

                new Rectangle(0, 600, 200, 200),

            }
        };

        static readonly FrameAnimation ParalyzeAttackFrames = new FrameAnimation
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
                new Rectangle(1400, 0, 200, 200),
                new Rectangle(1600, 0, 200, 200),
                new Rectangle(1800, 0, 200, 200),
 
                new Rectangle(0, 200, 200, 200),
                new Rectangle(200, 200, 200, 200),
                new Rectangle(400, 200, 200, 200),
                new Rectangle(600, 200, 200, 200),
                new Rectangle(800, 200, 200, 200),
                new Rectangle(1000, 200, 200, 200)

            }
        };

        static readonly FrameAnimation DashFrames = new FrameAnimation
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
                new Rectangle(1400, 0, 200, 200),
                new Rectangle(1600, 0, 200, 200),
                new Rectangle(1800, 0, 200, 200),
 
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
                new Rectangle(600, 0, 200, 200),
                new Rectangle(800, 0, 200, 200),
                new Rectangle(1000, 0, 200, 200),
                new Rectangle(1200, 0, 200, 200),
                new Rectangle(1400, 0, 200, 200),
                new Rectangle(1600, 0, 200, 200)
            }
        };

        #endregion

        #region Constructor

        public BoggimusTadpole()
        {
            Name = "Boggimus Tadpole";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BehaviorType = AIType.Knowledgeable;
            BaseAttackName = "BoggimusTadpoleAttack";
            HitNoiseSoundCue = AudioCues.Flesh;
            OnHitSoundCue = AudioCues.BoggimusHit;
            OnDeathSoundCue = AudioCues.BoggimusDeath;
          
            FighterStats = new FighterStats
            {
                BaseMaxHealth = 4000,
                Health = 4000,
                BaseMaxEnergy = 500,
                Energy = 500,
                BaseMaxStamina = 10,
                Stamina = 7,
                BaseStrength = 85,
                BaseDefense = 112,
                BaseIntelligence = 80,
                BaseWillpower = 90,
                BaseAgility = 110,
                Level = 17,
                Experience = 1450,
                Gold = 60,
            };

            //offsets
            defenseShieldOffset = new Vector2(0, 0);
            damageMessageOffset = new Vector2(15, 12 - 75);
            statusEffectMessageOffset = new Vector2(-125, -50);
            pointerOffset = new Vector2(-92, 52);
            projectileOriginOffset = new Vector2(40, 35);
            iconOffset = new Vector2(0, 20);

            CombatAnimation DashAnimation = new CombatAnimation()
            {
                Name = "Dash",
                TextureName = "boggimus_tadpole_idle",
                Animation = DashFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation AttackAnimation = new CombatAnimation()
            {
                Name = "Attack",
                TextureName = "boggimus_tadpole_attack",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = new Vector2(-65, -100)
            };

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "boggimus_tadpole_idle",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation ParalyzeAttackAnimation = new CombatAnimation
            {
                Name = "ParalyzeAttack",
                TextureName = "boggimus_tadpole_hypnotize",
                Animation = ParalyzeAttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };


            ItemsDropped = new List<string>()
            {
                "DivineRing",
                "TranslucentRing"
            };

            SkillNames = new List<string>()
            {
                "BoggimusTadpoleAttack",
                "BoggimusTadpoleParalyze",
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
            AddCombatAnimation(ParalyzeAttackAnimation);
        }

        #endregion
    }
}
