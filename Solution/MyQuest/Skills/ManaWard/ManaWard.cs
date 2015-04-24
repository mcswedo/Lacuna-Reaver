using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class ManaWard : Skill
    {
        #region Frame Animations


        static readonly FrameAnimation ManaWardFrames = new FrameAnimation()
        {
            FrameDelay = 0.15,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 75, 100),
                new Rectangle(75, 0, 75, 100),
                new Rectangle(150, 0, 75, 100),
                new Rectangle(225, 0, 75, 100),
                new Rectangle(300, 0, 75, 100),
                new Rectangle(375, 0, 75, 100),
                new Rectangle(450, 0, 75, 100),
                new Rectangle(525, 0, 75, 100),
                new Rectangle(600, 0, 75, 100),
                new Rectangle(675, 0, 75, 100),
                new Rectangle(750, 0, 75, 100),
                new Rectangle(900, 0, 75, 100),
                new Rectangle(975, 0, 75, 100),
                new Rectangle(1050, 0, 75, 100),
            }
        };


        #endregion

        #region Combat Animations


        CombatAnimation manaWardAnimation;


        #endregion

        public ManaWard()
        {
            Name = Strings.ZA425;
            Description = Strings.ZA426;

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

            manaWardAnimation = new CombatAnimation()
            {
                //Name = "HealthRingAnimation",
                Name = "ManaWardAnimation",
                TextureName = "cara_mana_shield",
                Animation = ManaWardFrames,
                Loop = false
            };

            manaWardAnimation.LoadContent(ContentPath.ToSkillTextures);
        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);
            SubtractCost(actor);
            manaWardAnimation.Play();
            actor.SetAnimation("AnalyzeAttack");
            SoundSystem.Play(AudioCues.Heal);
        }

        public override void Update(GameTime gameTime)
        {
            manaWardAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            if (manaWardAnimation.IsRunning == false)
            {
                foreach (FightingCharacter target in targets)
                {
                    StatusEffect effect = new Warded(3);
                    SetStatusEffect(actor, effect, target);
                }
                actor.SetAnimation("Idle");
                isRunning = false;
            }
        }

        public override void OutOfCombatActivate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            if (actor.FighterStats.Energy >= MpCost)
            {
                actor.FighterStats.Energy -= MpCost;
                foreach (FightingCharacter target in targets)
                {
                    target.FighterStats.Health += (int)CombatCalculations.RawMagicalDamage(actor.FighterStats, SpellPower);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (targets[0].Name.Equals(Party.cara))
            {
                manaWardAnimation.Draw(spriteBatch, targets[0].ScreenPosition + new Vector2(90, -50), SpriteEffects.None);
            }
            else
            {
                manaWardAnimation.Draw(spriteBatch, targets[0].ScreenPosition + new Vector2(30, -40), SpriteEffects.None);
            }
        }
    }
}
