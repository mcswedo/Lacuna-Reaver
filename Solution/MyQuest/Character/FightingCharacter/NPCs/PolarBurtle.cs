using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class PolarBurtle : NPCFightingCharacter
    {
        #region Static FrameAnimations

        static readonly Vector2 commonDrawOffset = new Vector2(-250, -285);
        //static readonly Vector2 commonDrawOffset = new Vector2(-340, -295); 

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = .25,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 500, 400),
                new Rectangle(500, 0, 500, 400),
                new Rectangle(998, 0, 500, 400),
                new Rectangle(1505, 0, 500, 400),
                new Rectangle(0, 400, 500, 400),
                new Rectangle(1505, 0, 500, 400),
                new Rectangle(998, 0, 500, 400),
                new Rectangle(500, 0, 500, 400)
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
            FrameDelay = 0.15,
            Frames = new List<Rectangle>
            {
                new Rectangle(0,     0, 500, 410),
                new Rectangle(500,   0, 500, 410),
                new Rectangle(1000,  0, 500, 410),
                new Rectangle(1500,  0, 500, 410),

                new Rectangle(0,     410, 500, 410),
                new Rectangle(500,   410, 500, 410),
                new Rectangle(1000,  410, 500, 410),
                new Rectangle(1500,  410, 500, 410),

                new Rectangle(0,    810, 500, 410),
                new Rectangle(500,  810, 500, 410),
                new Rectangle(1000, 810, 500, 410),
                new Rectangle(1500, 810, 500, 410),

                new Rectangle(0,    1200, 500, 400),
                new Rectangle(500,  1200, 500, 400),
                new Rectangle(1000, 1200, 500, 400)

            }
        };

        static readonly FrameAnimation RainFrames = new FrameAnimation()
        {
            FrameDelay = 0.115,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 500, 400),
                new Rectangle(500, 0, 500, 400),
                new Rectangle(1000, 0, 500, 400),
                new Rectangle(1500, 0, 500, 400),

                new Rectangle(0, 400, 500, 400),
                new Rectangle(500, 400, 500, 400),
                new Rectangle(1000, 400, 500, 400),
                new Rectangle(1500, 400, 500, 400),

                new Rectangle(0, 800, 500, 400),
                new Rectangle(500, 800, 500, 400),
                new Rectangle(1000, 800, 500, 400),
                new Rectangle(1500, 800, 500, 400),

                new Rectangle(0, 1200, 500, 400),
                new Rectangle(500, 1200, 500, 400),
                new Rectangle(1000, 1200, 500, 400),
                new Rectangle(1500, 1200, 500, 400),

                new Rectangle(0, 1600, 500, 400),
            }
        };


        #endregion

        #region Constructor


        public PolarBurtle()
        {
            Name = "Burtle";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "BurtleAttack";
            BehaviorType = AIType.Boss;
            AIController = new BurtleCombatAIController();
            HitNoiseSoundCue = AudioCues.Steel;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.MonsterHit;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 12000,
                Health = 12000,
                BaseMaxEnergy = 1000,
                Energy = 1000,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 160,
                BaseDefense = 185,
                BaseIntelligence = 55,
                BaseWillpower = 90,
                BaseAgility = 90,
                Level = 18,
                Experience = 2500,
                Gold = 84,
            };

            //ModifiedStats = new FighterStats();

            //offsets
            damageMessageOffset = new Vector2(70, -100);
            defenseShieldOffset = new Vector2(0, 0);
            statusEffectMessageOffset = new Vector2(-125, -50);
            pointerOffset = new Vector2(-20, 112);

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
                TextureName = "polar_burtle_attack",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "polar_burtle_idle",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation RainAnimation = new CombatAnimation()
            {
                Name = "SpikeRain",
                TextureName = "polar_burtle_rain",
                Animation = RainFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            ItemsDropped = new List<string>()
            {
                "ChainOfFlame",
                "EarthenGloves",
                "NatureCharm",
                "ChainOfTheSea",
                "ChainOfSkies"
            };

            SkillNames = new List<string>()
            {
                "BurtleAttack",
                "BurtleShield",   
                "SpikeRain",            
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
            AddCombatAnimation(RainAnimation);
        }
        #endregion
    }
}
