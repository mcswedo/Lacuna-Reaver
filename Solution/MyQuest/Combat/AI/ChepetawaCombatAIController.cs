using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MyQuest
{
    class ChepetawaCombatAIController : CombatAIController
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
                foreach (NPCFightingCharacter npc in TurnExecutor.Singleton.NPCFighters)
                {
                    if (npc.Name == "Boggimus" && npc.State == State.Dead)
                    {
                        partnerDead = true;

                        //Add enraged attributes to Chepetawa, high powered regeneration and add in the skill Great Monsoon.
                        StatusEffect regeneration = new Regeneration(999, 1.0f, (1f / 10f));
                        StatusEffect spRegeneration = new SpRegeneration(999, 1.0f);
                        StatusEffect rage = new ChepetawaRage();
                        npcFighter.AddStatusEffect(regeneration);
                        npcFighter.AddStatusEffect(spRegeneration);
                        npcFighter.AddStatusEffect(rage);

                        //Remove old monsoon skill
                        for (int i = npcFighter.SkillNames.Count - 1; i >= 0; i--)
                        {
                            String skillName = npcFighter.SkillNames[i];

                            if (skillName == "ChepetawaMonsoon")
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
            //Chepetawa will begin healing only when Boggimus or herself fall to half life.
            if (!partnerDead)
            {
                //Heal only if Boggimus is alive
                HealingCheck();
            }

            if (action != ActionType.Heal)
            {
                CreateAttackSkillList();

                ChepetawaSkillCheck();
            }

            NextTurnPrep();

            Debug.Assert(action == ActionType.Idle);
        }

        #endregion

        #region Skill Selection

        void ChepetawaSkillCheck()
        {
            //Creates a useable skill list based on what skills I can afford (mp/sp). The useable skill list should always contain the basic attack.
            CheckSkillCosts();

            //Creates a list of all NPC and PC fighting characters.
            CreateInitialTargetLists();

            //Check if I have a status effect skill available and have enemies who need status effects. 
            CheckStatusEffectSkills();

            //If I decided to use a status effect, then my action type will = skill, so I can just return
            if (action != ActionType.Skill)
            {
                //At this point I have decided not to use a status effect ability, so I can remove them from the useable skill list.
                RemoveStatusEffectSkills();
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
        }

        void ChepetawaStatusEffectCheck()
        {
            Skill pandorasBox = null;

            List<FightingCharacter> targetCandidates = new List<FightingCharacter>();

            double roll;

            //Check if Pandora's Box is in our useable skills
            foreach (Skill skill in useableSkills)
            {
                if (skill.Name == "Pandoras Box")
                {
                    pandorasBox = skill;
                }
            }

            // Check if we found a status effect skill. If not, we can just return.
            if (pandorasBox == null)
            {
                return;
            }

            foreach (PCFightingCharacter enemy in pcTargets)
            {
                if (enemy.GetNegativeStatusEffectsCount() < 2)
                {
                    targetCandidates.Add(enemy);
                }
            }

            roll = Utility.RNG.NextDouble();

            //If we found targets with less than two status effects we have a 55% chance of casting a status effect skill
            if (targetCandidates.Count > 0 && roll <= .65/*.55*/)
            {
                selectedSkill = pandorasBox;

                SelectTarget(targetCandidates);
                ResolveSkill(pcTargets);
            }
        }
        #endregion
    }
}
