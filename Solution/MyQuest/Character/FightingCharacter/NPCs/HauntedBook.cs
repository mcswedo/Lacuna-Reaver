using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class HauntedBook : NPCFightingCharacter
    {
        static readonly Vector2 commonDrawOffset = new Vector2(-80, -80);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.05,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 181, 151),
                new Rectangle(181, 0, 181, 151),
                new Rectangle(362, 0, 181, 151),
                new Rectangle(543, 0, 181, 151),
                new Rectangle(724, 0, 181, 151),
                new Rectangle(905, 0, 181, 151),
                new Rectangle(1086, 0, 181, 151),
                new Rectangle(1267, 0, 181, 151),
                new Rectangle(1448, 0, 181, 151),
                new Rectangle(1629, 0, 181, 151),
                new Rectangle(1810, 0, 181, 151),

                new Rectangle(0, 151, 181, 151),
                new Rectangle(181, 151, 181, 151),
                new Rectangle(362, 151, 181, 151),
                new Rectangle(543, 151, 181, 151),
                new Rectangle(724, 151, 181, 151),
                new Rectangle(905, 151, 181, 151),
                new Rectangle(1086, 151, 181, 151),
                new Rectangle(1267, 151, 181, 151),
                new Rectangle(1448, 151, 181, 151),
                new Rectangle(1629, 151, 181, 151),
                new Rectangle(1810, 151, 181, 151),

                new Rectangle(0, 302, 181, 151),
                new Rectangle(181, 302, 181, 151),
                new Rectangle(362, 302, 181, 151),
                new Rectangle(543, 302, 181, 151),
                new Rectangle(724, 302, 181, 151),
                new Rectangle(905, 302, 181, 151),
                new Rectangle(1086, 302, 181, 151),
                new Rectangle(1267, 302, 181, 151),
                new Rectangle(1448, 302, 181, 151),
                new Rectangle(1629, 302, 181, 151),
                new Rectangle(1810, 302, 181, 151),
            }
        };

        static readonly FrameAnimation AttackFrames = new FrameAnimation
        {
            FrameDelay = 0.15,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 180, 150),
                new Rectangle(180, 0, 180, 150),
                new Rectangle(360, 0, 180, 150),
                new Rectangle(540, 0, 180, 150),
                new Rectangle(720, 0, 180, 150),
                new Rectangle(900, 0, 180, 150),
               
            }
        };

        public HauntedBook()
        {
            Name = "Haunted Book";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "PossessedBookAttack";
            HitNoiseSoundCue = AudioCues.Thunk;
            OnDeathSoundCue = AudioCues.MonsterDeath;
            OnHitSoundCue = AudioCues.MonsterHit;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 1400, //1500
                Health = 1400,
                BaseMaxEnergy = 500,
                Energy = 500,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 61, //45
                BaseDefense = 150,
                BaseIntelligence = 80,
                BaseWillpower = 110,
                BaseAgility = 67,
                Level = 10,
                Experience = 250,
                Gold = 30,
            };

            //offsets
            damageMessageOffset = new Vector2(30, -10);
            defenseShieldOffset = new Vector2(-70, 30);
            statusEffectMessageOffset = new Vector2(-100, 15);
            pointerOffset = new Vector2(-38, 65);
            hitLocationOffset = new Vector2(30, 80);
            iconOffset = new Vector2(68, 67);
            //projectileOriginOffset = new Vector2(-80, 80);

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "hauntedbook_idle",
                Animation = IdleFrames,
                Loop = true
            };

            CombatAnimation AttackAnimation = new CombatAnimation()
            {
                Name = "Attack",
                TextureName = "possessed_book_attack",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = new Vector2(0,0)
            };
         
            CombatAnimation DashAnimation = new CombatAnimation()
             {
            Name = "Dash",
            TextureName = "nathan_stick_animations",
            Animation = AttackFrames,
            Loop = false,
            DrawOffset = commonDrawOffset
              };


            ItemsDropped = new List<string>()
            {
                "PearlBand",
                "JadeStatue",
                "ShadowStrikeRing"
            };

            SkillNames = new List<string>()
            {
                "PossessedBookAttack",
                "PossessedBookBlind",
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);
        }
    }
}
