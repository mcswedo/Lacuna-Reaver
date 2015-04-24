using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class Chepetawa : NPCFightingCharacter
    {
        #region Static FrameAnimations

        static readonly Vector2 commonDrawOffset = new Vector2(-136, -137);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.195,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 300, 300),
                new Rectangle(300, 0, 300, 300),
                new Rectangle(600, 0, 300, 300),
                new Rectangle(900, 0, 300, 300),
                new Rectangle(1200, 0, 300, 300),
                new Rectangle(1500, 0, 300, 300),

                new Rectangle(0, 300, 300, 300),
                new Rectangle(300, 300, 300, 300),
                new Rectangle(600, 300, 300, 300),
                new Rectangle(900, 300, 300, 300),
                new Rectangle(1200, 300, 300, 300),
                new Rectangle(1500, 300, 300, 300),

                new Rectangle(0, 600, 300, 300),
                new Rectangle(300, 600, 300, 300),
                new Rectangle(600, 600, 300, 300),
                new Rectangle(900, 600, 300, 300),
                new Rectangle(1200, 600, 300, 300),
                new Rectangle(1500, 600, 300, 300),

                new Rectangle(0, 900, 300, 300),
                new Rectangle(300, 900, 300, 300),

            }
        };

        static readonly FrameAnimation PandoraAttackFrames = new FrameAnimation
        {
            FrameDelay = 0.195,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 300, 300),
                new Rectangle(300, 0, 300, 300),
                new Rectangle(600, 0, 300, 300),
                new Rectangle(900, 0, 300, 300),
                new Rectangle(1200, 0, 300, 300),
                new Rectangle(1500, 0, 300, 300),

                new Rectangle(0, 300, 300, 300),
                new Rectangle(300, 300, 300, 300),
                new Rectangle(600, 300, 300, 300),
                new Rectangle(900, 300, 300, 300)
            }
        };

        static readonly FrameAnimation ChepetawaMonsoonAttackFrames = new FrameAnimation
        {
            FrameDelay = 0.125,
            Frames = new List<Rectangle>
            {
                new Rectangle(900, 0, 300, 300),
                new Rectangle(1200, 0, 300, 300),
                new Rectangle(1500, 0, 300, 300),

                new Rectangle(0, 300, 300, 300),
                new Rectangle(300, 300, 300, 300),
                new Rectangle(600, 300, 300, 300),
                new Rectangle(900, 300, 300, 300),
                new Rectangle(1200, 300, 300, 300),
                new Rectangle(1500, 300, 300, 300),

                new Rectangle(0, 600, 300, 300)
            }
        };

        static readonly FrameAnimation AttackFrames = new FrameAnimation
        {
            FrameDelay = 0.195,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 300, 300),
                new Rectangle(300, 0, 300, 300),
                new Rectangle(600, 0, 300, 300),
                new Rectangle(900, 0, 300, 300),
                new Rectangle(1200, 0, 300, 300),
                new Rectangle(1500, 0, 300, 300),

                new Rectangle(0, 300, 300, 300),
                new Rectangle(300, 300, 300, 300),
                new Rectangle(600, 300, 300, 300)
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

        #endregion

        #region Constructor

        public Chepetawa()
        {
            Name = "Chepetawa";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BehaviorType = AIType.Boss;
            BaseAttackName = "ChepetawaAttack";
            AIController = new ChepetawaCombatAIController();
            HitNoiseSoundCue = AudioCues.Flesh;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.MonsterHit;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 12000,//20000,
                Health = 12000,
                BaseMaxEnergy = 5000,
                Energy = 5000,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 100, //125 when enraged
                BaseDefense = 168, //187 when enraged
                BaseIntelligence = 160, //200 when enraged
                BaseWillpower = 147, //180 when enraged
                BaseAgility = 110,
                Level = 18,
                Experience = 12500,
                Gold = 500,
            };

            //offsets
            defenseShieldOffset = new Vector2(0, 0);
            damageMessageOffset = new Vector2(95, 12);
            statusEffectMessageOffset = new Vector2(-125, -50);
            pointerOffset = new Vector2(-117, 100);
            hitLocationOffset = new Vector2(-50, 0);
            iconOffset = new Vector2(-25, 0);

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "chepetawa_idle",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation PandoraAttackAnimation = new CombatAnimation
            {
                Name = "PandoraAttack",
                TextureName = "chepetawa_pandora",
                Animation = PandoraAttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation MonsoonAttackAnimation = new CombatAnimation
            {
                Name = "MonsoonAttack",
                TextureName = "chepetawa_monsoon",
                Animation = ChepetawaMonsoonAttackFrames,
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
                TextureName = "chepetawa_attack",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation HealAnimation = new CombatAnimation()
            {
                Name = "Heal",
                TextureName = "chepetawa_heal",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            ItemsDropped = new List<string>()
            {
                "HugeEnergyPotion",
                "HugeHealthPotion"
            };

            SkillNames = new List<string>()
            {
                "PandorasBox",
                "ChepetawaHeal",
                "ChepetawaAttack",
                "ChepetawaMonsoon",
                "Endowment"
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
            AddCombatAnimation(PandoraAttackAnimation);
            AddCombatAnimation(MonsoonAttackAnimation);
            AddCombatAnimation(HealAnimation);
        }

        #endregion
    }
}
