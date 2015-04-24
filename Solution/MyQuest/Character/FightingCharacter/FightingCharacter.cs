using System.Collections.Generic;
using System.Xml.Serialization;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{
    public enum State
    {
        Normal,
        Defending,
        Dead,

        /// <summary>
        /// Character is only capable of using an Item or Defending
        /// </summary>
        Stunned,

        /// <summary>
        /// Character loses a turn.
        /// </summary>
        Paralyzed,

        /// <summary>
        /// Character is immune to damage.
        /// </summary>
        Invulnerable
    }

    public enum Element
    {
        None,
        Fire,
        Water,
        Lightning,
        Ice,
        Shadow,
        Light
    }

    public abstract class FightingCharacter
    {
        private class Message
        {
            public string text;
            public double secondsToLive;
            public Color color;

            public Message(string text, Color color, double secondsToLive)
            {
                this.text = text;
                this.secondsToLive = secondsToLive;
                this.color = color;
            }
        }

        #region Name


        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        #endregion

        #region Graphics

        // I added the following to allow nega will to be flipped horizontally.
        public SpriteEffects spriteEffects = SpriteEffects.None;

        string portraitName;

        public string PortraitName
        {
            get { return portraitName; }
            set { portraitName = value; }
        }

        string iconName;

        public string IconName
        {
            get { return iconName; }
            set { iconName = value; }
        }

        string shieldName;

        public string ShieldName
        {
            get { return shieldName; }
            set { shieldName = value; }
        }

        Texture2D shield;

        [XmlIgnore]
        public Texture2D Shield
        {
            get { return shield; }
            set { shield = value; }
        }

        Texture2D portrait;

        [XmlIgnore]
        public Texture2D Portrait
        {
            get { return portrait; }
            set { portrait = value; }
        }

        Texture2D icon;

        [XmlIgnore]
        public Texture2D Icon
        {
            get { return icon; }
            set { icon = value; }
        }


        Dictionary<string, CombatAnimation> combatAnimations = new Dictionary<string,CombatAnimation>();

        [XmlIgnore]
        public Dictionary<string, CombatAnimation> CombatAnimations
        {
            get { return combatAnimations; }
            set { combatAnimations = value; }
        }

        protected void AddCombatAnimation(CombatAnimation combatAnimation)
        {
            combatAnimations.Add(combatAnimation.Name, combatAnimation);
        }

        CombatAnimation currentAnimation;

        [XmlIgnore]
        public CombatAnimation CurrentAnimation
        {
            get { return currentAnimation; }
            set { currentAnimation = value; }
        }


        Vector2 screenPosition;

        [XmlIgnore]
        public Vector2 ScreenPosition
        {
            get { return screenPosition; }
            set { screenPosition = value; }
        }


        Vector2 initialPosition;

        [XmlIgnore]
        public Vector2 InitialPosition
        {
            get { return initialPosition; }
            set { initialPosition = value; }
        }


        #endregion

        #region Skills


        protected List<string> skillNames = new List<string>();

        public List<string> SkillNames
        {
            get { return skillNames; }
            set { skillNames = value; }
        }

        public void AddSkillName(string skillName)
        {
            foreach (string name in skillNames)
            {
                if (name.Equals(skillName))
                {
                    return;
                }
            }
            skillNames.Insert(0, skillName);
        }

        string baseAttackName;

        public string BaseAttackName
        {
            get { return baseAttackName; }
            set { baseAttackName = value; }
        }


        #endregion

        #region Stats,Status, and Offsets


        FighterStats fighterStats;

        public FighterStats FighterStats
        {
            get { return fighterStats; }
            set { fighterStats = value; }
        }

        //FighterStats modifiedStats = new FighterStats();

        //public FighterStats ModifiedStats
        //{
        //    get { return modifiedStats; }
        //    //set { modifiedStats = value; }
        //}

        protected State state;

        [XmlIgnore]
        public State State
        {
            get { return state; }
        }

        protected Element elementalResistance;

        public Element ElementalResistance
        {
            get { return elementalResistance; }
        }

        protected Element elementalWeakness;

        public Element ElementalWeakness
        {
            get { return elementalWeakness; }
        }

        List<DamageModifier> damageModifiers = new List<DamageModifier>();

        [XmlIgnore]
        public List<DamageModifier> DamageModifiers
        {
            get { return damageModifiers; }
        }

        public List<StatusEffect> statusEffects = new List<StatusEffect>();

        /// <summary>
        /// The list of status effects currently active on the character
        /// </summary>
        //[XmlIgnore]
        //public List<StatusEffect> StatusEffects
        //{
        //    get { return statusEffects; }
        //}

        List<string> itemsDropped = new List<string>();
        public List<string> ItemsDropped
        {
            get { return itemsDropped; }
            set { itemsDropped = value; }
        }

        bool blind;

        /// <summary>
        /// Flag for blindness status effect
        /// </summary>
        [XmlIgnore]
        public bool Blind
        {
            get { return blind; }
            set { blind = value; }
        }

        public virtual void OnDamageReceived(int damage)
        {
        }

        protected Vector2 damageMessageOffset = new Vector2(0, 0);

        public Vector2 DamageMessagePosition
        {
            get { return screenPosition + damageMessageOffset; }
        }

        protected Vector2 statusEffectMessageOffset = new Vector2(50, 0); // This is a global value that should be adjusted for each subclass in the subclass's constructor.

        public Vector2 StatusEffectMessagePosition
        {
            get { return screenPosition + statusEffectMessageOffset; }
        }

        protected Vector2 defenseShieldOffset = new Vector2(-75, 0);

        public Vector2 DefenseShieldPosition
        {
            get { return screenPosition + defenseShieldOffset; }
        }

        protected Vector2 pointerOffset;

        /// <summary>
        /// The offset to the center of the character from the screenPosition.
        /// </summary>
        public Vector2 PointerPosition
        {
            get { return screenPosition + pointerOffset; }
        }

        protected Vector2 hitLocationOffset;

        public Vector2 HitLocation
        {
            get { return screenPosition + hitLocationOffset; }
        }

        protected Vector2 projectileOriginOffset;

        public Vector2 ProjectileOrigin
        {
            get { return screenPosition + projectileOriginOffset; }
        }

        protected Vector2 iconOffset = new Vector2(0, 0);

        public Vector2 IconLocation
        {
            get { return screenPosition + iconOffset; }
        }

        List<Message> statusEffectMessages = new List<Message>();

        Message damageMessage;

        Message healingMessage;

        CombatStatistics statistics;

        [XmlIgnore]
        public CombatStatistics Statistics
        {
            get { return statistics; }
        }

        String hitNoiseSoundCue;
        String onHitSoundCue;
        String onDeathSoundCue;
        public string HitNoiseSoundCue
        {
            get { return hitNoiseSoundCue; }
            set { hitNoiseSoundCue = value; }
        }
        public string OnHitSoundCue
        {
            get { return onHitSoundCue; }
            set { onHitSoundCue = value; }
        }
        public string OnDeathSoundCue
        {
            get { return onDeathSoundCue; }
            set { onDeathSoundCue = value; }
        }
       
        #endregion

        #region Initialization


        public virtual void Initialize()
        {
            FighterStats.ReapplyStatModifiers();
            if (this is PCFightingCharacter)
            {            
                FighterStats.RefillStamina();
            }
            statistics = new CombatStatistics();
        }

        public virtual void LoadContent()
        {
            Portrait = MyContentManager.LoadTexture(ContentPath.ToPortraits + PortraitName);

            if (IconName != null)
            {
                Icon = MyContentManager.LoadTexture(ContentPath.ToPortraits + IconName);
            }

            Shield = GameLoop.Instance.PermanentContentManager.Load<Texture2D>(Screen.interfaceTextureFolder + "blue_defend_shield_small");

            foreach (CombatAnimation anim in CombatAnimations.Values)
            {
                anim.LoadContent(ContentPath.ToCombatCharacterTextures);
            }            

            SetAnimation("Idle");
        }


        #endregion

        #region Update and Draw


        public virtual void Update(GameTime gameTime)
        {
            if (damageMessage != null)
            {
                damageMessage.secondsToLive -= gameTime.ElapsedGameTime.TotalSeconds;

                if (damageMessage.secondsToLive <= 0)
                {
                    damageMessage = null;
                }
            }

            if (healingMessage != null)
            {
                healingMessage.secondsToLive -= gameTime.ElapsedGameTime.TotalSeconds;

                if (healingMessage.secondsToLive <= 0)
                {
                    healingMessage = null;
                }
            }

            if (statusEffectMessages.Count > 0)
            {
                statusEffectMessages[0].secondsToLive -= gameTime.ElapsedGameTime.TotalSeconds;

                if (statusEffectMessages[0].secondsToLive <= 0)
                {
                    statusEffectMessages.RemoveAt(0);
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch, Color.White);
        }
        //public void DrawShield(SpriteBatch spriteBatch, Color color, bool isDead = false)
        //{
        //    if (!isDead)
        //    {
        //        spriteBatch.Draw(Shield, DefenseShieldPosition, color);
        //    }
        //}
        public virtual void Draw(SpriteBatch spriteBatch, Color color, bool isDead = false)
        {
            Debug.Assert(CurrentAnimation != null);
            CurrentAnimation.Draw(spriteBatch, ScreenPosition, spriteEffects, color);

            if (!isDead)
            {
                //Draw Status Effects
                int iconOffsetY = 0;
                float alpha = 1;
                float red = 1;
                float green = 1;
                float blue = 1;
                foreach (StatusEffect effect in statusEffects)
                {
                    if (effect.IconName != null && CurrentAnimation.Name == "Idle")
                    {
                        Texture2D icon = MyContentManager.LoadTexture(ContentPath.ToStatusEffectIcons + effect.IconName);
                        spriteBatch.Draw(icon, new Vector2(IconLocation.X - 10, IconLocation.Y + iconOffsetY - 35), Color.White);
                        iconOffsetY += 25;
                    }
                }

                if (damageMessage != null)
                {
                    spriteBatch.DrawString(Fonts.CombatDamageMessage, damageMessage.text, DamageMessagePosition, damageMessage.color);
                }

                if (healingMessage != null)
                {
                    spriteBatch.DrawString(Fonts.CombatHealingMessage, healingMessage.text, DamageMessagePosition, healingMessage.color);
                }

                if (state != State.Defending)
                {
                    alpha = .01f;
                }

                if (HasStatusEffect("Invulnerable"))
                {
                    red = 1f;
                    green = 215 / 255.0f;
                    blue = 0;
                }
                else if (HasStatusEffect("Warded") && HasStatusEffect("Armored"))
                {
                    red = .5f;
                    green = 1f;
                    blue = .5f;
                }
                else if (HasStatusEffect("Armored"))
                {
                    red = 1f;
                    green = .5f;
                    blue = .5f;
                }
                else if (HasStatusEffect("Warded"))
                {
                    red = .5f;
                    green = .5f;
                    blue = 1f;
                }

                if (HasStatusEffect("Invulnerable") || HasStatusEffect("Warded") || HasStatusEffect("Armored") || state == State.Defending)
                {
                    spriteBatch.Draw(Shield, DefenseShieldPosition, new Color(red, green, blue, alpha));
                }

                Vector2 offset = new Vector2(statusEffectMessageOffset.X, statusEffectMessageOffset.Y);

                foreach (Message message in statusEffectMessages)
                {
                    spriteBatch.DrawString(Fonts.CombatStatusEffectMessage, message.text, screenPosition + offset, message.color);
                    offset.Y += Fonts.CombatStatusEffectMessage.LineSpacing;
                }
            }
        }

        #endregion

        public void SetAnimation(string animName)
        {
            CurrentAnimation = GetAnimation(animName);
            CurrentAnimation.Play();
        }

        public CombatAnimation GetAnimation(string animName)
        {
            try
            {
                return CombatAnimations[animName];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception("Animation name undefined: " + animName);
              
            }
        }

        public abstract void SetState(State newState);

        public void ConsumeItem(ConsumableItem item)
        {
            item.Consume(FighterStats);
        }

        public bool HasDestiny()
        {
            foreach (StatusEffect statusEffect in statusEffects)
            {
                if (statusEffect is DestinyStatusEffect)
                {
                    return true;
                }
            }
            return false;
        }

        public void RemoveDestiny()
        {
            for (int i = statusEffects.Count - 1; i >= 0; i--)
            {
                StatusEffect effect = statusEffects[i];
                if (effect is DestinyStatusEffect)
                {
                    DestinyStatusEffect destinyEffect = (DestinyStatusEffect)effect;
                    destinyEffect.voodooDoll.destinyTarget = null;
                    statusEffects.RemoveAt(i);
                }
            }
            FighterStats.ReapplyStatModifiers();
        }

        public void AddDamageModifier(DamageModifier modifier)
        {
            DamageModifiers.Add(modifier);
        }

        public void AddStatusEffect(StatusEffect effect)
        {
            foreach (StatusEffect statusEffect in statusEffects)
            {
                if (effect.Name == statusEffect.Name)
                {
                    if (effect.Name == "Exposed")
                    { //Allow expose to stack.
                    }
                    else
                    {
                        statusEffect.TurnsRemaining = effect.TurnDuration;
                        return;
                    }
                }
            }

            statusEffects.Add(effect);
            effect.OnActivateEffect(this);
            FighterStats.ReapplyStatModifiers();
        }

        public void RemoveStatusEffect(StatusEffect effect)
        {
            statusEffects.Remove(effect);
            FighterStats.ReapplyStatModifiers();
        }

        public void RemoveAllNegativeStatusEffects()
        {
            for (int i = statusEffects.Count - 1; i >= 0; i--)
            {
                if (statusEffects[i].Removable && statusEffects[i].NegativeEffect)
                {
                    statusEffects.RemoveAt(i);
                }
            }
            if (state == State.Paralyzed || state == State.Stunned)
            {
                SetState(State.Normal);
            }
        }

        public void RemoveAllNegativeDamageModifiers() //Weaken is the only negative damage modifier.
        {
            for (int i = damageModifiers.Count - 1; i >= 0; i--)
            {
                if (!damageModifiers[i].IsPositive)
                {
                    damageModifiers.RemoveAt(i);
                }
            }
        }

        public bool HasStatusEffect(string name)
        {
            for (int i = 0; i < statusEffects.Count; i++)
            {
                if (statusEffects[i].Name.Equals(name))
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasStatusEffect()
        {
            if (statusEffects.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetNegativeStatusEffectsCount()
        {
            int count = 0;

            for (int i = 0; i < statusEffects.Count; i++)
            {
                if (statusEffects[i].NegativeEffect)
                {
                    count++;
                }
            }
            return count;
        }

        public virtual void OnDeath()
        {
            for (int i = statusEffects.Count - 1; i >= 0; i--)
            {
                statusEffects[i].OnDeath(this);
            }
            SoundSystem.Play(OnDeathSoundCue);
        }

        public virtual void OnHit()
        {
            SoundSystem.Play(HitNoiseSoundCue);
            SoundSystem.Play(OnHitSoundCue);
            currentAnimation.triggerRedFlux(); 
        }

        public void OnStartTurn()
        {
            for (int i = statusEffects.Count - 1; i >= 0; i--)
            {
                statusEffects[i].OnStartTurn(this);
            } 
        }

        public void OnEndCombat()
        {
            damageModifiers.Clear();

            for (int i = statusEffects.Count - 1; i >= 0; --i)
            {
                statusEffects[i].OnEndCombat(this);
            }
            statusEffects.Clear();
        }

        public void OnResurrection()
        {
            fighterStats.Health = fighterStats.ModifiedMaxHealth / 10;  
        }

        public void DisplayStatusEffect(string text, Color color)
        {
            Message statusMessage = new Message(text, color, .5);

            statusEffectMessages.Add(statusMessage);
        }
    
        public void DisplayDamage(int damage)
        {
            damageMessage = new Message("" + damage, Color.Red, .5);
        }

        public void DisplayHealing(int healing)
        {
            healingMessage = new Message("" + healing, Color.LimeGreen, .5);
        }
    }
}
