using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class HauntedTree : NPCFightingCharacter
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

        public HauntedTree()
        {
            Name = "Haunted Tree";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "HauntedTreeAttack";
            BehaviorType = AIType.Simple;
            HitNoiseSoundCue = AudioCues.Thunk;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.MonsterHit;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 650,
                Health = 650,
                BaseMaxEnergy = 50,
                Energy = 50,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength =  70,  //40,
                BaseDefense = 110,  //15,
                BaseIntelligence = 8,
                BaseWillpower = 125,
                BaseAgility = 8,
                Level = 1,
                Experience = 90,
                Gold = 25,
            };

            //ModifiedStats = new FighterStats();

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

            CombatAnimation WeaknessAnimation = new CombatAnimation()
            {
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
                "LapizLazuliRing",
                "PearlBand",
                "JadeStatue",
                "ObsidianCharm",
                "RubyRing"
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
            AddCombatAnimation(WeaknessAnimation);

        }

        #endregion
    }
}