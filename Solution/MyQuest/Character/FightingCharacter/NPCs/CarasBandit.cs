using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace MyQuest
{
    class CarasBandit : NPCFightingCharacter
    {
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
            //FrameDelay = 0.0625,
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

        public CarasBandit()
        {
            Name = "Angered Thief";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "BanditAttack";
            HitNoiseSoundCue = AudioCues.Flesh;
            OnHitSoundCue = AudioCues.MaxHit;
            OnDeathSoundCue = AudioCues.MaxDeath;
            if (Cara.Instance.FighterStats.Level <= 2)
            {
                FighterStats = new FighterStats
                {
                    BaseMaxHealth = 1000,
                    Health = 1000,
                    BaseMaxEnergy = 50,
                    Energy = 50,
                    BaseMaxStamina = 10,
                    Stamina = 10,
                    BaseStrength = 30,
                    BaseDefense = 25,
                    BaseIntelligence = 14,
                    BaseWillpower = 35,
                    BaseAgility = 10,
                    Level = 2,
                    Experience = 200,
                    Gold = 45,
                };
            } 
            else if (Cara.Instance.FighterStats.Level == 3)
            {
                FighterStats = new FighterStats
                {
                    BaseMaxHealth = 1250,
                    Health = 1250,
                    BaseMaxEnergy = 50,
                    Energy = 50,
                    BaseMaxStamina = 10,
                    Stamina = 10,
                    BaseStrength = 37,
                    BaseDefense = 12,
                    BaseIntelligence = 14,
                    BaseWillpower = 43,
                    BaseAgility = 10,
                    Level = 3,
                    Experience = 200,
                    Gold = 45,
                };
            } 
            else 
            {
                Debug.Assert(Cara.Instance.FighterStats.Level >= 4);
                FighterStats = new FighterStats
                {
                    BaseMaxHealth = 2000,
                    Health = 2000,
                    BaseMaxEnergy = 50,
                    Energy = 50,
                    BaseMaxStamina = 10,
                    Stamina = 10,
                    BaseStrength = 45,
                    BaseDefense = 25,
                    BaseIntelligence = 14,
                    BaseWillpower = 50,
                    BaseAgility = 10,
                    Level = 5,
                    Experience = 200,
                    Gold = 45,
                };
            }

            damageMessageOffset = new Vector2(0, -80);
            defenseShieldOffset = new Vector2(0, 0);
            statusEffectMessageOffset = new Vector2(-125, -50);
            pointerOffset = new Vector2(-100, 87);
            iconOffset = new Vector2(15, 0);

            CombatAnimation AttackAnimation = new CombatAnimation()
            {
                Name = "Attack",
                TextureName = "bandit_attack",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = new Vector2(-70, -285)

            };

            CombatAnimation DashAnimation = new CombatAnimation()
            {
                Name = "Dash",
                TextureName = "bandit_disappear",
                Animation = DashFrames,
                Loop = false,
                DrawOffset = new Vector2(-70, -285)

            };

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "desert_bandit_idle",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            ItemsDropped = new List<string>()
            {
                "LargeEnergyPotion",
                "LargeHealthPotion",
                "ChainOfSkies"
            };

            SkillNames = new List<string>()
            {
                "BanditAttack",
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
        }
    }
}
