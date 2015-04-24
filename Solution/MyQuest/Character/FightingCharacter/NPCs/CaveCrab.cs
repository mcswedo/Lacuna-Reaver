using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class CaveCrab : NPCFightingCharacter
    {
        #region Static FrameAnimations

        Vector2 commonDrawOffset = new Vector2(-100, -110); 
        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.195,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 10, 200, 200),
                new Rectangle(200, 10, 200, 200),
                new Rectangle(400, 10, 200, 200),
                new Rectangle(600, 10, 200, 200),
                new Rectangle(800, 10, 200, 200),
                new Rectangle(1000, 10, 200, 200),

                new Rectangle(0, 210, 200, 200),
                new Rectangle(200, 210, 200, 200),
                new Rectangle(400, 210, 200, 200),
                new Rectangle(600, 210, 200, 200)
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
            FrameDelay = 0.075,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 10, 200, 200),
                new Rectangle(200, 10, 200, 200),
                new Rectangle(400, 10, 200, 200),
                new Rectangle(600, 10, 200, 200),
                new Rectangle(800, 10, 200, 200),
                new Rectangle(1000, 10, 200, 200),

                new Rectangle(0, 210, 200, 200),
                new Rectangle(200, 210, 200, 200),
                new Rectangle(400, 210, 200, 200),
                new Rectangle(600, 210, 200, 200),
                new Rectangle(800, 210, 200, 200),
                new Rectangle(1000, 210, 200, 200),

                new Rectangle(0, 410, 200, 200),
                new Rectangle(200, 410, 200, 200),
                new Rectangle(400, 410, 200, 200),
                new Rectangle(600, 410, 200, 200)
            }
        };

        #endregion

        #region Constructor

        public CaveCrab()
        {
            Name = "Cave Crab";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "CaveCrabAttack";
            BehaviorType = AIType.Knowledgeable;
            HitNoiseSoundCue = AudioCues.Steel;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.MonsterHit;

            pointerOffset = new Vector2(0, 0);

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 15000,
                Health = 15000,
                BaseMaxEnergy = 1000,
                Energy = 1000,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 170,
                BaseDefense = 150,
                BaseIntelligence = 110,
                BaseWillpower = 125,
                BaseAgility = 130,
                Level = 20,
                Experience = 2375,
                Gold = 88,
            };

            //ModifiedStats = new FighterStats(); Do We need this?

            //offsets
            damageMessageOffset = new Vector2(70, -100);
            defenseShieldOffset = new Vector2(0, 0);
            statusEffectMessageOffset = new Vector2(-125, -50);
            pointerOffset = new Vector2(-100, 70);
            projectileOriginOffset = new Vector2(-50, -50);

            CombatAnimation DashAnimation = new CombatAnimation()
            {
                Name = "Dash",
                TextureName = "nathan_stick_animations",
                Animation = DashFrames,
                Loop = false
            };

            CombatAnimation AttackAnimation = new CombatAnimation()
            {
                Name = "Attack",
                TextureName = "cave_crab_attack",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "cave_crab_idle",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };


            ItemsDropped = new List<string>()
            {
                "LargeEnergyPotion",
                "LargeHealthPotion"
            };

            SkillNames = new List<string>()
            {
                "CaveCrabAttack",
                "CaveCrabFlameLance",
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
        }

        #endregion
    }
}
