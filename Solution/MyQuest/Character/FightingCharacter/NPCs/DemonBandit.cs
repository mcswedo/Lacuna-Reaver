using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class DemonBandit : NPCFightingCharacter
    {
        static readonly Vector2 commonDrawOffset = new Vector2(-70, -90);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.195,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(600, 0, 200, 200),
            }
        };

        static readonly FrameAnimation AttackFrames = new FrameAnimation
        {
            FrameDelay = .035,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 400),
                new Rectangle(200, 0, 200, 400),
                new Rectangle(400, 0, 200, 400),
                new Rectangle(600, 0, 200, 400),
                new Rectangle(800, 0, 200, 400),
                new Rectangle(1000, 0, 200, 400),
                new Rectangle(1200, 0, 200, 400),
                new Rectangle(1400, 0, 200, 400),
                new Rectangle(1600, 0, 200, 400),
                new Rectangle(1800, 0, 200, 400),
                new Rectangle(0, 400, 200, 400),
                new Rectangle(200, 400, 200, 400),
                new Rectangle(400, 400, 200, 400),
                new Rectangle(600, 400, 200, 400),
                new Rectangle(800, 400, 200, 400),
                new Rectangle(1000, 400, 200, 400),
                new Rectangle(1200, 400, 200, 400),
                new Rectangle(1400, 400, 200, 400),
                new Rectangle(1600, 400, 200, 400),
                new Rectangle(1800, 400, 200, 400),
                new Rectangle(0, 800, 200, 400),
                new Rectangle(200, 800, 200, 400),
                new Rectangle(400, 800, 200, 400),
                new Rectangle(600, 800, 200, 400),
                new Rectangle(800, 800, 200, 400),
                new Rectangle(1000, 800, 200, 400),
                new Rectangle(1200, 800, 200, 400),
                new Rectangle(1400, 800, 200, 400),
                new Rectangle(1600, 800, 200, 400),
                new Rectangle(1800, 800, 200, 400),
                new Rectangle(0, 1200, 200, 400),
                new Rectangle(200, 1200, 200, 400),
            }
        };

        static readonly FrameAnimation DashFrames = new FrameAnimation
        {
            FrameDelay = 0.0625,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 400),
                new Rectangle(200, 0, 200, 400),
                new Rectangle(400, 0, 200, 400),
                new Rectangle(600, 0, 200, 400),
                new Rectangle(800, 0, 200, 400),
                new Rectangle(1000, 0, 200, 400),
                new Rectangle(1200, 0, 200, 400),
                new Rectangle(1400, 0, 200, 400),
                new Rectangle(1600, 0, 200, 400),
                new Rectangle(1800, 0, 200, 400),
                new Rectangle(0, 400, 200, 400),
            }
        };

        public DemonBandit()
        {
            Name = "Demon Bandit";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "DemonBanditAttack";
            BehaviorType = AIType.Knowledgeable;
            HitNoiseSoundCue = AudioCues.Flesh;
            OnHitSoundCue = AudioCues.MaxHit;
            OnDeathSoundCue = AudioCues.MaxDeath;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 14500,
                Health = 14500,
                BaseMaxEnergy = 800,
                Energy = 800,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 164,
                BaseDefense = 190,
                BaseIntelligence = 138,
                BaseWillpower = 62,
                BaseAgility = 112,
                Level = 21,
                Experience = 2450,
                Gold = 91,
            };

            damageMessageOffset = new Vector2(0, -80);
            defenseShieldOffset = new Vector2(0, 0);
            statusEffectMessageOffset = new Vector2(-125, -50);
            pointerOffset = new Vector2(-100, 87);
            iconOffset = new Vector2(15, 0);
                  
         CombatAnimation IdleAnimation = new CombatAnimation
        {
            Name = "Idle",
            TextureName = "demon_bandit_idle",
            Animation = IdleFrames,
            Loop = true,
            DrawOffset = commonDrawOffset
        };

         CombatAnimation AttackAnimation = new CombatAnimation()
         {
             Name = "Attack",
             TextureName = "demon_bandit_attack",
             Animation = AttackFrames,
             Loop = false,
             DrawOffset = new Vector2(-70, -285)

         };

         CombatAnimation DashAnimation = new CombatAnimation()
         {
             Name = "Dash",
             TextureName = "demon_bandit_disappear",
             Animation = DashFrames,
             Loop = false,
             DrawOffset = new Vector2(-70, -285)

         };

            ItemsDropped = new List<string>()
            {
                "LargeEnergyPotion",
                "LargeHealthPotion"
            };

            SkillNames = new List<string>()
            {
                "DemonBanditAttack",
                //"DemonBanditFlameLance", I think this skill is a little un necessary so I'm leaving it out for the moment.
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
        }
    }
}
