using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{
    public abstract class Skill
    {
        #region Fields

        string name;
        string description;        
        
        int mpCost;
        int spCost;

        float spellPower;
        float damageModifierValue;

        Element elementalDamageType;

        bool battleSkill;
        bool mapSkill;
        bool healingSkill;
        bool magicSkill;
        bool grantsStatusEffect = false;
        bool isBasicAttack;
        bool skillHit = false;

        bool canTargetMany;
        bool canTargetParty;
        bool canTargetEnemy;

        Vector2 drawOffset;

        Vector2 destinationOffset;

        protected bool isRunning = false;

        protected List<FightingCharacter> targets;

        protected FightingCharacter actor;


        #endregion

        #region Properties


        /// <summary>
        /// The name of the skill
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        /// <summary>
        /// A description of what this skill does
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }


        /// <summary>
        /// The amount of magic points required to perform this skill
        /// </summary>
        public int MpCost
        {
            get { return mpCost; }
            set { mpCost = value; }
        }


        /// <summary>
        /// The amount of stamina points required to perform this skill
        /// </summary>
        public int SpCost
        {
            get { return spCost; }
            set { spCost = value; }
        }
        

        /// <summary>
        /// Please describe this
        /// </summary>
        public float SpellPower
        {
            get { return spellPower; }
            set { spellPower = value; }
        }


        /// <summary>
        /// The value of the damage modifier that is applied by the skill.
        /// </summary>
        public float DamageModifierValue
        {
            get { return damageModifierValue; }
            set { damageModifierValue = value; }
        }

        /// <summary>
        /// The type of elemental damage that the skill deals. Set to "None" by default. 
        /// </summary>
        public Element ElementalDamageType
        {
            get { return elementalDamageType; }
            set { elementalDamageType = value; }
        }


        /// <summary>
        /// Determines if this skill is usable in battle
        /// </summary>
        public bool BattleSkill
        {
            get { return battleSkill; }
            set { battleSkill = value; }
        }


        /// <summary>
        /// Determines if this skill is usable outside of battle
        /// </summary>
        public bool MapSkill
        {
            get { return mapSkill; }
            set { mapSkill = value; }
        }

        /// <summary>
        /// Determines if this skill heals
        /// </summary>
        public bool HealingSkill
        {
            get { return healingSkill; }
            set { healingSkill = value; }
        }

        /// <summary>
        /// True if this skill uses the magical damage formula, false if it uses the physical (if it deals damage)
        /// </summary>
        public bool MagicSkill
        {
            get { return magicSkill; }
            set { magicSkill = value; }
        }

        /// <summary>
        /// True if the skill grants a status effect on resolution.
        /// </summary>
        public bool GrantsStatusEffect
        {
            get { return grantsStatusEffect; }
            set { grantsStatusEffect = value; }
        }

        /// <summary>
        /// True if the skill is the basic attack of a character.
        /// </summary>
        public bool IsBasicAttack
        {
            get { return isBasicAttack; }
            set { isBasicAttack = value; }
        }

        /// <summary>
        /// Specifies whether this skill can target more than one character
        /// </summary>
        public bool TargetsAll
        {
            get { return canTargetMany; }
            set { canTargetMany = value; }
        }

        /// <summary>
        /// Specifies whether this skill can target the actor's own party
        /// </summary>
        public bool CanTargetAllies
        {
            get { return canTargetParty; }
            set { canTargetParty = value; }
        }

        /// <summary>
        /// Specifies whether this skill can target the opposing party
        /// </summary>
        public bool CanTargetEnemy
        {
            get { return canTargetEnemy; }
            set { canTargetEnemy = value; }
        }
        
        /// <summary>
        /// Specifies whether this skill is still running
        /// </summary>
        public bool IsRunning
        {
            get { return isRunning; }
        }

        public bool SkillHit
        {
            get { return skillHit; }
            set { skillHit = value; }
        }

        /// <summary>
        /// The offset used to center the sprite over a hit position.
        /// </summary>
        public Vector2 DrawOffset
        {
            get { return drawOffset; }
            set { drawOffset = value; }
        }

        public Vector2 DestinationOffset
        {
            get { return destinationOffset; }
            set { destinationOffset = value; }
        }


        #endregion

        #region Other methods

        public virtual void OutOfCombatActivate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            SoundSystem.Play(AudioCues.menuDeny);
        }

        public virtual void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            Debug.Assert(targets != null && targets.Length > 0);

            isRunning = true;

            this.targets = new List<FightingCharacter>(targets);
            this.actor = actor;
        }

        public abstract void Update(GameTime gameTime);


        public abstract void Draw(SpriteBatch spriteBatch);

        protected virtual void SubtractCost(FightingCharacter actor)
        {
            actor.FighterStats.Energy -= MpCost;
            actor.FighterStats.Stamina -= SpCost;
        }

        /// <summary>
        /// Calculates magical damage and defense values and applies the result to the target's hit points
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="targets"></param>
        protected virtual void DealMagicDamage(FightingCharacter actor, params FightingCharacter[] targets)
        {
            float rawDamage = CombatCalculations.RawMagicalDamage(actor.FighterStats, SpellPower);

            float modifiedDamage = CombatCalculations.ModifiedDamage(actor.DamageModifiers, rawDamage);

            skillHit = false;

            int finalDamage;

            //Debug.WriteLine(actor.Name + " dealing RawMagicDamage: " + rawDamage.ToString() + ", ModifiedDamage: " + modifiedDamage.ToString());

            foreach (FightingCharacter target in targets)
            {
                if (target.State == State.Invulnerable || target.HasStatusEffect("Invulnerable"))
                {
                    finalDamage = 0;
                }
                else if (actor.Blind && Utility.RNG.NextDouble() <= .5)
                {
                    finalDamage = 0;
                }
                else
                {
                    modifiedDamage = GetElementalDamage(target, modifiedDamage);
                    finalDamage = CombatCalculations.MagicalDefense(target.FighterStats, modifiedDamage);
                    target.OnHit();
                    skillHit = true;
                }
                
                //Debug.WriteLine("FinalDamage: " + finalDamage.ToString() + " dealt to " + target.Name);
                int initialHealth = target.FighterStats.Health;

                if ((target.FighterStats.Health - finalDamage) <= 0)
                {
                    actor.Statistics.KilledAnEnemy = true;
                    target.FighterStats.Health = 0;
                    target.OnDamageReceived(finalDamage);
                    target.OnDeath();
                }
                else
                {
                    target.FighterStats.Health -= finalDamage;
                    target.OnDamageReceived(finalDamage);
                }
                //Debug.WriteLine("\tInitial HP: " + initialHealth + ", EndHealth: " + target.Stats.Health);

                UpdateStatistics(actor, target, finalDamage);
                if (actor.Blind && finalDamage == 0)
                {
                    CombatMessage.AddMessage("Miss", target.DamageMessagePosition, Color.Red, .5);
                }
                else
                {
                    CombatMessage.AddMessage("" + finalDamage, target.DamageMessagePosition, Color.Red, .5);
                }
                
            }
            actor.DamageModifiers.Clear();
        }

        /// <summary>
        /// Calculates the physical damage and defense values and applies them to the target's hit points
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="targets"></param>
        protected void DealPhysicalDamage(FightingCharacter actor, params FightingCharacter[] targets)
        {
            float rawDamage = CombatCalculations.RawPhysicalDamage(actor.FighterStats);

            float modifiedDamage = CombatCalculations.ModifiedDamage(actor.DamageModifiers, rawDamage);

            skillHit = false;

            int finalDamage;

            //Debug.WriteLine(actor.Name + " dealing RawPhysicalDamage: " + rawDamage.ToString() + ", ModifiedDamage: " + modifiedDamage.ToString());

            foreach (FightingCharacter target in targets)
            {
                if (target.State == State.Invulnerable || target.HasStatusEffect("Invulnerable")) //In case state is changed while they have invulnerability.
                {
                    finalDamage = 0;
                }
                else if (actor.Blind && Utility.RNG.NextDouble() <= .7)
                {
                    finalDamage = 0;
                }
                else
                {
                    modifiedDamage = GetElementalDamage(target, modifiedDamage);
                    finalDamage = CombatCalculations.PhysicalDefense(target.FighterStats, modifiedDamage);
                    target.OnHit();
                    skillHit = true;
                }
                //Debug.WriteLine("FinalDamage: " + finalDamage.ToString() + " dealt to " + target.Name);
                int initialHealth = target.FighterStats.Health;

                if ((target.FighterStats.Health - finalDamage) <= 0)
                {
                    actor.Statistics.KilledAnEnemy = true;
                    target.FighterStats.Health = 0;
                    target.OnDamageReceived(finalDamage);
                    target.OnDeath();                    
                }
                else
                {
                    target.FighterStats.Health -= finalDamage;
                    target.OnDamageReceived(finalDamage);
                }
                
                //Debug.WriteLine("\tInitial HP: " + initialHealth + ", EndHealth: " + target.Stats.Health);

                UpdateStatistics(actor, target, finalDamage);
                if (actor.Blind && finalDamage == 0)
                {
                    CombatMessage.AddMessage("Miss", target.DamageMessagePosition, Color.Red, .5);
                }
                else
                {
                    CombatMessage.AddMessage("" + finalDamage, target.DamageMessagePosition, Color.Red, .5);
                }
            }
            actor.DamageModifiers.Clear();
        }

        /// <summary>
        /// Calculates whether a status modifier affects a target, and applies it to the target if it does
        /// </summary>
        protected virtual void SetStatusEffect(FightingCharacter actor, StatusEffect effect, FightingCharacter target)
        {
            if (Utility.RNG.NextDouble() <= effect.Probability)
            {
                target.AddStatusEffect(effect);

                actor.Statistics.LastTarget = target;

                target.DisplayStatusEffect(effect.Name, effect.StatusEffectMessageColor);
            }
            //Tell the player that the status effect wasn't applied.
            else
            {
                CombatMessage.AddMessage("Failed", target.StatusEffectMessagePosition, .5);
            }
        }

        float GetElementalDamage(FightingCharacter target, float modifiedDamage)
        {
            float newDamage = modifiedDamage;

            if (elementalDamageType == target.ElementalWeakness && target.ElementalWeakness != Element.None)
            {
                newDamage *= 2;
            }
            else if (elementalDamageType == target.ElementalResistance && target.ElementalResistance != Element.None)
            {
                newDamage /= 2;
            }
            return newDamage;
        }

        protected virtual void UpdateStatistics(FightingCharacter actor, FightingCharacter target, int damage)
        {

            actor.Statistics.LastTarget = target;

            if (!this.healingSkill)
            {
                actor.Statistics.DamageDealtLastTurn = damage;
                actor.Statistics.HealingDoneLastTurn = 0;
                actor.Statistics.TotalDamageDealt += damage;
                target.Statistics.TotalDamageReceived += damage;
                target.Statistics.DamageReceivedLastTurn = damage;
            }
            else
            {
                actor.Statistics.HealingDoneLastTurn = damage;
                actor.Statistics.DamageDealtLastTurn = 0;
                actor.Statistics.PerformedHealing = true;
                actor.Statistics.TotalHealingDone += damage;
                target.Statistics.HealingReceivedLastTurn = damage;
            }
        }

        #endregion
    }
}

