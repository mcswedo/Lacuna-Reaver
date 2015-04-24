using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MyQuest
{
    /// <summary>
    /// Represents a Player-Controlled Fighting Character
    /// </summary>
    [XmlInclude(typeof(Nathan))]
    [XmlInclude(typeof(Cara))]
    [XmlInclude(typeof(Will))]
    [XmlInclude(typeof(Max))]       // temporary
    [XmlInclude(typeof(Sid))]       // remove these
    public abstract class PCFightingCharacter : FightingCharacter
    {
        #region Equipables


        string slotOne;

        public string SlotOne
        {
            get { return slotOne; }
            set { slotOne = value; }
        }


        string slotTwo;

        public string SlotTwo
        {
            get { return slotTwo; }
            set { slotTwo = value; }
        }


        string slotThree;

        public string SlotThree
        {
            get { return slotThree; }
            set { slotThree = value; }
        }

        string slotFour;

        public string SlotFour
        {
            get { return slotFour; }
            set { slotFour = value; }
        }

        string slotFive;

        public string SlotFive
        {
            get { return slotFive; }
            set { slotFive = value; }
        }

        string armorClassName;

        public string ArmorClassName
        {
            get { return armorClassName; }
            set { armorClassName = value; }
        }


        string weaponClassName;

        public string WeaponClassName
        {
            get { return weaponClassName; }
            set { weaponClassName = value; }
        }

        public List<string> equipmentNames = new List<string>();

        #endregion

        protected abstract FighterStats CreateLevelOneFighterStats();

        public void Reset()
        {
            int numberOfSkills = this.SkillNames.Count;
            this.SkillNames.RemoveRange(0, numberOfSkills); //Removes all skills
            FighterStats = CreateLevelOneFighterStats();
            skillNames.Clear();
            armorClassName = null;
            weaponClassName = null;
            SlotOne = null;
            SlotTwo = null;
            SlotThree = null;
            SetLevel(1);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Debug.Assert(CurrentAnimation != null);

            int positiveCounter = 0;
            int negativeCounter = 0;
            double timeModifier = 1;

            if (CurrentAnimation.Name.Equals("Idle"))
            {
                foreach (DamageModifier modifier in this.DamageModifiers)
                {
                    if (modifier.IsPositive)
                    {
                        positiveCounter++;
                    }
                    else
                    {
                        negativeCounter++;
                    }
                }

                if (this.HasStatusEffect("Envigored"))
                {
                    positiveCounter++;
                }
                if (positiveCounter > negativeCounter)
                {
                    timeModifier *= 1.5;
                }
                else if (positiveCounter < negativeCounter)
                {
                    timeModifier *= .5;
                }
            }
            CurrentAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds, timeModifier);
        }

        public void UnequipAccessory(int slot)
        {
            Debug.Assert(slot < 3);

            Accessory toRemove = null;

            switch (slot)
            {
                case 0:
                    if(slotOne != null)
                        toRemove = ItemPool.RequestItem(slotOne) as Accessory;
                    slotOne = null;
                    break;
                case 1:
                    if(slotTwo != null)
                        toRemove = ItemPool.RequestItem(slotTwo) as Accessory;
                    slotTwo = null;
                    break;
                default:
                    if(slotThree != null)
                        toRemove = ItemPool.RequestItem(slotThree) as Accessory;
                    slotThree = null;
                    break;
            }

            if (toRemove != null)
            {
                foreach (StatModifier mod in toRemove.Modifiers)
                {
                    FighterStats.RemoveStatModifier(mod);
                }
            }
        }

        public void GenerateStatModifiersFromEquipment()
        {
            if (slotOne != null)
            {
                Item accessory = ItemPool.RequestItem(slotOne);
                foreach (StatModifier mod in (accessory as Accessory).Modifiers)
                {
                    FighterStats.statModifiers.Add(mod);
                }
            }

            if (slotTwo != null)
            {
                Item accessory = ItemPool.RequestItem(slotTwo);
                foreach (StatModifier mod in (accessory as Accessory).Modifiers)
                {
                    FighterStats.statModifiers.Add(mod);
                }
                //EquipAccessory(accessory as Accessory, 1);
            }

            if (slotThree != null)
            {
                Item accessory = ItemPool.RequestItem(slotThree);
                foreach (StatModifier mod in (accessory as Accessory).Modifiers)
                {
                    FighterStats.statModifiers.Add(mod);
                }
            }

            if (armorClassName != null)
            {
                Equipment armor = EquipmentPool.RequestEquipment(armorClassName);
                EquipArmor(armor);
            }

            if (weaponClassName != null)
            {
                Equipment weapon = EquipmentPool.RequestEquipment(weaponClassName);
                EquipWeapon(weapon);
            }
        }

        public void LevelUpReEquip()
        {
            if (slotOne != null)
            {
                Item accessory = ItemPool.RequestItem(slotOne);
                EquipAccessory(accessory as Accessory, 0);
            }

            if (slotTwo != null)
            {
                Item accessory = ItemPool.RequestItem(slotTwo);
                EquipAccessory(accessory as Accessory, 1);
            }

            if (slotThree != null)
            {
                Item accessory = ItemPool.RequestItem(slotThree);
                EquipAccessory(accessory as Accessory, 2);
            }

            if (armorClassName != null)
            {
                Equipment armor = EquipmentPool.RequestEquipment(armorClassName);
                foreach (StatModifier mod in armor.Modifiers)
                {
                    FighterStats.statModifiers.Add(mod);
                }
                //EquipArmor(armor);
            }

            if (weaponClassName != null)
            {
                Equipment weapon = EquipmentPool.RequestEquipment(weaponClassName);
                foreach (StatModifier mod in weapon.Modifiers)
                {
                    FighterStats.statModifiers.Add(mod);
                }
                //EquipWeapon(weapon);
            }
        }

        public void EquipAccessory(Accessory accessory, int slot)
        {
            Debug.Assert(slot < 3);

            foreach (StatModifier mod in accessory.Modifiers)
            {
                FighterStats.AddStatModifier(mod);
            }

            switch (slot)
            {
                case 0:
                    slotOne = accessory.GetType().Name;
                    break;
                case 1:
                    slotTwo = accessory.GetType().Name;
                    break;
                case 2:
                    slotThree = accessory.GetType().Name;
                    break;
            }
        }

        public void EquipWeapon(Equipment weapon)
        {
            Unequip(WeaponClassName);
            WeaponClassName = weapon.ClassName;
            foreach (StatModifier mod in weapon.Modifiers)
            {
                FighterStats.AddStatModifier(mod);
            }           
        }

        public void EquipArmor(Equipment armor)
        {
            Unequip(ArmorClassName);
            ArmorClassName = armor.ClassName;
            foreach (StatModifier mod in armor.Modifiers)
            {
                FighterStats.AddStatModifier(mod);
            }
        }

        private void Unequip(string equipmentClassName)
        {
            if (string.IsNullOrEmpty(equipmentClassName))
            {
                return;
            }

            Equipment equipment = EquipmentPool.RequestEquipment(equipmentClassName);

            foreach (StatModifier mod in equipment.Modifiers)
            {
                FighterStats.RemoveStatModifier(mod);
            }
        }

        public override void SetState(State newState)
        {
            switch (newState)
            {
                case MyQuest.State.Defending:
                    StatusEffect defending = new Defending();
                    AddStatusEffect(defending);
                    //StatusEffects.Add(defending);
                    //defending.AttachEffect(this);
                    SoundSystem.Play(AudioCues.Defend);
                    break;
                case MyQuest.State.Dead:
                    SetAnimation("Dead");    // "Dead"
                    break;
                case MyQuest.State.Normal:
                    SetAnimation("Idle");
                    break;
            }

            state = newState;
        }

        // Argument level ranges from 1 to max level.
        // Internally, level ranges from 0 to maxlevel - 1.
        public void SetLevel(int level)
        {
            Debug.Assert(level > 0);

            CharacterLevelUp.LevelCharacter(this, level - 1);
        }        
    }
}