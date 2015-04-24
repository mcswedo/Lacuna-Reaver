using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class SlashAll : Skill
    {
        #region Fields

        StrikeState state;

        Vector2 initialPosition;
        Vector2 destinationPosition;
        int targetsHit;

        #endregion

        #region Constructor


        public SlashAll()
        {
            Name = Strings.ZA482;
            Description = Strings.ZA483;

            MpCost = 40;
            SpCost = 8;

            SpellPower = 1.0f;
            DamageModifierValue = 0.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = false;

            TargetsAll = true;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            DrawOffset = new Vector2(-50, -10);
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            initialPosition = actor.ScreenPosition;

            destinationPosition = targets[0].HitLocation + DrawOffset;

            CombatAnimation dash = actor.GetAnimation("Dash");

            targetsHit = 0;
            state = StrikeState.Traveling;
            actor.SetAnimation("Dash");
            SoundSystem.Play(AudioCues.Swoosh);
         
        }

        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case StrikeState.Traveling:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.ScreenPosition = destinationPosition;
                        state = StrikeState.Striking;
                        actor.SetAnimation("Attack");
                        SoundSystem.Play(AudioCues.Attack);
                    }
                    break;

                case StrikeState.Striking:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        targetsHit++;
                        if (targetsHit < targets.Count())
                        {
                            destinationPosition = targets[targetsHit].HitLocation + DrawOffset;
                            actor.SetAnimation("DashReturn");
                            SoundSystem.Play(AudioCues.Swoosh);
                            state = StrikeState.Traveling;                          
                        }

                        else
                        {
                            actor.SetAnimation("DashReturn");
                            SoundSystem.Play(AudioCues.Swoosh);
                            DamageModifier modifier = new DamageModifier(true, DamageModifierValue);
                            actor.AddDamageModifier(modifier);
                            DealPhysicalDamage(actor, targets.ToArray());
                            state = StrikeState.Returning;
                        }
                    }
                    break;

                case StrikeState.Returning:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.ScreenPosition = initialPosition;
                        actor.SetAnimation("Idle");
                        isRunning = false;
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
