using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    /// <summary>
    /// Represents a Non-Player-Controlled Fighting Character
    /// </summary>
    public class NPCFightingCharacter : FightingCharacter
    {
        AIType behaviorType;

        public AIType BehaviorType
        {
            get { return behaviorType; }
            set { behaviorType = value; }
        }

        CombatAIController aIController = new CombatAIController();

        /// <summary>
        /// Access to the AI Controller
        /// </summary>
        public CombatAIController AIController
        {
            get { return aIController; }
            set { aIController = value; }
        }


        public NPCFightingCharacter()
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

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
                if (positiveCounter > negativeCounter) // speed up the idle animation
                {
                    timeModifier *= 1.5;
                }
                else if (positiveCounter < negativeCounter) // slow down the idle animation
                {
                    timeModifier *= .5;
                }
            }

            CurrentAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds, timeModifier);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {            
            CurrentAnimation.Draw(spriteBatch, ScreenPosition, SpriteEffects.None);

            base.Draw(spriteBatch);
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
                    break;
                case MyQuest.State.Normal:
                    SetAnimation("Idle");
                    break;
            }

            state = newState;
        }
    }
}
