using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class Arlan : NPCFightingCharacter
    {
        #region Static FrameAnimations

        static readonly Vector2 commonDrawOffset = new Vector2(-85, -85);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.195,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(600, 0, 200, 200),
                
                new Rectangle(0, 200, 200, 200),
                new Rectangle(200, 200, 200, 200)
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
             
                new Rectangle(0, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(600, 0, 200, 200),
                new Rectangle(0, 200, 200, 200),
                
                new Rectangle(200, 200, 200, 200),
                new Rectangle(400, 200, 200, 200)
            }
        };

      
        #endregion

        #region Constructor

        public Arlan()
        {
            Name = "Arlan";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "ArlanFireBall";
            BehaviorType = AIType.Boss;
            AIController = new ArlanCombatAIController();
            HitNoiseSoundCue = AudioCues.Flesh;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.MonsterHit;

            FighterStats = new FighterStats //Arlan may be too easy, get feedback!
            {
                BaseMaxHealth = 115000,
                //Health = 100,  //Use this for testing final stuff if necessary.
                Health = 115000,
                BaseMaxEnergy = 5000,
                Energy = 5000,
                BaseMaxStamina = 20,
                Stamina = 20,
                BaseStrength = 181,
                BaseDefense = 208,
                BaseIntelligence = 245,
                BaseWillpower = 182,
                BaseAgility = 199,
                Level = 27,
                Experience = 63000,
                Gold = 1500,
            };

            //ModifiedStats = new FighterStats();

            //offsets
            damageMessageOffset = new Vector2(0, -105);
            defenseShieldOffset = new Vector2(0, 0);
            statusEffectMessageOffset = new Vector2(-125, -50);
            pointerOffset = new Vector2(-117, 102);
            projectileOriginOffset = new Vector2(0, -45);


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
                TextureName = "arlan_attack",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "arlan_idle",
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
                //Has a 30% chance to use Siphon, followed by a 50% chance to use nemesis cannon, or else it will use ArlanFireBall.
                "ArlanFireBall",
                //"Lightning",
                "ArlanSiphon", //This ability is under Soul Drain. It's a child class of Siphon. Weakens target and focuses actor while dealing damage and healing self.
                "NemesisCannon" //Hits all and may poison target.
            };

   
            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
        }

        #endregion
    }
}
