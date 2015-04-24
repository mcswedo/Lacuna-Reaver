using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class Flyder : NPCFightingCharacter
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
      
        public Flyder()
        {
            Name = "Flyder";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "FlyderAttack";
            BehaviorType = AIType.Simple;
            HitNoiseSoundCue = AudioCues.Flesh;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.MonsterHit;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 150, //100
                Health = 150,
                BaseMaxEnergy = 50,
                Energy = 50,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 10, //12
                BaseDefense = 15,
                BaseIntelligence = 8,
                BaseWillpower = 11,
                BaseAgility = 8,
                Level = 1,
                Experience = 35,
                Gold = 25,
            };

            //offsets
            damageMessageOffset = new Vector2(0, -80);
            defenseShieldOffset = new Vector2(0, 0);
            statusEffectMessageOffset = new Vector2(-125, -50);
            pointerOffset = new Vector2(-122, 21);
            projectileOriginOffset = new Vector2(-100, 00);

            ItemsDropped = new List<string>()
            {
                "SmallEnergyPotion",
                "SmallHealthPotion"
            };

            SkillNames = new List<string>()
            {
                "FlyderAttack",
                "FlyderWhirlwind",
            };
            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "flyder_idle",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation DashAnimation = new CombatAnimation()
            {
                Name = "Dash",
                TextureName = "flyder_attack",
                Animation = DashFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation AttackAnimation = new CombatAnimation()
            {
                Name = "Attack",
                TextureName = "flyder_attack",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation FlapAnimation = new CombatAnimation()
            {
                Name = "Flap",
                TextureName = "flyder_flap",
                Animation = FlapFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
            AddCombatAnimation(FlapAnimation);
        }
    }
}
