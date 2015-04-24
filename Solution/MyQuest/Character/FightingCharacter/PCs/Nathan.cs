using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class Nathan : PCFightingCharacter
    {
        #region Animations

        static readonly Vector2 commonDrawOffset = new Vector2(-94, -113);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.075,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 230),
                new Rectangle(200, 0, 200, 230),
                new Rectangle(400, 0, 200, 230),
                new Rectangle(600, 0, 200, 230),
                new Rectangle(800, 0, 200, 230),
                new Rectangle(1000, 0, 200, 230),
                new Rectangle(1200, 0, 200, 230),
                new Rectangle(1400, 0, 200, 230),
                new Rectangle(1600, 0, 200, 230),
                new Rectangle(1800, 0, 200, 230)
            }
        };

        static readonly FrameAnimation FocusFrames = new FrameAnimation
        {
            FrameDelay = .075,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 230),
                new Rectangle(200, 0, 200, 230),
                new Rectangle(400, 0, 200, 230),
                new Rectangle(600, 0, 200, 230),
                new Rectangle(800, 0, 200, 230),
                new Rectangle(1000, 0, 200, 230),
                new Rectangle(1200, 0, 200, 230),
                new Rectangle(1400, 0, 200, 230),
                new Rectangle(1600, 0, 200, 230),
                new Rectangle(1800, 0, 200, 230),
                new Rectangle(0, 0, 200, 230),
                new Rectangle(200, 0, 200, 230),
                new Rectangle(400, 0, 200, 230),
                new Rectangle(600, 0, 200, 230),
                new Rectangle(800, 0, 200, 230),
                new Rectangle(1000, 0, 200, 230),
                new Rectangle(1200, 0, 200, 230),
                new Rectangle(1400, 0, 200, 230),
                new Rectangle(1600, 0, 200, 230),
                new Rectangle(1800, 0, 200, 230)
            }
        };

        static readonly FrameAnimation DashFrames = new FrameAnimation
        {
            FrameDelay = 0.025,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 230),
                new Rectangle(200, 0, 200, 230),
                new Rectangle(400, 0, 200, 230),
                new Rectangle(600, 0, 200, 230),
                new Rectangle(800, 0, 200, 230),

                new Rectangle(0, 230, 200, 230),
                new Rectangle(200, 230, 200, 230),
                new Rectangle(400, 230, 200, 230),
                new Rectangle(600, 230, 200, 230),
                new Rectangle(800, 230, 200, 230),

                new Rectangle(0, 460, 200, 230),
                new Rectangle(200, 460, 200, 230),
                new Rectangle(400, 460, 200, 230),
                new Rectangle(600, 460, 200, 230),
                new Rectangle(800, 460, 200, 230),

                new Rectangle(0, 690, 200, 230),
                new Rectangle(200, 690, 200, 230),
                new Rectangle(400, 690, 200, 230),
                new Rectangle(600, 690, 200, 230),
            }
        };

        static readonly FrameAnimation ReflectedDashFrames = new FrameAnimation
        {
            FrameDelay = 0.025,
            Frames = new List<Rectangle>
            {
                new Rectangle(800, 0, 200, 230),
                new Rectangle(600, 0, 200, 230),
                new Rectangle(400, 0, 200, 230),
                new Rectangle(200, 0, 200, 230),                
                new Rectangle(0, 0, 200, 230),

                new Rectangle(800, 230, 200, 230),
                new Rectangle(600, 230, 200, 230),
                new Rectangle(400, 230, 200, 230),
                new Rectangle(200, 230, 200, 230),
                new Rectangle(0, 230, 200, 230),

                new Rectangle(800, 460, 200, 230),
                new Rectangle(600, 460, 200, 230),
                new Rectangle(400, 460, 200, 230),
                new Rectangle(200, 460, 200, 230),
                new Rectangle(0, 460, 200, 230),

                new Rectangle(600, 690, 200, 230),
                new Rectangle(400, 690, 200, 230),
                new Rectangle(200, 690, 200, 230),
                new Rectangle(0, 690, 200, 230)                                            
            }
        };
        static readonly FrameAnimation AttackFrames = new FrameAnimation
        {
            FrameDelay = 0.060,
            Frames = new List<Rectangle>
            {
                new Rectangle(10, 0, 300, 230),
                new Rectangle(310, 0, 300, 230),
                new Rectangle(610, 0, 300, 230),

                new Rectangle(10, 230, 300, 230),
                new Rectangle(310, 230, 300, 230),
                new Rectangle(610, 230, 300, 230)     
            }
        };

        static readonly FrameAnimation SlashFrames = new FrameAnimation
        {
            FrameDelay = 0.060,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 256, 256),
                new Rectangle(256, 0, 256, 256),
                new Rectangle(512, 0, 256, 256),
                new Rectangle(768, 0, 256, 256),

                new Rectangle(0, 256, 256, 256),
                new Rectangle(256, 256, 256, 256),
                new Rectangle(256, 256, 256, 256)
            }
        };

        static readonly FrameAnimation ChargeFrames = new FrameAnimation
        {
            FrameDelay = 0.060,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 512, 512),
                new Rectangle(512, 0, 512, 512),
                new Rectangle(1024, 0, 512, 512),
                new Rectangle(1536, 0, 512, 512),

                new Rectangle(0, 512, 512, 512),
                new Rectangle(512, 512, 512, 512),
                new Rectangle(1024, 512, 512, 512),
                new Rectangle(1536, 512, 512, 512),
                   
                new Rectangle(1024, 512, 512, 512),
                new Rectangle(512, 512, 512, 512),
                new Rectangle(0, 512, 512, 512),   
                        
                new Rectangle(1536, 0, 512, 512),
                new Rectangle(1024, 0, 512, 512),
                new Rectangle(512, 0, 512, 512),
                new Rectangle(0, 0, 512, 512),

                new Rectangle(0, 0, 512, 512),
                new Rectangle(512, 0, 512, 512),
                new Rectangle(1024, 0, 512, 512),
                new Rectangle(1536, 0, 512, 512),

                new Rectangle(0, 512, 512, 512),
                new Rectangle(512, 512, 512, 512),
                new Rectangle(1024, 512, 512, 512),
                new Rectangle(1536, 512, 512, 512),
                   
                new Rectangle(1024, 512, 512, 512),
                new Rectangle(512, 512, 512, 512),
                new Rectangle(0, 512, 512, 512),   
                        
                new Rectangle(1536, 0, 512, 512),
                new Rectangle(1024, 0, 512, 512),
                new Rectangle(512, 0, 512, 512),
                new Rectangle(0, 0, 512, 512),
                
            }
        };


        static readonly FrameAnimation ReflectedAttackFrames = new FrameAnimation
        {
            FrameDelay = 0.080,
            Frames = new List<Rectangle>
            {
                new Rectangle(600, 0, 300, 230),
                new Rectangle(300, 0, 300, 230),
                new Rectangle(0, 0, 300, 230),
               
                new Rectangle(600, 230, 300, 230), 
                new Rectangle(300, 230, 300, 230),
                new Rectangle(0, 230, 300, 230)                                  
            }
        };

        static readonly FrameAnimation DeadFrames = new FrameAnimation
        {
            FrameDelay = 0.75,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 209, 200),
            }
        };

        static readonly CombatAnimation DeadAnimation = new CombatAnimation()
        {
            Name = "Dead",
            TextureName = "nathan_dead",
            Animation = DeadFrames,
            Loop = true,
            DrawOffset = commonDrawOffset
        };

        static readonly CombatAnimation DashAnimation = new CombatAnimation()
        {
            Name = "Dash",
            TextureName = "nathan_dash",
            Animation = DashFrames,
            Loop = false,
            DrawOffset = commonDrawOffset
        };

        static readonly CombatAnimation ReflectedDashAnimation = new CombatAnimation()
        {
            Name = "Dash",
            TextureName = "nathan_dash_reflected",
            Animation = ReflectedDashFrames,
            Loop = false,
            DrawOffset = commonDrawOffset
        };

        static readonly CombatAnimation DashReturnAnimation = new CombatAnimation()
        {
            Name = "DashReturn",
            TextureName = "nathan_dash",
            Animation = DashFrames,
            Loop = false,
            DrawOffset = new Vector2(-200, -113)
        };

        readonly CombatAnimation AttackAnimation = new CombatAnimation()
        {
            Name = "Attack",
            TextureName = "nathan_attack",
            Animation = AttackFrames,
            Loop = false,
            DrawOffset = new Vector2(-200, -113)
        };

        readonly CombatAnimation SlashAnimation = new CombatAnimation()
        {
            Name = "Slash",
            TextureName = "nathan_slash",
            Animation = SlashFrames,
            Loop = false,
            DrawOffset = new Vector2(-100, -128)
        };

        readonly CombatAnimation SlashGlowAnimation = new CombatAnimation()
        {
            Name = "SlashGlow",
            TextureName = "nathan_slash_glow",
            Animation = SlashFrames,
            Loop = false,
            DrawOffset = new Vector2(-100, -128)
        };

        readonly CombatAnimation ChargeAnimation = new CombatAnimation()
        {
            Name = "Charge",
            TextureName = "nathan_charge",
            Animation = ChargeFrames,
            Loop = false,
            DrawOffset = new Vector2(-200, -256)
        };

        readonly CombatAnimation PoisonStrikeAnimation = new CombatAnimation()
        {
            Name = "PoisonStrike",
            TextureName = "nathan_poison_strike",
            Animation = AttackFrames,
            Loop = false,
            DrawOffset = new Vector2(-200, -113)
        };

        readonly CombatAnimation PowerStrikeAnimation = new CombatAnimation()
        {
            Name = "PowerStrike",
            TextureName = "nathan_power_strike",
            Animation = AttackFrames,
            Loop = false,
            DrawOffset = new Vector2(-200, -113)
        };


        readonly CombatAnimation StunningStrikeAnimation = new CombatAnimation()
        {
            Name = "ParalyzingStrike",
            TextureName = "nathan_stunning_strike",
            Animation = AttackFrames,
            Loop = false,
            DrawOffset = new Vector2(-200, -113)
        };

        readonly CombatAnimation ReflectedAttackAnimation = new CombatAnimation()
        {
            Name = "ReflectedAttack",
            TextureName = "nathan_attack_reflected",
            Animation = ReflectedAttackFrames,
            Loop = false,
            DrawOffset = new Vector2(-200, -113)
        };

        readonly CombatAnimation IdleAnimation = new CombatAnimation()
        {
            Name = "Idle",
            TextureName = "nathan_idle",
            Animation = IdleFrames,
            Loop = true,
            DrawOffset = commonDrawOffset
        };

        readonly CombatAnimation FocusAnimation = new CombatAnimation()
        {
            Name = "Idle",
            TextureName = "nathan_focus",
            Animation = FocusFrames,
            Loop = false,
            DrawOffset = commonDrawOffset
        };

        readonly CombatAnimation DefendingAnimation = new CombatAnimation()
        {
            Name = "Defending",
            TextureName = "nathan_stick_animations",
            Animation = IdleFrames,
            Loop = true,
            DrawOffset = commonDrawOffset
        };



        #endregion

        static Nathan instance = new Nathan();

        public static Nathan Instance
        {
            get { return instance; }
            set { instance = value; }
        }

        protected override FighterStats CreateLevelOneFighterStats()
        {
            unlockedElathiaRiftDestinations.Clear();
            unlockedAgoraRiftDestinations.Clear();
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
                Experience = 0,//4073000,
                Gold = 0,
                NextLevelXp = 0, //4083000,
                MaxLevel = 25
            };
        }
        
        #region Rift Destinations

        public List<int> unlockedElathiaRiftDestinations = new List<int>();
        public List<int> unlockedAgoraRiftDestinations = new List<int>();

        #endregion

        private Nathan()
        {
            Name = Party.nathan;
            PortraitName = "NathanPortrait";
            IconName = "NathanIcon";
            BaseAttackName = "NathanAttack";
            HitNoiseSoundCue = AudioCues.Flesh;
            OnHitSoundCue = AudioCues.NathanHit;
            OnDeathSoundCue = AudioCues.NathanDeath;

            FighterStats = CreateLevelOneFighterStats();

            //offsets
//            pointerOffset = new Vector2(-100, 0);
//            turnIndicatorOffset = new Vector2(-25, 204) + commonDrawOffset;
            pointerOffset = new Vector2(-25, 204) + commonDrawOffset;
            damageMessageOffset = new Vector2(15, -110);
            defenseShieldOffset = new Vector2(20, 0);
            statusEffectMessageOffset = new Vector2(25, -50);
            iconOffset = new Vector2(0, 0);
           // hitLocationOffset = new Vector2(50, 0);
            //ModifiedStats = new FighterStats();

            CombatAnimations = new Dictionary<string, CombatAnimation>()
            {
                { "Idle", IdleAnimation },
                { "Focus", FocusAnimation },
                { "Defending", DefendingAnimation },
                { "Dash", DashAnimation },
                { "DashReturn", DashReturnAnimation },
                { "ReflectedDash", ReflectedDashAnimation },
                { "Dead", DeadAnimation },
                { "Attack", AttackAnimation },
                { "PoisonStrike", PoisonStrikeAnimation },
                { "PowerStrike", PowerStrikeAnimation },
                { "ParalyzingStrike", StunningStrikeAnimation },
                { "DoubleStrike", AttackAnimation },
                { "Slash", SlashAnimation },
                { "SlashGlow", SlashGlowAnimation },
                { "Charge", ChargeAnimation },
                { "ReflectedAttack", ReflectedAttackAnimation }
            };
            instance = this;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
