using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace MyQuest
{
    public class Cara : PCFightingCharacter
    {
        #region Animations

        static readonly Vector2 commonDrawOffset = new Vector2(-40, -100);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
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

        static readonly FrameAnimation DoubleStrikeFrames = new FrameAnimation
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

        static readonly FrameAnimation AttackFrames = new FrameAnimation
        {
            FrameDelay = 0.15,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 200),
            }
        };

        static readonly FrameAnimation SkillAttackFrames = new FrameAnimation
        {
            FrameDelay = 0.05,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(600, 0, 200, 200),

                new Rectangle(0, 200, 200, 200),
                new Rectangle(200, 200, 200, 200),
                new Rectangle(400, 200, 200, 200),
                new Rectangle(600, 200, 200, 200),

                new Rectangle(0, 400, 200, 200),
                new Rectangle(200, 400, 200, 200),
                new Rectangle(400, 400, 200, 200),
                new Rectangle(600, 400, 200, 200),

                new Rectangle(400, 400, 200, 200),
                new Rectangle(200, 400, 200, 200),
                new Rectangle(0, 400, 200, 200),

                new Rectangle(600, 200, 200, 200),
                new Rectangle(400, 200, 200, 200),
                new Rectangle(200, 200, 200, 200),
                new Rectangle(0, 200, 200, 200),

                new Rectangle(600, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(0, 0, 200, 200)
            }
        };

        static readonly FrameAnimation DeadFrames = new FrameAnimation
        {
            FrameDelay = 0.75,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 200),
            }
        };

        static readonly CombatAnimation DeadAnimation = new CombatAnimation()
        {
            Name = "Dead",
            TextureName = "cara_death_temp",
            Animation = DeadFrames,
            Loop = true,
            DrawOffset = commonDrawOffset + new Vector2(0, 50)
        };

        static readonly CombatAnimation AttackAnimation = new CombatAnimation()
        {
            Name = "Attack",
            TextureName = "cara_attack",
            Animation = AttackFrames,
            Loop = false,
            DrawOffset = commonDrawOffset
        };


        static readonly CombatAnimation AnalyzeAnimation = new CombatAnimation()
        {
            Name = "Attack",
            TextureName = "cara_blue_attack",
            Animation = SkillAttackFrames,
            Loop = true,
            DrawOffset = commonDrawOffset
        };

        static readonly CombatAnimation EarthAnimation = new CombatAnimation()
        {
            Name = "Attack",
            TextureName = "cara_earth_attack",
            Animation = SkillAttackFrames,
            Loop = true,
            DrawOffset = commonDrawOffset
        };

        static readonly CombatAnimation RegenAnimation = new CombatAnimation()
        {
            Name = "Attack",
            TextureName = "cara_green_attack",
            Animation = SkillAttackFrames,
            Loop = true,
            DrawOffset = commonDrawOffset
        };

        static readonly CombatAnimation VigorAnimation = new CombatAnimation()
        {
            Name = "Attack",
            TextureName = "cara_red_attack",
            Animation = SkillAttackFrames,
            Loop = true,
            DrawOffset = commonDrawOffset
        };

        static readonly CombatAnimation HealAnimation = new CombatAnimation()
        {
            Name = "Attack",
            TextureName = "cara_yellow_attack",
            Animation = SkillAttackFrames,
            Loop = true,
            DrawOffset = commonDrawOffset
        };

        static readonly CombatAnimation DispelAnimation = new CombatAnimation()
        {
            Name = "Attack",
            TextureName = "cara_purple_attack",
            Animation = SkillAttackFrames,
            Loop = true,
            DrawOffset = commonDrawOffset
        };

        static readonly CombatAnimation InvulnAnimation = new CombatAnimation()
        {
            Name = "Attack",
            TextureName = "cara_white_attack",
            Animation = SkillAttackFrames,
            Loop = true,
            DrawOffset = commonDrawOffset
        };

        static readonly CombatAnimation IdleAnimation = new CombatAnimation()
        {
            Name = "Idle",
            TextureName = "cara_combat_idle",
            Animation = IdleFrames,
            Loop = true,
            DrawOffset = commonDrawOffset
        };

        static readonly CombatAnimation DefendingAnimation = new CombatAnimation()
        {
            Name = "Defending",
            TextureName = "TestCombatAnimation2Defend",
            Animation = IdleFrames,
            Loop = true
        };



        static readonly CombatAnimation DashAnimation = new CombatAnimation()
        {
            Name = "Dash",
            TextureName = "nathan_stick_animations",
            Animation = DashFrames,
            Loop = false
        };

        static readonly CombatAnimation DoubleStrikeAnimation = new CombatAnimation()
        {
            Name = "DoubleStrike",
            TextureName = "nathan_stick_animations",
            Animation = DoubleStrikeFrames,
            Loop = false
        };


        #endregion

        static Cara instance = new Cara();

        public static Cara Instance
        {
            get { return instance; }
            set { instance = value; }
        }

        protected override FighterStats CreateLevelOneFighterStats()
        {
            return new FighterStats()
            {
                BaseMaxHealth = 0,
                Health = 0,
                BaseMaxEnergy = 0,
                Energy = 0,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 0,
                BaseDefense = 0,
                BaseIntelligence = 0,
                BaseWillpower = 0,
                BaseAgility = 0,
                Level = 1,
                Experience = 0,
                Gold = 0,
                NextLevelXp = 0,
                MaxLevel = 25
            };
        }

        private Cara()
        {
            Name = Party.cara;
            PortraitName = "CaraPortrait";
            IconName = "CaraIcon";
            BaseAttackName = "CaraAttack";
            HitNoiseSoundCue = AudioCues.Flesh;
            OnHitSoundCue = AudioCues.CaraHit;
            OnDeathSoundCue = AudioCues.CaraGrunt;//will change once we have CaraDeath

            FighterStats = CreateLevelOneFighterStats();
            
            //offsets
            damageMessageOffset = new Vector2(50, -110);
            defenseShieldOffset = new Vector2(95, -20);
            statusEffectMessageOffset = new Vector2(120+80, -20);
            hitLocationOffset = new Vector2(40, 0);
            iconOffset = new Vector2(70, 0);
            //pointerOffset = new Vector2(0, 0);
            //turnIndicatorOffset = new Vector2(-50, 75);
            pointerOffset = new Vector2(-50 - 10, 75);

            hitLocationOffset = new Vector2(60, 0);

            projectileOriginOffset = new Vector2(100, -40);
            
            CombatAnimations = new Dictionary<string, CombatAnimation>()
            {
                { "Idle", IdleAnimation },
                { "Defending", DefendingAnimation },
                { "Dead", DeadAnimation },
                { "Dash", DashAnimation },
                { "Attack", AttackAnimation },
                { "AnalyzeAttack", AnalyzeAnimation },
                { "EarthAttack",  EarthAnimation },
                { "HealAttack",   HealAnimation },
                { "RegenAttack",  RegenAnimation },
                { "DispelAttack", DispelAnimation },
                { "VigorAttack",  VigorAnimation },
                { "InvulnAttack", InvulnAnimation },
                { "DoubleStrike", DoubleStrikeAnimation }
            };

            instance = this;
        }
    }
}
