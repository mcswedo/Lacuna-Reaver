using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class Plague : Skill
    {
 
        #region Frame Animations


        static readonly FrameAnimation plagueFrames = new FrameAnimation()
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

        #region Fields


        CombatAnimation plagueAnimation;
        StrikeState state;
        int targetsHit;
        Vector2 destinationPosition;

        #endregion

        #region Constructor


        public Plague()
        {
            Name = Strings.ZA441;
            Description = Strings.ZA442;

            MpCost = 20;
            SpCost = 4;

            SpellPower = 0f;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;
            GrantsStatusEffect = true;

            TargetsAll = true;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            DrawOffset = new Vector2(-65, -105);

            plagueAnimation = new CombatAnimation()
            {
                Name = "PlagueAnimation",
                TextureName = "plague",
                Animation = plagueFrames,
                Loop = false
            };

            plagueAnimation.LoadContent(ContentPath.ToSkillTextures);
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);
            SubtractCost(actor);

            destinationPosition = targets[0].HitLocation + DrawOffset;

            targetsHit = 0;

            state = StrikeState.Traveling;
            plagueAnimation.Play();
            if (actor.Name == Party.will)
            {
                actor.SetAnimation("Glow");
            }
            else
            {
                actor.CurrentAnimation.IsPaused = true;
            }
            SoundSystem.Play(AudioCues.Cloud);
        }

        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case StrikeState.Traveling:
                    plagueAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                    if (plagueAnimation.IsRunning == false)
                    {                      
                        state = StrikeState.Striking;
                    }
                    break;

                case StrikeState.Striking:
                        targetsHit++;
                        if (targetsHit < targets.Count)
                        {
                            destinationPosition = targets[targetsHit].HitLocation + DrawOffset;
                            plagueAnimation.Play();
                            SoundSystem.Play(AudioCues.Cloud);
                            state = StrikeState.Traveling;
                        }

                        else
                        {
                            foreach (FightingCharacter target in targets)
                            {

                                StatusEffect poisoned = new Poisoned();

                                SetStatusEffect(actor, poisoned, target);
                                
                            }
                            if (actor.Name == Party.will)
                            {
                                actor.SetAnimation("Idle");
                            }
                            else
                            {
                                actor.CurrentAnimation.IsPaused = false;
                            }
                            isRunning = false;
                        }
                  
                    break;
            }
        }
  
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (state == StrikeState.Traveling)
            {
                plagueAnimation.Draw(spriteBatch, destinationPosition, SpriteEffects.None);
            }
        }

        protected override void SetStatusEffect(FightingCharacter actor, StatusEffect effect, FightingCharacter target)
        {
            if (target is Boggimus || target is Chepetawa || target is Serlynx ||
                target is Arlan || target is Malticar)
            {
                CombatMessage.AddMessage("Immune", target.StatusEffectMessagePosition, .5);
            }
            else
            {
                base.SetStatusEffect(actor, effect, target);
            }
        }
    }
}
