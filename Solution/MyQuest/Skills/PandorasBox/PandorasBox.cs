using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class PandorasBox : Skill
    {
        enum PandoraState
        {
            Charging,
            NextTarget,
            Impact
        }

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

        #region Fields


        CombatAnimation pandorasBoxAnimation;

        Vector2 screenPosition;

        PandoraState state;
        int targetsHit;

        #endregion

        #region Constructor


        public PandorasBox()
        {
            Name = Strings.ZA432;
            Description = Strings.ZA433;

            MpCost = 20;
            SpCost = 3;

            SpellPower = 5f;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;
            GrantsStatusEffect = true;

            TargetsAll = true;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            DrawOffset = new Vector2(-100,-100);
            pandorasBoxAnimation = new CombatAnimation()
                {
                    Name = "Pandora's Box",
                    TextureName = "pandora",
                    Loop = false,
                    Animation = PandoraFrames
                };

            
            pandorasBoxAnimation.LoadContent(ContentPath.ToSkillTextures);
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            if (actor.HasStatusEffect("Rage"))
            {
                this.Description = Strings.ZA657;
            }

            screenPosition = targets[0].ScreenPosition + DrawOffset;
            targetsHit = 0;
           
            state = PandoraState.Charging;
            actor.SetAnimation("PandoraAttack");
            SoundSystem.Play(AudioCues.GazeOfDespair);
        }

        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case PandoraState.Charging:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.SetAnimation("Idle");
                        pandorasBoxAnimation.Play();
                        SoundSystem.Play(AudioCues.Cloud);
                        state = PandoraState.NextTarget;
                    }
                    break;

                case PandoraState.NextTarget:
                    pandorasBoxAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                    if (pandorasBoxAnimation.IsRunning == false)
                    {
                        state = PandoraState.Impact;
                    }
                    break;

                case PandoraState.Impact:
                    pandorasBoxAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                    targetsHit++;
                    if (targetsHit < targets.Count && pandorasBoxAnimation.IsRunning == false)
                    {
                        screenPosition = targets[targetsHit].HitLocation + DrawOffset;
                        pandorasBoxAnimation.Play();
                        SoundSystem.Play(AudioCues.Cloud);
                        state = PandoraState.NextTarget;
                    }
                    else
                    {
                        int random;
                        StatusEffect effect;

                        foreach (FightingCharacter target in targets)
                        {
                            random = Utility.RNG.Next(4);

                            switch (random)
                            {
                                case 0: effect = new Poisoned(); SetStatusEffect(actor, effect, target); break;
                                case 1: effect = new Blindness(3); SetStatusEffect(actor, effect, target); break;
                                case 2: effect = new Paralyzed(2); SetStatusEffect(actor, effect, target); break;
                                case 3: effect = new Weakened(); SetStatusEffect(actor, effect, target); break;
                            }
                        }
                        if (actor.HasStatusEffect("Rage"))
                        {
                            DealMagicDamage(actor, targets.ToArray());
                        }

                        isRunning = false;
                    }

                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (state == PandoraState.NextTarget)
            {
                pandorasBoxAnimation.Draw(spriteBatch, screenPosition, SpriteEffects.None);
            }
        }
    }
}
