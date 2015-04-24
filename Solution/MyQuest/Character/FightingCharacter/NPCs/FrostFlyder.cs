using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class FrostFlyder : NPCFightingCharacter
    {
        static readonly Vector2 commonDrawOffset = new Vector2(-75, -110);

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

                new Rectangle(0, 200, 200, 200),
                new Rectangle(200, 200, 200, 200),
                new Rectangle(400, 200, 200, 200),
                new Rectangle(600, 200, 200, 200),
                new Rectangle(800, 200, 200, 200),

            }
        };


        static readonly FrameAnimation DashFrames = new FrameAnimation
        {
            FrameDelay = 0.1,
            Frames = new List<Rectangle>
            {
                new Rectangle(0,  0, 200, 200),
                new Rectangle(200,  0, 200, 200),
                new Rectangle(400,  0, 200, 200),
                new Rectangle(600,  0, 200, 200),
                new Rectangle(800,  0, 200, 200),
            }
        };

        static readonly FrameAnimation FlapFrames = new FrameAnimation
        {
            FrameDelay = 0.1,
            Frames = new List<Rectangle>
            {
                 new Rectangle(0, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(600, 0, 200, 200),
                new Rectangle(800, 0, 200, 200),

                new Rectangle(0, 200, 200, 200),
                new Rectangle(200, 200, 200, 200),
                new Rectangle(400, 200, 200, 200),
                new Rectangle(600, 200, 200, 200),
                new Rectangle(800, 200, 200, 200),
            }
        };

        static readonly FrameAnimation AttackFrames = new FrameAnimation
        {
            FrameDelay = 0.15,
            Frames = new List<Rectangle>
            {
                new Rectangle(800,  0, 200, 200)
            }
        };

        public FrostFlyder()
        {
            Name = "Frost Flyder";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "FrostFlyderAttack";
            BehaviorType = AIType.Knowledgeable;
            HitNoiseSoundCue = AudioCues.Flesh;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.MonsterHit;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 7000,
                Health = 7000,
                BaseMaxEnergy = 100,
                Energy = 100,
                BaseMaxStamina = 10,
                Stamina = 7,
                BaseStrength = 138,
                BaseDefense = 140,
                BaseIntelligence = 50,
                BaseWillpower = 110,
                BaseAgility = 130,
                Level = 16,
                Experience = 2100,
                Gold = 65,
            };

            //offsets
            damageMessageOffset = new Vector2(0, -80);
            defenseShieldOffset = new Vector2(0, 0);
            statusEffectMessageOffset = new Vector2(-125, -50);
            pointerOffset = new Vector2(-122, 21);

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "frost_flyder_idle",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation DashAnimation = new CombatAnimation()
            {
                Name = "Dash",
                TextureName = "frost_flyder_attack",
                Animation = DashFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation AttackAnimation = new CombatAnimation()
            {
                Name = "Attack",
                TextureName = "frost_flyder_attack",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation FlapAnimation = new CombatAnimation()
            {
                Name = "Flap",
                TextureName = "frost_flyder_flap",
                Animation = FlapFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            ItemsDropped = new List<string>()
            {
                "ChainOfFlame",
                "NatureCharm"
            };

            SkillNames = new List<string>()
            {
                "FrostFlyderAttack",
                "FrostFlyderParalyze",
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
            AddCombatAnimation(FlapAnimation);
        }
    }
}
