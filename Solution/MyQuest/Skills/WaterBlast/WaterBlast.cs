using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{
    enum WaterBlastState
    {
        Charging,
        Bubbles
    }

    public class WaterBlast : Skill
    {
        #region Frame Animations

        static readonly FrameAnimation BubbleFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 260, 50, 50),
                new Rectangle(50, 260, 50, 50),
                new Rectangle(100, 260, 50, 50),
                new Rectangle(150, 260, 50, 50)
            }
        };

        #endregion

        #region Fields

        CombatAnimation bubbleAnimation;
        WaterBlastState state;
        Vector2 screenPosition;
        Vector2 destinationPosition;
        Vector2 velocity;
        SpriteEffects effect;

        #endregion

        #region Constructor

        public WaterBlast()
        {
            Name = Strings.ZA496;
            Description = Strings.ZA497;

            MpCost = 10;
            SpCost = 2;

            SpellPower = 2.5f;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            bubbleAnimation = new CombatAnimation()
            {
                Name = "Bubbles",
                TextureName = "old_feesh",
                Loop = true,
                Animation = BubbleFrames
            };
         
            bubbleAnimation.LoadContent(ContentPath.ToCombatCharacterTextures);
        }

        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            screenPosition = actor.ProjectileOrigin;

            destinationPosition = targets[0].HitLocation;

            effect = (destinationPosition.X < screenPosition.X ? SpriteEffects.FlipHorizontally : SpriteEffects.None);

            velocity = (destinationPosition - screenPosition);
            velocity.Normalize();
            velocity.X *= 7.5f;
            velocity.Y *= 7.5f;

            state = WaterBlastState.Charging;
            actor.SetAnimation("Charging");
        }

        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case WaterBlastState.Charging:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        state = WaterBlastState.Bubbles;
                        bubbleAnimation.Play();
                        actor.SetAnimation("Recoiling");
                    }
                    break;

                case WaterBlastState.Bubbles:
                    bubbleAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                    screenPosition += velocity;
                    
                    if(Vector2.Distance(screenPosition, destinationPosition) < velocity.Length())
                    {
                        screenPosition = destinationPosition;
                        SubtractCost(actor);
                        DealMagicDamage(actor, targets.ToArray());
                        isRunning = false;
                    }

                    // If recoiling is over, then go to idle.
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.SetAnimation("Idle");
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (state == WaterBlastState.Bubbles)
            {
                bubbleAnimation.Draw(spriteBatch, screenPosition + DrawOffset, effect);
            }
        }
    }
}
