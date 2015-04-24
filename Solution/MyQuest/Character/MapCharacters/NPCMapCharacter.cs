using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class NPCMapCharacter : MapCharacter
    {
        #region Controller


        string controllerName;

        /// <summary>
        /// The class name of the Dialog that this NPC uses
        /// </summary>
        public string ControllerName
        {
            get { return controllerName; }
            set { controllerName = value; }
        }


        #endregion

        #region Movement


        bool idleOnly;

        public bool IdleOnly
        {
            get { return idleOnly; }
            set { idleOnly = value; }
        }


        double moveDelay;

        [ContentSerializer(Optional = true)]
        public double MoveDelay
        {
            get { return moveDelay; }
            set { moveDelay = value; }
        }


        bool collidingWithPlayer = false;

        [ContentSerializerIgnore]
        public bool CollidingWithPlayer
        {
            get { return collidingWithPlayer; }
            set { collidingWithPlayer = value; }
        }

        bool canInteract = false;

        [ContentSerializerIgnore]
        public bool CanInteract
        {
            get { return canInteract; }
            set { canInteract = value; }
        }

        double movementTicker = 0;


        #endregion

        #region Methods


        public override void Initialize(Map currentMap)
        {
            base.Initialize(currentMap);

            if (CurrentAnimation == null)
                IdleOnly = true;

            movementTicker = TimeSpan.FromSeconds(MoveDelay).TotalMilliseconds;
            WorldPosition = Utility.ToWorldCoordinates(TilePosition, currentMap.TileDimensions);

            UpdateBoundingBox();
        }
        
        public override void Update(double elapsedTime, Map currentMap)
        {
            if (IdleAnimation == null)
                return; 

            CurrentAnimation.Update(elapsedTime); //Moved this to execute first because some NPC's may have Idle animation

            if (idleOnly)
                return;

            if (!IsMoving)
            {
                movementTicker -= elapsedTime;
                if (movementTicker <= 0.0)
                {
                    movementTicker = TimeSpan.FromSeconds(MoveDelay).TotalMilliseconds;

                    /// NPCs only move in the cardinal directions which happen 
                    /// to be 0, 2, 4 and 6 within the Direction enum.
                    Direction = (Direction)(Utility.RNG.Next(4) * 2);

                    Velocity = Vector2.Zero;

                    SetAutoMovement(Direction, currentMap);
                }
            }
            else
            {
                UpdateAutoMovement(elapsedTime, currentMap);
            }           

            UpdateBoundingBox();
        }

        public override void SetAutoMovement(Direction direction, Map currentMap)
        {
            //if (idleOnly == true)
            //    throw new Exception("Calling SetAutoMovement on an idle only npc will cause undefined behavior");

            idleOnly = false;
            base.SetAutoMovement(direction, currentMap);
        }

        public void Interact()
        {
            if (string.IsNullOrEmpty(ControllerName))
                return;

            NPCMapCharacterController controller = Utility.CreateInstanceFromName<NPCMapCharacterController>(
                this.GetType().Namespace,
                ControllerName);

            controller.Interact();
        }

        protected override void UpdateBoundingBox()
        {
            boundingBox.X = (int)worldPosition.X + 8;
            boundingBox.Y = (int)worldPosition.Y;
        }

        public void AltDrawDialogBubble(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TileMapScreen.Instance.DialogBubble, WorldPosition + new Vector2(-5, -34) + new Vector2(Camera.Singleton.OffsetX, Camera.Singleton.OffsetY), Color.White);
        }

        public void DrawDialogBubble(SpriteBatch spriteBatch)
        {
            //            Matrix transform = Camera.Singleton.Transformation;
            //            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, transform);
            spriteBatch.Draw(TileMapScreen.Instance.DialogBubble, WorldPosition + new Vector2(-5, -34), Color.White);
            //            spriteBatch.End();
        }

        public override void AltDraw(SpriteBatch spriteBatch)
        {
            if (CurrentAnimation != null)
            {
                CurrentAnimation.AltDraw(spriteBatch, WorldPosition);
                if (canInteract && TileMapScreen.Instance.WillHandleUserInput)
                {
                    //                    GameLoop.Instance.npcWithDialogBubble = this;
                    TileMapScreen.Instance.NpcWithDialogBubble = this;
                }
            }
        }

        public void DrawForGameMap(SpriteBatch spriteBatch)
        {
            if (CurrentAnimation != null)
            {
                CurrentAnimation.DrawForGameMap(spriteBatch, new Vector2(WorldPosition.X / 2 + Camera.Singleton.OffsetX,
                WorldPosition.Y / 2 + Camera.Singleton.OffsetY));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (CurrentAnimation != null)
            {
                CurrentAnimation.Draw(spriteBatch, WorldPosition);
                if (canInteract && TileMapScreen.Instance.WillHandleUserInput)
                {
                    //                    GameLoop.Instance.npcWithDialogBubble = this;
                    TileMapScreen.Instance.NpcWithDialogBubble = this;
                }
            }
        }


        #endregion
    }
}
