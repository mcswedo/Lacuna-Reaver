using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{
    public class Poison : Skill
    {
       #region Frame Animations

       static readonly FrameAnimation paralyzeFrames = new FrameAnimation()
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



        CombatAnimation poisonAnimation;


        #endregion

        #region Constructor


        public Poison()
        {
            Name = Strings.ZA445;
            Description = Strings.ZA446;

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

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            DrawOffset = new Vector2(-65, -105);

            poisonAnimation = new CombatAnimation()
            {
                Name = "PoisonAnimation",
                TextureName = "poison",
                Animation = paralyzeFrames,
                Loop = false
            };

            poisonAnimation.LoadContent(ContentPath.ToSkillTextures);
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);
            SubtractCost(actor);
            poisonAnimation.Play();
            if (actor.Name == Party.will)
            {
                SoundSystem.Play(AudioCues.Cloud);
                actor.SetAnimation("Glow");
            }
            else
            {
                actor.CurrentAnimation.IsPaused = true;
            }
           
        }

        public override void Update(GameTime gameTime)
        {
              poisonAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

              if (poisonAnimation.IsRunning == false)
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
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            poisonAnimation.Draw(spriteBatch, targets[0].ScreenPosition + DrawOffset, SpriteEffects.None);
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
