using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class PossessedBookAttack : Attack
    {

        static readonly FrameAnimation TravelingFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0,   0, 180, 150),
            }
        };

        CombatAnimation paperAnimation;
        Vector2 screenPosition;

        public PossessedBookAttack() : base()
        {
            Name = Strings.ZA329;
            Description = Strings.ZA345;

            MpCost = 0;
            SpCost = 0;

            SpellPower = 0.0f;
            DamageModifierValue = 1.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = true;

            DrawOffset = new Vector2(-90, -70);

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;


            paperAnimation = new CombatAnimation()
               {
                   Name = "Traveling1",
                   TextureName = "possessed_book_projectile",
                   Loop = true,
                   Animation = TravelingFrames
               };

            paperAnimation.LoadContent(ContentPath.ToSkillTextures);
        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            initialPosition = actor.ScreenPosition + new Vector2(80, 80);

            destinationPosition = targets[0].HitLocation + DrawOffset;

            screenPosition = initialPosition;

            //targets[0].ScreenPosition + new Vector2(-50, -10);


            velocity =
                (destinationPosition - actor.ScreenPosition) / (float)TimeSpan.FromSeconds((1)).TotalMilliseconds;

            actor.SetAnimation("Attack");

            paperAnimation.Play(); 
        }

        public override void Update(GameTime gameTime)
        {
            paperAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            screenPosition += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (actor.CurrentAnimation.IsRunning == false)
            {
                actor.SetAnimation("Idle");
               
            }

            if (screenPosition.X <= destinationPosition.X)
            {

                DealPhysicalDamage(actor, targets.ToArray());

                SoundSystem.Play(AudioCues.Stab);

                isRunning = false;
            }
         
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
           paperAnimation.Draw(spriteBatch, screenPosition + DrawOffset, SpriteEffects.None);
        }
    }
}
