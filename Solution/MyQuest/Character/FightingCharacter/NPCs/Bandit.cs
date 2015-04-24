using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class Bandit : NPCFightingCharacter
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

        public Bandit()
        {
            Name = "Bandit";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "BanditAttack";
            HitNoiseSoundCue = AudioCues.Flesh;
            OnHitSoundCue = AudioCues.MaxHit;
            OnDeathSoundCue = AudioCues.MaxDeath;

            if (Party.Singleton.CurrentMap.Name.Equals(Maps.healersVillage) && Nathan.Instance.FighterStats.Level >= 4)
            {
                FighterStats = new FighterStats
                {
                    BaseMaxHealth = 425,
                    Health = 425,
                    BaseMaxEnergy = 50,
                    Energy = 50,
                    BaseMaxStamina = 10,
                    Stamina = 10,
                    BaseStrength = 26,
                    BaseDefense = 37,
                    BaseIntelligence = 14,
                    BaseWillpower = 80,//11,
                    BaseAgility = 17,
                    Level = 7,
                    Experience = 75,
                    Gold = 45,
                };
            }
            else
            {
                FighterStats = new FighterStats
                {
                    BaseMaxHealth = 300,
                    Health = 300,
                    BaseMaxEnergy = 50,
                    Energy = 50,
                    BaseMaxStamina = 10,
                    Stamina = 10,
                    BaseStrength = 20,
                    BaseDefense = 25,
                    BaseIntelligence = 14,
                    BaseWillpower = 80,//11,
                    BaseAgility = 10,
                    Level = 6,
                    Experience = 75,
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
                "RingOfJustice",
                "RingOfBravura",
                "RingOfVeneration",
                "RingOfTheSages",
                "RingOfCalling"
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
