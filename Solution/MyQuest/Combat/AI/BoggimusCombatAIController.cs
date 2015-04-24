using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MyQuest
{
    class BoggimusCombatAIController : CombatAIController
    {
        #region Fields

        bool partnerDead = false;

        #endregion

        #region Action Selection

        public override void PerformAction(NPCFightingCharacter npcFighter)
        {
            this.npcFighter = npcFighter;

            //Do this only on the first round that Boggimus is dead. 
            if (!partnerDead)
            {
                //Determine if Boggimus is dead this turn. If he is, Chepetawa gets a power up. This only occurs the first time he dies
                foreach (NPCFightingCharacter ally in TurnExecutor.Singleton.NPCFighters)
                {
                    if (ally.Name == "Chepetawa" && ally.State == State.Dead)
                    {
                        partnerDead = true;

                        //Add enraged attributes to Boggimus, high powered regeneration and add in the skill Great Monsoon.
                        StatusEffect regeneration = new Regeneration(999, 1.0f, (1f / 10f));
                        StatusEffect spRegeneration = new SpRegeneration(999, 1.0f);
                        StatusEffect rage = new ChepetawaRage();
                        npcFighter.AddStatusEffect(regeneration);
                        npcFighter.AddStatusEffect(spRegeneration);
                        npcFighter.AddStatusEffect(rage);
                        //rage.OnActivateEffect(npcFighter);
                        //regeneration.OnActivateEffect(npcFighter);
                        //spRegeneration.OnActivateEffect(npcFighter);

                        npcFighter.SetAnimation("RageIdle");

                        //Remove old monsoon skill
                        for (int i = npcFighter.SkillNames.Count - 1; i >= 0; i--)
                        {
                            String skillName = npcFighter.SkillNames[i];

                            if (skillName == "BoggimusMonsoon")
                            {
                                npcFighter.SkillNames.RemoveAt(i);
                            }
                        }

                        //Immediately use Great Monsoon and return.
                        npcFighter.AddSkillName("GreatMonsoon");

                        selectedSkill = SkillPool.RequestByName("GreatMonsoon");

                        //Activate the skill in Turn Executor.
                        TurnExecutor.Singleton.Action = FighterAction.Skill;
                        TurnExecutor.Singleton.ActiveSkill = selectedSkill;

                        List<FightingCharacter> selectedTargets = new List<FightingCharacter>();

                        foreach (FightingCharacter enemy in TurnExecutor.Singleton.PCFighters)
                        {
                            selectedTargets.Add(enemy);
                        }

                        TurnExecutor.Singleton.ActiveSkill.Activate(npcFighter, selectedTargets.ToArray());
                        return;
                    }
                }
            }
            SelectAction();

        }


        void SelectAction()
        {
            if (action != ActionType.Heal)
            {
                CreateAttackSkillList();

                BoggimusSkillCheck();
            }

            NextTurnPrep();

            Debug.Assert(action == ActionType.Idle);
        }

        #endregion

        #region Skill Selection

        void BoggimusSkillCheck()
        {
            //Creates a useable skill list based on what skills I can afford (mp/sp). The useable skill list should always contain the basic attack.
            CheckSkillCosts();

            //Creates a list of all NPC and PC fighting characters.
            CreateInitialTargetLists();

            SelectSkill();

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

        #endregion
    }
}
