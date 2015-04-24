using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class EtherBlast : Skill
    {

        static readonly FrameAnimation fireFrames = new FrameAnimation()
        {
            FrameDelay = 0.05,
            Frames = new List<Rectangle>()
            {
            
                new Rectangle(0, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(600, 0, 200, 200),
                new Rectangle(800, 0, 200, 200),

                new Rectangle(0, 200, 200, 200),
                new Rectangle(200, 200, 200, 200),
                new Rectangle(400, 200, 200, 200),
                new Rectangle(600, 200, 200, 200),
                new Rectangle(800, 200, 200, 200),

                new Rectangle(0, 400, 200, 200),
                new Rectangle(200, 400, 200, 200),
                new Rectangle(400, 400, 200, 200),
                new Rectangle(600, 400, 200, 200),
                new Rectangle(800, 400, 200, 200),

                new Rectangle(0, 600, 200, 200),
                new Rectangle(200, 600, 200, 200),
                new Rectangle(400, 600, 200, 200),
                new Rectangle(600, 600, 200, 200),
                new Rectangle(800, 600, 200, 200),               
            }
        };

        static readonly FrameAnimation BlindingFrames = new FrameAnimation()
        {
            FrameDelay = 0.05,
            Frames = new List<Rectangle>()
            {
            
                new Rectangle(0, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(600, 0, 200, 200),
                new Rectangle(800, 0, 200, 200),

                new Rectangle(0, 200, 200, 200),
                new Rectangle(200, 200, 200, 200),
                new Rectangle(400, 200, 200, 200),
                new Rectangle(600, 200, 200, 200),
                new Rectangle(800, 200, 200, 200),

                new Rectangle(0, 400, 200, 200),
                new Rectangle(200, 400, 200, 200),
                new Rectangle(400, 400, 200, 200),
                new Rectangle(600, 400, 200, 200),
                new Rectangle(800, 400, 200, 200),

                new Rectangle(0, 600, 200, 200),
                new Rectangle(200, 600, 200, 200),
                new Rectangle(400, 600, 200, 200),
                new Rectangle(600, 600, 200, 200),
                new Rectangle(800, 600, 200, 200),               
            }
        };

        static readonly FrameAnimation MiasmaFrames = new FrameAnimation()
        {
            FrameDelay = 0.05,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(600, 0, 200, 200),
                new Rectangle(800, 0, 200, 200),

                new Rectangle(0, 200, 200, 200),
                new Rectangle(200, 200, 200, 200),
                new Rectangle(400, 200, 200, 200),
                new Rectangle(600, 200, 200, 200),
                new Rectangle(800, 200, 200, 200),

                new Rectangle(0, 400, 200, 200),
                new Rectangle(200, 400, 200, 200),
                new Rectangle(400, 400, 200, 200),
                new Rectangle(600, 400, 200, 200),
                new Rectangle(800, 400, 200, 200),

                new Rectangle(0, 600, 200, 200),
                new Rectangle(200, 600, 200, 200),
                new Rectangle(400, 600, 200, 200),
                new Rectangle(600, 600, 200, 200),
                new Rectangle(800, 600, 200, 200),
              
            }
        };

        static readonly FrameAnimation PandoraFrames = new FrameAnimation()
        {
            FrameDelay = 0.05,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(600, 0, 200, 200),
                new Rectangle(800, 0, 200, 200),

                new Rectangle(0, 200, 200, 200),
                new Rectangle(200, 200, 200, 200),
                new Rectangle(400, 200, 200, 200),
                new Rectangle(600, 200, 200, 200),
                new Rectangle(800, 200, 200, 200),

                new Rectangle(0, 400, 200, 200),
                new Rectangle(200, 400, 200, 200),
                new Rectangle(400, 400, 200, 200),
                new Rectangle(600, 400, 200, 200),
                new Rectangle(800, 400, 200, 200),

                new Rectangle(0, 600, 200, 200),
                new Rectangle(200, 600, 200, 200),
                new Rectangle(400, 600, 200, 200),
                new Rectangle(600, 600, 200, 200),
                new Rectangle(800, 600, 200, 200),
              
            }
        };

        static readonly FrameAnimation paralyzeFrames = new FrameAnimation()
        {
            FrameDelay = 0.05,
            Frames = new List<Rectangle>()
            {
            
                new Rectangle(0, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(600, 0, 200, 200),
                new Rectangle(800, 0, 200, 200),

                new Rectangle(0, 200, 200, 200),
                new Rectangle(200, 200, 200, 200),
                new Rectangle(400, 200, 200, 200),
                new Rectangle(600, 200, 200, 200),
                new Rectangle(800, 200, 200, 200),

                new Rectangle(0, 400, 200, 200),
                new Rectangle(200, 400, 200, 200),
                new Rectangle(400, 400, 200, 200),
                new Rectangle(600, 400, 200, 200),
                new Rectangle(800, 400, 200, 200),

                new Rectangle(0, 600, 200, 200),
                new Rectangle(200, 600, 200, 200),
                new Rectangle(400, 600, 200, 200),
                new Rectangle(600, 600, 200, 200),
                new Rectangle(800, 600, 200, 200),               
            }
        };

        #region CombatAnimations

        CombatAnimation fireAnimation;
        CombatAnimation blindAnimation;
        CombatAnimation miasmaAnimation;
        CombatAnimation pandoraAnimation;
        CombatAnimation poisonAnimation;

        #endregion

        #region Fields

        StrikeState state;
        int targetsHit;
        Vector2 destinationPosition;
        Vector2 destinationPosition2;
        Vector2 destinationPosition3;
        Vector2 destinationPosition4;
        Vector2 destinationPosition5;

        #endregion

        #region Constructor

        public EtherBlast()
        {
            Name = Strings.ZA376;
            Description = Strings.ZA377;

            MpCost = 200;
            SpCost = 4;

            SpellPower = 20;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            TargetsAll = true;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            DrawOffset = new Vector2(-85, -105);

            fireAnimation = new CombatAnimation()
            {
                Name = "FireAnimation",
                TextureName = "red_cloud",
                Animation = fireFrames,
                Loop = false
            };

            blindAnimation = new CombatAnimation()
            {
                Name = "blindAnimation",
                TextureName = "blind",
                Animation = BlindingFrames,
                Loop = false
            };

            miasmaAnimation = new CombatAnimation()
            {
                Name = "Miasma",
                TextureName = "miasma",
                Animation = MiasmaFrames,
                Loop = false
            };

            poisonAnimation = new CombatAnimation()
            {
                Name = "PoisonAnimation",
                TextureName = "poison",
                Animation = paralyzeFrames,
                Loop = false
            };

            pandoraAnimation = new CombatAnimation()
            {
                Name = "Pandora's Box",
                TextureName = "pandora",
                Loop = false,
                Animation = PandoraFrames
            };

            pandoraAnimation.LoadContent(ContentPath.ToSkillTextures);
            poisonAnimation.LoadContent(ContentPath.ToSkillTextures);
            miasmaAnimation.LoadContent(ContentPath.ToSkillTextures);
            blindAnimation.LoadContent(ContentPath.ToSkillTextures);
            fireAnimation.LoadContent(ContentPath.ToSkillTextures);
        }

        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);
            SubtractCost(actor);

            destinationPosition = targets[0].HitLocation + DrawOffset;
            destinationPosition2 = targets[0].HitLocation + DrawOffset + new Vector2(-35, 35);
            destinationPosition3 = targets[0].HitLocation + DrawOffset + new Vector2(35, -35);
            destinationPosition4 = targets[0].HitLocation + DrawOffset + new Vector2(35, 35);
            destinationPosition5 = targets[0].HitLocation + DrawOffset + new Vector2(-35, -35);

            targetsHit = 0;

            state = StrikeState.Traveling;
            fireAnimation.Play();
            miasmaAnimation.Play();
            blindAnimation.Play();
            pandoraAnimation.Play();
            poisonAnimation.Play();

            actor.CurrentAnimation.IsPaused = true;
            SoundSystem.Play(AudioCues.Cloud);
        }

        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case StrikeState.Traveling:
                    fireAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                    blindAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                    miasmaAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                    pandoraAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                    poisonAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

                    if (fireAnimation.IsRunning == false) // We check one animation only, all animations take the same amount of time.
                    {
                        state = StrikeState.Striking;
                    }
                    break;

                case StrikeState.Striking:
                    targetsHit++;
                    if (targetsHit < targets.Count)
                    {
                        destinationPosition = targets[targetsHit].HitLocation + DrawOffset;
                        destinationPosition2 = targets[targetsHit].HitLocation + DrawOffset + new Vector2(-35, 35);
                        destinationPosition3 = targets[targetsHit].HitLocation + DrawOffset + new Vector2(35, -35);
                        destinationPosition4 = targets[targetsHit].HitLocation + DrawOffset + new Vector2(35, 35);
                        destinationPosition5 = targets[targetsHit].HitLocation + DrawOffset + new Vector2(-35, -35);

                        fireAnimation.Play();
                        miasmaAnimation.Play();
                        blindAnimation.Play();
                        pandoraAnimation.Play();
                        poisonAnimation.Play();
                        SoundSystem.Play(AudioCues.Cloud);
                        state = StrikeState.Traveling;
                    }
                        
                    else
                    {
                        foreach (FightingCharacter target in targets)
                        {
                            int initialStamina = target.FighterStats.Stamina;
                            int deltaStamina;
                            Random random = new Random();
                            if (target.FighterStats.Stamina == target.FighterStats.ModifiedMaxStamina)
                            {
                                deltaStamina = random.Next(target.FighterStats.Stamina, target.FighterStats.ModifiedMaxStamina);
                            }
                            else if (target.FighterStats.Stamina <= 9)
                            {
                                deltaStamina = random.Next(10, target.FighterStats.ModifiedMaxStamina); //set it to 10 or higher.
                            }
                            else
                            {
                                deltaStamina = random.Next(target.FighterStats.Stamina + 1, target.FighterStats.ModifiedMaxStamina);
                            }
                            target.FighterStats.Stamina = deltaStamina;
                            int staminaDifference = deltaStamina - initialStamina;

                            SpellPower += staminaDifference * 2; //Maybe lower initial spellPower and double the gain from the Stamina difference.

                            DealMagicDamage(actor, target);

                            SpellPower -= staminaDifference * 2; //re-initialize SpellPower.
                        }

                        actor.CurrentAnimation.IsPaused = false;
                        isRunning = false;

                    }

                    break;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (state == StrikeState.Traveling)
            {
                fireAnimation.Draw(spriteBatch, destinationPosition, SpriteEffects.None);
                blindAnimation.Draw(spriteBatch, destinationPosition2, SpriteEffects.None);
                miasmaAnimation.Draw(spriteBatch, destinationPosition3, SpriteEffects.None);
                pandoraAnimation.Draw(spriteBatch, destinationPosition4, SpriteEffects.None);
                poisonAnimation.Draw(spriteBatch, destinationPosition5, SpriteEffects.None);
            }
        }
    }
}