using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MyQuest
{
    public enum AIType
    {
        Simple, //Stupid AI, randomly chooses a skill and targets.
        Knowledgeable, //AI can use details about itself and its party
        Boss, //AI is a boss character
        AlwaysDefend // This is for setting shield offsets.
    }

    public enum ActionType
    {
        Idle,
        Heal,
        Skill,
        Defend,
    }

    public class CombatAIController
    {
        #region Fields

        protected NPCFightingCharacter npcFighter = null;
        protected List<FightingCharacter> pcTargets = new List<FightingCharacter>();
        protected List<FightingCharacter> npcTargets = new List<FightingCharacter>();
        protected List<Skill> healingSkills = new List<Skill>();
        protected List<Skill> useableSkills = new List<Skill>();
        protected List<Skill> nonUseableSkills = new List<Skill>();
        protected List<Skill> skillList = new List<Skill>();
        protected Skill selectedSkill = null;
        protected ActionType action = ActionType.Idle;

        #endregion

        #region Initialization

        public void Initialize()
        {
            pcTargets.Clear();
            npcTargets.Clear();
            healingSkills.Clear();
            useableSkills.Clear();
            nonUseableSkills.Clear();
            skillList.Clear();

            selectedSkill = null;
            npcFighter = null;
            action = ActionType.Idle;

        }

        #endregion

        #region Action Selection

        public virtual void PerformAction(NPCFightingCharacter npcFighter)
        {
            this.npcFighter = npcFighter;

            switch (npcFighter.BehaviorType)
            {
                case AIType.Simple:
                    SimpleSelectAction();
                    break;
                case AIType.Knowledgeable:
                    KnowledgableSelectAction();
                    break;
                case AIType.AlwaysDefend:
                    AlwaysDefendAction();
                    break;
            }
        }

        void AlwaysDefendAction()
        {
            TurnExecutor.Singleton.Action = FighterAction.Defend;
            action = ActionType.Defend;
        }

        void SimpleSelectAction()
        {
            //Select a random skill
            int skillIndex = Utility.RNG.Next(0, npcFighter.SkillNames.Count);
            selectedSkill = SkillPool.RequestByName(npcFighter.SkillNames[skillIndex]);

            if (selectedSkill.SpCost > npcFighter.FighterStats.Stamina || selectedSkill.MpCost > npcFighter.FighterStats.Energy)
            {
                selectedSkill = SkillPool.RequestByName(npcFighter.BaseAttackName);
            }

            //Select a random target.
            int i = Utility.RNG.Next(0, TurnExecutor.Singleton.PCFighters.Count);

            while (TurnExecutor.Singleton.PCFighters[i].State == State.Dead)
            {
                if (++i >= TurnExecutor.Singleton.PCFighters.Count)
                {
                    i = 0;
                }
            }

            //Activate the skill in Turn Executor.
            TurnExecutor.Singleton.Action = FighterAction.Skill;
            TurnExecutor.Singleton.ActiveSkill = selectedSkill;

            List<FightingCharacter> selectedTargets = new List<FightingCharacter>();

            selectedTargets.Add(TurnExecutor.Singleton.PCFighters[i]);

            TurnExecutor.Singleton.ActiveSkill.Activate(npcFighter, selectedTargets.ToArray());
        }

        void KnowledgableSelectAction()
        {
            Debug.Assert(action == ActionType.Idle);

            HealingCheck();

            if (action != ActionType.Heal)
            {
                CreateGeneralSkillList();

                DefendCheck();

                if (action != ActionType.Defend)
                {
                    SkillCheck();
                    //not defending or healing, so we've decided to attack.
                }

            }

            NextTurnPrep();

            Debug.Assert(action == ActionType.Idle);
        }

        #endregion

        #region Healing

        /// <summary>
        /// Checks whether or not the character is capable of healing, and decides what to do if it does have that capability
        /// </summary>
        protected void HealingCheck()
        {
            //First check if current npc has healing a healing skill. If a healing skill is not in the skill list, return.
            bool hasHealing = CheckHealingSkills();

            if (hasHealing)
            {
                //Check if there are sufficient mana point and stamina point levels to cast a healing spell.
                bool canCast = CheckHealingCost();

                if (canCast)
                {
                    //construct a list of who needs healing
                    List<NPCFightingCharacter> needsHealing = new List<NPCFightingCharacter>();

                    foreach (NPCFightingCharacter teammate in TurnExecutor.Singleton.NPCFighters)
                    {
                        if (teammate.FighterStats.Health <= teammate.FighterStats.ModifiedMaxHealth / 2)
                        {
                            if (needsHealing.Count == 0)
                            {   
                                //First in list, just add
                                needsHealing.Add(teammate);
                            }
                            else
                            {
                                //sort by most damaged to least damaged
                                int index = 0;
                                while (teammate.FighterStats.Health < needsHealing[index].FighterStats.Health)
                                {
                                    if (index + 1 >= needsHealing.Count)
                                        break;

                                    ++index;
                                }

                                needsHealing.Insert(index, teammate);
                            }
                        }
                    }

                    if (needsHealing.Count == 0)
                    {
                        return;
                    }

                    //If able, cast a group heal. 
                    //If a group heal is unavailable, decide what skill to use (if more than 1)
                    //or, whether to heal self or heal most damaged teammate
                    bool groupHealAvailable = CheckGroupHeal();

                    if (groupHealAvailable)
                    {
                        DecideHealSkill(true);
                        ResolveGroupHealing(needsHealing);
                    }
                    else
                    {
                        DecideHealSkill(false);
                        ResolveHealing(needsHealing);
                    }
              
                    //At this point, the AI may have decided to not heal after all. If that is the case
                    //we continue on back in the KnowledgeableSelectAction method, otherwise we set action to heal
                    //and bubble out return to the turnexecutor
                }
            }
        }

        /// <summary>
        /// Checks if the character has any useable healing skills.
        /// </summary>
        bool CheckHealingSkills()
        {
            bool hasHealing = false;
            Skill skillCandidate;

            foreach (string skillName in npcFighter.SkillNames)
            {
                skillCandidate = SkillPool.RequestByName(skillName);

                if (skillCandidate.HealingSkill)
                {
                    healingSkills.Add(skillCandidate);
                    hasHealing = true;
                }
            }

            return hasHealing;
        }

        /// <summary>
        /// Checks the costs of available healing skills and sees if the character can cast any of them.
        /// </summary>
        bool CheckHealingCost()
        {
            //checks whether or not we have the resources to use the skill
            for (int i = healingSkills.Count - 1; i >= 0; i--)
            {
                Skill healingSkill = healingSkills[i];

                if (healingSkill.MpCost > npcFighter.FighterStats.Energy || healingSkill.SpCost > npcFighter.FighterStats.Stamina)
                {
                    healingSkills.RemoveAt(i);
                }
            }

            return (healingSkills.Count > 0);
        }

        /// <summary>
        /// Checks to see if an available healing skill is a group heal.
        /// </summary>
        bool CheckGroupHeal()
        {
            bool groupHealAvailable = false;

            foreach (Skill healingSkill in healingSkills)
            {
                if (healingSkill.TargetsAll)
                {
                    groupHealAvailable = true;
                }
            }

            return groupHealAvailable;
        }

        /// <summary>
        /// Decides what skill to use amongst the available healing skills
        /// </summary>
        void DecideHealSkill(bool isGroupHeal)
        {
            //First finalize the list of healing skills by removing group or individual heal skills from the list.
            for (int i = healingSkills.Count - 1; i >= 0; i--)
            {
                Skill healingSkill = healingSkills[i];

                Debug.Assert(healingSkill.HealingSkill);

                if (isGroupHeal && !healingSkill.TargetsAll)
                {
                    healingSkills.RemoveAt(i);
                }
                else if(!isGroupHeal && healingSkill.TargetsAll)
                {
                    healingSkills.RemoveAt(i);
                }
            }

            //if the healing skill list size is 1, then cast the skill. If it's bigger, then decide between the skills which to cast.
            //For now, go with more powerful of the spells.
            //Should be re-written to take into account how much damage was dealt last turn and use the more powerful
            //if there was a lot of damage, or the weaker if there was not much.
            selectedSkill = healingSkills[0];

            foreach (Skill healingSkill in healingSkills)
            {
                if (selectedSkill.SpellPower <=/*<*/ healingSkill.SpellPower)
                {
                    selectedSkill = healingSkill;
                }
            }

        }

        void ResolveGroupHealing(List<NPCFightingCharacter> healCandidates)
        {
            //Activate the skill in Turn Executor and select entire allied party as targets.
            TurnExecutor.Singleton.Action = FighterAction.Skill;
            TurnExecutor.Singleton.ActiveSkill = selectedSkill;

            List<FightingCharacter> selectedTargets = new List<FightingCharacter>();

            for (int i = 0; i < TurnExecutor.Singleton.NPCFighters.Count; i++)
            {
                selectedTargets.Add(TurnExecutor.Singleton.NPCFighters[i]);
            }

            TurnExecutor.Singleton.ActiveSkill.Activate(npcFighter, selectedTargets.ToArray());
            action = ActionType.Heal;
        }

        /// <summary>
        /// ResolveHealing gets targets and makes a decision on who is getting healed
        /// </summary>
        void ResolveHealing(List<NPCFightingCharacter> healCandidates)
        {

            // Heal myself if I am the first priority in the healing candidate list.
            if (npcFighter.Name == healCandidates[0].Name)
            {
                TurnExecutor.Singleton.Action = FighterAction.Skill;
                TurnExecutor.Singleton.ActiveSkill = selectedSkill;

                List<FightingCharacter> selectedTargets = new List<FightingCharacter>();

                selectedTargets.Add(healCandidates[0]);

                TurnExecutor.Singleton.ActiveSkill.Activate(npcFighter, selectedTargets.ToArray());
                action = ActionType.Heal;

                return;
            }

            // Compare my stats with my teammate's stats.
            float npcFighterPercentHealth = (float)npcFighter.FighterStats.Health / (float)npcFighter.FighterStats.ModifiedMaxHealth;
            float teammatePercentHealth;

            // Check if I am in the healCandidates list.
            bool npcFighterInList = false;

            for (int i = 0; i < healCandidates.Count; i++)
            {
                if (npcFighter.Name == healCandidates[i].Name)
                {
                    npcFighterInList = true;
                }
            }

            // If I am not in list, heal the first priority teammate.
            if (!npcFighterInList)
            {
                TurnExecutor.Singleton.Action = FighterAction.Skill;
                TurnExecutor.Singleton.ActiveSkill = selectedSkill;

                List<FightingCharacter> selectedTargets = new List<FightingCharacter>();


                selectedTargets.Add(healCandidates[0]);

                TurnExecutor.Singleton.ActiveSkill.Activate(npcFighter, selectedTargets.ToArray());
                action = ActionType.Heal;

                return;
            }

            Debug.Assert(healCandidates.Count >= 2);

            // Compare my stats against my teammate's in the first priority position.
            teammatePercentHealth = (float)healCandidates[0].FighterStats.Health / (float)healCandidates[0].FighterStats.ModifiedMaxHealth;

            // Only heal that teammate if it is worth it. If they are about to die, healing probably wont save them.
            // Also, only heal them if the health disparity is somewhat large between them and myself.
            if (teammatePercentHealth >= .10f && (npcFighterPercentHealth - teammatePercentHealth) >= .15f)
            {
                TurnExecutor.Singleton.Action = FighterAction.Skill;
                TurnExecutor.Singleton.ActiveSkill = selectedSkill;

                List<FightingCharacter> selectedTargets = new List<FightingCharacter>();


                selectedTargets.Add(healCandidates[0]);

                TurnExecutor.Singleton.ActiveSkill.Activate(npcFighter, selectedTargets.ToArray());
                action = ActionType.Heal;
                //should include an else if after this to check if the teammate is defending and can survive a hit after a potential heal
                //use combat statistics to determine this.

                return;
            }

            Debug.Assert(npcFighterPercentHealth <= .5f);
            // Heal self only if it is worth it. If health is too low, healing won't mean much.
            // This section should be re-writeen to make use of the combat statistics. If damage dealt last turn is < current health
            //then healing will be meaningful. Otherwise, better to attack or defend.
            if (npcFighterPercentHealth > .10f)
            {
                TurnExecutor.Singleton.Action = FighterAction.Skill;
                TurnExecutor.Singleton.ActiveSkill = selectedSkill;

                List<FightingCharacter> selectedTargets = new List<FightingCharacter>();

                selectedTargets.Add(npcFighter);

                TurnExecutor.Singleton.ActiveSkill.Activate(npcFighter, selectedTargets.ToArray());
                action = ActionType.Heal;
            }
        }

        #endregion

        #region Defend

        /// <summary>
        /// Decides whether the character should defend, or if there is any benefit from doing so.
        /// </summary>
        protected void DefendCheck()
        {
            //Check current sp and mp values against the sp and mp costs of all skills and create the useableSkillList.
            bool canUseSkills = CheckSkillCosts();

            if (canUseSkills)
            {
                Debug.Assert(useableSkills.Count != 0);

                //If there are skills available, check if there might be a better skill used next turn that cannot be used this turn.
                if (nonUseableSkills.Count != 0)
                {
                    Skill bestSkillAvailableNextTurn = CheckBestSkillAvailableNextTurn();

                    // If there is a better skill avaialble next turn, and my health is low, but not too low, defend.
                    if (bestSkillAvailableNextTurn != null && 
                        (npcFighter.FighterStats.Health / npcFighter.FighterStats.ModifiedMaxHealth) <= .40 &&
                         (npcFighter.FighterStats.Health / npcFighter.FighterStats.ModifiedMaxHealth) >= .20)
                    {
                        TurnExecutor.Singleton.Action = FighterAction.Defend;
                        action = ActionType.Defend; 
                    }
                }
            }
            else
            {
                //If there is no skill that can be cast, and the current hp is 40% or below, then the npc may defend.
                //The npcFighter will only defend if healing will be available to it next turn if it has a healing skill.
                bool healingNextTurn = CheckHealingNextTurn();

                if (healingNextTurn && (npcFighter.FighterStats.Health / npcFighter.FighterStats.ModifiedMaxHealth) <=  .40)
                {
                    TurnExecutor.Singleton.Action = FighterAction.Defend;
                    action = ActionType.Defend;
                }
            }
            
        }

        bool CheckHealingNextTurn()
        {
            bool healingNextTurn = false;

            foreach (Skill skill in skillList)
            {
                // If a skill is a healing skill and there is sufficient mp and if there will be sufficient sp by defending, then defend.
                if (skill.HealingSkill && skill.MpCost <= npcFighter.FighterStats.Energy && (skill.SpCost - npcFighter.FighterStats.Stamina) <= 3)
                {
                    healingNextTurn = true;
                }
            }

            return healingNextTurn;
        }

        Skill CheckBestSkillAvailableNextTurn()
        {
            Skill bestSkill = nonUseableSkills[0];
            bool betterNonUseable = true;

            // Find the best skill out of the skills that can not be used this turn.
            foreach (Skill skill in nonUseableSkills)
            {
                if (bestSkill.SpellPower < skill.SpellPower)
                {
                    bestSkill = skill;
                }
            }

            // Check the best of the non useable skills against all of the useable skills to see if it is better.
            foreach (Skill skill in useableSkills)
            {
                if (bestSkill.SpellPower < skill.SpellPower)
                {
                    betterNonUseable = false;
                }
            }

            if (betterNonUseable)
            {
                //If we found a skill that is better than the rest, check if it can be used next turn if we defend and gain 3 stamina instead of 1.
                if (bestSkill.MpCost < npcFighter.FighterStats.Energy && (bestSkill.SpCost - npcFighter.FighterStats.Stamina) <= 3)
                {
                    return bestSkill;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Skill Selection and Resolution

        protected void SkillCheck()
        {
            if (useableSkills.Count != 0)
            {
                CreateInitialTargetLists();
                CheckStatusEffectSkills();

                if (action != ActionType.Skill)
                {
                    //If we are not going to use a status effect, then we can choose one of our other skills

                    RemoveStatusEffectSkills();
                    SelectSkill();
                    Debug.Assert(selectedSkill != null);

                    if (selectedSkill.CanTargetEnemy)
                    {
                        if (selectedSkill.TargetsAll)
                        {
                            ResolveSkill(pcTargets);
                        }
                        else
                        {
                            SelectTarget(pcTargets);
                            ResolveSkill(pcTargets);
                        }
                    }
                    else
                    {
                        if (selectedSkill.TargetsAll)
                        {
                            ResolveSkill(npcTargets);
                        }
                        else
                        {
                            SelectTarget(npcTargets);
                            ResolveSkill(pcTargets);
                        }
                    }
                }
            }
            else
            {
                selectedSkill = SkillPool.RequestByName(npcFighter.BaseAttackName);
                SelectTarget(pcTargets);
                ResolveSkill(pcTargets);
            }
        }

        protected void SelectSkill()
        {
            Debug.Assert(useableSkills.Count <= 5);

            double randomNumber = Utility.RNG.NextDouble();

            if (useableSkills.Count == 1)
            {
                selectedSkill = useableSkills[0];
            }
            else if (useableSkills.Count == 2)
            {
                if (randomNumber <= .6)
                {
                    selectedSkill = useableSkills[0];
                }
                else
                {
                    selectedSkill = useableSkills[1];
                }
            }
            else if (useableSkills.Count == 3)
            {
                if (randomNumber <= .5)
                {
                    selectedSkill = useableSkills[0];
                }
                else if (randomNumber > .5 && randomNumber <= .7)
                {
                    selectedSkill = useableSkills[1];
                }
                else
                {
                    selectedSkill = useableSkills[2];
                }
            }
            else if (useableSkills.Count == 4)
            {
                if (randomNumber <= .4)
                {
                    selectedSkill = useableSkills[0];
                }
                else if (randomNumber > .4 && randomNumber <= .6)
                {
                    selectedSkill = useableSkills[1];
                }
                else if(randomNumber > .6 && randomNumber <= .8)
                {
                    selectedSkill = useableSkills[2];
                }
                else
                {
                    selectedSkill = useableSkills[3];
                }
            }
            else if (useableSkills.Count == 5)
            {
                if (randomNumber <= .3)
                {
                    selectedSkill = useableSkills[0];
                }
                else if (randomNumber > .3 && randomNumber <= .5)
                {
                    selectedSkill = useableSkills[1];
                }
                else if (randomNumber > .5 && randomNumber <= .7)
                {
                    selectedSkill = useableSkills[2];
                }
                else if (randomNumber > .7 && randomNumber <= .85)
                {
                    selectedSkill = useableSkills[3];
                }
                else
                {
                    selectedSkill = useableSkills[4];
                }
            }
        }

        protected void RemoveStatusEffectSkills()
        {
            for (int i = useableSkills.Count - 1; i >= 0; i--)
            {
                Skill useableSkill = useableSkills[i];

                if (useableSkill.GrantsStatusEffect && !useableSkill.IsBasicAttack)
                {
                    useableSkills.RemoveAt(i);
                }
            }
        }

        protected void CheckStatusEffectSkills()
        {
            Skill statusEffectSkill = null;
            List<bool> enemyNeedsStatusEffect = new List<bool>();//true;
            //bool allyNeedsStatusEffect = true;
            List<FightingCharacter> targetCandidates = new List<FightingCharacter>();
            double roll;
            int iterator = 0;

            foreach (Skill skill in useableSkills)
            {
                if (skill.GrantsStatusEffect && (skill.MpCost <= npcFighter.FighterStats.Energy && skill.SpCost <= npcFighter.FighterStats.Stamina)) //Check MP and SP here?
                {
                    statusEffectSkill = skill;
                }
            }

            // Check if we found a status effect skill. If not, we can just return.
            if (statusEffectSkill == null)
            {
                return;
            }

            if (statusEffectSkill.CanTargetEnemy)
            {
                //check if enemies have the status effect

                foreach (PCFightingCharacter enemy in pcTargets)
                {

                    if (enemy.HasStatusEffect(statusEffectSkill.Name))
                    {
                        enemyNeedsStatusEffect.Add(false);
                    }
                    //if (enemy.HasStatusEffect(statusEffectSkill.Name))
                    //{
                    //    enemyNeedsStatusEffect = false;
                    //    break;
                    //}
                    else
                    {
                        enemyNeedsStatusEffect.Add(true);
                    }
                    //else
                    //{
                    //    enemyNeedsStatusEffect = true;
                    //}

                    if (enemyNeedsStatusEffect[iterator])
                    {
                        targetCandidates.Add(enemy);
                    }
                    iterator++;
                    //if (enemyNeedsStatusEffect)
                    //{
                    //    targetCandidates.Add(enemy);
                    //    break;
                    //}
                }

                roll = Utility.RNG.NextDouble();
                //Do we have enough MP/SP here?
                if (targetCandidates.Count > 0 && roll <= .65)
                {
                    selectedSkill = statusEffectSkill;

                    if (!statusEffectSkill.TargetsAll)
                    {
                        SelectTarget(targetCandidates);
                        ResolveSkill(pcTargets);
                    }
                    else
                    {
                        ResolveSkill(pcTargets);
                        return;
                    }
                }
            }

            if (statusEffectSkill.CanTargetAllies)
            {
                //check if self and allies have the status effect.
                foreach (NPCFightingCharacter ally in TurnExecutor.Singleton.NPCFighters)
                {
                    if (!ally.HasStatusEffect(statusEffectSkill.Name))
                    {
                        targetCandidates.Add(ally);
                    }
                }

                roll = Utility.RNG.NextDouble();

                if (targetCandidates.Count > 0 && roll <= .65)
                {
                    selectedSkill = statusEffectSkill;

                    if (!statusEffectSkill.TargetsAll)
                    {
                        SelectTarget(targetCandidates);
                        ResolveSkill(npcTargets);
                    }
                    else
                    {
                        ResolveSkill(npcTargets);
                    }

                    action = ActionType.Skill;
                }
            }
        }

        /// <summary>
        /// Activates the selected skill in the Turn Executor and sets our action to Skill
        /// </summary>
        protected void ResolveSkill(List<FightingCharacter> targets)
        {
            Debug.Assert(selectedSkill != null);

            TurnExecutor.Singleton.Action = FighterAction.Skill;
            TurnExecutor.Singleton.ActiveSkill = selectedSkill;

            TurnExecutor.Singleton.ActiveSkill.Activate(npcFighter, targets.ToArray());
            action = ActionType.Skill;
        }

        #endregion

        #region Target Selection

        /// <summary>
        /// Selects the best target from a given list of targets.
        /// </summary>
        protected void SelectTarget(List<FightingCharacter> targetCandidates)
        {
            Debug.Assert(targetCandidates.Count > 0);
            Debug.Assert(selectedSkill != null);

            List<FightingCharacter> targets = new List<FightingCharacter>(targetCandidates);
            FightingCharacter target;

            //If using the default lists generated by the CreateInitialTargetList function, the targetCandidates list should be prioritized by combat damage 
            //dealt during the battle if the list contains PCFightingCharacters.
            //If the list contains NPCFightingCharacters, the list is prioritized in the same way, but with the current acting NPC always first in the list.
            //We should now take into consideration other statistics such as if the enemy has done any healing recently, or if they have killed an ally.

            if (selectedSkill.CanTargetAllies)
            {
                bool npcFighterInList = false;

                //Check if I am in the target list.
                foreach (FightingCharacter ally in targets)
                {
                    if (npcFighter.Name == ally.Name)
                    {
                        npcFighterInList = true;
                    }
                }

                //If I am in the target list, then I want to target myself
                if (npcFighterInList)
                {
                    npcTargets.Clear();
                    npcTargets.Add(npcFighter);
                    return;
                }

                //If I am not in the list, then I need to decide who in the list to use the skill on.
                //If there is only one person in the list, then they are the target.
                if (targets.Count == 1)
                {
                    npcTargets.Clear();
                    npcTargets.Add(targets[0]);
                }
                else
                {
                    //For right now, we just select an ally at random.
                    //Later we should probably select an ally based on the type of skill chosen, but the skill class will need to be updated to support this
                    target = SelectRandomTarget(targets);

                    npcTargets.Clear();
                    npcTargets.Add(target);
                }
            }
            else if (selectedSkill.CanTargetEnemy)
            {
                //On the first round we probably don't have much information on our targets, so just pick one at random.
                if (npcFighter.Statistics.Round == 1)
                {
                    target = SelectRandomTarget(targets);
                    pcTargets.Clear();
                    pcTargets.Add(target);

                    return;
                }

                //On subsequent rounds, we will compare our previous target with the highest priority target in the target list and potentially switch.
                target = GetNewTarget(targets);
                pcTargets.Clear();
                pcTargets.Add(target);
            }
        }

        /// <summary>
        /// Reprioritize the target list and compare the NPC's combat statistics LastTarget with each target in the target list and returns a new target if needed
        /// </summary>
        FightingCharacter GetNewTarget(List<FightingCharacter> targets)
        {
            FightingCharacter oldTarget = npcFighter.Statistics.LastTarget;
            FightingCharacter newTarget = null;
            List<FightingCharacter> rePrioritizedTargets = new List<FightingCharacter>();

            //Old target was not set in the previous round, so I just want to get a random one
            if (oldTarget == null)
            {
                newTarget = SelectRandomTarget(targets);
                return newTarget;
            }

            //Our old target is dead, so we need a new one
            if (oldTarget.State == State.Dead || oldTarget is NPCFightingCharacter)
            {
                rePrioritizedTargets = RePrioritizeTargetList(targets, null);
                newTarget = rePrioritizedTargets[0];
            }
            else
            {
                rePrioritizedTargets = RePrioritizeTargetList(targets, oldTarget);

                //If our last target is highest priority in the list, continue to attack them.
                if (oldTarget.Name == rePrioritizedTargets[0].Name)
                {
                    return oldTarget;
                }
                else
                {
                    newTarget = rePrioritizedTargets[0];
                }
            }

            return newTarget;
        }

        /// <summary>
        /// Returns a re-prioritized list based on combat statistics
        /// </summary>
        List<FightingCharacter> RePrioritizeTargetList(List<FightingCharacter> targets, FightingCharacter oldTarget)
        {
            Debug.WriteLine("Re-Prioritizing Targets");

            List<FightingCharacter> rePrioritizedTargets = new List<FightingCharacter>();

            if (oldTarget != null)
            {
                rePrioritizedTargets.Add(oldTarget);
            }

            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i].Statistics.HealingReceivedLastTurn > targets[i].Statistics.DamageReceivedLastTurn && Utility.RNG.NextDouble() <= .60)
                { //If the healing received last turn exceeds the damage that was dealt, then that target is likely to become a high priority
                    rePrioritizedTargets.Insert(0, targets[i]);
                }
                else if (targets[i].Statistics.KilledAnEnemy && Utility.RNG.NextDouble() <= .45)
                { //If the target has previously slain an ally, it is possible that they will become high priority, though the chance is low
                    rePrioritizedTargets.Insert(0, targets[i]);
                }
                else if (targets[i].Statistics.HealingDoneLastTurn > npcFighter.Statistics.DamageDealtLastTurn && Utility.RNG.NextDouble() <= .40)
                { //If the target has healed more damage than I dealt last turn, then it there is a good chance I will make them high priority
                    rePrioritizedTargets.Insert(0, targets[i]);
                }
                else if (Utility.RNG.NextDouble() <= .50)
                { //To keep things interesting, we want to switch target at random occasionally
                    rePrioritizedTargets.Insert(0, targets[i]);
                }
                else
                { //We didn't do anything else, just add the target to the end of the list.
                    rePrioritizedTargets.Add(targets[i]);
                }
            }

            return rePrioritizedTargets;
        }

        /// <summary>
        /// Selects a target at random from a given list of targets
        /// </summary>
        /// <param name="targets"></param>
        /// <returns></returns>
        FightingCharacter SelectRandomTarget(List<FightingCharacter> targets)
        {
            return targets[Utility.RNG.Next(targets.Count)];
        }

        /// <summary>
        /// Removes from the target list combatants that have contributed the least to combat.
        /// </summary>
        List<FightingCharacter> PruneWeakestTargets(List<FightingCharacter> targets)
        {
            List<FightingCharacter> goodTargets = new List<FightingCharacter>();
            FightingCharacter potentialTarget = targets[0];

            for (int i = 1; i < targets.Count; i++)
            {

            }

            return goodTargets;
        }

        #endregion

        #region General Helpers

        /// <summary>
        /// Calculates a final damage based on the npcFighter's stats, a defined skill, and a potential target's stats.
        /// </summary>
        int CalculateDamage(Skill skill, FightingCharacter target)
        {
            int finalDamage;

            if (skill.MagicSkill)
            {
                List<DamageModifier> modifiers = new List<DamageModifier>();

                foreach(DamageModifier modifier in npcFighter.DamageModifiers)
                {
                    modifiers.Add(modifier);
                }

                if (skill.DamageModifierValue > 0)
                {
                    DamageModifier skillMod = new DamageModifier(true, skill.DamageModifierValue);
                    modifiers.Add(skillMod);
                }

                float rawDamage = CombatCalculations.RawMagicalDamage(npcFighter.FighterStats, skill.SpellPower);
                float modifiedDamage = CombatCalculations.ModifiedDamage(modifiers, rawDamage);

                finalDamage = CombatCalculations.MagicalDefense(target.FighterStats, modifiedDamage);
            }
            else
            {
                List<DamageModifier> modifiers = new List<DamageModifier>();

                foreach (DamageModifier modifier in npcFighter.DamageModifiers)
                {
                    modifiers.Add(modifier);
                }

                if (skill.DamageModifierValue > 0)
                {
                    DamageModifier skillMod = new DamageModifier(true, skill.DamageModifierValue);
                    modifiers.Add(skillMod);
                }

                float rawDamage = CombatCalculations.RawPhysicalDamage(npcFighter.FighterStats);
                float modifiedDamage = CombatCalculations.ModifiedDamage(npcFighter.DamageModifiers, rawDamage);

                finalDamage = CombatCalculations.PhysicalDefense(target.FighterStats, modifiedDamage);
            }

            return finalDamage;
        }

        /// <summary>
        /// Checks to see if the npcFighter has the mp and sp to use any skills, and creates a useable skill list.
        /// </summary>
        /// <returns></returns>
        protected bool CheckSkillCosts()
        {
            bool canUseSkills = false;

            //Look at each skill and see if it can be used. If it can be, add it to the useable skill list.
            foreach (Skill skill in skillList)
            {
                if (skill.MpCost <= npcFighter.FighterStats.Energy && skill.SpCost <= npcFighter.FighterStats.Stamina)
                {
                    useableSkills.Add(skill);
                    canUseSkills = true;
                }
                else
                {
                    nonUseableSkills.Add(skill);
                }
            }
            
            return canUseSkills;
        }

        /// <summary>
        /// Creates a master skill list of all available skills. 
        /// </summary>
        protected void CreateGeneralSkillList()
        {
            foreach (string skillName in npcFighter.SkillNames)
            {
                Skill skill = SkillPool.RequestByName(skillName);
                skillList.Add(skill);
            }
        }

        /// <summary>
        /// Creates a skill list of all available attack skills (excludes healing skills).
        /// </summary>
        protected void CreateAttackSkillList()
        {
            foreach (string skillName in npcFighter.SkillNames)
            {
                Skill skill = SkillPool.RequestByName(skillName);

                if (!skill.HealingSkill)
                {
                    skillList.Add(skill);
                }
            }
        }

        /// <summary>
        /// Creates a list of targets prioritized based on the combat statistics of the player characters
        /// </summary>
        protected void CreateInitialTargetLists()
        {
            //Right now the list is sorted by the total damage dealt. 
            foreach (PCFightingCharacter enemy in TurnExecutor.Singleton.PCFighters)
            {
                if (pcTargets.Count == 0 && enemy.State != State.Dead)
                {
                    pcTargets.Add(enemy);
                }
                else if(enemy.State != State.Dead)
                {
                    //sort by who has dealt the most damage overall.
                    int index = 0;
                    while (enemy.Statistics.TotalDamageDealt <= pcTargets[index].Statistics.TotalDamageDealt)
                    {
                        if (index + 1 >= pcTargets.Count)
                            break;

                        ++index;
                    }

                    pcTargets.Insert(index, enemy);
                }
            }

            //Right now the list is sorted by the total damage dealt. 
            foreach (NPCFightingCharacter ally in TurnExecutor.Singleton.NPCFighters)
            {
                if (npcTargets.Count == 0 && ally.Name != npcFighter.Name && ally.State != State.Dead)
                {
                    npcTargets.Add(ally);
                }
                else if (ally.Name != npcFighter.Name && ally.State != State.Dead)
                {
                    //sort by who has dealt the most damage overall.
                    int index = 0;
                    while (ally.Statistics.TotalDamageDealt <= npcTargets[index].Statistics.TotalDamageDealt)
                    {
                        if (index + 1 >= npcTargets.Count)
                            break;

                        ++index;
                    }

                    npcTargets.Insert(index, ally);
                }
            }

            //The current acting NPC Fighting Character should always be first in the list.
            npcTargets.Insert(0, npcFighter);
        }

        /// <summary>
        /// Prepares all variables for the next time a npcFighter makes an action
        /// </summary>
        protected void NextTurnPrep()
        {
            pcTargets.Clear();
            npcTargets.Clear();
            healingSkills.Clear();
            useableSkills.Clear();
            nonUseableSkills.Clear();
            skillList.Clear();

            npcFighter = null;
            selectedSkill = null;

            action = ActionType.Idle;
        }

        #endregion
    }
}
