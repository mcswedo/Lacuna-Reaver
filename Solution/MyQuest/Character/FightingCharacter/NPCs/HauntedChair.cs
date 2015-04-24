using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class HauntedChair : NPCFightingCharacter
    {
        static readonly Vector2 commonDrawOffset = new Vector2(-60, -89);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.195,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(600, 0, 200, 200),
                new Rectangle(800, 0, 200, 200),
                new Rectangle(1000, 0, 200, 200),
                new Rectangle(1200, 0, 200, 200),
                new Rectangle(1400, 0, 200, 200),
                new Rectangle(1600, 0, 200, 200),
                new Rectangle(1800, 0, 200, 200),
                new Rectangle(0, 200, 200, 200),
                new Rectangle(200, 200, 200, 200),
                new Rectangle(400, 200, 200, 200),
            }
        };

        static readonly FrameAnimation DashFrames = new FrameAnimation
        {
            FrameDelay = 0.75,
            Frames = new List<Rectangle>
            {
               new Rectangle(0, 0, 200, 200),
            }
        };

        static readonly FrameAnimation AttackFrames = new FrameAnimation
        {
            FrameDelay = 0.15,
            Frames = new List<Rectangle>
            {
               new Rectangle(0, 0, 200, 200),
            }
        };

        public HauntedChair()
        {
            Name = "Haunted Chair";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "PossessedChairAttack";
            HitNoiseSoundCue = AudioCues.Thunk;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.MonsterHit;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 1200, //1400
                Health = 1200,
                BaseMaxEnergy = 300,
                Energy = 300,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 40, //90
                BaseDefense = 100,
                BaseIntelligence = 40,
                BaseWillpower = 60,
                BaseAgility = 78,
                Level = 10,
                Experience = 350,//26000,
                Gold = 30,
            };

            //offsets
            damageMessageOffset = new Vector2(30, -80);
            defenseShieldOffset = new Vector2(-70, 0);
            statusEffectMessageOffset = new Vector2(-100, -15);
            pointerOffset = new Vector2(-87, 53);

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "hauntedchair_idle",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation DashAnimation = new CombatAnimation()
            {
                Name = "Dash",
                TextureName = "hauntedchair_idle",
                Animation = DashFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation AttackAnimation = new CombatAnimation()
            {
                Name = "Attack",
                TextureName = "hauntedchair_idle",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            ItemsDropped = new List<string>()
            {
                "JadeStatue",
                "RubyRing",
                "ObsidianCharm"
            };

            SkillNames = new List<string>()
            {
                "PossessedChairAttack",
                "PossessedChairPowerStrike", //never seems to call this. 11/2
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
        }
    }
}
