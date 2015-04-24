using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class SewnBogHauntedTree : NPCFightingCharacter
    {
        #region Static FrameAnimations

        static readonly Vector2 commonDrawOffset = new Vector2(-85, -85);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.195,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 252, 195),
                new Rectangle(252, 0, 252, 195),
                new Rectangle(504, 0, 252, 195),
                new Rectangle(756, 0, 252, 195),

                new Rectangle(0, 195, 252, 195),
                new Rectangle(252, 195, 252, 195),
                new Rectangle(504, 195, 252, 195),
                new Rectangle(756, 195, 252, 195),

            }
        };

        static readonly FrameAnimation WitherFrames = new FrameAnimation
        {
            FrameDelay = 0.195,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 252, 195),
                new Rectangle(252, 0, 252, 195),
                new Rectangle(504, 0, 252, 195),
                new Rectangle(756, 0, 252, 195),

                new Rectangle(0, 195, 252, 195),
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
                new Rectangle(0, 0, 252, 195),
                new Rectangle(252, 0, 252, 195),
                new Rectangle(504, 0, 252, 195),
                new Rectangle(756, 0, 252, 195),

                new Rectangle(0, 195, 252, 195),
                new Rectangle(252, 195, 252, 195),
                new Rectangle(504, 195, 252, 195),
                new Rectangle(756, 195, 252, 195),

            }
        };

        static readonly FrameAnimation HauntFrames = new FrameAnimation
        {
            FrameDelay = 0.15,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 252, 195),
                new Rectangle(252, 0, 252, 195),
                new Rectangle(504, 0, 252, 195),
                new Rectangle(756, 0, 252, 195),

                new Rectangle(0, 195, 252, 195),
                new Rectangle(252, 195, 252, 195),
                new Rectangle(504, 195, 252, 195),
                new Rectangle(756, 195, 252, 195),

                new Rectangle(0,390, 252, 195),
                new Rectangle(756, 195, 252, 195),
                new Rectangle(0,390, 252, 195),
                new Rectangle(756, 195, 252, 195),
                new Rectangle(0,390, 252, 195),
                new Rectangle(756, 195, 252, 195),
            }
        };

        #endregion

        #region Constructor

        public SewnBogHauntedTree()
        {
            Name = "Swamp Tree";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "HauntedTreeAttack";
            BehaviorType = AIType.Knowledgeable;
            HitNoiseSoundCue = AudioCues.Thunk;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.MonsterHit;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 5000,
                Health = 5000,
                BaseMaxEnergy = 500,
                Energy = 500,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 95,
                BaseDefense = 130,
                BaseIntelligence = 100,
                BaseWillpower = 80,
                BaseAgility = 70,
                Level = 17,
                Experience = 1650,
                Gold = 65,
            };

            //offsets
            damageMessageOffset = new Vector2(0, -80);
            defenseShieldOffset = new Vector2(0, 0);
            statusEffectMessageOffset = new Vector2(-125, -50);
            pointerOffset = new Vector2(-60, 98);
            iconOffset = new Vector2(20, 50);

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "swamp_tree_idle",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };


            CombatAnimation HauntAnimation = new CombatAnimation
            {
                Name = "ShadowStrike",
                TextureName = "swamp_tree_haunt",
                Animation = HauntFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation DashAnimation = new CombatAnimation()
            {
                Name = "Dash",
                TextureName = "nathan_stick_animations",
                Animation = DashFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation WitherAnimation = new CombatAnimation()
            {
                //Name = "Wither",
                Name = "Weakness",
                TextureName = "swamp_tree_wither",
                Animation = WitherFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation AttackAnimation = new CombatAnimation()
            {
                Name = "Attack",
                TextureName = "swamp_tree_root",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            ItemsDropped = new List<string>()
            {
                "TranslucentRing",
                "SkirmishRing",
                "ShadowStrikeRing",
                "DivineRing",
                "SpikedChain"
            };

            SkillNames = new List<string>()
            {
                 "HauntedTreeAttack",
                 "HauntedTreeShadowStrike",
                 "HauntedTreeWeakness",
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
            AddCombatAnimation(HauntAnimation);
            AddCombatAnimation(WitherAnimation);
        }

        #endregion
    }
}