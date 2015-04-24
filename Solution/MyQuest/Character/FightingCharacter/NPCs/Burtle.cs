using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace MyQuest
{
    public class Burtle : NPCFightingCharacter
    {
        #region Static FrameAnimations

        static readonly Vector2 commonDrawOffset = new Vector2(-250, -285);
        //static readonly Vector2 commonDrawOffset = new Vector2(-340, -295); 

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.160,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 500, 400),
                new Rectangle(500, 0, 500, 400),
                new Rectangle(1000, 0, 500, 400),
                new Rectangle(1500, 0, 500, 400),
                new Rectangle(0, 400, 500, 400),
                new Rectangle(1000, 0, 500, 400),
                new Rectangle(500, 0, 500, 400)
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

        #endregion

        #region Constructor


        public Burtle()
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

            if (Nathan.Instance.FighterStats.Level <= 5 && Cara.Instance.FighterStats.Level <= 5)
            {
                FighterStats = new FighterStats
                {
                    BaseMaxHealth = 4000,
                    Health = 4000,
                    BaseMaxEnergy = 250,
                    Energy = 250,
                    BaseMaxStamina = 10,
                    Stamina = 10,
                    BaseStrength = 85,
                    BaseDefense = 70,//70,
                    BaseIntelligence = 55,
                    BaseWillpower = 35,
                    BaseAgility = 55,
                    Level = 5,//8,
                    Experience = 2000,//10000,
                    Gold = 200,
                };
            }
            else if (Cara.Instance.FighterStats.Level <= 5)
            {
                FighterStats = new FighterStats
                {
                    BaseMaxHealth = 4500,
                    Health = 4500,
                    BaseMaxEnergy = 250,
                    Energy = 250,
                    BaseMaxStamina = 10,
                    Stamina = 10,
                    BaseStrength = 89,
                    BaseDefense = 100,//70,
                    BaseIntelligence = 55,
                    BaseWillpower = 35,
                    BaseAgility = 55,
                    Level = 7,
                    Experience = 2000,//10000,
                    Gold = 200,
                };
            }
            else
            {
                FighterStats = new FighterStats
                {
                    BaseMaxHealth = 5500,
                    Health = 5500,
                    BaseMaxEnergy = 250,
                    Energy = 250,
                    BaseMaxStamina = 10,
                    Stamina = 10,
                    BaseStrength = 100,
                    BaseDefense = 110,//70,
                    BaseIntelligence = 55,
                    BaseWillpower = 45,
                    BaseAgility = 55,
                    Level = 8,
                    Experience = 2000,
                    Gold = 200,
                };
            }

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
                TextureName = "burtle_attacks",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };


            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "burtle_idle",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation RainAnimation = new CombatAnimation()
            {
                Name = "SpikeRain",
                TextureName = "burtle_rain",
                Animation = RainFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            ItemsDropped = new List<string>()
            {
                "MediumEnergyPotion",
                "MediumHealthPotion",
                "PearlBand",
                "LapizLazuliRing"
            };

            SkillNames = new List<string>()
            {
                "BurtleAttack",
                "BurtleShield",
                "SpikeRain",
                "Rage"
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
            AddCombatAnimation(RainAnimation);
        }


        #endregion
    }
}
