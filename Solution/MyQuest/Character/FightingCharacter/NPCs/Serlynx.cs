using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class Serlynx : NPCFightingCharacter
    {
        #region Static FrameAnimations

        static readonly Vector2 commonDrawOffset = new Vector2(-256, -300);
        static readonly Vector2 attackDrawOffset = new Vector2(-560, -560);
       
        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.065,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 512, 512),
                new Rectangle(512, 0, 512, 512),
                new Rectangle(1024, 0, 512, 512),
                new Rectangle(1536, 0, 512, 512),

                new Rectangle(0, 512, 512, 512),
                new Rectangle(512, 512, 512, 512),
                new Rectangle(1024, 512, 512, 512),
                new Rectangle(1536, 512, 512, 512),

                new Rectangle(0, 1024, 512, 512),
                new Rectangle(512, 1024, 512, 512),
                new Rectangle(1024, 1024, 512, 512),
                new Rectangle(1536, 1024, 512, 512),

                new Rectangle(0, 1536, 512, 512),

                new Rectangle(1536, 1024, 512, 512),
                new Rectangle(1024, 1024, 512, 512),
                new Rectangle(512, 1024, 512, 512),
                new Rectangle(0, 1024, 512, 512),

                new Rectangle(1536, 512, 512, 512),
                new Rectangle(1024, 512, 512, 512),
                new Rectangle(512, 512, 512, 512),
                new Rectangle(0, 512, 512, 512),

                new Rectangle(1536, 0, 512, 512),
                new Rectangle(1024, 0, 512, 512),
                new Rectangle(512, 0, 512, 512),
                new Rectangle(0, 0, 512, 512)

            }
        };

        static readonly FrameAnimation OpenFrames = new FrameAnimation
       {
           FrameDelay = 0.085,
           Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 512, 512),
                new Rectangle(512, 0, 512, 512),
                new Rectangle(1024, 0, 512, 512),
                new Rectangle(1536, 0, 512, 512),

                new Rectangle(0, 512, 512, 512),

                new Rectangle(1536, 0, 512, 512),
                new Rectangle(1024, 0, 512, 512),
                new Rectangle(512, 0, 512, 512),
                new Rectangle(0, 0, 512, 512)
            }
       };
        static readonly FrameAnimation ChargeFrames = new FrameAnimation
        {
            FrameDelay = 0.065,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 512, 512),
                new Rectangle(512, 0, 512, 512),
                new Rectangle(1024, 0, 512, 512),
                new Rectangle(1536, 0, 512, 512),

                new Rectangle(0, 512, 512, 512),
                new Rectangle(512, 512, 512, 512),
                new Rectangle(1024, 512, 512, 512),

                new Rectangle(512, 512, 512, 512),
                new Rectangle(0, 512, 512, 512),

                new Rectangle(1536, 0, 512, 512),
                new Rectangle(1024, 0, 512, 512),
                new Rectangle(512, 0, 512, 512),
                new Rectangle(0, 0, 512, 512)
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
            FrameDelay = 0.055,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 950, 950),
                new Rectangle(950, 0, 950, 950),
                new Rectangle(0, 950, 950, 950),
                new Rectangle(950, 950, 950, 950),
      
            }
        };

        static readonly FrameAnimation AttackReturnFrames = new FrameAnimation
        {  
            FrameDelay = 0.055,
            Frames = new List<Rectangle>
            {
               new Rectangle(950, 950, 950, 950),
               new Rectangle(0, 950, 950, 950),
               new Rectangle(950, 0, 950, 950),
               new Rectangle(0, 0, 950, 950),
            }
        };


        #endregion

        #region Constructor

        public Serlynx()
        {
            Name = "Serlynx";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "SerlynxAttack";
            BehaviorType = AIType.Boss;
            AIController = new SerlynxCombatAIController();
            HitNoiseSoundCue = AudioCues.Flesh;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.MonsterHit;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 35000,
                Health = 35000,
                BaseMaxEnergy = 2500,
                Energy = 2500,
                BaseMaxStamina = 15, //10
                Stamina = 15, //10
                BaseStrength = 200,
                BaseDefense = 290,
                BaseIntelligence = 215,
                BaseWillpower = 170,
                BaseAgility = 205,
                Level = 21,
                Experience = 45000,
                Gold = 1200,
            };

            //offsets
            damageMessageOffset = new Vector2(70, -100);
            defenseShieldOffset = new Vector2(-64, -128);
            statusEffectMessageOffset = new Vector2(-125, -50);
            pointerOffset = new Vector2(-80, 200);
            projectileOriginOffset = new Vector2(-220, -175);
            ItemsDropped = new List<string>()
            {
                "HugeEnergyPotion",
                "HugeHealthPotion"
            };

            SkillNames = new List<string>()
            {
                "SerlynxAttack",
                "Immolate",
                "Miasma",
            };

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
                TextureName = "serlynx_attack",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = attackDrawOffset
            };

            CombatAnimation AttackAnimation2 = new CombatAnimation()
            {
                Name = "Attack2",
                TextureName = "serlynx_attack2",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = attackDrawOffset
            };

            CombatAnimation AttackAnimation3 = new CombatAnimation()
            {
                Name = "Attack3",
                TextureName = "serlynx_attack3",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = attackDrawOffset
            };

            CombatAnimation AttackReturnAnimation = new CombatAnimation()
            {
                Name = "AttackReturn",
                TextureName = "serlynx_return",
                Animation = AttackReturnFrames,
                Loop = false,
                DrawOffset = attackDrawOffset
            };

            CombatAnimation AttackReturnAnimation2 = new CombatAnimation()
            {
                Name = "AttackReturn2",
                TextureName = "serlynx_attack",
                Animation = AttackReturnFrames,
                Loop = false,
                DrawOffset = attackDrawOffset
            };

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "serlynx_idle",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation OpenAnimation = new CombatAnimation
            {
                Name = "Open",
                TextureName = "serlynx_open",
                Animation = OpenFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation ChargeAnimation = new CombatAnimation
            {
                Name = "Charge",
                TextureName = "serlynx_charge",
                Animation = ChargeFrames,
                Loop = true,
                DrawOffset =  new Vector2(-256, -280)
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
            AddCombatAnimation(AttackAnimation2);
            AddCombatAnimation(AttackAnimation3);
            AddCombatAnimation(AttackReturnAnimation);
            AddCombatAnimation(AttackReturnAnimation2);
            AddCombatAnimation(OpenAnimation);
            AddCombatAnimation(ChargeAnimation);
        }
        
        #endregion
    }
}
