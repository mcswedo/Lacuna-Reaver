using Microsoft.Xna.Framework;

namespace MyQuest
{
    /// <summary>
    /// SceneHelper that handles only PCMapCharacter movement
    /// </summary>
    class MovePCMapCharacterHelper : MoveMapCharacterHelper
    {
        float initialMoveSpeed;

        /// <summary>
        /// Constructs a new MovePCMapCharacterHelper
        /// </summary>
        /// <param name="destination">The tile position the character will be moved to</param>
        public MovePCMapCharacterHelper(Point destination)
            : base()
        {
            Initialize(destination, 0.0f);
        }


        /// <summary>
        /// Constructs a new MovePCMapCharacterHelper
        /// </summary>
        /// <param name="destination">The tile position the character will be moved to</param>
        /// <param name="moveSpeed">The speed you want the character to move at. The 
        /// character's original speed will be restored when the character reaches his destination</param>
        /// <remarks>This constructor should be used if the Npc doesn't currently exist in the CurrentMap</remarks>
        public MovePCMapCharacterHelper(Point destination, float moveSpeed)
            : base()
        {
            Initialize(destination, moveSpeed);
        }


        void Initialize(Point destination, float moveSpeed)
        {
            this.initialMoveSpeed = Party.Singleton.Leader.MoveSpeed;

            if (moveSpeed >= 0.00001f)
            {
                Party.Singleton.Leader.MoveSpeed = moveSpeed;
            }

            base.Initialize(Party.Singleton.Leader, destination);
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            /// once the helper is complete, restore the character's movespeed
            if (isComplete == true)
            {
                MyCharacter.MoveSpeed = initialMoveSpeed;
            }
        }
    }
}
