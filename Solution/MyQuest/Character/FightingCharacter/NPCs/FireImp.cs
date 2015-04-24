using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class FireImp : NPCFightingCharacter
    {
        static readonly Vector2 commonDrawOffset = new Vector2(-60, -85);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.195,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 215),
                new Rectangle(200, 0, 200, 215),
                new Rectangle(400, 0, 200, 215),
                new Rectangle(600, 0, 200, 215),
                new Rectangle(800, 0, 200, 215),
                new Rectangle(1000, 0, 200, 215),
                      
                new Rectangle(0, 215, 200, 215),
                new Rectangle(200, 215, 200, 215)          
            }
        };

        static readonly FrameAnimation DashFrames = new FrameAnimation
        {
            FrameDelay = 0.75,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 200)
            }
        };

        static readonly FrameAnimation AttackFrames = new FrameAnimation
        {
            FrameDelay = 0.1,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 300, 200),
                new Rectangle(300, 0, 300, 200),
                new Rectangle(600, 0, 300, 200),
                new Rectangle(900, 0, 300, 200),
                      
                new Rectangle(0,    200, 300, 200),
                new Rectangle(300,  200, 300, 200),
                new Rectangle(600,  200, 300, 200),
                new Rectangle(900,  200, 300, 200),

                new Rectangle(0,   400, 300, 200),               
                new Rectangle(300,   400, 300, 200),           
                new Rectangle(600,   400, 300, 200),               
                new Rectangle(900,   400, 300, 200)
            }
        };

        public FireImp()
        {
            Name = "FireImp";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BehaviorType = AIType.Knowledgeable;
            BaseAttackName = "ImpFireBall";
            HitNoiseSoundCue = AudioCues.Flesh;
            OnHitSoundCue = AudioCues.Demon2Hit;
            OnDeathSoundCue = AudioCues.Demon2Death;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 3200,
                Health = 3200,
                BaseMaxEnergy = 250,
                Energy = 250,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 110,
                BaseDefense = 80,
                BaseIntelligence = 80,
                BaseWillpower = 90,
                BaseAgility = 90,
                Level = 12,
                Experience = 250000,
                Gold = 2500,
            };

            //offsets
            damageMessageOffset = new Vector2(20, -110);
            defenseShieldOffset = new Vector2(-125, 0);
            statusEffectMessageOffset = new Vector2(-155, -15);
            pointerOffset = new Vector2(-90, 92);

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "fire_imp_idle",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation DashAnimation = new CombatAnimation()
            {
                Name = "Dash",
                TextureName = "fire_imp_idle",
                Animation = DashFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation AttackAnimation = new CombatAnimation()
            {
                Name = "Attack",
                TextureName = "imp_basic_attack",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            ItemsDropped = new List<string>()
            {

            };

            SkillNames = new List<string>()
            {
                //"Attack",
                "ImpFireBall"
                //"Siphon",
            };


            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);

        }
    }
}
