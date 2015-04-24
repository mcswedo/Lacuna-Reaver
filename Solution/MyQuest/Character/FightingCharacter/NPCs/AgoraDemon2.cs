using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class AgoraDemon2 : NPCFightingCharacter
    {
        #region Static FrameAnimations

        static readonly Vector2 commonDrawOffset = new Vector2(-85, -85);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.15,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(600, 0, 200, 200),
                new Rectangle(800, 0, 200, 200),
                new Rectangle(600, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                
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

     


        #endregion

        #region Constructor

        public AgoraDemon2()
        {
            Name = "Demon Gear";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "Demon2Attack";
            BehaviorType = AIType.Simple;
            HitNoiseSoundCue = AudioCues.Flesh;
            OnHitSoundCue = AudioCues.Demon2Hit;
            OnDeathSoundCue = AudioCues.Demon2Death;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 18000,
                Health = 18000,
                BaseMaxEnergy = 5000,
                Energy = 5000,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 185,
                BaseDefense = 250,
                BaseIntelligence = 37,
                BaseWillpower = 150,
                BaseAgility = 42,
                Level = 22,
                Experience = 2530,
                Gold = 95,
            };

            //offsets
            damageMessageOffset = new Vector2(0, -80);
            defenseShieldOffset = new Vector2(0, 0);
            statusEffectMessageOffset = new Vector2(-125, -50);
            pointerOffset = new Vector2(-108, 87);


            CombatAnimation DashAnimation = new CombatAnimation()
            {
                Name = "Dash",
                TextureName = "demon_gear",
                Animation = DashFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation AttackAnimation = new CombatAnimation()
            {
                Name = "Attack",
                TextureName = "demon_gear",
                Animation = IdleFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "demon_gear",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            ItemsDropped = new List<string>()
            {
                "SkirmishRing",
                "DivineRing"
            };

            SkillNames = new List<string>()
            {
                "DemonGearAttack",
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
        }

        #endregion
    }
}
