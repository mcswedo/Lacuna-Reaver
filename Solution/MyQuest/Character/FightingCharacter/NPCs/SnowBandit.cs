﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class SnowBandit : NPCFightingCharacter
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
            }
        };

        static readonly FrameAnimation AttackFrames = new FrameAnimation
        {
            FrameDelay = .035,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 400),
                new Rectangle(200, 0, 200, 400),
                new Rectangle(400, 0, 200, 400),
                new Rectangle(600, 0, 200, 400),
                new Rectangle(800, 0, 200, 400),
                new Rectangle(1000, 0, 200, 400),
                new Rectangle(1200, 0, 200, 400),
                new Rectangle(1400, 0, 200, 400),
                new Rectangle(1600, 0, 200, 400),
                new Rectangle(1800, 0, 200, 400),
                new Rectangle(0, 400, 200, 400),
                new Rectangle(200, 400, 200, 400),
                new Rectangle(400, 400, 200, 400),
                new Rectangle(600, 400, 200, 400),
                new Rectangle(800, 400, 200, 400),
                new Rectangle(1000, 400, 200, 400),
                new Rectangle(1200, 400, 200, 400),
                new Rectangle(1400, 400, 200, 400),
                new Rectangle(1600, 400, 200, 400),
                new Rectangle(1800, 400, 200, 400),
                new Rectangle(0, 800, 200, 400),
                new Rectangle(200, 800, 200, 400),
                new Rectangle(400, 800, 200, 400),
                new Rectangle(600, 800, 200, 400),
                new Rectangle(800, 800, 200, 400),
                new Rectangle(1000, 800, 200, 400),
                new Rectangle(1200, 800, 200, 400),
                new Rectangle(1400, 800, 200, 400),
                new Rectangle(1600, 800, 200, 400),
                new Rectangle(1800, 800, 200, 400),
                new Rectangle(0, 1200, 200, 400),
                new Rectangle(200, 1200, 200, 400),
            }
        };

        static readonly FrameAnimation DashFrames = new FrameAnimation
        {
            FrameDelay = 0.0625,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 400),
                new Rectangle(200, 0, 200, 400),
                new Rectangle(400, 0, 200, 400),
                new Rectangle(600, 0, 200, 400),
                new Rectangle(800, 0, 200, 400),
                new Rectangle(1000, 0, 200, 400),
                new Rectangle(1200, 0, 200, 400),
                new Rectangle(1400, 0, 200, 400),
                new Rectangle(1600, 0, 200, 400),
                new Rectangle(1800, 0, 200, 400),
                new Rectangle(0, 400, 200, 400),
            }
        };


        #endregion

        #region Constructor

        public SnowBandit()
        {
            Name = "Snow Bandit";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "SnowBanditAttack";
            HitNoiseSoundCue = AudioCues.Flesh;
            OnHitSoundCue = AudioCues.MaxHit;
            OnDeathSoundCue = AudioCues.MaxDeath;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 9000,
                Health = 9000,
                BaseMaxEnergy = 1000,
                Energy = 1000,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 147,
                BaseDefense = 165,
                BaseIntelligence = 116,
                BaseWillpower = 130,
                BaseAgility = 150,
                Level = 17,
                Experience = 2325,
                Gold = 71,
            };

            damageMessageOffset = new Vector2(0, -80);
            defenseShieldOffset = new Vector2(0, 0);
            statusEffectMessageOffset = new Vector2(-125, -50);
            pointerOffset = new Vector2(-100, 87);
            iconOffset = new Vector2(15, 0);

            CombatAnimation AttackAnimation = new CombatAnimation()
            {
                Name = "Attack",
                TextureName = "bandit_snow_attack",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = new Vector2(-70, -285)

            };

            CombatAnimation DashAnimation = new CombatAnimation()
            {
                Name = "Dash",
                TextureName = "bandit_snow_disappear",
                Animation = DashFrames,
                Loop = false,
                DrawOffset = new Vector2(-70, -285)

            };

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "bandit_snow_idle",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            ItemsDropped = new List<string>()
            {

            };

            SkillNames = new List<string>()
            {
                "SnowBanditAttack",
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
        }

        #endregion
    }
}
