using Microsoft.Xna.Framework;

namespace MyQuest
{
    /// <summary>
    /// SceneHelper that handles only NPCMapCharacter movement
    /// </summary>
    public class MoveNpcCharacterHelper : MoveMapCharacterHelper
    {
        float initialMoveSpeed;
        bool setIdleOnly = false;

        #region Constructors


        /// <summary>
        /// Constructs a new MoveNpcCharacterHelper
        /// </summary>
        /// <param name="characterName">The name of the character you want to move</param>
        /// <param name="destination">The tile position the character will be moved to</param>
        public MoveNpcCharacterHelper(string characterName, Point destination)
            : base()
        {
            Initialize(characterName, destination, 0.0f);
        }

        /// <summary>
        /// Constructs a new MoveNpcCharacterHelper
        /// </summary>
        /// <param name="characterName">The name of the character you want to move</param>
        /// <param name="destination">The tile position the character will be moved to</param>
        /// <param name="moveSpeed">The speed you want the character to move at. The 
        /// character's original speed will be restored when the character reaches his destination</param>
        public MoveNpcCharacterHelper(string characterName, Point destination, float moveSpeed)
            : base()
        {
            Initialize(characterName, destination, moveSpeed);
        }


        /// <summary>
        /// Constructs a new MoveNpcCharacterHelper
        /// </summary>
        /// <param name="characterName">The name of the character you want to move</param>
        /// <param name="spawnPosition">The position you want the character to spawn at</param>
        /// <param name="isPermanent">Whether the map modification required to add the character is permanent or not</param>
        /// <param name="destination">The tile position the character will be moved to</param>
        /// <remarks>This constructor should be used if the Npc doesn't currently exist in the CurrentMap</remarks>
        public MoveNpcCharacterHelper(string characterName, Point spawnPosition, bool isPermanent, Point destination)
            : base()
        {
            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                characterName,
                spawnPosition,
                ModAction.Add,
                isPermanent);

            Initialize(characterName, destination, 0.0f);
        }


        /// <summary>
        /// Constructs a new MoveNpcCharacterHelper
        /// </summary>
        /// <param name="characterName">The name of the character you want to move</param>
        /// <param name="spawnPosition">The position you want the character to spawn at</param>
        /// <param name="isPermanent">Whether the map modification required to add the character is permanent or not</param>
        /// <param name="destination">The tile position the character will be moved to</param>
        /// <param name="moveSpeed">The speed you want the character to move at. The 
        /// character's original speed will be restored when the character reaches his destination</param>
        /// <remarks>This constructor should be used if the Npc doesn't currently exist in the CurrentMap</remarks>
        public MoveNpcCharacterHelper(string characterName, Point spawnPosition, bool isPermanent, Point destination, float moveSpeed)
            : base()
        {
            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                characterName,
                spawnPosition,
                ModAction.Add,
                isPermanent);

            Initialize(characterName, destination, moveSpeed);
        }


        #endregion

        #region Methods


        void Initialize(string characterName, Point destination, float moveSpeed)
        {
            NPCMapCharacter character = Party.Singleton.CurrentMap.GetNPC(characterName);

            this.initialMoveSpeed = character.MoveSpeed;

            if (moveSpeed >= 0.00001f)
            {                
                character.MoveSpeed = moveSpeed;
            }
            
            base.Initialize(character, destination);
        }

        /// <summary>
        /// Make the npc IdleOnly after he reaches his destination
        /// </summary>
        public void SetIdleOnlyOnComplete()
        {
            setIdleOnly = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (isComplete == true)
            {
                /// We need to update the npc's spawn position so he will respawn correctly if the map is reset
                Party.Singleton.CurrentMap.ResetSpawnPosition(MyCharacter.Name, MyCharacter.TilePosition);
                MyCharacter.MoveSpeed = initialMoveSpeed;

                if (setIdleOnly)
                {
                    NPCMapCharacter character = MyCharacter as NPCMapCharacter;
                    character.IdleOnly = true;
                }
            }
        }


        #endregion
    }
}
