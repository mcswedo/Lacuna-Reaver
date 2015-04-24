using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    /// <summary>
    /// SceneHelper that handles map character movement
    /// </summary>
    public class MoveMapCharacterHelper : SceneHelper
    {
        #region Fields


        /// <summary>
        /// The character that needs to be moved
        /// </summary>
        MapCharacter character;

        /// <summary>
        /// Returns the name of the character this helper is
        /// controlling.
        /// </summary>
        protected MapCharacter MyCharacter
        {
            get { return character; }
        }


        /// <summary>
        /// The steps the character is required to take
        /// in order to reach his destination
        /// </summary>
        List<Direction> steps = new List<Direction>();

        /// <summary>
        /// The current step the character is performing
        /// </summary>
        int currentStep = 0;


        #endregion

        protected MoveMapCharacterHelper()
            : base()
        {
        }

        /// <summary>
        /// Initializes the Character movement helper
        /// </summary>
        /// <param name="character">The character to be moved</param>
        /// <param name="destination">The desired tile destination for the character</param>
        protected void Initialize(MapCharacter character, Point destination)
        {
            this.character = character;
            this.steps = Utility.GetPathTo(character.TilePosition, destination);

            if (steps.Count == 0)
            {
                isComplete = true;
            }
            else
            {
                character.SetAutoMovement(steps[currentStep], Party.Singleton.CurrentMap);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if(isComplete == true)
            {
                return;
            }

            character.Update(gameTime.ElapsedGameTime.TotalMilliseconds, Party.Singleton.CurrentMap);

            /// The character will stop moving once he reaches his currentDestination
            if (character.IsMoving == false)
            {
                /// If the character has reached his final destination
                if (++currentStep >= steps.Count)
                {
                    isComplete = true;
                }
                else
                {
                    /// Otherwise, tell the character to move to the next tile
                    character.SetAutoMovement(steps[currentStep], Party.Singleton.CurrentMap);
                }  
            }
        }
    }
}
