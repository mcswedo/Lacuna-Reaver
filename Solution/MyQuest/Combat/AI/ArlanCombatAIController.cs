using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MyQuest
{
    class ArlanCombatAIController : CombatAIController
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

                ArlanSkillCheck();
            }

            NextTurnPrep();

            Debug.Assert(action == ActionType.Idle);
        }

        void ArlanSkillCheck()
        {
            //Creates a useable skill list based on what skills I can afford (mp/sp). The useable skill list should always contain the basic attack.
            CheckSkillCosts();

            //Creates a list of all NPC and PC fighting characters.
            CreateInitialTargetLists();

            ArlanSelectSkill();
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

        void ArlanSelectSkill()
        {
            CheckPowerDrain();

            if (action != ActionType.Skill)
            {
                CheckNemesisCannon();

                if (action != ActionType.Skill)
                {
                    //CheckLightning(); We no longer have this skill.

                    //We decided not to or were not able to use any other skills.
                    if (action != ActionType.Skill)
                    {
                        selectedSkill = SkillPool.RequestByName("ArlanFireBall");
                        action = ActionType.Skill;
                    }
                }
            }
        }

        void CheckPowerDrain()
        {
            bool powerDrainUseable = false;
            int powerDrainIndex = 0;

            if (useableSkills.Count <= 0)
            {
                return;
            }

            //Check that our skill is available to use.
            for (powerDrainIndex = 0; powerDrainIndex < useableSkills.Count; powerDrainIndex++)
            {
                if (useableSkills[powerDrainIndex].Name == "Soul Drain")
                {
                    powerDrainUseable = true;
                    break;
                }
            }

            if (powerDrainUseable && Utility.RNG.NextDouble() >= .62)
            {
                selectedSkill = useableSkills[powerDrainIndex];
                action = ActionType.Skill;
            }
        }

        void CheckNemesisCannon()
        {
            bool nemesisCannonUseable = false;
            int nemesisCannonIndex = 0;

            if (useableSkills.Count <= 0)
            {
                return;
            }

            //Check that our skill is available to use.
            for (nemesisCannonIndex = 0; nemesisCannonIndex < useableSkills.Count; nemesisCannonIndex++)
            {
                if (useableSkills[nemesisCannonIndex].Name == "Nemesis Cannon")
                {
                    nemesisCannonUseable = true;
                    break;
                }
            }

            if (nemesisCannonUseable && Utility.RNG.NextDouble() <= .55)
            {
                selectedSkill = useableSkills[nemesisCannonIndex];
                action = ActionType.Skill;
            }
        }

        void CheckLightning()
        {
            bool lightningUseable = false;
            int lightningIndex = 0;
            Skill skill;

            if (useableSkills.Count <= 0)
            {
                return;
            }

            //Check that our skill is available to use.
            for (lightningIndex = 0; lightningIndex < useableSkills.Count; lightningIndex++)
            {
                skill = useableSkills[lightningIndex];

                if (skill.Name == "Lightning")
                {
                    lightningUseable = true;
                    break;
                }
            }

            if (lightningUseable && Utility.RNG.NextDouble() <= .75)
            {
                selectedSkill = useableSkills[lightningIndex];
                action = ActionType.Skill;
            }
        }
    }
}
