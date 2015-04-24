using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class Tohmey : NPCFightingCharacter
    {

        #region Static FrameAnimations

        static readonly Vector2 commonDrawOffset = new Vector2(-70, -125);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.2,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 139, 250),
                new Rectangle(139, 0, 139, 250),
                new Rectangle(278, 0, 139, 250),
                new Rectangle(417, 0, 139, 250),
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
            FrameDelay = 0.08,
            Frames = new List<Rectangle>
            {
                new Rectangle(0,     0, 139, 250),
                new Rectangle(139,   0, 139, 250),
                new Rectangle(278,   0, 139, 250),
                new Rectangle(417,   0, 139, 250),
                new Rectangle(556,   0, 139, 250),
                new Rectangle(695,   0, 139, 250),
                new Rectangle(834,   0, 139, 250),
                new Rectangle(973,   0, 139, 250),
                new Rectangle(1112,  0, 139, 250),
                new Rectangle(1251,  0, 139, 250),
                new Rectangle(1390,  0, 139, 250),
                new Rectangle(1529,  0, 139, 250),
                new Rectangle(1668,  0, 139, 250),
                new Rectangle(1807,  0, 139, 250),

                new Rectangle(0,    250, 139, 250),
                new Rectangle(139,  250, 139, 250),
                new Rectangle(278,  250, 139, 250)
            }
        };

        static readonly FrameAnimation JumpFrames = new FrameAnimation
        {
            FrameDelay = 0.15,
            Frames = new List<Rectangle>
            {
                new Rectangle(0,     0, 139, 500),
                new Rectangle(139,   0, 139, 500),
                new Rectangle(278,   0, 139, 500),
                new Rectangle(417,   0, 139, 500),
                new Rectangle(556,   0, 139, 500),
                new Rectangle(695,   0, 139, 500),
                new Rectangle(834,   0, 139, 500),
                new Rectangle(973,   0, 139, 500),
                new Rectangle(1112,  0, 139, 500)
            }
        };
        static readonly FrameAnimation HeadlessFrames = new FrameAnimation
        {
            FrameDelay = 0.15,
            Frames = new List<Rectangle>
            {
                new Rectangle(417,  250, 139, 250),
            }
        };

        #endregion

        #region Constructor

        public Tohmey()
        {
            Name = "Toh-mey";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "TohMeyAttack";
            BehaviorType = AIType.Simple;
            HitNoiseSoundCue = AudioCues.Flesh;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.MonsterHit;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 1600,
                Health = 1600,
                BaseMaxEnergy = 400,
                Energy = 400,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 85,
                BaseDefense = 95,
                BaseIntelligence = 70,
                BaseWillpower = 85,
                BaseAgility = 80,
                Level = 10,
                Experience = 475,//400,
                Gold = 100,
            };

            //offsets
            damageMessageOffset = new Vector2(0, -105);
            defenseShieldOffset = new Vector2(-80, 0);
            statusEffectMessageOffset = new Vector2(-110, -15);
            pointerOffset = new Vector2(-130, 85);
            hitLocationOffset = new Vector2(-20, 0);
            projectileOriginOffset = new Vector2(0, -50);

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "tohmey_idle",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation DashAnimation = new CombatAnimation()
            {
                Name = "Dash",
                TextureName = "tohmey_idle",
                Animation = IdleFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation AttackAnimation = new CombatAnimation()
            {
                Name = "Attack",
                TextureName = "tohmey_attack",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation JumpAnimation = new CombatAnimation()
            {
                Name = "Jump",
                TextureName = "tohmey_jump",
                Animation = JumpFrames,
                Loop = false,
                DrawOffset = new Vector2(-70, -380)
            };

            CombatAnimation HeadlessAnimation = new CombatAnimation()
            {
                Name = "Headless",
                TextureName = "tohmey_attack",
                Animation = HeadlessFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            ItemsDropped = new List<string>()
            {
            };

            SkillNames = new List<string>()
            {
                "TohMeyAttack",
                "TohMeyPowerStrike", 
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
            AddCombatAnimation(HeadlessAnimation);
            AddCombatAnimation(JumpAnimation);
        }

        #endregion
    }
}
