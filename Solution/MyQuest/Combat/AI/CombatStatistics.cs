using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyQuest
{
    public class CombatStatistics
    {
        #region Fields
        
        int damageDealtLastTurn;

        /// <summary>
        /// The amount of damage the character dealt on their last turn
        /// </summary>
        public int DamageDealtLastTurn
        {
            get { return damageDealtLastTurn; }
            set { damageDealtLastTurn = value; }
        }

        int totalDamageDealt;

        /// <summary>
        /// The total ammount of damage the character has dealt over the course of the battle
        /// </summary>
        public int TotalDamageDealt
        {
            get { return totalDamageDealt; }
            set { totalDamageDealt = value; }
        }

        int healingDoneLastTurn;

        /// <summary>
        /// The amount of healing done by the character on their last turn
        /// </summary>
        public int HealingDoneLastTurn
        {
            get { return healingDoneLastTurn; }
            set { healingDoneLastTurn = value; }
        }

        int totalHealingDone;
        
        /// <summary>
        /// The ammount of healing done over the course of the battle
        /// </summary>
        public int TotalHealingDone
        {
            get { return totalHealingDone; }
            set { totalHealingDone = value; }
        }

        int healingReceivedLastTurn;

        /// <summary>
        /// The ammount of healing received last turn
        /// </summary>
        public int HealingReceivedLastTurn
        {
            get { return healingReceivedLastTurn; }
            set { healingReceivedLastTurn = value; }
        }

        int damageReceivedLastTurn;

        /// <summary>
        /// The ammount of damage dealt to the character last turn by all sources
        /// </summary>
        public int DamageReceivedLastTurn
        {
            get { return damageReceivedLastTurn; }
            set { damageReceivedLastTurn = value; }
        }

        int totalDamageReceived;

        /// <summary>
        /// The ammount of damage dealt to the character over the course of the battle
        /// </summary>
        public int TotalDamageReceived
        {
            get { return totalDamageReceived; }
            set { totalDamageReceived = value; }
        }

        bool performedHealing;

        /// <summary>
        /// True if the character has healed another during the course of battle
        /// </summary>
        public bool PerformedHealing
        {
            get { return performedHealing; }
            set { performedHealing = value; }
        }

        bool killedAnEnemy;

        /// <summary>
        /// True if the character killed an NPC's ally during battle
        /// </summary>
        public bool KilledAnEnemy
        {
            get { return killedAnEnemy; }
            set { killedAnEnemy = value; }
        }

        int round;

        /// <summary>
        /// The current combat round
        /// </summary>
        public int Round
        {
            get { return round; }
            set { round = value; }
        }

        FightingCharacter lastTarget;

        //The last character that was targeted.
        public FightingCharacter LastTarget
        {
            get { return lastTarget; }
            set { lastTarget = value; }
        }

        #endregion

        #region Constructor

        public CombatStatistics()
        {
            damageDealtLastTurn = 0;
            totalDamageDealt = 0;
            damageReceivedLastTurn = 0;
            totalDamageReceived = 0;
            healingDoneLastTurn = 0;
            totalHealingDone = 0;
            totalHealingDone = 0;
            performedHealing = false;
            killedAnEnemy = false;
            round = 1;
        }

        #endregion
    }
}
