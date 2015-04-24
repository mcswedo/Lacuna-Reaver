using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class IceFeesh : NPCFightingCharacter
    {
        #region Static FrameAnimations

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


        #endregion

        #region Constructor


        public IceFeesh()
        {
            Name = "Ice Feesh";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "FrostFeeshAttack";
            HitNoiseSoundCue = AudioCues.Flesh;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.MonsterHit;
            BehaviorType = AIType.Knowledgeable;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 7500,
                Health = 7500,
                BaseMaxEnergy = 10,
                BaseMaxStamina = 10,
                Stamina = 8,
                Energy = 2500,
                BaseStrength = 142,
                BaseDefense = 140,
                BaseIntelligence = 85,
                BaseWillpower = 102,
                BaseAgility = 90,
                Level = 16,
                Experience = 2150,
                Gold = 65,
            };

            //ModifiedStats = new FighterStats();

            //offsets
            damageMessageOffset = new Vector2(50, 30);
            defenseShieldOffset = new Vector2(-125, 0);
            statusEffectMessageOffset = new Vector2(-155, -15);
            pointerOffset = new Vector2(-105, 50);

            //ItemsDropped = new List<string>();
            //ItemsDropped.Add("LargeEnergyPotion");
            //ItemsDropped.Add("LargeHealthPotion");

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "ice_feesh",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation ChargeAnimation = new CombatAnimation
            {
                Name = "Charging",
                TextureName = "old_feesh",
                Animation = ChargingFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation RecoilAnimation = new CombatAnimation
            {
                Name = "Recoiling",
                TextureName = "old_feesh",
                Animation = RecoilFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation ThrowingAnimation = new CombatAnimation
            {
                Name = "Throwing",
                TextureName = "ice_feesh_sting",
                Animation = ThrowingFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation RetractingAnimation = new CombatAnimation
            {
                Name = "Retracting",
                TextureName = "ice_feesh_sting",
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
                TextureName = "ice_feesh_sting",
                Animation = ThrowPoseFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            //CombatAnimation AttackAnimation = new CombatAnimation()
            //{
            //    Name = "Attack",
            //    TextureName = "nathan_stick_animations",
            //    Animation = AttackFrames,
            //    Loop = false,
            //    DrawOffset = commonDrawOffset
            //};


            ItemsDropped = new List<string>()
            {
                "ChainOfFlame",
                "EarthenGloves"
            };

            SkillNames = new List<string>()
            {
                "FrostFeeshAttack",
                "IceFeeshSuicideSting",
                "FrostFeeshParalyze",
            };


            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            //AddCombatAnimation(AttackAnimation);
            AddCombatAnimation(ChargeAnimation);
            AddCombatAnimation(RecoilAnimation);
            AddCombatAnimation(RetractingAnimation);
            AddCombatAnimation(ThrowingAnimation);
            AddCombatAnimation(ThrowPoseAnimation);
        }
        
        #endregion
    }
}