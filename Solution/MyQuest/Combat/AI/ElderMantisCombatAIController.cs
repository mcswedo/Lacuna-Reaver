using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MyQuest
{
    class ElderMantisCombatAIController : CombatAIController
    {
        public override void PerformAction(NPCFightingCharacter npcFighter)
        {
            SelectAction();
        }

        void SelectAction()
        {
            if (action != ActionType.Heal)
            {
                CreateAttackSkillList();

                ElderMantisSkillCheck();
            }

            NextTurnPrep();

            Debug.Assert(action == ActionType.Idle);
        }

        void ElderMantisSkillCheck()
        {
            //Creates a useable skill list based on what skills I can afford (mp/sp). The useable skill list should always contain the basic attack.
            CheckSkillCosts();

            //Creates a list of all NPC and PC fighting characters.
            CreateInitialTargetLists();

            //We want to see if any of our enemies have a status effect we can grant, and give it to them if they don't have it
            CheckGazeOfDespair();

            if (action != ActionType.Skill)
            {
                ElderMantisSelectSkill();
                Debug.Assert(selectedSkill != null);

                if (selectedSkill.CanTargetEnemy)
                {
                    //If my skill targets everyone, then I can immedaitely go to resolve skill, passing in all my PC targets
                    if (selectedSkill.TargetsAll)
                    {
                        ResolveSkill(pcTargets);
                    }
                    else
                    {
                        //if it's a single target skill, I need to narrow down my PC targets list down to 1.
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

        void ElderMantisSelectSkill()
        {
            CheckUnholyRevelations();

            //We decided not to or were not able to use Unholy Revelations
            if (action != ActionType.Skill)
            {
                selectedSkill = SkillPool.RequestByName("ElderMantisAttack");
                action = ActionType.Skill;
            }
        }

        void CheckUnholyRevelations()
        {
            bool unholyRevelationsUseable = false;
            int unholyRevelationsIndex = 0;

            if (useableSkills.Count <= 0)
            {
                return;
            }

            //Check that our skill is available to use.
            for (unholyRevelationsIndex = 0; unholyRevelationsIndex < useableSkills.Count; unholyRevelationsIndex++)
            {
                if (useableSkills[unholyRevelationsIndex].Name == "UnholyRevelations")
                {
                    unholyRevelationsUseable = true;
                    break;
                }
            }

            if (unholyRevelationsUseable)
            {
                selectedSkill = useableSkills[unholyRevelationsIndex];
                action = ActionType.Skill;
            }
        }

        void CheckGazeOfDespair()
        {
            bool gazeUseable = false;
            bool enemyNeedsStatusEffect;
            int gazeIndex = 0;
            List<FightingCharacter> targetCandidates = new List<FightingCharacter>();

            if (useableSkills.Count <= 0)
            {
                return;
            }

            //Check that we have the gaze of despair skill available 
            for (gazeIndex = 0; gazeIndex < useableSkills.Count; gazeIndex++)
            {
                if (useableSkills[gazeIndex].Name == "GazeOfDespair")
                {
                    gazeUseable = true;
                    break;
                }
            }

            if (gazeUseable)
            {
                //Create a target list based on which enemy does not have the status effect
                foreach (PCFightingCharacter enemy in pcTargets)
                {
                    if (enemy.HasStatusEffect("GazeOfDespair"))
                    {
                        enemyNeedsStatusEffect = false;
                        break;
                    }
                    else
                    {
                        enemyNeedsStatusEffect = true;
                    }

                    if (enemyNeedsStatusEffect)
                    {
                        targetCandidates.Add(enemy);
                        break;
                    }
                }
            }

            if (targetCandidates.Count > 0)
            {
                //No one has the status effect, so we definitely want to have a good chance to use it in this case if possible.
                if (targetCandidates.Count == 3 && Utility.RNG.NextDouble() <= .75)
                {
                    selectedSkill = useableSkills[gazeIndex];
                    action = ActionType.Skill;
                    SelectTarget(targetCandidates);
                    ResolveSkill(pcTargets);
                }
                else if (targetCandidates.Count == 2 && Utility.RNG.NextDouble() <= .5)
                {
                    //One person has the status effect, two do not, 50% chance to use it on the one of the remaining two.
                    selectedSkill = useableSkills[gazeIndex];
                    action = ActionType.Skill;
                    SelectTarget(targetCandidates);
                    ResolveSkill(pcTargets);
                }
                else if (Utility.RNG.NextDouble() <= .2)
                {
                    //Only 1 enemy does not have the status effect, 2/3 is good enough, so we aren't that eager to use this ability
                    selectedSkill = useableSkills[gazeIndex];
                    action = ActionType.Skill;
                    SelectTarget(targetCandidates);
                    ResolveSkill(pcTargets);
                }
            }
            else
            {
                //Target list is empty meaning everyone has the status effect. This attack still does damage so we may wish to use it still.
                //We only want to use this skill if we can't use Unholy Revelations, so we check stamina cost. We also don't want to use this skill
                //if we could possibly have Unholy Revelations available to us next turn. 
            }
        }
    }
}

