using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class FrostFlyderParalyze : Skill
    {
        #region Frame Animations

        static readonly FrameAnimation frostFrames = new FrameAnimation()
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


        #endregion

        CombatAnimation frostAnimation;

        public FrostFlyderParalyze()
        {
            Name = Strings.ZA437;
            Description = Strings.ZA438;

            MpCost = 21;
            SpCost = 3;

            SpellPower = 3;
            DamageModifierValue = 0;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;
            GrantsStatusEffect = true;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;
            DrawOffset = new Vector2(-100, -100);

            frostAnimation = new CombatAnimation()
            {
                Name = "Frost",
                TextureName = "frost",
                Loop = false,
                Animation = frostFrames
            };


            frostAnimation.LoadContent(ContentPath.ToSkillTextures);
        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);
            //SubtractCost(actor);
            frostAnimation.Play();
            actor.SetAnimation("Flap");
        }

        public override void Update(GameTime gameTime)
        {
            frostAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            if (frostAnimation.IsRunning == false)
            {
                foreach (FightingCharacter target in targets)
                {
                    if (target.State != State.Invulnerable)
                    {
                        StatusEffect paralyzed = new Paralyzed(2);
                        SetStatusEffect(actor, paralyzed, target);
                    }
                }

                actor.SetAnimation("Idle");
                isRunning = false;
            }
        }

        public override void OutOfCombatActivate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            //Debug.Assert(actor.Stats.Energy >= MpCost);

            if (actor.FighterStats.Energy >= MpCost)
            {
                actor.FighterStats.Energy -= MpCost;
                foreach (FightingCharacter target in targets)
                {

                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            frostAnimation.Draw(spriteBatch, targets[0].ScreenPosition + DrawOffset, SpriteEffects.None);
        }
    }
}
