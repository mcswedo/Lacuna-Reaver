using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MyQuest
{
    class BurtleCombatAIController : CombatAIController
    {
        bool enraged = false;
        bool spRegeneration = false;
        bool burning = false;

        public override void PerformAction(NPCFightingCharacter npcFighter)
        {
            this.npcFighter = npcFighter;

            //Adds the burning Status effect to Tippers only(damages all enemies by 100)
            if (npcFighter.Name.Equals("Tippers") && !burning)
            {
                StatusEffect burningStatusEffect = new Burning();
                npcFighter.AddStatusEffect(burningStatusEffect);                
                burning = true;
            }

            if (npcFighter.Name.Equals("Tippers") && (!spRegeneration && npcFighter.FighterStats.Health <= npcFighter.FighterStats.BaseMaxHealth * .65))
            {
                StatusEffect spRegen = new SpRegeneration(10, 1f);
                npcFighter.AddStatusEffect(spRegen);
                spRegeneration = true;
            }            

            if (!enraged && npcFighter.FighterStats.Health <= npcFighter.FighterStats.BaseMaxHealth / 2)
            {
                enraged = true;
                StatusEffect rage = new BurtleRage();                
                npcFighter.AddStatusEffect(rage);

                //Immediately use Spike Rain
                selectedSkill = SkillPool.RequestByName("SpikeRain");

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

            SelectAction();
        }

        void SelectAction()
        {

            CreateAttackSkillList();

            BurtleSkillCheck();

            NextTurnPrep();

            Debug.Assert(action == ActionType.Idle);
        }

        void BurtleSkillCheck()
        {
            //Creates a useable skill list based on what skills I can afford (mp/sp). The useable skill list should always contain the basic attack.
            CheckSkillCosts();

            //Creates a list of all NPC and PC fighting characters.
            CreateInitialTargetLists();

            BurtleSelectSkill();

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

        void BurtleSelectSkill()
        {
            CheckWithdraw();

            //We decided not to or were not able to use the skill Withdraw
            if (action != ActionType.Skill)
            {
                CheckSpikeRain();

                //If we decided not to, or were unable to use the Spike Rain skill, we will just use Maul
                if (action != ActionType.Skill)
                {
                    selectedSkill = SkillPool.RequestByName("BurtleAttack");
                    action = ActionType.Skill;
                }
            }
        }

        void CheckWithdraw()
        {
            bool armorAvailable = false;
            int withdrawIndex = 0;
            Skill skill;

            if (useableSkills.Count <= 0)
            {
                return;
            }

            //Check if we have the armored status effect
            if (npcFighter.HasStatusEffect("Armored"))
            {
                return;
            }

            //Check that we have the withdraw skill available to give burtle armor. 
            for (withdrawIndex = 0; withdrawIndex < useableSkills.Count; withdrawIndex++) 
            {
                skill = useableSkills[withdrawIndex];

                if (skill.Name == "Withdraw")
                {
                    armorAvailable = true;
                    break;
                }
            }

            //If I need the status effect and the skill to grant it is available
            if (armorAvailable)
            {
                //if My health is between 75% and 25% life we can use withdraw
                if (npcFighter.FighterStats.Health <= (int)(npcFighter.FighterStats.BaseMaxHealth * .75) && npcFighter.FighterStats.Health >= (int)(npcFighter.FighterStats.BaseMaxHealth * .25))
                {
                    if (Utility.RNG.NextDouble() <= .65)
                    {
                        selectedSkill = useableSkills[withdrawIndex];
                        action = ActionType.Skill;
                    }
                }
            }
        }

        void CheckSpikeRain()
        {
            bool spikeRainUseable = false;
            int spikeRainIndex = 0;
            Skill skill;

            if (useableSkills.Count <= 0)
            {
                return;
            }

            //Check that our skill is actually available to use. 
            for (spikeRainIndex = 0; spikeRainIndex < useableSkills.Count; spikeRainIndex++)
            {
                skill = useableSkills[spikeRainIndex];

                if (skill.Name == "Spike Rain")
                {
                    spikeRainUseable = true;
                    break;
                }
            }

            if (spikeRainUseable)
            {
                selectedSkill = useableSkills[spikeRainIndex];
                action = ActionType.Skill;
            }
        }
    }
}
