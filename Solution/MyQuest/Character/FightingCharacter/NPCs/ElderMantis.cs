using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class ElderMantis : NPCFightingCharacter
    {
        static readonly Vector2 commonDrawOffset = new Vector2(-100, -190);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.1,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 300, 360),
                new Rectangle(300, 0, 300, 360),
                new Rectangle(600, 0, 300, 360),
                new Rectangle(900, 0, 300, 360),
                new Rectangle(1200, 0, 300, 360),

                new Rectangle(0, 360, 300, 360),
                new Rectangle(300, 360, 300, 360),
                new Rectangle(600, 360, 300, 360),
                new Rectangle(900, 360, 300, 360),
                new Rectangle(1200, 360, 300, 360),

                new Rectangle(0, 720, 300, 360),
                new Rectangle(300, 720, 300, 360),
                new Rectangle(600, 720, 300, 360),
                new Rectangle(900, 720, 300, 360),
                new Rectangle(1200, 720, 300, 360),
                
            }
        };

        static readonly FrameAnimation GazeAttackFrames = new FrameAnimation
        {
            FrameDelay = 0.1,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 300, 360),
                new Rectangle(300, 0, 300, 360),
                new Rectangle(600, 0, 300, 360),
                new Rectangle(900, 0, 300, 360),
                new Rectangle(1200, 0, 300, 360),

                new Rectangle(0, 360, 300, 360),
                new Rectangle(300, 360, 300, 360),
                new Rectangle(600, 360, 300, 360),
                new Rectangle(900, 360, 300, 360)               
            }
        };

        static readonly FrameAnimation RevelationAttackFrames = new FrameAnimation
        {
            FrameDelay = 0.1,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 300, 360),
                new Rectangle(300, 0, 300, 360),
                new Rectangle(600, 0, 300, 360),
                new Rectangle(900, 0, 300, 360),
                new Rectangle(1200, 0, 300, 360),

                new Rectangle(0, 360, 300, 360),
                new Rectangle(300, 360, 300, 360),
                new Rectangle(600, 360, 300, 360),
                new Rectangle(900, 360, 300, 360),
                new Rectangle(1200, 360, 300, 360),

                new Rectangle(0, 720, 300, 360),
                new Rectangle(300, 720, 300, 360),
                new Rectangle(600, 720, 300, 360),
                new Rectangle(900, 720, 300, 360),
                new Rectangle(1200, 720, 300, 360),
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
                new Rectangle(0, 0, 300, 360),
            }
        };

        public ElderMantis()
        {
            Name = "Elder Mantis";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "ElderMantisFireBall";
            HitNoiseSoundCue = AudioCues.Flesh;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.MonsterHit;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 8750, //7500
                Health = 8750,
                BaseMaxEnergy = 350,
                Energy = 350,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 124,
                BaseDefense = 159,
                BaseIntelligence = 136,
                BaseWillpower = 83,
                BaseAgility = 87,
                Level = 13,
                Experience = 12500,
                Gold = 414,
            };

            damageMessageOffset = new Vector2(-22, -127);
            defenseShieldOffset = new Vector2(0, 0);
            statusEffectMessageOffset = new Vector2(-125, -50);
            projectileOriginOffset = new Vector2(-100,-50);
            pointerOffset = new Vector2(-90, 155);
            hitLocationOffset = new Vector2(-50, 0);
            iconOffset = new Vector2(20, 0);

            ItemsDropped = new List<string>()
            {
                "LargeEnergyPotion",
                "LargeHealthPotion",
                "LapizLazuliRing",
                "PearlBand",
                "DivineRing",
                "ShadowStrikeRing",
                "TranslucentRing"
            };

            SkillNames = new List<string>()
            {
                "ElderMantisFireBall",
                "GazeOfDespair",
                "UnholyRevelations",
            };

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "elder_mantis_idle",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation GazeAttackAnimation = new CombatAnimation
            {
                Name = "GazeAttack",
                TextureName = "elder_mantis_gaze",
                Animation = GazeAttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation RevelationAttackAnimation = new CombatAnimation
            {
                Name = "RevelationAttack",
                TextureName = "elder_mantis_revelation",
                Animation = RevelationAttackFrames,
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

            CombatAnimation AttackAnimation = new CombatAnimation()
            {
                Name = "Attack",
                TextureName = "elder_mantis_attack",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
            AddCombatAnimation(GazeAttackAnimation);
            AddCombatAnimation(RevelationAttackAnimation);
        }
    }
}
