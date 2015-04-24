using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace MyQuest
{
    public class Will : PCFightingCharacter
    {
        #region Animations

        static readonly Vector2 commonDrawOffset = new Vector2(-168, -95);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.075,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 250, 200),
                new Rectangle(250, 0, 250, 200),
                new Rectangle(500, 0, 250, 200),
                new Rectangle(750, 0, 250, 200),
                new Rectangle(1000, 0, 250, 200),
                new Rectangle(1250, 0, 250, 200),
                new Rectangle(1500, 0, 250, 200),
                new Rectangle(1750, 0, 250, 200),
                new Rectangle(0, 200, 250, 200),
                new Rectangle(250, 200, 250, 200),
                new Rectangle(500, 200, 250, 200),
                new Rectangle(750, 200, 250, 200),
                new Rectangle(1000, 200, 250, 200),
                new Rectangle(1250, 200, 250, 200),
                new Rectangle(1500, 200, 250, 200),
                new Rectangle(1750, 200, 250, 200),
                new Rectangle(0, 400, 250, 200),
                new Rectangle(250, 400, 250, 200),
                new Rectangle(500, 400, 250, 200),
                new Rectangle(750, 400, 250, 200)
            }
        };

        static readonly FrameAnimation ScytheWarpFrames = new FrameAnimation
        {
            FrameDelay = 0.05,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 251, 199),
                new Rectangle(251, 0, 251, 199),
                new Rectangle(502, 0, 251, 199),
                new Rectangle(753, 0, 251, 199),

                new Rectangle(0, 199, 251, 199),
                new Rectangle(251, 199, 251, 199),
                new Rectangle(502, 199, 251, 199),
                new Rectangle(753, 199, 251, 199),

                new Rectangle(0, 398, 251, 199),
                new Rectangle(251, 398, 251, 199),
                new Rectangle(502, 398, 251, 199),
            }
        };

        static readonly FrameAnimation ScytheWarpBackFrames = new FrameAnimation
        {
            FrameDelay = 0.05,
            Frames = new List<Rectangle>
            {
                new Rectangle(502, 398, 251, 199),
                new Rectangle(251, 398, 251, 199),
                new Rectangle(0, 398, 251, 199),

                new Rectangle(753, 199, 251, 199),
                new Rectangle(502, 199, 251, 199),
                new Rectangle(251, 199, 251, 199),
                new Rectangle(0, 199, 251, 199),

                new Rectangle(753, 0, 251, 199),
                new Rectangle(502, 0, 251, 199),
                new Rectangle(251, 0, 251, 199),
                new Rectangle(0, 0, 251, 199),
            }
        };

        static readonly FrameAnimation IdleNoScytheFrames = new FrameAnimation
        {
            FrameDelay = 0.75,
            Frames = new List<Rectangle>
            {
                new Rectangle(502, 398, 251, 199),
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

        static readonly FrameAnimation GlowFrames = new FrameAnimation
        {
            FrameDelay = 0.125,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 300, 250),
                new Rectangle(300, 0, 300, 250),
                new Rectangle(600, 0, 300, 250),
                new Rectangle(900, 0, 300, 250),
                new Rectangle(1200, 0, 300, 250),
                new Rectangle(1500, 0, 300, 250)

            }
        };

        static readonly FrameAnimation DeadFrames = new FrameAnimation
        {
            FrameDelay = 0.75,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 265, 135),
            }
        };

        static readonly FrameAnimation WitherFrames = new FrameAnimation
        {
            FrameDelay = 0.195,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 252, 195),
                new Rectangle(252, 0, 252, 195),
                new Rectangle(504, 0, 252, 195),
                new Rectangle(756, 0, 252, 195),

                new Rectangle(0, 195, 252, 195),
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

        static readonly FrameAnimation ShadowBlastFrames = new FrameAnimation()
        {
            FrameDelay = 0.085,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 300, 200),
                new Rectangle(300, 0, 300, 200),
                new Rectangle(600, 0, 300, 200),
                new Rectangle(900, 0, 300, 200),
                new Rectangle(1200, 0, 300, 200),
                new Rectangle(1500, 0, 300, 200),
                new Rectangle(0, 200, 300, 200),
                new Rectangle(300, 200, 300, 200),
                new Rectangle(600, 200, 300, 200),
                new Rectangle(900, 200, 300, 200),
                new Rectangle(1200, 200, 300, 200),
                new Rectangle(1500, 200, 300, 200),
                new Rectangle(0, 400, 300, 200),
                new Rectangle(300, 400, 300, 200),
                new Rectangle(600, 400, 300, 200),
                new Rectangle(900, 400, 300, 200),
                new Rectangle(1200, 400, 300, 200),
                new Rectangle(1500, 400, 300, 200),
                new Rectangle(0, 600, 300, 200),
                new Rectangle(300, 600, 300, 200),
                new Rectangle(600, 600, 300, 200),
                new Rectangle(900, 600, 300, 200),
                new Rectangle(1200, 600, 300, 200),
                new Rectangle(1500, 600, 300, 200),
                new Rectangle(0, 800, 300, 200),
                new Rectangle(300, 800, 300, 200),
                new Rectangle(600, 800, 300, 200),
                new Rectangle(900, 800, 300, 200),
                new Rectangle(1200, 800, 300, 200),
                new Rectangle(1500, 800, 300, 200),
                new Rectangle(0, 1000, 300, 200),
                new Rectangle(300, 1000, 300, 200),
                new Rectangle(600, 1000, 300, 200),
                new Rectangle(900, 1000, 300, 200),
                new Rectangle(1200, 1000, 300, 200),
                new Rectangle(1500, 1000, 300, 200),
                new Rectangle(0, 1200, 300, 200),
                new Rectangle(300, 1200, 300, 200),
                new Rectangle(600, 1200, 300, 200),
                new Rectangle(900, 1200, 300, 200),
                new Rectangle(0, 1400, 315, 200),
                new Rectangle(315, 1400, 315, 200),
                new Rectangle(630, 1400, 315, 200),
                new Rectangle(945, 1400, 315, 200)
            }
        };


        static readonly CombatAnimation DeadAnimation = new CombatAnimation()
        {
            Name = "Dead",
            TextureName = "will_dead",
            Animation = DeadFrames,
            Loop = true,
            DrawOffset = new Vector2(-100, 0)
        };

        static readonly CombatAnimation IdleAnimation = new CombatAnimation()
        {
            Name = "Idle",
            TextureName = "will_combat_idle",
            Animation = IdleFrames,
            Loop = true,
            DrawOffset = commonDrawOffset
        };

        static readonly CombatAnimation GlowAnimation = new CombatAnimation()
        {
            Name = "Glow",
            TextureName = "will_glow",
            Animation = GlowFrames,
            Loop = true,
            DrawOffset = new Vector2(-192,-120)
        };

        static readonly CombatAnimation DashAnimation = new CombatAnimation()
        {
            Name = "Dash",
            TextureName = "nathan_stick_animations",
            Animation = DashFrames,
            Loop = false,
            DrawOffset = commonDrawOffset
        };

        static readonly CombatAnimation DefendingAnimation = new CombatAnimation()
        {
            Name = "Defending",
            TextureName = "TestCombatAnimation2Defend",
            Animation = IdleFrames,
            Loop = true
        };

        static readonly CombatAnimation ScytheWarpAnimation = new CombatAnimation()
        {
            Name = "ScytheWarp",
            TextureName = "will_attack",
            Animation = ScytheWarpFrames,
            Loop = false,
            DrawOffset = commonDrawOffset
        };

        static readonly CombatAnimation ScytheWarpBackAnimation = new CombatAnimation()
        {
            Name = "ScytheWarpBack",
            TextureName = "will_attack",
            Animation = ScytheWarpBackFrames,
            Loop = false,
            DrawOffset = commonDrawOffset
        };

        static readonly CombatAnimation IdleNoScytheAnimation = new CombatAnimation()
        {
            Name = "IdleNoScythe",
            TextureName = "will_attack",
            Animation = IdleNoScytheFrames,
            Loop = true,
            DrawOffset = commonDrawOffset
        };

        static readonly CombatAnimation DoubleStrikeAnimation = new CombatAnimation()
        {
            Name = "DoubleStrike",
            TextureName = "nathan_stick_animations",
            Animation = DoubleStrikeFrames,
            Loop = false,
            DrawOffset = commonDrawOffset
        };

        CombatAnimation WeaknessAnimation = new CombatAnimation()
        {
            Name = "Weakness",
            TextureName = "will_combat_idle",
            Animation = IdleFrames,
            Loop = false,
            DrawOffset = commonDrawOffset
        };

        CombatAnimation ShadowBlastAnimation = new CombatAnimation()
        {
            Name = "ShadowBlast",
            TextureName = "will_shadow_blast",
            Animation = ShadowBlastFrames,
            Loop = false,
            DrawOffset = commonDrawOffset
        };


        #endregion

        static Will instance = new Will();

        public static Will Instance
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

        private Will()
        {
            Name = Party.will;
            PortraitName = "WillPortrait";
            IconName = "WillIcon";
            BaseAttackName = "WillAttack";
            HitNoiseSoundCue = AudioCues.Flesh;
            OnHitSoundCue = AudioCues.WillHit;
            OnDeathSoundCue = AudioCues.WillDeath;

            FighterStats = CreateLevelOneFighterStats();

            //pointerOffset = new Vector2(-100, 0);
            //turnIndicatorOffset = new Vector2(-125, 84);
            pointerOffset = new Vector2(-125, 84);
            defenseShieldOffset = new Vector2(30, 0);
            projectileOriginOffset = new Vector2(67, -16);
            damageMessageOffset = new Vector2(-18, -125);
            statusEffectMessageOffset = new Vector2(100, -50);
            iconOffset = new Vector2(0, 0);

            CombatAnimations = new Dictionary<string, CombatAnimation>()
            {
                { "Idle", IdleAnimation },
                { "Glow", GlowAnimation },
                { "IdleNoScythe", IdleNoScytheAnimation },
                { "Defending", DefendingAnimation },
                { "Dash", DashAnimation },
                { "Dead", DeadAnimation },
                { "ScytheWarp", ScytheWarpAnimation },
                { "ScytheWarpBack",ScytheWarpBackAnimation },
                { "DoubleStrike", DoubleStrikeAnimation },
                { "Weakness", WeaknessAnimation},
                { "ShadowBlast", ShadowBlastAnimation},
            };

            instance = this;
        }
    }
}
