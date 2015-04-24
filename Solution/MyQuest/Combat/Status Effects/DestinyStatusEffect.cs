using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class DestinyStatusEffect : StatusEffect
    {
        public VoodooDoll voodooDoll;

        #region Constructors

        public DestinyStatusEffect(VoodooDoll voodooDoll)
        {
            Name = "Destiny";
            IconName = "burn_icon";
            TurnDuration = 3;
            Probability = .75f;
            Removable = true;
            NegativeEffect = true;
            StatusEffectMessageColor = Color.Orange;
            this.voodooDoll = voodooDoll;
        }

        #endregion

        public override void OnStartTurn(FightingCharacter target)
        {
            if (voodooDoll.FighterStats.Health <= 0)
            {
                target.RemoveDestiny();
            }
            else
            {
                base.OnStartTurn(target);
                if (TurnsRemaining == 0)
                {
                    voodooDoll.destinyTarget = null;
                }
            }            
        }
    }
}
