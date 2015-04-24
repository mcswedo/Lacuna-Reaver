using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class SpikeRain : Skill
    {
        public SpikeRain()
        {
            Name = Strings.ZA484;
            Description = Strings.ZA485;

            MpCost = 100;
            SpCost = 5;

            SpellPower = 0;
            DamageModifierValue = 0;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = false;

            TargetsAll = true;
            CanTargetAllies = false;
            CanTargetEnemy = true;
        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);
            SubtractCost(actor);

            actor.SetAnimation("SpikeRain");
            SoundSystem.Play(AudioCues.IceStorm);
        }

        public override void Update(GameTime gameTime)
        {
            if (actor.CurrentAnimation.IsRunning == false)
            {
                isRunning = false;

                DealPhysicalDamage(actor, targets.ToArray());

                actor.SetAnimation("Idle");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}