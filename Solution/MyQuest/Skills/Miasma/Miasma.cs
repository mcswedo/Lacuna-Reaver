using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace MyQuest
{
    class Miasma : Skill
    {
        enum MiasmaState
        {
            Charging,
            NextTarget,
            Impact
        }

        #region Frame Animations

        static readonly FrameAnimation MiasmaFrames = new FrameAnimation()
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

        #region Combat Animations

        CombatAnimation MiasmaAnimation;

        #endregion
        
        #region Fields

        Vector2 screenPosition;

        MiasmaState state;
        int targetsHit;

        #endregion

        public Miasma()
        {
            Name = Strings.ZA663;
            Description = Strings.ZA664;

            MpCost = 30;
            SpCost = 8;

            SpellPower = 10; //15;
            DamageModifierValue = 0;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false; 

            TargetsAll = true;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            DrawOffset = new Vector2(-100, -100);

            MiasmaAnimation = new CombatAnimation()
            {
                Name = "Miasma",
                TextureName = "miasma",
                Animation = MiasmaFrames,
                Loop = false
            };

            MiasmaAnimation.LoadContent(ContentPath.ToSkillTextures);
        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            screenPosition = targets[0].ScreenPosition + DrawOffset;
            targetsHit = 0;            

            state = MiasmaState.Charging;
            actor.SetAnimation("Charge");
            SoundSystem.Play(AudioCues.Focus);
        }

        public override void Update(GameTime gameTime)
        {
            Debug.Assert(actor != null);
            switch (state)
            {
                case MiasmaState.Charging:
                        MiasmaAnimation.Play();
                        SoundSystem.Play(AudioCues.Cloud);
                        state = MiasmaState.NextTarget;
                    break;

                case MiasmaState.NextTarget:
                    MiasmaAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                    if (MiasmaAnimation.IsRunning == false)
                    {                        
                        StatusEffect blind = new Blindness(2);
                        StatusEffect weak = new Weakened();
                        state = MiasmaState.Impact;

                        SetStatusEffect(actor, blind, targets[targetsHit]);
                        SetStatusEffect(actor, weak, targets[targetsHit]);

                    }
                    break;

                case MiasmaState.Impact:
                    MiasmaAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                    targetsHit++;
                    if (targetsHit < targets.Count && MiasmaAnimation.IsRunning == false)
                    {
                        screenPosition = targets[targetsHit].HitLocation + DrawOffset;
                        MiasmaAnimation.Play();
                        SoundSystem.Play(AudioCues.Cloud);
                        state = MiasmaState.NextTarget;
                    }
                    else
                    {
                        //StatusEffect blind = new Blindness(2);
                        //StatusEffect weak = new Weakened();

                        //foreach (FightingCharacter target in targets)
                        //{
                        //    SetStatusEffect(actor, blind, target);
                        //    SetStatusEffect(actor, weak, target);
                        //}

                        DealMagicDamage(actor, targets.ToArray());
                        actor.SetAnimation("Idle");
                        isRunning = false;
                    }

                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (state == MiasmaState.NextTarget)
            {
                MiasmaAnimation.Draw(spriteBatch, screenPosition, SpriteEffects.None);
            }
        }
    }
}
