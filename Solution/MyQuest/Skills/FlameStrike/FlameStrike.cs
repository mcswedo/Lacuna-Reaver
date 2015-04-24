using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{
    enum FlameStrikeState
    {
        Phasing,
        Striking,
        Returning,
    }

    public class FlameStrike : Skill
    {

       #region Frame Animations

        static readonly FrameAnimation fireCloudFrames = new FrameAnimation()
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

        #region Fields

        List<CombatAnimation> flameStrikeAnimations;
        CombatAnimation fireCloudAnimation;

        FlameStrikeState state;

        Vector2 destinationPosition;

        #endregion

        #region Constructor


        public FlameStrike() //will's FlameLance
        {
            Name = Strings.ZA395;
            Description = Strings.ZA396; 

            MpCost = 50;
            SpCost = 5;

            if (Will.Instance.FighterStats.Level <= 20)
            {
                SpellPower = Will.Instance.FighterStats.Level + 2.5f;//13.0f;
            }
            else
            {
                SpellPower = Will.Instance.FighterStats.Level + 5f;
            }
            DamageModifierValue = 1.2f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            flameStrikeAnimations = new List<CombatAnimation>()
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

            foreach (CombatAnimation anim in flameStrikeAnimations)
                anim.LoadContent(ContentPath.ToCombatCharacterTextures);

            fireCloudAnimation = new CombatAnimation()
            {
                Name = "Fire",
                TextureName = "red_cloud",
                Loop = false,
                Animation = fireCloudFrames
            };


            fireCloudAnimation.LoadContent(ContentPath.ToSkillTextures);

            DrawOffset = new Vector2(-100, -100);
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            destinationPosition = targets[0].HitLocation;

            state = FlameStrikeState.Phasing;
            actor.SetAnimation("ScytheWarp");
        }

        public override void Update(GameTime gameTime)
        {

            if (flameStrikeAnimations[0].IsRunning == true)
            {
                flameStrikeAnimations[0].Update(gameTime.ElapsedGameTime.TotalMilliseconds);
            }

            if (state == FlameStrikeState.Returning)
            {
                fireCloudAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
            }
            switch (state)
            {
                case FlameStrikeState.Phasing:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.SetAnimation("IdleNoScythe");
                        actor.CurrentAnimation.IsPaused = true;
                        flameStrikeAnimations[0].Play();
                        SoundSystem.Play(AudioCues.WillSwoosh);
                        state = FlameStrikeState.Striking;
                    }
                    break;

                case FlameStrikeState.Striking:
                    if (flameStrikeAnimations[0].IsRunning == false)
                    {
                        actor.CurrentAnimation.IsPaused = false;
                        actor.SetAnimation("ScytheWarpBack");
                        SoundSystem.Play(AudioCues.Stab);
                        DealMagicDamage(actor, targets.ToArray());
                        fireCloudAnimation.Play();
                        SoundSystem.Play(AudioCues.Cloud);
                        state = FlameStrikeState.Returning;
                    }
                    break;

                case FlameStrikeState.Returning:

                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.SetAnimation("Idle");                       
                    }

                    if (fireCloudAnimation.IsRunning == false)
                    {
                        isRunning = false;
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (flameStrikeAnimations[0].IsRunning)
            {
                flameStrikeAnimations[0].Draw(spriteBatch, destinationPosition, SpriteEffects.None);
            }

            if (state == FlameStrikeState.Returning)
            {
                if (fireCloudAnimation.IsRunning)
                {
                    fireCloudAnimation.Draw(spriteBatch, targets[0].ScreenPosition + DrawOffset, SpriteEffects.None);
                }
            }
       }
    }
}
