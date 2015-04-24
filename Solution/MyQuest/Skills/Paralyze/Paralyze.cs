using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class Paralyze : Skill
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

        #region Combat Animations


        CombatAnimation paralyzeAnimation;


        #endregion

        public Paralyze()
        {
            Name = Strings.ZA439;
            Description = Strings.ZA440;

            MpCost = 15;
            SpCost = 3;

            SpellPower = 3;
            DamageModifierValue = 0;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;
            DrawOffset = new Vector2(-65, -105);

            paralyzeAnimation = new CombatAnimation()
            {
                Name = "ParalyzeAnimation",
                TextureName = "frost",
                Animation = paralyzeFrames,
                Loop = false
            };

            paralyzeAnimation.LoadContent(ContentPath.ToSkillTextures);
        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);
            SubtractCost(actor);
            paralyzeAnimation.Play();
            if (actor.Name == Party.will)
            {
                actor.SetAnimation("Glow");
            }
            SoundSystem.Play(AudioCues.Cloud);
        }

        public override void Update(GameTime gameTime)
        {
            paralyzeAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            if (paralyzeAnimation.IsRunning == false)
            {
                foreach (FightingCharacter target in targets)
                {
                    if (target.State != State.Invulnerable)
                    {
                        StatusEffect paralyzed = new Paralyzed(2);
                        SetStatusEffect(actor, paralyzed, target);
                    }
                }
                if (actor.Name == Party.will)
                {
                    actor.SetAnimation("Idle");
                }
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
            paralyzeAnimation.Draw(spriteBatch, targets[0].ScreenPosition + DrawOffset, SpriteEffects.None);
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
