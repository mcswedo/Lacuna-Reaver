using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class FinalMalticar : NPCFightingCharacter
    {
        #region Static FrameAnimations

        static readonly Vector2 commonDrawOffset = new Vector2(-175, -360);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = .165,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 500, 500),
                new Rectangle(500, 0, 500, 500),
                new Rectangle(990, 0, 500, 500),
                new Rectangle(1500, 0, 500, 500),
                new Rectangle(0, 501, 500, 500),
                new Rectangle(490, 501, 500, 500)
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

        #endregion

        #region Constructor

        public FinalMalticar()
        {
            Name = "Mal'ticar";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "MalticarAttack";
            BehaviorType = AIType.Boss;
            AIController = new MalticarCombatAIController();
            HitNoiseSoundCue = AudioCues.Flesh;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.MonsterHit;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 215000,
                Health = 215000,
                //Health = 100,
                BaseMaxEnergy = 10000,
                Energy = 10000,
                BaseMaxStamina = 20,
                Stamina = 20,
                BaseStrength = 237,
                BaseDefense = 259,
                BaseIntelligence = 272,
                BaseWillpower = 177,
                BaseAgility = 0,
                Level = 30,
                Experience = 0, //these don't matter, last fight!
                Gold = 0,
            };

            //offsets
            damageMessageOffset = new Vector2(40, -200);
            defenseShieldOffset = new Vector2(0, 0);
            projectileOriginOffset = new Vector2(0, 25);
            pointerOffset = new Vector2(-15, 115);
            statusEffectMessageOffset = new Vector2(-125, -50);

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "malticar_idle",
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
                TextureName = "nathan_stick_animations",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            ItemsDropped = new List<string>()
            {

            };

            SkillNames = new List<string>()
            {
                "MalticarAttack",
                "MalticarSiphon", //Under file name MalticarSoulDrain
                "Cleave",
                "EtherBlast"
            };
    
            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
        }

        #endregion
    }
}
