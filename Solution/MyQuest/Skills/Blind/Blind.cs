using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MyQuest
{
    public class Blind : Skill
    {
        #region Frame Animations


        static readonly FrameAnimation BlindingFrames = new FrameAnimation()
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


        CombatAnimation blindnessAnimation;


        #endregion

        #region Fields

        Vector2 screenPosition;

        #endregion

        public Blind()
        {
            Name = Strings.ZA355;
            Description = Strings.ZA356;

            MpCost = 15;
            SpCost = 3;

            SpellPower = 0;
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

            DrawOffset = new Vector2(-65, -105);

            blindnessAnimation = new CombatAnimation()
            {
                Name = "blindAnimation",
                TextureName = "blind",
                Animation = BlindingFrames,
                Loop = false
            };

            blindnessAnimation.LoadContent(ContentPath.ToSkillTextures);
        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            screenPosition = targets[0].ScreenPosition;

            blindnessAnimation.Play();
            if (actor.Name == Party.will)
            {
                SoundSystem.Play(AudioCues.Cloud);
                actor.SetAnimation("Glow");
            }
       
        }

        public override void Update(GameTime gameTime)
        {
            blindnessAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            if (blindnessAnimation.IsRunning == false)
            {
                isRunning = false;
                if (actor.Name == Party.will)
                {
                    actor.SetAnimation("Idle");
                }
                foreach (FightingCharacter target in targets)
                {
                    StatusEffect effect = new Blindness(3);
                    SetStatusEffect(actor, effect, target);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            blindnessAnimation.Draw(spriteBatch, screenPosition + DrawOffset, SpriteEffects.None);
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
