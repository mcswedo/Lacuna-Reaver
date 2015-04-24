using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class Boggimus : NPCFightingCharacter
    {
        #region Static FrameAnimations

        static readonly Vector2 commonDrawOffset = new Vector2(-158, -220);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.195,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 400, 400),
                new Rectangle(400, 0, 400, 400),
                new Rectangle(800, 0, 400, 400),
                new Rectangle(1200, 0, 400, 400),

                new Rectangle(0, 400, 400, 400),
                new Rectangle(400, 400, 400, 400),
                new Rectangle(800, 400, 400, 400),
                new Rectangle(1200, 400, 400, 400),

                new Rectangle(0, 800, 400, 400),
                new Rectangle(400, 800, 400, 400),
                new Rectangle(800, 800, 400, 400),
                new Rectangle(1200, 800, 400, 400)
            }
        };

        static readonly FrameAnimation RageIdleFrames = new FrameAnimation
        {
            FrameDelay = 0.195,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 400, 400),
                new Rectangle(400, 0, 400, 400),
                new Rectangle(800, 0, 400, 400),
                new Rectangle(1200, 0, 400, 400),
                new Rectangle(1600, 0, 400, 400),

                new Rectangle(0, 400, 400, 400),
                new Rectangle(400, 400, 400, 400),
                new Rectangle(800, 400, 400, 400),
                new Rectangle(1200, 400, 400, 400),
                new Rectangle(1600, 400, 400, 400),

                new Rectangle(0, 800, 400, 400),
                new Rectangle(400, 800, 400, 400),
                new Rectangle(800, 800, 400, 400),
                new Rectangle(1200, 800, 400, 400),
                new Rectangle(1600, 800, 400, 400),
                
                new Rectangle(0, 1200, 400, 400),
                new Rectangle(400, 1200, 400, 400),
                new Rectangle(800, 1200, 400, 400),
                new Rectangle(1200, 1200, 400, 400),
                new Rectangle(1600, 1200, 400, 400),

                new Rectangle(0, 1600, 400, 400),

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

        static readonly FrameAnimation MonsoonAttackFrames = new FrameAnimation
        {
            FrameDelay = 0.15,
            Frames = new List<Rectangle>
            {
            
                new Rectangle(0, 0, 400, 400),
                new Rectangle(400, 0, 400, 400),
                new Rectangle(800, 0, 400, 400),
                new Rectangle(1200, 0, 400, 400),
                new Rectangle(1600, 0, 400, 400),

                new Rectangle(0, 400, 400, 400),
                new Rectangle(400, 400, 400, 400),
                new Rectangle(800, 400, 400, 400),
                new Rectangle(1200, 400, 400, 400),
                new Rectangle(1600, 400, 400, 400),

                new Rectangle(0, 800, 400, 400),
                new Rectangle(400, 800, 400, 400),
                new Rectangle(800, 800, 400, 400)
            }
        };

        static readonly FrameAnimation AttackFrames = new FrameAnimation
        {
            FrameDelay = 0.15,
            Frames = new List<Rectangle>
            {
            
                new Rectangle(0, 0, 400, 400),
                new Rectangle(400, 0, 400, 400),
                new Rectangle(800, 0, 400, 400),
                new Rectangle(1200, 0, 400, 400),
                new Rectangle(1600, 0, 400, 400),

                new Rectangle(0, 400, 400, 400),
                new Rectangle(400, 400, 400, 400),
                new Rectangle(800, 400, 400, 400)
            }
        };

     
        #endregion

        #region Constructor

        public Boggimus()
        {
            Name = "Boggimus";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BehaviorType = AIType.Boss;
            BaseAttackName = "BoggimusAttack";
            //elementalWeakness = Element.Lightning;
            AIController = new BoggimusCombatAIController();
            HitNoiseSoundCue = AudioCues.Flesh;
            OnHitSoundCue = AudioCues.BoggimusHit;
            OnDeathSoundCue = AudioCues.BoggimusDeath;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 15000,
                Health = 15000,
                BaseMaxEnergy = 500,
                Energy = 500,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 135/*120*/,
                BaseDefense = 225,
                BaseIntelligence = 80,
                BaseWillpower = 110,
                BaseAgility = 80,
                Level = 18,
                Experience = 12500,
                Gold = 500,
            };
            //offsets
            defenseShieldOffset = new Vector2(0, 0);
            damageMessageOffset = new Vector2(95, 12);
            statusEffectMessageOffset = new Vector2(-125, -50);
            pointerOffset = new Vector2(-100, 150);
            hitLocationOffset = new Vector2(-100, 0);

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
                TextureName = "boggimus_attack",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation MonsoonAttackAnimation = new CombatAnimation()
            {
                Name = "MonsoonAttack",
                TextureName = "boggimus_monsoon",
                Animation = MonsoonAttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation RageIdleAnimation = new CombatAnimation()
            {
                Name = "RageIdle",
                TextureName = "boggimus_rage",
                Animation = RageIdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "boggimus_idle",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            ItemsDropped = new List<string>()
            {
                "HugeEnergyPotion",
                "HugeHealthPotion"
            };

            SkillNames = new List<string>()
            {
                  "BoggimusAttack",
                  "BoggimusMonsoon",
                  "Endowment"
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(RageIdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
            AddCombatAnimation(MonsoonAttackAnimation);
        }

        #endregion
    }
}
