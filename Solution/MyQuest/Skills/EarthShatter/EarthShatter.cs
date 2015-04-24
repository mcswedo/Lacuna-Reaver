using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace MyQuest
{
    public class EarthShatter : Skill
    {
        #region Frame Animations

        static readonly FrameAnimation ImpactFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 600, 250),
                new Rectangle(600, 0, 600, 250),
                new Rectangle(1200, 0, 600, 250),
                new Rectangle(0, 250, 600, 250),
                new Rectangle(600, 250, 600, 250),
                new Rectangle(1200, 250, 600, 250),
                new Rectangle(0, 500, 600, 250),
                new Rectangle(600, 500, 600, 250),
                new Rectangle(1200, 500, 600, 250),
                new Rectangle(0, 750, 600, 250),
                new Rectangle(600, 750, 600, 250),
                new Rectangle(1200, 750, 600, 250),
                new Rectangle(0, 1000, 600, 250),
                new Rectangle(600, 1000, 600, 250),
                new Rectangle(1200, 1000, 600, 250),
            }
        };


        #endregion

        #region Fields

        bool damageApplied;

        CombatAnimation earthShatterAnimation;
        Vector2 screenPosition;
        Vector2 destinationPosition;

        #endregion

        #region Constructor


        public EarthShatter()
        {
            Name = Strings.ZA374;
            Description = Strings.ZA375;

            MpCost = 40;
            SpCost = 5;
            if (Cara.Instance.FighterStats.Level < 6)
            {
                SpellPower = 4.8f;
            }
            else
            {
                SpellPower = Cara.Instance.FighterStats.Level;
            }
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            earthShatterAnimation = new CombatAnimation()
                {
                    Name = "Impact",
                    TextureName = "cara_earth_shatter",
                    Loop = false,
                    Animation = ImpactFrames,
                    DrawOffset = new Vector2(-220, -135)
                };

                earthShatterAnimation.LoadContent(ContentPath.ToSkillTextures);
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            screenPosition = actor.ScreenPosition;

            destinationPosition = targets[0].HitLocation;

            damageApplied = false;

            earthShatterAnimation.Play();
            actor.SetAnimation("EarthAttack");
            SoundSystem.Play(AudioCues.EarthShatter);
        }

        public override void Update(GameTime gameTime)
        {
            Debug.Assert(isRunning);
            if (earthShatterAnimation.IsRunning)
            {
                earthShatterAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                return;
            }
            if (!damageApplied)
            {
                DealMagicDamage(actor, targets.ToArray());
                isRunning = false;
                actor.SetAnimation("Idle");
                damageApplied = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (earthShatterAnimation.IsRunning)
            {
                earthShatterAnimation.Draw(spriteBatch, destinationPosition, SpriteEffects.None);
            }
        }
    }
}
