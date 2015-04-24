using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class CaveDemon1 : NPCFightingCharacter
    {
        #region Static FrameAnimations

        static readonly Vector2 commonDrawOffset = new Vector2(-70, -90);

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
                      
                new Rectangle(0,    200, 200, 200),
                new Rectangle(200,  200, 200, 200),
                new Rectangle(400,  200, 200, 200),
                new Rectangle(600,  200, 200, 200),
                new Rectangle(800,  200, 200, 200),
                new Rectangle(1000, 200, 200, 200),

                new Rectangle(0,   400, 200, 200)
            }
        };

        static readonly FrameAnimation DashFrames = new FrameAnimation
        {
            FrameDelay = 0.5,
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
                new Rectangle(0, 0, 300, 210),
                new Rectangle(300, 0, 300, 210),
                new Rectangle(600, 0, 300, 210),
                new Rectangle(900, 0, 300, 210),

                new Rectangle(0, 210, 300, 210),
                new Rectangle(300, 210, 300, 210),
                new Rectangle(600, 210, 300, 210),
                new Rectangle(900, 210, 300, 210),

                new Rectangle(0, 410, 300, 210),
                new Rectangle(300, 410, 300, 210),
                new Rectangle(600, 410, 300, 210),
                new Rectangle(900, 410, 300, 210)

            }
        };

        #endregion

        #region Constructor

        public CaveDemon1()
        {
            Name = "Wraith";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BehaviorType = AIType.Knowledgeable;
            BaseAttackName = "Demon1Attack";
            HitNoiseSoundCue = AudioCues.Flesh;
            OnHitSoundCue = AudioCues.Demon1Hit;
            OnDeathSoundCue = AudioCues.Demon1Death;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 18000,
                Health = 18000,
                BaseMaxEnergy = 5000,
                Energy = 5000,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 175,
                BaseDefense = 217,
                BaseIntelligence = 167,
                BaseWillpower = 147,
                BaseAgility = 167,
                Level = 23,
                Experience = 2700,
                Gold = 96,
            };

            //offsets
            damageMessageOffset = new Vector2(0, -100);
            defenseShieldOffset = new Vector2(0, 0);
            statusEffectMessageOffset = new Vector2(-125, -50);
            pointerOffset = new Vector2(-103, 100);
            iconOffset = new Vector2(20, 0);

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "demon1_idle",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };
            CombatAnimation DashAnimation = new CombatAnimation()
            {
                Name = "Dash",
                TextureName = "demon1_idle",
                Animation = DashFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation AttackAnimation = new CombatAnimation()
            {
                Name = "Attack",
                TextureName = "demon_basic_attack",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset + new Vector2(-100, -10)
            };

            ItemsDropped = new List<string>()
            {
                "SkirmishRing",
                "SpikedChain"
            };

            SkillNames = new List<string>()
            {
                "Demon1Attack",
                //"Demon2Siphon"
                "Demon1ShadowStrike"
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
        }

        #endregion
    }
}
