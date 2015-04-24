using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class Shield : Skill
    {
        #region Frame Animations


        static readonly FrameAnimation ShieldFrames = new FrameAnimation()
        {
            FrameDelay = 0.15,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 150, 75),
                new Rectangle(150, 0, 150, 75),
                new Rectangle(300, 0, 150, 75),
                new Rectangle(450, 0, 150, 75),
                new Rectangle(600, 0, 150, 75),
                new Rectangle(750, 0, 150, 75),
                new Rectangle(900, 0, 150, 75),
                new Rectangle(1050, 0, 150, 75),
                new Rectangle(1200, 0, 150, 75),
                new Rectangle(1350, 0, 150, 75),
                new Rectangle(1500, 0, 150, 75),
                new Rectangle(1650, 0, 150, 75),
                new Rectangle(1800, 0, 150, 75),
                new Rectangle(0, 75, 150, 75),
                new Rectangle(150, 75, 150, 75),
                new Rectangle(300, 75, 150, 75),
                new Rectangle(450, 75, 150, 75),
                new Rectangle(600, 75, 150, 75),

            }
        };


        #endregion

        #region Combat Animations


        CombatAnimation shieldAnimation;


        #endregion

        public Shield()
        {
            Name = Strings.ZA475;
            Description = Strings.ZA476;

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
            CanTargetAllies = true;
            CanTargetEnemy = false;

            shieldAnimation = new CombatAnimation()
            {
                //Name = "HealthRingAnimation",
                Name = "ShieldAnimation",
                TextureName = "cara_armor_spell",
                Animation = ShieldFrames,
                Loop = false
            };

            shieldAnimation.LoadContent(ContentPath.ToSkillTextures);
        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            SoundSystem.Play(AudioCues.Shield);
            
            base.Activate(actor, targets);
            SubtractCost(actor);
            if (actor.Name == "Cara")
            {
                actor.SetAnimation("HealAttack");
            }
            shieldAnimation.Play();
        }

        public override void Update(GameTime gameTime)
        {
            shieldAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            if (shieldAnimation.IsRunning == false)
            {
                foreach (FightingCharacter target in targets)
                {
                    StatusEffect effect = new Armored(3);
                    SetStatusEffect(actor, effect, target);
                }
                if (actor.Name == "Cara")
                {
                    actor.SetAnimation("Idle");
                }
                isRunning = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (targets[0].Name.Equals(Party.cara))
            {
                shieldAnimation.Draw(spriteBatch, targets[0].ScreenPosition + new Vector2(15, -50), SpriteEffects.None);
            }
            else
            {
                shieldAnimation.Draw(spriteBatch, targets[0].ScreenPosition + new Vector2(-45, -50), SpriteEffects.None);
            }
        }
    }
}
