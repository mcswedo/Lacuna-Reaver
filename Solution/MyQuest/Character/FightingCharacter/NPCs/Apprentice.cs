using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class Apprentice : NPCFightingCharacter
    {
        #region Static FrameAnimations

        static readonly Vector2 commonDrawOffset = new Vector2(-100, -90);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.08,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 350, 230),
                new Rectangle(350, 0, 350, 230),
                new Rectangle(700, 0, 350, 230),
                new Rectangle(1050, 0, 350, 230),
                new Rectangle(1400, 0, 350, 230),

                new Rectangle(0, 230, 350, 230),
                new Rectangle(350, 230, 350, 230),
                new Rectangle(700, 230, 350, 230),
                new Rectangle(1050, 230, 350, 230),
                new Rectangle(1400, 230, 350, 230),

                new Rectangle(0, 460, 350, 230),
            }
        };

        static readonly FrameAnimation AttackFrames = new FrameAnimation
        {
            FrameDelay = 0.15,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 350, 230)
            }
        };

        #endregion

        #region Constructor

        public Apprentice()
        {
            Name = "Apprentice";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "ApprenticeFireBall";
            HitNoiseSoundCue = AudioCues.Flesh;
            OnHitSoundCue = AudioCues.ApprenticeHit;
            OnDeathSoundCue = AudioCues.ApprenticeDeath;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 2250,//1800,
                Health = 2250,
                BaseMaxEnergy = 350,
                Energy = 350,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 117,
                BaseDefense = 112,
                BaseIntelligence = 75,
                BaseWillpower = 70,
                BaseAgility = 83,
                Level = 10,
                Experience = 575,//350,
                Gold = 43,
            };

            //offsets
            damageMessageOffset = new Vector2(50, -100);
            defenseShieldOffset = new Vector2(-100, 0);
            statusEffectMessageOffset = new Vector2(-125, -30);
            pointerOffset = new Vector2(-42, 122);
            projectileOriginOffset = new Vector2(0, -50);
            hitLocationOffset = new Vector2(35, 15);
            iconOffset = new Vector2(55, 35);

            CombatAnimation AttackAnimation = new CombatAnimation()
            {
                Name = "Attack",
                TextureName = "apprentice_attack",
                Animation = AttackFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };


            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "apprentice_idle",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };


            ItemsDropped = new List<string>()
            {
                "LapizLazuliRing",
                "PearlBand",
                "DivineRing",
                "ShadowStrikeRing",
                "TranslucentRing"
            };

            SkillNames = new List<string>()
            {
                "ApprenticePoison",
                "ApprenticeFireBall",
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(AttackAnimation);
        }

        #endregion
    }
}
