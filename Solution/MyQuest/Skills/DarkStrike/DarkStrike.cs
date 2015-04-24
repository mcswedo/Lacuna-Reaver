using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
        class DarkStrike : Skill
        {
            #region Frame Animations

            static readonly FrameAnimation poisonFrames = new FrameAnimation()
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


            #endregion
            #region Fields

            StrikeState state;
            Vector2 screenPosition;
            Vector2 initialPosition;
            Vector2 destinationPosition;
            CombatAnimation blindCloudAnimation;

            #endregion

            #region Constructor


            public DarkStrike()
            {
                Name = Strings.ZA361;
                Description = Strings.ZA362;

                MpCost = 40;
                SpCost = 2;

                SpellPower = 1.5f;
                DamageModifierValue = .2f;

                BattleSkill = true;
                MapSkill = false;
                HealingSkill = false;
                MagicSkill = false;
                IsBasicAttack = false;

                TargetsAll = false;
                CanTargetAllies = false;
                CanTargetEnemy = true;

                DrawOffset = new Vector2(-100, -100);

                blindCloudAnimation = new CombatAnimation()
                {
                    Name = "Blind",
                    TextureName = "blind",
                    Loop = false,
                    Animation = poisonFrames
                };


                blindCloudAnimation.LoadContent(ContentPath.ToSkillTextures);
            }


            #endregion

            public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
            {
                base.Activate(actor, targets);

                SubtractCost(actor);
                screenPosition = actor.ProjectileOrigin;
                initialPosition = actor.ScreenPosition;

                destinationPosition = targets[0].ScreenPosition + new Vector2 (-125, -25);

                CombatAnimation dash = actor.GetAnimation("Dash");

                state = StrikeState.Traveling;
                actor.SetAnimation("Dash");
                SoundSystem.Play(AudioCues.Swoosh);
               
            }

            public override void Update(GameTime gameTime)
            {
                if (state == StrikeState.Returning)
                {
                    blindCloudAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                }

                switch (state)
                {
                    case StrikeState.Traveling:

                        if (actor.CurrentAnimation.IsRunning == false)
                        {
                            actor.ScreenPosition = destinationPosition;
                            state = StrikeState.Striking;
                            actor.SetAnimation("DarkStrike");
                            SoundSystem.Play(AudioCues.SwordHitShield);
                        }
                        break;

                    case StrikeState.Striking:

                        if (actor.CurrentAnimation.IsRunning == false)
                        {
                            state = StrikeState.Returning;

                            actor.SetAnimation("Dash");

                            DamageModifier modifier = new DamageModifier(true, DamageModifierValue);
                            actor.AddDamageModifier(modifier);
                            DealPhysicalDamage(actor, targets.ToArray());
                            SoundSystem.Play(AudioCues.Swoosh);
                            foreach (FightingCharacter target in targets)
                            {
                                StatusEffect blind = new Blindness(3);

                                SetStatusEffect(actor, blind, target);
                            }

                            blindCloudAnimation.Play();
                            SoundSystem.Play(AudioCues.GazeOfDespair);
                        }
                        break;

                    case StrikeState.Returning:

                        if (actor.CurrentAnimation.IsRunning == false)
                        {
                            actor.ScreenPosition = initialPosition;
                            actor.SetAnimation("Idle");
                        }

                        if (actor.CurrentAnimation.Name == ("Idle") && blindCloudAnimation.IsRunning == false)
                        {
                            isRunning = false;
                        }
                        break;
                }
            }

            public override void Draw(SpriteBatch spriteBatch)
            {
                if (state == StrikeState.Returning)
                {
                    if (blindCloudAnimation.IsRunning)
                        blindCloudAnimation.Draw(spriteBatch, targets[0].ScreenPosition + DrawOffset, SpriteEffects.None);
                }
            }
        }
    }
