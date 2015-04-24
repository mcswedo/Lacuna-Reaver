using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class CaveCrabAttack : Attack
    {
        #region Constructor

        #region Frame Animations

        static readonly FrameAnimation PillarFrames = new FrameAnimation()
     {
         FrameDelay = 0.09,
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

                new Rectangle(0, 410, 200, 200),
                new Rectangle(200, 410, 200, 200),
                new Rectangle(400, 410, 200, 200),
                new Rectangle(600, 410, 200, 200),
                new Rectangle(800, 410, 200, 200),
            }
     };

        #endregion

        CombatAnimation glacierAnimation;
        Vector2 screenPosition;
        public CaveCrabAttack()
        {
            Name = Strings.ZA329;
            Description = Strings.ZA524;

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

            DrawOffset = new Vector2(-50, 0);

            glacierAnimation = new CombatAnimation()
            {
                Name = "Pillar",
                TextureName = "pillar",
                Loop = false,
                Animation = PillarFrames,
                DrawOffset = new Vector2(0, -80)
            };

            glacierAnimation.LoadContent(ContentPath.ToSkillTextures);

        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            initialPosition = actor.ScreenPosition;

            destinationPosition = targets[0].HitLocation + DrawOffset;

            screenPosition = destinationPosition;
            glacierAnimation.Play();
            actor.SetAnimation("Attack");
        }

        public override void Update(GameTime gameTime)
        {
            glacierAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            if (actor.CurrentAnimation.IsRunning == false)
            {
                actor.SetAnimation("Idle");
            }

            if (glacierAnimation.IsRunning == false)
            {
                DealPhysicalDamage(actor, targets.ToArray());
                SoundSystem.Play(AudioCues.SwordHitArmor);
                actor.SetAnimation("Idle");
                isRunning = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            glacierAnimation.Draw(spriteBatch, screenPosition + DrawOffset, SpriteEffects.None);

        }
    }
}