using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class Imp : NPCFightingCharacter
    {
        static readonly Vector2 commonDrawOffset = new Vector2(-60, -85);

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
                new Rectangle(0,    200, 200, 200)          
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
                new Rectangle(  2, 0, 300, 200),
                new Rectangle(302, 0, 300, 200),
                new Rectangle(602, 0, 300, 200),
                new Rectangle(902, 0, 300, 200),
                      
                new Rectangle(  2,  200, 300, 200),
                new Rectangle(302,  200, 300, 200),
                new Rectangle(602,  200, 300, 200),
                new Rectangle(902,  200, 300, 200),

                new Rectangle(  2,   400, 300, 200),               
                new Rectangle(302,   400, 300, 200),           
                new Rectangle(602,   400, 300, 200),               
                new Rectangle(902,   400, 300, 200)
            }
        };

        public Imp()
        {
            Name = "Imp";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BehaviorType = AIType.Knowledgeable;
            BaseAttackName = "ImpFireBall";
            HitNoiseSoundCue = AudioCues.Flesh;
            OnHitSoundCue = AudioCues.Demon2Hit;
            OnDeathSoundCue = AudioCues.Demon2Death;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 17000,
                Health = 17000,
                BaseMaxEnergy = 2500,
                Energy = 2500,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 188,
                BaseDefense =  180,
                BaseIntelligence = 149,
                BaseWillpower = 110,
                BaseAgility = 105,
                Level = 22,
                Experience = 2500,
                Gold = 92,
            };

            //offsets
            damageMessageOffset = new Vector2(20, -110);
            defenseShieldOffset = new Vector2(-125, 0);
            statusEffectMessageOffset = new Vector2(-155, -15);
            pointerOffset = new Vector2(-90, 92);
            iconOffset = new Vector2(30, 0);

            CombatAnimation DashAnimation = new CombatAnimation()
            {
                Name = "Dash",
                TextureName = "imp_idle",
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
                DrawOffset = commonDrawOffset - new Vector2(87, 0) //this attack offset is only needed for this animation.
            };

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "imp_idle",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };

            ItemsDropped = new List<string>()
            {
                "ChainOfFlame",
                "EarthenGloves",
                "NatureCharm"
            };

            SkillNames = new List<string>()
            {
                "ImpAttack",
                "ImpSiphon",
                "ImpFireBall"
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
        }
    }
}
