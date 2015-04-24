using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class AbominableSnowMan : NPCFightingCharacter
    {
        static readonly Vector2 commonDrawOffset = new Vector2(-110, -125);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.250,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(600, 0, 200, 200),
                new Rectangle(800, 0, 200, 200),

                new Rectangle(0, 200, 200, 200),
                new Rectangle(200, 200, 200, 200),
                new Rectangle(396, 200, 200, 200),
                new Rectangle(606, 200, 200, 200),
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
                new Rectangle(0, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(600, 0, 200, 200),
                new Rectangle(800, 0, 200, 200),

                new Rectangle(0, 200, 200, 200),
                new Rectangle(200, 200, 200, 200),
                new Rectangle(396, 200, 200, 200),
                new Rectangle(606, 200, 200, 200),
            }
        };

        public AbominableSnowMan()
        {
            Name = "Abominable Snow Man";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "AbominableSnowManAttack";
            BehaviorType = AIType.Knowledgeable;
            HitNoiseSoundCue = AudioCues.Thunk;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.MonsterHit;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 7300,
                Health = 7300,
                BaseMaxEnergy = 250,
                Energy = 250,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 156,
                BaseDefense = 152,
                BaseIntelligence = 65,
                BaseWillpower = 125,
                BaseAgility = 165,
                Level = 17,
                Experience = 2300,
                Gold = 72,
            };

            //offsets
            damageMessageOffset = new Vector2(70, -100);
            defenseShieldOffset = new Vector2(0, 0);
            statusEffectMessageOffset = new Vector2(-125, -50);
            pointerOffset = new Vector2(-138, 50);
            hitLocationOffset = new Vector2(-60,0);

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "abominable_snow_man_idle",
                Animation = IdleFrames,
                Loop = true,
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
                TextureName = "abominable_snow_man_attack",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            ItemsDropped = new List<string>()
            {
                "ChainOfFlame",
                "EarthenGloves",
                "NatureCharm"
            };

            SkillNames = new List<string>()
            {           
               "AbominableSnowManAttack"    
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);

        }
    }
}
