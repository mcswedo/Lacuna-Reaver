using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{
    enum WillShadowAttackState
    {
        Phasing,
        Striking,
        Returning,
    }

    public class ShadowStrike : Skill
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

        WillShadowAttackState state;

        #region Constructor


        public ShadowStrike()
        {
            Name = Strings.ZA470;
            Description = Strings.ZA471;

            MpCost = 12;
            SpCost = 3;
            if (Will.Instance.FighterStats.Level % 2 == 0)
            {
                SpellPower = Will.Instance.FighterStats.Level + 1f;
            }
            else
            {
                SpellPower = Will.Instance.FighterStats.Level;
            }
            //SpellPower = 7f;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;


            scytheAnimations = new List<CombatAnimation>()
            {
                new CombatAnimation()
                {
                    Name = "Scythe",
                    TextureName = "will_shadow_scythe",
                    Loop = false,
                    Animation = ScytheFrames,
                    DrawOffset =  new Vector2(-100,-100)
                }
            };

            foreach (CombatAnimation anim in scytheAnimations)
                anim.LoadContent(ContentPath.ToCombatCharacterTextures);
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            destinationPosition = targets[0].HitLocation;

            state = WillShadowAttackState.Phasing;
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
                case WillShadowAttackState.Phasing:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.SetAnimation("IdleNoScythe");
                        actor.CurrentAnimation.IsPaused = true;
                        scytheAnimations[0].Play();
                        SoundSystem.Play(AudioCues.WillSwoosh);
                        state = WillShadowAttackState.Striking;
                    }
                    break;

                case WillShadowAttackState.Striking:
                    if (scytheAnimations[0].IsRunning == false)
                    {
                        actor.CurrentAnimation.IsPaused = false;
                        actor.SetAnimation("ScytheWarpBack");
                        SoundSystem.Play(AudioCues.Stab);
                        DealMagicDamage(actor, targets.ToArray());
                        state = WillShadowAttackState.Returning;
                    }
                    break;

                case WillShadowAttackState.Returning:
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
                scytheAnimations[0].Draw(spriteBatch, destinationPosition, SpriteEffects.None);
            }
        }
    }
}

