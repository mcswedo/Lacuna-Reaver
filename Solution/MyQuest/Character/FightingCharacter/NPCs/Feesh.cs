using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class Feesh : NPCFightingCharacter
    {
        static readonly Vector2 commonDrawOffset = new Vector2(-85, -105);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.075,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(600, 0, 200, 200),
                new Rectangle(800, 0, 200, 200),
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
                new Rectangle(0, 400, 200, 200),
                new Rectangle(200, 400, 200, 200),
                new Rectangle(400, 400, 200, 200),
                new Rectangle(600, 400, 200, 200),
                new Rectangle(800, 400, 200, 200),
                new Rectangle(1000, 400, 200, 200),
            }
        };

        static readonly FrameAnimation ThrowingFrames = new FrameAnimation
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

        static readonly FrameAnimation RetractingFrames = new FrameAnimation
        {
            FrameDelay = 0.15,
            Frames = new List<Rectangle>
            {
                new Rectangle(800, 0, 200, 200),
                new Rectangle(600, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(0, 0, 200, 200),
            }
        };

        static readonly FrameAnimation ThrowPoseFrames = new FrameAnimation
        {
            FrameDelay = 0.75,
            Frames = new List<Rectangle>
            {
                new Rectangle(800, 0, 200, 200),
            }
        };

        static readonly FrameAnimation ChargingFrames = new FrameAnimation()
        {
            FrameDelay = 0.15,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 130, 200, 130),
                new Rectangle(200, 130, 200, 130),
                new Rectangle(400, 130, 200, 130)}
        };

        static readonly FrameAnimation RecoilFrames = new FrameAnimation()
        {
            FrameDelay = 0.15,
            Frames = new List<Rectangle>()
            {
                new Rectangle(600, 130, 200, 130),
                new Rectangle(800, 130, 200, 130)
            }
        };


        public Feesh()
        {
            Name = "Feesh";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "FeeshWaterBlast";
            HitNoiseSoundCue = AudioCues.Flesh;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.MonsterHit;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 200,
                Health = 200,
                BaseMaxEnergy = 25,
                BaseMaxStamina = 10,
                Stamina = 2, //10
                Energy = 25,
                BaseStrength = 17, //20
                BaseDefense = 7,
                BaseIntelligence = 12,
                BaseWillpower = 7,
                BaseAgility = 5,
                Level = 4,
                Experience = 50,
                Gold = 20,
            };

            //offsets
            damageMessageOffset = new Vector2(-20, -70);
            defenseShieldOffset = new Vector2(0, 0);
            statusEffectMessageOffset = new Vector2(-125, -50);
            pointerOffset = new Vector2(-105, 50);
            projectileOriginOffset = new Vector2(-20, 20);

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "feesh",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation ChargeAnimation = new CombatAnimation
            {
                Name = "Charging",
                TextureName = "feesh",
                Animation = IdleFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation RecoilAnimation = new CombatAnimation
            {
                Name = "Recoiling",
                TextureName = "feesh",
                Animation = IdleFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation ThrowingAnimation = new CombatAnimation
            {
                Name = "Throwing",
                TextureName = "feesh_sting",
                Animation = ThrowingFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation RetractingAnimation = new CombatAnimation
            {
                Name = "Retracting",
                TextureName = "feesh_sting",
                Animation = RetractingFrames,
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

            CombatAnimation ThrowPoseAnimation = new CombatAnimation()
            {
                Name = "ThrowPose",
                TextureName = "feesh_sting",
                Animation = ThrowPoseFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation AttackAnimation = new CombatAnimation()
            {
                Name = "Attack",
                TextureName = "nathan_stick_animations",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };


            BehaviorType = AIType.Simple;

            ItemsDropped = new List<string>()
            {
                "SmallHealthPotion",
                "SmallEnergyPotion"
            };

            SkillNames = new List<string>()
            {
                "FeeshAttack",
                "FeeshPoisonStrike",
                "FeeshWaterBlast"
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
            AddCombatAnimation(ChargeAnimation);
            AddCombatAnimation(RecoilAnimation);
            AddCombatAnimation(RetractingAnimation);
            AddCombatAnimation(ThrowingAnimation);
            AddCombatAnimation(ThrowPoseAnimation);
        }
    }
}