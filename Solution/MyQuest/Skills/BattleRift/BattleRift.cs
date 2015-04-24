using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class BattleRift : Skill
    {
        protected enum VortexState
        {
            Initiating,
            Opening,
            Twirling,
            Closeing,
            Concludeing
        }

        #region Fields

        VortexState state;

        Vector2 initialPosition;
        Vector2 destinationPosition;
        List<CombatAnimation> vortexAnimations;
        List<CombatAnimation> malticarAnimations;
        int currentAnimation;
        int currentAnimationMal;
        Vector2 screenPosition;
        double ticker;
        #endregion

        static readonly FrameAnimation OpeningFrames = new FrameAnimation()
        {
            FrameDelay = .05,
            Frames = new List<Rectangle>()
            {
                new Rectangle(512, 1536, 512, 512),
                new Rectangle(0, 1536, 512, 512),

                new Rectangle(1536, 1024, 512, 512),
                new Rectangle(1024, 1024, 512, 512),
                new Rectangle(512, 1024, 512, 512),
                new Rectangle(0, 1024, 512, 512),
            
                new Rectangle(1536, 512, 512, 512),
                new Rectangle(1024, 512, 512, 512),
                new Rectangle(512, 512, 512, 512),
                new Rectangle(0, 512, 512, 512),
                
                new Rectangle(1536, 0, 512, 512),
                new Rectangle(1024, 0, 512, 512),
                new Rectangle(512, 0, 512, 512),
                new Rectangle(0, 0, 512, 512)
                           
            }
        };

        static readonly FrameAnimation CloseingFrames = new FrameAnimation()
        {
            FrameDelay = .05,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 512, 512),
                new Rectangle(512, 0, 512, 512),
                new Rectangle(1024, 0, 512, 512),
                new Rectangle(1536, 0, 512, 512),
               
                new Rectangle(0, 512, 512, 512),
                new Rectangle(512, 512, 512, 512),
                new Rectangle(1024, 512, 512, 512),
                new Rectangle(1536, 512, 512, 512),

                new Rectangle(0, 1024, 512, 512),
                new Rectangle(512, 1024, 512, 512),
                new Rectangle(1024, 1024, 512, 512),
                new Rectangle(1536, 1024, 512, 512),

                new Rectangle(0, 1536, 512, 512),
                new Rectangle(512, 1536, 512, 512)
            }
        };

        static readonly FrameAnimation TwirlingFrames = new FrameAnimation()
        {
            FrameDelay = 0.1,
            Frames = new List<Rectangle>()
            {
             
                new Rectangle(0, 0, 512, 512),
                new Rectangle(512, 0, 512, 512),
                new Rectangle(1024, 0, 512, 512),
                new Rectangle(1536, 0, 512, 512),
               
                new Rectangle(0, 512, 512, 512),
                new Rectangle(512, 512, 512, 512),
                new Rectangle(1024, 512, 512, 512),
                new Rectangle(1536, 512, 512, 512),

                new Rectangle(0, 1024, 512, 512),
                new Rectangle(512, 1024, 512, 512),
                new Rectangle(1024, 1024, 512, 512),
                new Rectangle(1536, 1024, 512, 512)
            }
        };


        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = .165,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 500, 500),
                new Rectangle(500, 0, 500, 500),
                new Rectangle(990, 0, 500, 500),
                new Rectangle(1500, 0, 500, 500),
                new Rectangle(0, 501, 500, 500),
                new Rectangle(490, 501, 500, 500)
            }
        };

        static readonly FrameAnimation SuckedFrames = new FrameAnimation
        {
            FrameDelay = .1,
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

                new Rectangle(0, 1024, 512, 512),
                new Rectangle(512, 1024, 512, 512),
                new Rectangle(1024, 1024, 512, 512),
                new Rectangle(1536, 1024, 512, 512),

                new Rectangle(0, 1536, 512, 512),
                new Rectangle(512, 1536, 512, 512)

            }
        };

        #region Constructor


        public BattleRift()
        {
            Name = "Rift";
            Description = "A standard melee or ranged attack.";

            MpCost = Nathan.Instance.FighterStats.Energy;
            SpCost = Nathan.Instance.FighterStats.Stamina;

            SpellPower = 1000f;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = true;

            
            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;
 
            DrawOffset = new Vector2(-160, -380);

            ticker = 0;
   
            vortexAnimations = new List<CombatAnimation>()
            {
                new CombatAnimation()
                {
                    Name = "Charging",
                    TextureName = "vortex_open",
                    Loop = false,
                    Animation = OpeningFrames
                },
                new CombatAnimation()
                {
                    Name = "Traveling1",
                    TextureName = "vortex_twirl",
                    Loop = true,
                    Animation = TwirlingFrames
                },

                     new CombatAnimation()
                {
                    Name = "Traveling1",
                    TextureName = "vortex_open",
                    Loop = false,
                    Animation = CloseingFrames
                }
            };

            foreach (CombatAnimation anim in vortexAnimations)
            {
                anim.LoadContent(ContentPath.ToSkillTextures);
            }

            malticarAnimations = new List<CombatAnimation>()
            {
                new CombatAnimation()
                {
                    Name = "Idle",
                    TextureName = "malticar_idle",
                    Animation = IdleFrames,
                    Loop = true,
                    DrawOffset =  new Vector2(-175, -360)
                },

                   new CombatAnimation()
                {
                    Name = "Sucked",
                    TextureName = "malticar_sucked",
                    Animation = SuckedFrames,
                    Loop = false,
                    DrawOffset =  new Vector2(-240, -300)
                }
            };

            foreach (CombatAnimation anim in malticarAnimations)
            {
                anim.LoadContent(ContentPath.ToCombatCharacterTextures);
            }

        }

        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            ticker = TimeSpan.FromSeconds(1.25).TotalMilliseconds;

            SubtractCost(actor);

            initialPosition = actor.ScreenPosition;

            destinationPosition = targets[0].HitLocation + DrawOffset;

            state = VortexState.Initiating;
            actor.SetAnimation("Charge");
            actor.CurrentAnimation.Loop = false;
            currentAnimation = 0;
            currentAnimationMal = 0;
            screenPosition = targets[0].ScreenPosition;

            malticarAnimations[currentAnimationMal].Play();
                    
            targets[0].CurrentAnimation.Alpha = 0;
            SoundSystem.Play(AudioCues.Focus);
         
        }

        public override void Update(GameTime gameTime)
        {  
            switch (state)
            {
                case VortexState.Initiating:
                    actor.CurrentAnimation.Loop = false; //To stop Nathan from being stuck in a looped state if there's a bug.
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        state = VortexState.Opening;
                        actor.SetAnimation("SlashGlow");
                    }
                    break;

                case VortexState.Opening:
                   
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.SetAnimation("Idle");
                        SoundSystem.Play(AudioCues.Attack);
                        SoundSystem.Play(AudioCues.BattleRift);
                        vortexAnimations[currentAnimation].Play();                      
                    }

                    if(actor.CurrentAnimation.Name == "Idle")

                    {
                        if (vortexAnimations[currentAnimation].GetCurrentFrame == 13)
                        {
                            vortexAnimations[++currentAnimation].Play();
                            malticarAnimations[++currentAnimationMal].Play();
                            state = VortexState.Twirling;

                        }
                    }

                    break;

                case VortexState.Twirling:

                    if (malticarAnimations[currentAnimationMal].GetCurrentFrame == 13)
                    {
                        vortexAnimations[++currentAnimation].Play();
                        state = VortexState.Closeing;                
                    }

                    break;

                case VortexState.Closeing:

                    if (vortexAnimations[currentAnimation].IsRunning == false)
                    {
                        ticker -= gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (ticker <= 0)
                        {
                            targets[0].SetState(State.Dead);
                            isRunning = false;
                        }
                    }

                    break;
            }

            if (state != VortexState.Closeing)
            {
                malticarAnimations[currentAnimationMal].Update(gameTime.ElapsedGameTime.TotalMilliseconds);
            }

            if (actor.CurrentAnimation.Name == "Idle")
            {
                vortexAnimations[currentAnimation].Update(gameTime.ElapsedGameTime.TotalMilliseconds);
            }
            
           
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (actor.CurrentAnimation.Name == "Idle" && vortexAnimations[currentAnimation].IsRunning != false)
            {
                vortexAnimations[currentAnimation].Draw(spriteBatch, screenPosition + DrawOffset, SpriteEffects.None);
            }

            if (state != VortexState.Closeing)
            {
                malticarAnimations[currentAnimationMal].Draw(spriteBatch, screenPosition, SpriteEffects.None);
            }

        }
    }
}
