using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace MyQuest
{
    public class Max : PCFightingCharacter
    {
        #region Animations

        static readonly Vector2 commonDrawOffset = new Vector2(-81, -89);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.085,
            Frames = new List<Rectangle>
            {
                new Rectangle(0,   0, 200, 234),
                new Rectangle(200, 0,  200, 234),
                new Rectangle(400, 0,  200, 234),
                new Rectangle(600, 0,  200, 234),

                new Rectangle(0,   234, 200, 234),
                new Rectangle(200, 234, 200, 234),
                new Rectangle(400, 234, 200, 234),
                new Rectangle(600, 234, 200, 234),

                new Rectangle(0,   468, 200, 234),
                new Rectangle(200, 468, 200, 234),
                new Rectangle(400, 468, 200, 234),
                new Rectangle(600, 468, 200, 234),

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

        static readonly FrameAnimation DarkStrikeFrames = new FrameAnimation
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

        static readonly FrameAnimation PoisonStrikeFrames = new FrameAnimation
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
            TextureName = "max_dead",
            Animation = DeadFrames,
            Loop = true,
            DrawOffset = new Vector2(0, 0)
        };

        static readonly CombatAnimation IdleAnimation = new CombatAnimation()
        {
            Name = "Idle",
            TextureName = "max_idle",
            Animation = IdleFrames,
            Loop = true,
            DrawOffset = new Vector2(-81, -115)
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
            TextureName = "max_dash",
            Animation = DashFrames,
            Loop = false,
            DrawOffset = new Vector2(-85, -185)
        };


        static readonly CombatAnimation PoisonStrikeAnimation = new CombatAnimation()
        {
            Name = "PoisonStrike",
            TextureName = "max_poison_strike",
            Animation = PoisonStrikeFrames,
            Loop = false,
            DrawOffset = commonDrawOffset
        };


        static readonly CombatAnimation AttackAnimation = new CombatAnimation()
        {
            Name = "Attack",
            TextureName = "max_attack",
            Animation = AttackFrames,
            Loop = false,
            DrawOffset = commonDrawOffset
        };

        static readonly CombatAnimation DarkStrikeAnimation = new CombatAnimation()
        {
            Name = "DarkStrike",
            TextureName = "max_dark_strike",
            Animation = DarkStrikeFrames,
            Loop = false,
            DrawOffset = commonDrawOffset
        };

        #endregion

        static Max instance = new Max();

        public static Max Instance
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

        public Max()
        {
            Name = Party.max;
            PortraitName = "MaxPortrait";
            IconName = "MaxIcon";
            BaseAttackName = "MaxAttack";
            HitNoiseSoundCue = AudioCues.Flesh;
            OnHitSoundCue = AudioCues.MaxHit;
            OnDeathSoundCue = AudioCues.MaxDeath;

            FighterStats = CreateLevelOneFighterStats();

            //offsets
            damageMessageOffset = new Vector2(0, -115);
//            pointerOffset = new Vector2(-100, 0);
            defenseShieldOffset = new Vector2(0, 0);
            projectileOriginOffset = new Vector2(67, -16);
//            turnIndicatorOffset = new Vector2(-125, 80);
            pointerOffset = new Vector2(-125, 80);

//            SkillNames.Add("DoubleStrike");  these should be called in MaxLevelUp.
//            SkillNames.Add("PowerStrike");

            CombatAnimations = new Dictionary<string, CombatAnimation>()
            {
                { "Idle", IdleAnimation },
                { "Defending", DefendingAnimation },
                { "Dead", DeadAnimation },
                { "Dash", DashAnimation },
                { "PoisonStrike", PoisonStrikeAnimation },
                { "DarkStrike", DarkStrikeAnimation },
                { "Attack", AttackAnimation }
            };

            instance = this;
        }
    }
}
