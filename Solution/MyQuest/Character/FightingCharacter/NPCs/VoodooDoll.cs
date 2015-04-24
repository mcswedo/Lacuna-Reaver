using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class VoodooDoll : NPCFightingCharacter
    {
        #region Static FrameAnimations

        static readonly Vector2 commonDrawOffset = new Vector2(-100, -110);

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
                new Rectangle(600, 200, 200, 200),
                new Rectangle(800, 200, 200, 200),
                new Rectangle(1000, 200, 200, 200),
                new Rectangle(1200, 200, 200, 200),
                new Rectangle(1400, 200, 200, 200),
                new Rectangle(1600, 200, 200, 200),
                new Rectangle(1800, 200, 200, 200),

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
            FrameDelay = 0.1,
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
            }
        };


        static readonly FrameAnimation ThrowFrame = new FrameAnimation
        {
            FrameDelay = 0.195,
            Frames = new List<Rectangle>
            {
                 new Rectangle(1800, 0, 200, 200) 
            }
        };

        static readonly FrameAnimation DestinyFrames = new FrameAnimation
        {
            FrameDelay = 0.1,
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
                new Rectangle(600, 200, 200, 200),
                new Rectangle(800, 200, 200, 200),
                new Rectangle(1000, 200, 200, 200)           
            }
        };

        #endregion

        #region Constructor

        public VoodooDoll()
        {
            Name = "Voodoo Doll";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "VoodooDollAttack";
            HitNoiseSoundCue = AudioCues.Flesh;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.MonsterHit;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 3650,
                Health = 3650,
                BaseMaxEnergy = 600,
                Energy = 600,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 95,
                BaseDefense = 125,
                BaseIntelligence = 95,
                BaseWillpower = 70,
                BaseAgility = 75,
                Level = 14,
                Experience = 1000,
                Gold = 48,
            };

            //ModifiedStats = new FighterStats();

            //offsets
            damageMessageOffset = new Vector2(-20, -80);
            defenseShieldOffset = new Vector2(-100, -30);
            statusEffectMessageOffset = new Vector2(-130, -45);
            pointerOffset = new Vector2(-120, 10);
            projectileOriginOffset = new Vector2(-30, -5); 
            BehaviorType = AIType.Knowledgeable;

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "voodoodoll_idle",
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
                TextureName = "voodoodoll_attack",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation ThrowAnimation = new CombatAnimation()
            {
                Name = "Throw",
                TextureName = "voodoodoll_attack",
                Animation = ThrowFrame,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation DestinyAnimation = new CombatAnimation()
            {
                Name = "Destiny",
                TextureName = "voodoodoll_destiny",
                Animation = DestinyFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            ItemsDropped = new List<string>()
            {
                "TranslucentRing",
                "ShadowStrikeRing"
            };

            SkillNames = new List<string>()
            {
                "VoodooDollAttack",
                "VoodooDollDestiny",
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
            AddCombatAnimation(ThrowAnimation);
            AddCombatAnimation(DestinyAnimation);
        }

        public PCFightingCharacter destinyTarget = null;
        public override void OnDamageReceived(int damage)
        {
            if (destinyTarget != null)
            {
                int finalDamage = (int)(damage * .35f);
                CombatMessage.AddMessage("" + finalDamage, destinyTarget.DamageMessagePosition, Color.Orange, .5);

                if ((destinyTarget.FighterStats.Health - finalDamage) <= 0)
                {
                    this.Statistics.KilledAnEnemy = true;
                    destinyTarget.FighterStats.Health = 0;
                    destinyTarget.OnDamageReceived(finalDamage);
                    destinyTarget.OnDeath();
                }
                else
                {
                    destinyTarget.FighterStats.Health -= finalDamage;
                    destinyTarget.OnDamageReceived(finalDamage);
                }
            }
        }

        #endregion
    }
}
