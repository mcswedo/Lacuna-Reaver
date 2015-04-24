using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace MyQuest
{
    public class Sid : PCFightingCharacter
    {
        #region Animations

        static readonly Vector2 commonDrawOffset = new Vector2(-81,-89);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.075,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 224),
                new Rectangle(200, 0,  200, 224),
                new Rectangle(400, 0,  200, 224),
                new Rectangle(600, 0,  200, 224),

                new Rectangle(0, 224, 200, 224),
                new Rectangle(200, 224, 200, 224),
                new Rectangle(400, 224, 200, 224),
                new Rectangle(600, 224, 200, 224),

                new Rectangle(0, 448, 200, 224),
                new Rectangle(200, 448, 200, 224),
                new Rectangle(400, 448, 200, 224),
                new Rectangle(600, 448, 200, 224),

            }
        };

        static readonly FrameAnimation DashFrames = new FrameAnimation
        {
            FrameDelay = 0.025,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 200, 350),
                new Rectangle(200, 0,  200, 350),
                new Rectangle(400, 0,  200, 350),
                new Rectangle(600, 0,  200, 350),
                new Rectangle(800, 0,  200, 350),

                new Rectangle(0, 350, 200, 350),
                new Rectangle(200, 350, 200, 350),
                new Rectangle(400, 350, 200, 350),
                new Rectangle(600, 350, 200, 350),
                new Rectangle(800, 350, 200, 350),

                new Rectangle(0, 700, 200, 350),
                new Rectangle(200, 700, 200, 350),
                new Rectangle(400, 700, 200, 350),
                new Rectangle(600, 700, 200, 350),
                new Rectangle(800, 700, 200, 350),

                new Rectangle(0, 1050, 200, 350),
                new Rectangle(200, 1050, 200, 350),
                new Rectangle(400, 1050, 200, 350),
                new Rectangle(400, 1050, 200, 350)
                  
            }
        };

    
        static readonly FrameAnimation DoubleStrikeFrames = new FrameAnimation
        {
            FrameDelay = 0.025,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 251, 239),
                new Rectangle(251, 0, 251, 239),
                new Rectangle(502, 0, 251, 239),
                new Rectangle(753, 0, 251, 239),
        
                new Rectangle(0, 239, 251, 239),
                new Rectangle(251, 239, 251, 239),
                new Rectangle(502, 239, 251, 239),
                new Rectangle(753, 239, 251, 239),
                
                new Rectangle(0, 478, 251, 239),
                new Rectangle(251, 478, 251, 239),
                new Rectangle(502, 478, 251, 239),
                new Rectangle(753, 478, 251, 239),
                   
                new Rectangle(0, 717, 251, 239),
                new Rectangle(251, 717, 251, 239),
                new Rectangle(502, 717, 251, 239),

                new Rectangle(502, 717, 251, 239),
                new Rectangle(251, 717, 251, 239),
                new Rectangle(0, 717, 251, 239),    
                              
                new Rectangle(753, 478, 251, 239),
                new Rectangle(502, 478, 251, 239),
                new Rectangle(251, 478, 251, 239),
                new Rectangle(0, 478, 251, 239),

                new Rectangle(753, 239, 251, 239),
                new Rectangle(502, 239, 251, 239),
                new Rectangle(251, 239, 251, 239),
                new Rectangle(0, 239, 251, 239),

                new Rectangle(753, 0, 251, 239),
                new Rectangle(502, 0, 251, 239),
                new Rectangle(251, 0, 251, 239),
                new Rectangle(0, 0, 251, 239),

                new Rectangle(0, 0, 251, 239),
                new Rectangle(251, 0, 251, 239),
                new Rectangle(502, 0, 251, 239),
                new Rectangle(753, 0, 251, 239),
        
                new Rectangle(0, 239, 251, 239),
                new Rectangle(251, 239, 251, 239),
                new Rectangle(502, 239, 251, 239),
                new Rectangle(753, 239, 251, 239),
                
                new Rectangle(0, 478, 251, 239),
                new Rectangle(251, 478, 251, 239),
                new Rectangle(502, 478, 251, 239),
                new Rectangle(753, 478, 251, 239),
                   
                new Rectangle(0, 717, 251, 239),
                new Rectangle(251, 717, 251, 239),
                new Rectangle(502, 717, 251, 239),
           
            
            }
        };

        static readonly FrameAnimation DeadFrames = new FrameAnimation
        {
            FrameDelay = 0.75,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 229, 118),
            }
        };

        static readonly FrameAnimation AttackFrames = new FrameAnimation
        {
            FrameDelay = 0.025,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 251, 239),
                new Rectangle(251, 0, 251, 239),
                new Rectangle(502, 0, 251, 239),
                new Rectangle(753, 0, 251, 239),
        
                new Rectangle(0, 239, 251, 239),
                new Rectangle(251, 239, 251, 239),
                new Rectangle(502, 239, 251, 239),
                new Rectangle(753, 239, 251, 239),
                
                new Rectangle(0, 478, 251, 239),
                new Rectangle(251, 478, 251, 239),
                new Rectangle(502, 478, 251, 239),
                new Rectangle(753, 478, 251, 239),
                   
                new Rectangle(0, 717, 251, 239),
                new Rectangle(251, 717, 251, 239),
                new Rectangle(502, 717, 251, 239),
            }
        };

        static readonly FrameAnimation PowerStrikeFrames = new FrameAnimation
        {
            FrameDelay = 0.025,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 250, 239),
                new Rectangle(250, 0, 250, 239),
                new Rectangle(500, 0, 250, 239),
                new Rectangle(750, 0, 250, 239),
        
                new Rectangle(0, 239, 250, 239),
                new Rectangle(250, 239, 250, 239),
                new Rectangle(500, 239, 250, 239),
                new Rectangle(750, 239, 250, 239),
                
                new Rectangle(0, 478, 250, 239),
                new Rectangle(250, 478, 250, 239),
                new Rectangle(500, 478, 250, 239),
                new Rectangle(750, 478, 250, 239),
                   
                new Rectangle(0, 717, 250, 239),
                new Rectangle(250, 717, 250, 239),
                new Rectangle(500, 717, 250, 239)
            }
        };

        static readonly CombatAnimation DeadAnimation = new CombatAnimation()
        {
            Name = "Dead",
            TextureName = "sid_dead",
            Animation = DeadFrames,
            Loop = true,
            DrawOffset = new Vector2(0,0)
        };

        static readonly CombatAnimation IdleAnimation = new CombatAnimation()
        {
            Name = "Idle",
            TextureName = "sid_idle",
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
            TextureName = "sid_dash",
            Animation = DashFrames,
            Loop = false,
            DrawOffset = new Vector2(-85,-170)
        };

        static readonly CombatAnimation DashReturnAnimation = new CombatAnimation()
        {
            Name = "DashReturn",
            TextureName = "sid_dash",
            Animation = DashFrames,
            Loop = false,
            DrawOffset = new Vector2(-85, -170)
        };


        static readonly CombatAnimation DoubleStrikeAnimation = new CombatAnimation()
        {
            Name = "DoubleStrike",
            TextureName = "sid_attack",
            Animation = DoubleStrikeFrames,
            Loop = false,
            DrawOffset = commonDrawOffset
        };


        static readonly CombatAnimation AttackAnimation = new CombatAnimation()
        {
            Name = "Attack",
            TextureName = "sid_attack",
           // TextureName = "sid_attack",
            Animation = AttackFrames,
            Loop = false,
            DrawOffset = commonDrawOffset
        };

        static readonly CombatAnimation PowerStrikeAnimation = new CombatAnimation()
        {
            Name = "PowerStrike",
            TextureName = "sid_power_strike",
            Animation = PowerStrikeFrames,
            Loop = false,
            DrawOffset = commonDrawOffset
        };

        #endregion

        static Sid instance = new Sid();

        public static Sid Instance
        {
            get { return instance; }
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
                Level = 12,
                Experience = 0,
                Gold = 0,
                NextLevelXp = 0,
                MaxLevel = 15
            };
        }

        public Sid()
        {
            Name = Party.sid;
            PortraitName = "SidPortrait";
            IconName = "SidIcon";
            BaseAttackName = "SidAttack";
            HitNoiseSoundCue = AudioCues.Flesh;
            OnHitSoundCue = AudioCues.MaxHit;
            OnDeathSoundCue = AudioCues.MaxDeath;//change once we get the right sound

            FighterStats = CreateLevelOneFighterStats();

            //ModifiedStats = new FighterStats();

//            pointerOffset = new Vector2(-100, 0);
            defenseShieldOffset = new Vector2(0, 0);
            damageMessageOffset = new Vector2(-10, -115);
//            turnIndicatorOffset = new Vector2(-25, 200) + commonDrawOffset;
            pointerOffset = new Vector2(-125, 110);
            //pointerOffset = new Vector2(-25, 200) + commonDrawOffset;

            //SkillNames.Add("SidDoubleStrike");
            //SkillNames.Add("SidPowerStrike");

            CombatAnimations = new Dictionary<string, CombatAnimation>()
            {
                { "Idle", IdleAnimation },
                { "Defending", DefendingAnimation },
                { "Dead", DeadAnimation },
                { "Dash", DashAnimation },
                { "DashReturn", DashReturnAnimation },
                { "DoubleStrike", DoubleStrikeAnimation },
                { "PowerStrike", PowerStrikeAnimation },
                { "Attack", AttackAnimation }
            };

            instance = this;
        }
    }
}
