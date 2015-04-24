using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MyQuest
{
    class SerlynxCombatAIController : CombatAIController
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

                SerlynxSkillCheck();
            }

            NextTurnPrep();

            Debug.Assert(action == ActionType.Idle);
        }

        void SerlynxSkillCheck()
        {
            //Creates a useable skill list based on what skills I can afford (mp/sp). The useable skill list should always contain the basic attack.
            CheckSkillCosts();

            //Creates a list of all NPC and PC fighting characters.
            CreateInitialTargetLists();

            SerlynxSelectSkill();
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

        void SerlynxSelectSkill()
        {
            CheckMiasma();

            if (action != ActionType.Skill)
            {
                CheckImmolate();

                //We decided not to or were not able to use Immolate
                if (action != ActionType.Skill)
                {
                    selectedSkill = SkillPool.RequestByName("SerlynxAttack");
                    action = ActionType.Skill;
                }
            }
        }

        void CheckMiasma()
        {
            bool miasmaUseable = false;
            int miasmaIndex = 0;
            Skill skill;

            if (useableSkills.Count <= 0)
            {
                return;
            }

            //Check that our skill is available to use.
            for (miasmaIndex = 0; miasmaIndex < useableSkills.Count; miasmaIndex++)
            {
                skill = useableSkills[miasmaIndex];

                if (skill.Name == "Miasma")
                {
                    miasmaUseable = true;
                    break;
                }
            }

            if (miasmaUseable && Utility.RNG.NextDouble() <= .65)
            {
                selectedSkill = useableSkills[miasmaIndex];
                action = ActionType.Skill;
            }
        }

        void CheckImmolate()
        {
            bool immolateUseable = false;
            int immolateIndex = 0;
            Skill skill;

            if (useableSkills.Count <= 0)
            {
                return;
            }

            //Check that our skill is available to use.
            for (immolateIndex = 0; immolateIndex < useableSkills.Count; immolateIndex++)
            {
                skill = useableSkills[immolateIndex];

                if (skill.Name == "Immolate")
                {
                    immolateUseable = true;
                    break;
                }
            }

            if (immolateUseable && Utility.RNG.NextDouble() <= .8)
            {
                selectedSkill = useableSkills[immolateIndex];
                action = ActionType.Skill;
            }
        }
    }
}
