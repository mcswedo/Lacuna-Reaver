using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    enum WillAttackState
    {
        Phasing,
        Striking,
        Returning,
    }

    public class WillAttack : Skill
    {

        #region Scythe Frame Animations

        static readonly FrameAnimation ScytheFrames = new FrameAnimation
        {
            FrameDelay = 0.05,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 251, 199),
                new Rectangle(251, 0, 251, 199),
                new Rectangle(502, 0, 251, 199),
                new Rectangle(753, 0, 251, 199),

                new Rectangle(0, 199, 251, 199),
                new Rectangle(251, 199, 251, 199),
                new Rectangle(502, 199, 251, 199),
                new Rectangle(753, 199, 251, 199),

                new Rectangle(0, 398, 251, 199),
                new Rectangle(251, 398, 251, 199),
                new Rectangle(502, 398, 251, 199),
                new Rectangle(753, 398, 251, 199),
                
                new Rectangle(1004, 597, 251, 199)
            }
        };


        #endregion

        List<CombatAnimation> scytheAnimations;
        Vector2 destinationPosition;

        WillAttackState state;

        public WillAttack()
        {
            Name = Strings.ZA329;
            Description = Strings.ZA350;

            MpCost = 0;
            SpCost = 0;

            SpellPower = 0;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = true;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            scytheAnimations = new List<CombatAnimation>()
            {
                new CombatAnimation()
                {
                    Name = "Scythe",
                    TextureName = "will_scythe",
                    Loop = false,
                    Animation = ScytheFrames,
                    DrawOffset =  new Vector2(-100,-100)
                }
            };

            foreach (CombatAnimation anim in scytheAnimations)
                anim.LoadContent(ContentPath.ToCombatCharacterTextures);

        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            destinationPosition = targets[0].HitLocation;

            state = WillAttackState.Phasing;
            actor.SetAnimation("ScytheWarp");
        }

        public override void Update(GameTime gameTime)
        {
            if (scytheAnimations[0].IsRunning == true)
            {
                scytheAnimations[0].Update(gameTime.ElapsedGameTime.TotalMilliseconds);
            }

            switch (state)
            {
                case WillAttackState.Phasing:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.SetAnimation("IdleNoScythe");
                        actor.CurrentAnimation.IsPaused = true;
                        scytheAnimations[0].Play();
                        SoundSystem.Play(AudioCues.WillSwoosh);
                        state = WillAttackState.Striking;
                    }
                    break;

                case WillAttackState.Striking:
                    if (scytheAnimations[0].IsRunning == false)
                    {
                        actor.CurrentAnimation.IsPaused = false;
                        actor.SetAnimation("ScytheWarpBack");
                        SoundSystem.Play(AudioCues.Stab);
                        DealPhysicalDamage(actor, targets.ToArray());
                        state = WillAttackState.Returning;
                    }
                    break;

                case WillAttackState.Returning:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.SetAnimation("Idle");
                        isRunning = false;
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (scytheAnimations[0].IsRunning)
            {
                if (actor is NegaWill)
                {
                    scytheAnimations[0].Draw(spriteBatch, destinationPosition, SpriteEffects.FlipHorizontally);
                }
                else
                {
                    scytheAnimations[0].Draw(spriteBatch, destinationPosition, SpriteEffects.None);
                }
            }
        }
    }
}
