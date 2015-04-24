using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class ChepetawaAttack : Attack
    {
        #region Frame Animations

        static readonly FrameAnimation ImpactFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 400, 400),
                new Rectangle(400, 0, 400, 400),
                new Rectangle(800, 0, 400, 400),
                new Rectangle(1200, 0, 400, 400),
                new Rectangle(1600, 0, 400, 400),
                new Rectangle(0, 400, 400, 400),
                new Rectangle(400, 400, 400, 400),
                new Rectangle(800, 400, 400, 400),
                new Rectangle(1200, 400, 400, 400),
                new Rectangle(1600, 400, 400, 400),
            }
        };


        #endregion

        #region Fields

        CombatAnimation lightningAnimation;
        Vector2 screenPosition;

        #endregion

        #region Constructor


        public ChepetawaAttack()
        {
            Name = Strings.ZA329;
            Description = Strings.ZA335;

            MpCost = 0;
            SpCost = 0;

            SpellPower = 7.5f;
            DamageModifierValue = 0.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = true;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            lightningAnimation = new CombatAnimation()
                {
                    Name = "Impact",
                    TextureName = "cara_lightning",
                    Loop = false,
                    Animation = ImpactFrames,
                    DrawOffset = new Vector2(-205, -295)
                };

            lightningAnimation.LoadContent(ContentPath.ToSkillTextures);
        }

        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            //SubtractCost(actor);

            screenPosition = actor.ScreenPosition + new Vector2(20, 30);

            destinationPosition = targets[0].HitLocation;

            CombatAnimation dash = actor.GetAnimation("Dash");

            state = AttackState.Striking;

            screenPosition = destinationPosition;
           
            actor.SetAnimation("Attack");          
        }

        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case AttackState.Striking:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        lightningAnimation.Play();
                        SoundSystem.Play(AudioCues.ScreenFlashCrack);  
                        actor.SetAnimation("Idle");
                        state = AttackState.Traveling;
                    }
                    break;
                case AttackState.Traveling:
                    {
                        lightningAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                        if (lightningAnimation.IsRunning == false)
                        {
                            DealMagicDamage(actor, targets.ToArray());
                            isRunning = false;
                        }
                        break;

                    }
            }
                             
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (state == AttackState.Traveling)
            {
                lightningAnimation.Draw(spriteBatch, destinationPosition, SpriteEffects.None);
            }
        }
    }
}

