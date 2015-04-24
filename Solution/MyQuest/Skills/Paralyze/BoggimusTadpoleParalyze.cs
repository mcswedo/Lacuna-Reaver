using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class BoggimusTadpoleParalyze : Paralyze
    {
        #region Fields

        StrikeState state;

        Vector2 initialPosition;
        Vector2 destinationPosition;
        Vector2 velocity;


        #endregion

        #region Constructor

        public BoggimusTadpoleParalyze()
            : base()
        {
            Name = Strings.ZA435;
            Description = Strings.ZA436;

            MpCost = 250;
            SpCost = 10;

            SpellPower = 0.0f;
            DamageModifierValue = 0.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = false;

            DrawOffset = new Vector2(50, 0);

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;
        }
        #endregion
        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            initialPosition = actor.ScreenPosition;

            destinationPosition = targets[0].HitLocation + DrawOffset;

            CombatAnimation dash = actor.GetAnimation("Dash");

            //Decrease the time this takes!
            velocity =
                (destinationPosition - actor.ScreenPosition) / (float)TimeSpan.FromSeconds((dash.Animation.FrameDelay * dash.Animation.Frames.Count)).TotalMilliseconds;

            state = StrikeState.Traveling;
            actor.SetAnimation("Dash");
        }

        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case StrikeState.Traveling:
                    actor.ScreenPosition += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        state = StrikeState.Striking;
                        actor.SetAnimation("ParalyzeAttack");
                    }
                    break;

                case StrikeState.Striking:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        state = StrikeState.Returning;

                        actor.SetAnimation("Dash");

                        foreach (FightingCharacter target in targets)
                        {
                            if (target.State != State.Invulnerable)
                            {
                                StatusEffect paralyze = new Paralyzed(2);
                                SetStatusEffect(actor, paralyze, target);
                            }
                        }
                    }
                    break;

                case StrikeState.Returning:
                    actor.ScreenPosition -= velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (Vector2.Distance(actor.ScreenPosition, initialPosition) < velocity.Length())
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