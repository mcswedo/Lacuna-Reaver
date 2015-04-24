using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class Ghost : NPCFightingCharacter
    {
        static readonly Vector2 commonDrawOffset = new Vector2(-30, -140);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.195,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 140, 200),
                new Rectangle(140, 0, 140, 200),
                new Rectangle(280, 0, 140, 200),
                new Rectangle(420, 0, 140, 200),
                new Rectangle(560, 0, 140, 200),
                new Rectangle(700, 0, 140, 200),
                new Rectangle(840, 0, 140, 200),
                new Rectangle(980, 0, 140, 200),
                new Rectangle(1120, 0, 140, 200),
                new Rectangle(1260, 0, 140, 200),
                new Rectangle(1400, 0, 140, 200),
                new Rectangle(1540, 0, 140, 200),

            }
        };

        static readonly FrameAnimation DashFrames = new FrameAnimation
        {
            FrameDelay = 0.07,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 140, 200),
                new Rectangle(140, 0, 140, 200),
                new Rectangle(280, 0, 140, 200),
                new Rectangle(420, 0, 140, 200),
                new Rectangle(560, 0, 140, 200),
                new Rectangle(700, 0, 140, 200),
                new Rectangle(840, 0, 140, 200),
                new Rectangle(980, 0, 140, 200),
                new Rectangle(1120, 0, 140, 200),
                new Rectangle(1260, 0, 140, 200),
                new Rectangle(1400, 0, 140, 200),
                new Rectangle(1540, 0, 140, 200),
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
            }
        };

        static readonly FrameAnimation VeilFrames = new FrameAnimation
        {
            FrameDelay = 0.15,
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
            }
        };
        public Ghost()
        {
            Name = "Ghost";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "GhostAttack";
            HitNoiseSoundCue = AudioCues.Swoosh;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.MonsterHit;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 1525,
                Health = 1525,
                BaseMaxEnergy = 350,
                Energy = 350,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 72, //100
                BaseDefense = 94,
                BaseIntelligence = 85,
                BaseWillpower = 75,
                BaseAgility = 120,
                Level = 10,
                Experience = 410,
                Gold = 35,
            };

            //offsets
            damageMessageOffset = new Vector2(0, -150);
            defenseShieldOffset = new Vector2(-80, -30);
            statusEffectMessageOffset = new Vector2(-110, -45);
            pointerOffset = new Vector2(-88, 27);
            iconOffset = new Vector2(20, -20);

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "ghost_idle",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation DashAnimation = new CombatAnimation()
            {
                Name = "Dash",
                TextureName = "ghost_idle",
                Animation = DashFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation AttackAnimation = new CombatAnimation()
            {
                Name = "Attack",
                TextureName = "ghost_basic_attack",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = new Vector2(-30, -140)
            };

            CombatAnimation VeilAnimation = new CombatAnimation()
            {
                Name = "Veil",
                TextureName = "ghost_veil",
                Animation = VeilFrames,
                Loop = false,
                DrawOffset = new Vector2(-90, -140)
            };

            ItemsDropped = new List<string>()
            {
                "JadeStatue",
                "RubyRing",
                "ObsidianCharm",
                "SkirmishRing"
            };

            SkillNames = new List<string>()
            {
                "GhostAttack",
                "GhostInvulnerability",
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
            AddCombatAnimation(VeilAnimation);
        }
    }
}
