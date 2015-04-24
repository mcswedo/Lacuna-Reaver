using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class Burning : StatusEffect
    {
        #region Constructors

        public Burning()
        {
            Name = "Burning";
            IconName = "burn_icon";
            //RoundDuration = 10;
            TurnDuration = 999;
            Probability = 1f;
            //FlatDamage = 0;
            //PersistsOutOfCombat = false;
            Removable = true;
            //AttributeModifier = false;
            NegativeEffect = true;
            StatusEffectMessageColor = Color.Orange;
            //SkillName = "Poison";
        }

        #endregion

        public override void OnActivateEffect(FightingCharacter target)
        {
            base.OnActivateEffect(target);
        }

        public override void OnStartTurn(FightingCharacter target)
        {
            base.OnStartTurn(target);

            //Enemy has the status effect. Damage Playable characters only.
            if (target is TippersCombat)
            {
                foreach (PCFightingCharacter pcFighter in TurnExecutor.Singleton.PCFighters)
                {
                    if (pcFighter.State != State.Dead)
                    {
                        if (pcFighter.State != State.Invulnerable)
                        {
                            pcFighter.FighterStats.Health -= 100;
                            pcFighter.FighterStats.Health = (int)MathHelper.Clamp(pcFighter.FighterStats.Health, 0, pcFighter.FighterStats.ModifiedMaxHealth);
                            CombatMessage.AddMessage(100.ToString(), pcFighter.DamageMessagePosition, Color.DarkOrange, .5);
                        }
                        else
                        {
                            CombatMessage.AddMessage(0.ToString(), pcFighter.DamageMessagePosition, Color.DarkOrange, .5);
                        }
                    }
                }
            }
            //Player is burned. Damage PC characters for extra damage
            else
            {
                if (this.TurnDuration >= 995)
                {
                    this.TurnDuration = 4;
                }

                foreach (PCFightingCharacter pcFighter in TurnExecutor.Singleton.PCFighters)
                {
                    if (pcFighter.State != State.Dead)
                    {
                        if (pcFighter.State != State.Invulnerable)
                        {
                            pcFighter.FighterStats.Health -= 500;
                            pcFighter.FighterStats.Health = (int)MathHelper.Clamp(pcFighter.FighterStats.Health, 0, pcFighter.FighterStats.ModifiedMaxHealth);
                            
                            CombatMessage.AddMessage(500.ToString(), pcFighter.DamageMessagePosition, Color.DarkOrange, .5);
                        }
                        else
                        {
                            CombatMessage.AddMessage(0.ToString(), pcFighter.DamageMessagePosition, Color.DarkOrange, .5);
                        }
                    }
                }
            }
        }
    }
}
