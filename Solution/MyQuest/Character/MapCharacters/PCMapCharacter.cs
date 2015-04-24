using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

/// Make it so the interaction box is only updated when the character moves
/// Replace the interaction box magic numbers with sensible constants

namespace MyQuest
{
    public class PCMapCharacter : MapCharacter
    {
        const int XTolerance = 14;
        const int LowerYTolerance = 1;
        const int UpperYTolerance = 25;
        float runSpeed = 6;

        Vector2 lastWorldPosition;

        //internal bool justPortaled = true;

        #region Fields


        bool autoMove;

        Rectangle interactionBox = new Rectangle();

        public Rectangle InteractionBox
        {
            get { return interactionBox; }
        }


        #endregion

        #region Public Methods


        public override void Initialize(Map currentMap)
        {
            base.Initialize(currentMap);

            UpdateBoundingBox();

            //currentMap.SetOccupancy(TilePosition, false);
        }

        public override void SetAutoMovement(Direction direction, Map currentMap)
        {
            autoMove = true;
            base.SetAutoMovement(direction, currentMap);
        }

        public override void Update(double elapsedTime, Map currentMap)
        {
            CurrentAnimation.Update(elapsedTime);

            if (autoMove)
            {
                UpdateAutoMovement(elapsedTime, currentMap);

                autoMove = IsMoving;
            }
            else
            {
                MoveFromCharacterInput(elapsedTime, currentMap);
            }

            UpdateBoundingBox();
        }

        public override void AltDrawShadow(SpriteBatch spriteBatch, Vector2 offSet)
        {
            base.AltDrawShadow(spriteBatch, offSet);

            if (GlobalSettings.CharDebugInfo)
            {
//                spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, transform);
                //                Rectangle destinationRectangle = new Rectangle(interactionBox.X + (int)Camera.Singleton.OffsetX, interactionBox.Y + (int)Camera.Singleton.OffsetY, interactionBox.Width, interactionBox.Height);
                Rectangle destinationRectangle = new Rectangle(interactionBox.X + (int)Camera.Singleton.OffsetX, interactionBox.Y + (int)Camera.Singleton.OffsetY, interactionBox.Width, interactionBox.Height);
                spriteBatch.Draw(ScreenManager.Singleton.BlankTexture, destinationRectangle, Color.Purple);
//                spriteBatch.End();
            }
        }

        //public override void DrawShadow(SpriteBatch spriteBatch, Matrix transform, Vector2 offSet)
        //{
        //    base.DrawShadow(spriteBatch, transform, offSet);

        //    if (GlobalSettings.CharDebugInfo)
        //    {
        //        spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, transform);
        //        Rectangle destinationRectangle = new Rectangle(interactionBox.X + (int)offSet.X, interactionBox.Y + (int)offSet.Y, interactionBox.Width, interactionBox.Height);
        //        //spriteBatch.Draw(ScreenManager.Singleton.BlankTexture, interactionBox, Color.Purple);
        //        spriteBatch.Draw(ScreenManager.Singleton.BlankTexture, destinationRectangle, Color.Purple);
        //        spriteBatch.End();
        //    }
        //}

        public void CollideWithNpc(NPCMapCharacter npc, Map currentMap)
        {
           // Debug.Assert(false); //We're removing NPC Collision.

            if (Velocity.LengthSquared() <= 0.00001f)
                return;

            Rectangle horizontalBounds = new Rectangle(
                BoundingBox.X,
                (int)lastWorldPosition.Y + UpperYTolerance,
                BoundingBox.Width - 1,
                BoundingBox.Height);

            if (horizontalBounds.Intersects(npc.BoundingBox) == true)
            {
                if (Velocity.X < 0)
                {
                    WorldPosition = new Vector2(
                        npc.BoundingBox.Right - XTolerance,
                        WorldPosition.Y);
                }
                else if (Velocity.X > 0)
                {
                    WorldPosition = new Vector2(
                        npc.BoundingBox.Left - (BoundingBox.Width + XTolerance),
                        WorldPosition.Y);
                }
            }

            Rectangle verticalBounds = new Rectangle(
                (int)lastWorldPosition.X + XTolerance,
                BoundingBox.Y,
                currentMap.TileDimensions.X - 2 * XTolerance - 1,
                BoundingBox.Height);

            if (verticalBounds.Intersects(npc.BoundingBox) == true)
            {
                if (Velocity.Y < 0)
                {
                    WorldPosition = new Vector2(
                        WorldPosition.X,
                        npc.BoundingBox.Bottom - UpperYTolerance);
                }
                else if (Velocity.Y > 0)
                {
                    WorldPosition = new Vector2(
                        WorldPosition.X,
                        npc.BoundingBox.Top - (BoundingBox.Height + UpperYTolerance));
                }
            }

            UpdateBoundingBox();
        }

        #endregion

        #region Helpers


        /// <summary>
        /// Replace the magic numbers with sensible constants and make sure
        /// this update is only called when the player actually moves
        /// </summary>
        protected override void UpdateBoundingBox()
        {
            base.UpdateBoundingBox();

            boundingBox.X += XTolerance;
            boundingBox.Y += UpperYTolerance;
            boundingBox.Height = UpperYTolerance - LowerYTolerance;
            boundingBox.Width = 64 - 2 * XTolerance;

            //string sdirection = Direction.ToString();

            if (Direction == MyQuest.Direction.East || Direction == MyQuest.Direction.SouthEast || Direction == MyQuest.Direction.NorthEast)
            {
                interactionBox.X = (int)WorldPosition.X + 48;
                interactionBox.Y = (int)WorldPosition.Y + 32;
                interactionBox.Width = 48;
                interactionBox.Height = 16;
            }
            else if (Direction == MyQuest.Direction.West || Direction == MyQuest.Direction.SouthWest || Direction == MyQuest.Direction.NorthWest)
            {
                interactionBox.X = (int)WorldPosition.X - 32;
                interactionBox.Y = (int)WorldPosition.Y + 32;
                interactionBox.Width = 48;
                interactionBox.Height = 16;
            }
            else if (Direction == MyQuest.Direction.North)
            {
                interactionBox.X = (int)WorldPosition.X + 24;
                interactionBox.Y = (int)WorldPosition.Y - 24;
                interactionBox.Width = 16;
                interactionBox.Height = 48;
            }
            else if (Direction == MyQuest.Direction.South)
            {
                interactionBox.X = (int)WorldPosition.X + 24;
                interactionBox.Y = (int)WorldPosition.Y + 56;
                interactionBox.Width = 16;
                interactionBox.Height = 48;
            }
        }
                
        protected override void UpdateAutoMovement(double elapsedTime, Map currentMap)
        {
            WorldPosition += Velocity;

            if (Vector2.Distance(WorldPosition, DestinationPosition) <= Velocity.Length())
            {
                /// Recalculate positions
                TilePosition = Utility.ToTileCoordinates(
                    new Vector2(DestinationPosition.X + currentMap.TileDimensions.X / 2, DestinationPosition.Y + currentMap.TileDimensions.Y / 2),
                    currentMap.TileDimensions);

                WorldPosition = DestinationPosition;

                CurrentAnimation = IdleAnimation;
                CurrentAnimation.Play(Direction.ToString());
                IsMoving = false;

                NewTileReachedEvent(null, null);

                Velocity = Vector2.Zero;
            }
        }

        private void MoveFromCharacterInput(double elapsedTime, Map currentMap)
        {
            //if (justPortaled)
            //{
            //    justPortaled = false;
            //    return;
            //}

            Point currentTile = Utility.ToTileCoordinates(
                WorldPosition + new Vector2(currentMap.TileDimensions.X / 2, currentMap.TileDimensions.Y / 2), currentMap.TileDimensions);

            Vector2 motion = InputState.GetPlayerMoveVector();

            Velocity = motion;

            float modifiedMoveSpeed = MoveSpeed;

            if (InputState.IsRunning())
            {
                #if DEBUG
                    modifiedMoveSpeed = runSpeed;
                #endif

                if (Party.Singleton.GameState.Inventory.ItemCount(typeof(QuickeningBoots)) == 1)
                {
                    modifiedMoveSpeed = runSpeed;
                }
            }
            else
            {
                modifiedMoveSpeed = MoveSpeed;
            }

            if (motion.LengthSquared() > 0.0001f)
            {
                Vector2 desiredWorldPosition = WorldPosition + (motion * modifiedMoveSpeed * currentMap.GetCollisionValue(currentTile));

                Rectangle horizontalBounds = new Rectangle(
                    (int)desiredWorldPosition.X + XTolerance,
                    (int)WorldPosition.Y + UpperYTolerance,
                    currentMap.TileDimensions.X - 2 * XTolerance,
                    currentMap.TileDimensions.Y - LowerYTolerance - UpperYTolerance - 1);

                Rectangle verticalBounds = new Rectangle(
                    (int)WorldPosition.X + XTolerance,
                    (int)desiredWorldPosition.Y + UpperYTolerance,
                    currentMap.TileDimensions.X - 2 * XTolerance - 1,
                    currentMap.TileDimensions.Y - LowerYTolerance - UpperYTolerance - 1);

                if (motion.X < 0)
                {
                    Point upperLeftTile = Utility.ToTileCoordinates(
                        new Vector2(horizontalBounds.Left, horizontalBounds.Top), currentMap.TileDimensions);

                    Point lowerLeftTile = Utility.ToTileCoordinates(
                        new Vector2(horizontalBounds.Left, horizontalBounds.Bottom), currentMap.TileDimensions);

                    if (!currentMap.CanEnterTile(upperLeftTile) || !currentMap.CanEnterTile(lowerLeftTile))
                        desiredWorldPosition.X = (upperLeftTile.X + 1) * currentMap.TileDimensions.X - XTolerance;
                }
                else if (motion.X > 0)
                {
                    Point upperRightTile = Utility.ToTileCoordinates(
                        new Vector2(horizontalBounds.Right, horizontalBounds.Top), currentMap.TileDimensions);

                    Point lowerRightTile = Utility.ToTileCoordinates(
                        new Vector2(horizontalBounds.Right, horizontalBounds.Bottom), currentMap.TileDimensions);

                    if (!currentMap.CanEnterTile(upperRightTile) || !currentMap.CanEnterTile(lowerRightTile))
                        desiredWorldPosition.X = (upperRightTile.X - 1) * currentMap.TileDimensions.X + XTolerance;
                }

                if (motion.Y < 0)
                {
                    Point upperLeftTile = Utility.ToTileCoordinates(
                        new Vector2(verticalBounds.Left, verticalBounds.Top), currentMap.TileDimensions);

                    Point upperRightTile = Utility.ToTileCoordinates(
                        new Vector2(verticalBounds.Right, verticalBounds.Top), currentMap.TileDimensions);

                    if (!currentMap.CanEnterTile(upperLeftTile) || !currentMap.CanEnterTile(upperRightTile))
                        desiredWorldPosition.Y = (upperRightTile.Y + 1) * currentMap.TileDimensions.Y - UpperYTolerance;
                }
                else if (motion.Y > 0)
                {
                    Point lowerLeftTile = Utility.ToTileCoordinates(
                        new Vector2(verticalBounds.Left, verticalBounds.Bottom), currentMap.TileDimensions);

                    Point lowerRightTile = Utility.ToTileCoordinates(
                        new Vector2(verticalBounds.Right, verticalBounds.Bottom), currentMap.TileDimensions);

                    if (!currentMap.CanEnterTile(lowerLeftTile) || !currentMap.CanEnterTile(lowerRightTile))
                        desiredWorldPosition.Y = (lowerRightTile.Y - 1) * currentMap.TileDimensions.Y + LowerYTolerance;
                }

                lastWorldPosition = WorldPosition;
                WorldPosition = desiredWorldPosition;

                Direction = InputState.GetPlayerMoveDirection();
                CurrentAnimation = WalkingAnimation;

                Point desiredTile = Utility.ToTileCoordinates(
                        WorldPosition + new Vector2(currentMap.TileDimensions.X / 2, currentMap.TileDimensions.Y / 2), currentMap.TileDimensions);

                if (desiredTile != currentTile)
                {
                    TilePosition = desiredTile;
                    NewTileReachedEvent(null, null);
                }
            }
            else
            {
                CurrentAnimation = IdleAnimation;
            }

            CurrentAnimation.Play(Direction.ToString());
        }
        

        #endregion
    }
}
