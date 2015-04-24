using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class MaxPoisonStrike : PoisonStrike
    {
        public MaxPoisonStrike() : base()
        {
            SpCost = 2;
        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            destinationPosition = targets[0].ScreenPosition + new Vector2(-125, -25);
        }

        public override void Update(GameTime gameTime)
        {
            if (state == StrikeState.Returning)
            {
                poisonCloudAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
            }

            switch (state)
            {
                case StrikeState.Traveling:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.ScreenPosition = destinationPosition;
                        state = StrikeState.Striking;
                        actor.SetAnimation("PoisonStrike");
                    }
                    break;

                case StrikeState.Striking:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        state = StrikeState.Returning;

                        actor.SetAnimation("Dash");
                        SoundSystem.Play(AudioCues.Swoosh);
                        DamageModifier modifier = new DamageModifier(true, DamageModifierValue);
                        actor.AddDamageModifier(modifier);
                        DealPhysicalDamage(actor, targets.ToArray());

                        foreach (FightingCharacter target in targets)
                        {
                            StatusEffect poison = new Poisoned();

                            SetStatusEffect(actor, poison, target);
                        }
                        poisonCloudAnimation.Play();
                    }
                    break;

                case StrikeState.Returning:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.ScreenPosition = initialPosition;
                        actor.SetAnimation("Idle");                              
                    }

                    if (actor.CurrentAnimation.Name == ("Idle") && poisonCloudAnimation.IsRunning == false)
                    {
                        isRunning = false;
                    }
                    break;
            }
        }
    }
}