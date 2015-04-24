using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MyQuest
{
    class MalticarCombatAIController : CombatAIController
    {
        public override void PerformAction(NPCFightingCharacter npcFighter)
        { 
            this.npcFighter = npcFighter;
            SelectAction();
        }

        void SelectAction()
        {
            if (action != ActionType.Heal)
            {
                CreateAttackSkillList();

                MalticarSkillCheck();
            }

            NextTurnPrep();

            Debug.Assert(action == ActionType.Idle);
        }

        void MalticarSkillCheck()
        {
            //Creates a useable skill list based on what skills I can afford (mp/sp). The useable skill list should always contain the basic attack.
            CheckSkillCosts();

            //Creates a list of all NPC and PC fighting characters.
            CreateInitialTargetLists();

            MalticarSelectSkill();
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
                    ResolveSkill(npcTargets);
                }
            }
        }

        void MalticarSelectSkill()
        {
            double randomizeSkillCheckOrder = Utility.RNG.NextDouble();

            //randomizeSkillCheckOrder = .24; //GET RID OF THIS!!! THIS IS TO TEST SOULDRAIN!

            if (randomizeSkillCheckOrder <= .25) //26% chance to check in this order. Higher chance of using soul drain.
            {
                CheckSoulDrain();

                if (action != ActionType.Skill)
                {
                    CheckEtherBlast();

                    if (action != ActionType.Skill)
                    {
                        CheckTailSwipe();

                        //We decided not to or were not able to use any abilities
                        if (action != ActionType.Skill)
                        {
                            selectedSkill = SkillPool.RequestByName("MalticarAttack");
                            action = ActionType.Skill;
                        }

                    }
                }
            }

            else if (randomizeSkillCheckOrder >= .26 && randomizeSkillCheckOrder <= .66) //40% chance to check in this order. Higher chance of using ether blast.
            {
                CheckEtherBlast();

                if (action != ActionType.Skill)
                {
                    CheckTailSwipe();

                    if (action != ActionType.Skill)
                    {
                        CheckSoulDrain();

                        //We decided not to or were not able to use any abilities
                        if (action != ActionType.Skill)
                        {
                            selectedSkill = SkillPool.RequestByName("MalticarAttack");
                            action = ActionType.Skill;
                        }

                    }
                }
            }

            else //34% chance to check in this order. Higher chance of using tail swipe.
            {
                CheckTailSwipe();

                if (action != ActionType.Skill)
                {
                    CheckSoulDrain();

                    if (action != ActionType.Skill)
                    {
                        CheckEtherBlast();

                        //We decided not to or were not able to use any abilities
                        if (action != ActionType.Skill)
                        {
                            selectedSkill = SkillPool.RequestByName("MalticarAttack");
                            action = ActionType.Skill;
                        }

                    }
                }
            }
        }

        void CheckSoulDrain()
        {
            bool soulDrainUseable = false;
            int soulDrainIndex = 0;

            if (useableSkills.Count <= 0)
            {
                return;
            }

            //Check that our skill is available to use.
            for (soulDrainIndex = 0; soulDrainIndex < useableSkills.Count; soulDrainIndex++)
            {
                if (useableSkills[soulDrainIndex].Name == "Soul Drain")
                {
                    soulDrainUseable = true;
                    break;
                }
            }

            double random = Utility.RNG.NextDouble();

            //random = .44; //GET RID OF THIS OR ELSE MALTICAR WILL ONLY USE SOULDRAIN!!!

            if (soulDrainUseable && random <= .45)
            {
                selectedSkill = useableSkills[soulDrainIndex];
                action = ActionType.Skill;
            }
        }

        void CheckEtherBlast()
        {
            bool etherBlastUseable = false;
            int etherBlastIndex = 0;

            if (useableSkills.Count <= 0)
            {
                return;
            }

            //Check that our skill is available to use.
            for (etherBlastIndex = 0; etherBlastIndex < useableSkills.Count; etherBlastIndex++)
            {
                if (useableSkills[etherBlastIndex].Name == "Ether Blast")
                {
                    etherBlastUseable = true;
                    break;
                }
            }

            double random = Utility.RNG.NextDouble();

            //random = .71; //GET RID OF THIS, THIS IS JUST FOR TESTING!!!

            if (etherBlastUseable && random <= .70)
            {
                selectedSkill = useableSkills[etherBlastIndex];
                action = ActionType.Skill;
            }
        }

        void CheckTailSwipe()
        {
            bool tailSwipeUseable = false;
            int tailSwipeIndex = 0;
            Skill skill;

            if (useableSkills.Count <= 0)
            {
                return;
            }

            //Check that our skill is available to use.
            for (tailSwipeIndex = 0; tailSwipeIndex < useableSkills.Count; tailSwipeIndex++)
            {
                skill = useableSkills[tailSwipeIndex];

                if (skill.Name == "TailSwipe" || skill.Name.Equals("Cleave"))
                {
                    tailSwipeUseable = true;
                    break;
                }
            }

            double random = Utility.RNG.NextDouble();

            //random = .56;

            if (tailSwipeUseable && Utility.RNG.NextDouble() <= .55)
            {
                selectedSkill = useableSkills[tailSwipeIndex];
                action = ActionType.Skill;
            }
        }

    }
}
