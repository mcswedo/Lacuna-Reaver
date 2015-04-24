using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

/// FC State bug: I go into a Stunned state and choose Defend as my action, my state becomes Defend.

namespace MyQuest
{
    /// <summary>
    /// Describes the actions a FightingCharacter may choose
    /// </summary>
    public enum FighterAction
    {
        /// <summary>
        /// An action has yet to be chosen
        /// </summary>
        Deciding,
        Skill,
        Item,
        Defend,
        Run
    }

    /// <summary>
    /// Controls a given FightingCharacter for one turn of combat. 
    /// Once that turn is complete, the TurnComplete flag is set.
    /// </summary>
    public class TurnExecutor
    {
        /// <summary>
        /// The number of stamina points rewarded to the character regardless of what they choose during their last turn
        /// </summary>
        const int defaultStaminaReward = 1;

        bool runningEnabled = true;

        #region Singleton


        static TurnExecutor singleton = new TurnExecutor();

        public static TurnExecutor Singleton
        {
            get { return singleton; }
        }

        private TurnExecutor()
        {
            turnComplete = true;
        }


        #endregion

        #region FightingCharacters


        int currentFighterIndex;

        public int CurrentFighterIndex
        {
            get { return currentFighterIndex; }
        }


        List<FightingCharacter> fighters;

        /// <summary>
        /// The list fighters in order of initiative
        /// </summary>
        public List<FightingCharacter> Fighters
        {
            get { return fighters; }
        }


        List<FightingCharacter> pcFighters;
        
        /// <summary>
        /// List of PC characters sorted by screen position
        /// </summary>
        public List<FightingCharacter> PCFighters
        {
            get { return pcFighters; }
        }


        List<FightingCharacter> npcFighters;
        
        /// <summary>
        /// List of NPC characters sorted by screen position
        /// </summary>
        public List<FightingCharacter> NPCFighters
        {
            get { return npcFighters; }
        }


        #endregion

        #region State and Status


        FighterAction action;

        /// <summary>
        /// The action chosen by the current fighter
        /// </summary>
        public FighterAction Action
        {
            get { return action; }
            set {
                    action = value;

                    if (action == FighterAction.Defend)
                    {
                        fighters[currentFighterIndex].SetState(State.Defending); //CHECK THIS LOGIC!!!
                    }
                    else if (action == FighterAction.Run)
                    {
                        AttemptRun();
                    }
                }
        }


        Skill activeSkill;

        /// <summary>
        /// The skill, if any, chosen by the current fighter
        /// </summary>
        public Skill ActiveSkill
        {
            get { return activeSkill; }
            set { activeSkill = value; }
        }

        List<int> pcTargets = new List<int>();
        List<int> npcTargets = new List<int>();
        List<int> itemTargets = new List<int>();

        public List<int> PCTargets
        {
            get { return pcTargets; }
        }

        public List<int> NPCTargets
        {
            get { return npcTargets; }
        }

        public List<int> ItemTargets
        {
            get { return itemTargets; }
        }


        bool turnComplete;

        /// <summary>
        /// Determines whether the current turn has ended
        /// </summary>
        public bool TurnComplete
        {
            get { return turnComplete; }
        }

        bool finalBattle = false;

        public bool FinalBattle
        {
            set { finalBattle = value; }
        }


        bool partyRetreatSuccessful;

        /// <summary>
        /// Determines whether or not the party has successfully ran from combat
        /// </summary>
        public bool PartyRetreatSuccessful
        {
            get { return partyRetreatSuccessful; }
        }


        #endregion

        #region Methods

        public void Initialize(
            List<FightingCharacter> fighters,
            List<FightingCharacter> pcFighters,
            List<FightingCharacter> npcFighters,
            bool runningEnabled)
        {
            this.pcFighters = pcFighters;
            this.npcFighters = npcFighters;
            this.fighters = fighters;
            this.runningEnabled = runningEnabled;
            turnComplete = true;
            partyRetreatSuccessful = false;
        }

        public void StartTurn(int currentFighterIndex)
        {
            Debug.Assert(turnComplete == true);
            turnComplete = false;
            bool currentFighterIsDead = false;
            
            Debug.Assert(fighters != null && currentFighterIndex >= 0);

            this.currentFighterIndex = currentFighterIndex;
            activeSkill = null;
            FightingCharacter currentFighter = fighters[currentFighterIndex];

            currentFighter.OnStartTurn();

            Debug.Assert(currentFighter.State != State.Dead);

            //Check to see if any enemy has died from Poison or the burning status effects.
            foreach (FightingCharacter fighter in fighters)
            {
                //Debug.Assert(fighter.State != State.Dead);
                if (fighter.FighterStats.Health <= 0)
                {
                    if (fighter.HasDestiny())
                    {
                        fighter.RemoveDestiny();
                    }
                    fighter.SetState(State.Dead);

                    //If current fighter died set currentFighterIsDead and turnComplete to true.
                    if (fighter == currentFighter)
                    {
                        currentFighterIsDead = true;
                        turnComplete = true;
                    }                    
                }
                //Once we've iterated through everybody and the current fighter died from status effects, then return.
                

                //if (currentFighter.FighterStats.Health <= 0)
                //{
                //    if (currentFighter.HasDestiny())
                //    {
                //        currentFighter.RemoveDestiny();
                //    }
                //    currentFighter.SetState(State.Dead);
                //    turnComplete = true;
                //    return;
                //}
            }
            if (currentFighterIsDead)
            {
                return;
            }

            action = FighterAction.Deciding;

            if (currentFighter.State == State.Paralyzed)
            {
                turnComplete = true;
                return;
            }

            //If we were defending in the previous round, then we get a stamina bonus when we start a new round. This is now done in the defending status effect.
            if (currentFighter.State == State.Defending)
            {
                currentFighter.SetState(State.Normal);                
            }
            else
            {
                currentFighter.FighterStats.Stamina += defaultStaminaReward;
            }

            if (currentFighter is NPCFightingCharacter)
            {
                NPCFightingCharacter npcFighter = (NPCFightingCharacter)currentFighter;
                npcFighter.AIController.PerformAction(npcFighter);
            }
            else
            {
                CombatMenuScreen combatMenuScreen = new CombatMenuScreen((PCFightingCharacter)currentFighter, runningEnabled);
                combatMenuScreen.FinalBattle = finalBattle;

                ScreenManager.Singleton.AddScreen(combatMenuScreen);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (turnComplete)
            {
                // I think this means the current fighter is dead.
                // In this case, let CombatScreen update method set the next fighter.
                return;
            }

            switch (action)
            {
                case FighterAction.Skill:
                    Debug.Assert(activeSkill != null);
                    activeSkill.Update(gameTime); 
                    if (!activeSkill.IsRunning)
                    {
                        activeSkill = null;
                        turnComplete = true;
                        RemoveVigorOrFocus();
                    }                    
                    break;

                case FighterAction.Item:
                    turnComplete = true;
                    RemoveVigorOrFocus();
                    break;

                case FighterAction.Defend:
                    turnComplete = true;
                    RemoveVigorOrFocus();
                    break;

                case FighterAction.Run:
                    turnComplete = true;
                    RemoveVigorOrFocus();
                    break;

                case FighterAction.Deciding:
                    break;
            }
        }

        private void RemoveVigorOrFocus()
        {
            FightingCharacter currentFighter = fighters[currentFighterIndex];

            for (int i = currentFighter.statusEffects.Count - 1; i >= 0; i--)
            {
                StatusEffect effect = currentFighter.statusEffects[i];

                if (effect.Name.Equals("Envigored"))
                {
                    if (effect.TurnsRemaining == 1)
                    {
                        effect.OnStartTurn(currentFighter);                        
                    }
                }
                if (effect.Name.Equals("Focused"))
                {
                    if (effect.TurnsRemaining == 1)
                    {
                        effect.OnStartTurn(currentFighter);
                    }
                }
            }
        }

        private void AttemptRun()
        {
            int npcLevel = 0;
            int pcLevel = 0;
            int pcCount = 0;
            int npcCount = 0;

            foreach (FightingCharacter fighter in fighters)
            {
                if (fighter is NPCFightingCharacter)
                {
                    npcCount++;
                    npcLevel += fighter.FighterStats.Level;
                }
                else
                {
                    pcCount++;
                    pcLevel += fighter.FighterStats.Level;
                }
            }

            if (CombatCalculations.RunSuccessful(pcLevel / pcCount, npcLevel / npcCount, pcCount))
            {
                partyRetreatSuccessful = true;
            }
            else
            {
                CombatMessage.AddMessage("Run Failed", new Vector2(400, 300), Color.Black, 1.5);
            }
        }

        public int GetNextNPC(int startIndex, bool livingOnly)
        {
            return GetNextFighter(npcFighters, startIndex, livingOnly);
        }

        public int GetNextPC(int startIndex, bool livingOnly)
        {
            return GetNextFighter(pcFighters, startIndex, livingOnly);
        }

        public int GetPreviousNPC(int startIndex, bool livingOnly)
        {
            return GetPreviousFighter(npcFighters, startIndex, livingOnly);
        }

        public int GetPreviousPC(int startIndex, bool livingOnly)
        {
            return GetPreviousFighter(pcFighters, startIndex, livingOnly);
        }

        int GetNextFighter(List<FightingCharacter> characters, int startIndex, bool livingOnly)
        {
            for (int i = startIndex; i < characters.Count; ++i)
            {
                if (livingOnly == true)
                {
                    if (characters[i].State != State.Dead)
                        return i;
                }
                else
                {
                    return i;
                }
            }

            return GetNextFighter(characters, 0, livingOnly);
        }

        int GetPreviousFighter(List<FightingCharacter> characters, int startIndex, bool livingOnly)
        {
            for (int i = startIndex; i >= 0; --i)
            {
                if (livingOnly == true)
                {
                    if (characters[i].State != State.Dead)
                        return i;
                }
                else
                {
                    return i;
                }
            }

            return GetPreviousFighter(characters, characters.Count - 1, livingOnly);
        }
        #endregion
    }
}
