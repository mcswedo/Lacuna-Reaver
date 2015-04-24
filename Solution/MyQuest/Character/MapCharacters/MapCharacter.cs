using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

/// Make it so the bounding box is only updated when the character changes position

namespace MyQuest
{
    /// <summary>
    /// The direction the character is facing
    /// </summary>
    public enum Direction
    {
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest
    }

    /// <summary>
    /// Represents a character on the tile map
    /// </summary>
    public abstract class MapCharacter
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// change to a delegate ?
        /// </summary>
        internal event EventHandler newTileReachedEvent;

        protected void NewTileReachedEvent(object sender, EventArgs e)
        {
            if (newTileReachedEvent != null)
                newTileReachedEvent(sender, e);
        }

        #region Graphics


        string portraitName;

        public string PortraitName
        {
            get { return portraitName; }
            set { portraitName = value; }
        }


        Texture2D portrait;

        [ContentSerializerIgnore]
        public Texture2D Portrait
        {
            get { return portrait; }
            set { portrait = value; }
        }

        Texture2D shadow;

        [ContentSerializerIgnore]
        public Texture2D Shadow
        {
            get { return shadow; }
            set { shadow = value; }
        }
        
        bool isCastingShadow = true;

        [ContentSerializer(Optional = true)]
        public bool IsCastingShadow
        {
            get { return isCastingShadow; }
            set { isCastingShadow = value; }
        } //GW

        SpriteAnimation idleAnimation;

        [ContentSerializer(Optional = true)]
        public SpriteAnimation IdleAnimation
        {
            get { return idleAnimation; }
            set { idleAnimation = value; }
        }


        SpriteAnimation walkingAnimation;

        [ContentSerializer(Optional = true)]
        public SpriteAnimation WalkingAnimation
        {
            get { return walkingAnimation; }
            set { walkingAnimation = value; }
        }


        SpriteAnimation currentAnimation;

        [ContentSerializerIgnore]
        public SpriteAnimation CurrentAnimation
        {
            get { return currentAnimation; }
            set { currentAnimation = value; }
        }


        #endregion

        #region Movement


        Point tilePosition;

        [ContentSerializer(Optional = true)]
        public Point TilePosition
        {
            get { return tilePosition; }
            set { tilePosition = value; }
        }


        protected Vector2 worldPosition;

        [ContentSerializer(Optional = true)]
        public Vector2 WorldPosition
        {
            get { return worldPosition; }
            set { worldPosition = value; }
        }


        Direction direction;

        [ContentSerializerIgnore]
        public Direction Direction
        {
            get { return direction; }
            set { direction = value; }
        }


        float moveSpeed;

        [ContentSerializer(Optional = true)]
        public float MoveSpeed
        {
            get { return moveSpeed; }
            set { moveSpeed =  value ; }
        }

        bool isMoving;

        [ContentSerializerIgnore]
        public bool IsMoving
        {
            get { return isMoving; }
            set { isMoving = value; }
        }


        Vector2 destinationPosition;

        [ContentSerializerIgnore]
        public Vector2 DestinationPosition
        {
            get { return destinationPosition; }
            set { destinationPosition = value; }
        }


        Vector2 velocity;

        [ContentSerializerIgnore]
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }


        float collisionRadius = 28;

        [ContentSerializer(Optional = true)]
        public float CollisionRadius
        {
            get { return collisionRadius; }
            set { collisionRadius = value; }
        }


        bool isColliding;

        [ContentSerializerIgnore]
        public bool IsColliding
        {
            get { return isColliding; }
            set { isColliding = value; }
        }


        protected Rectangle boundingBox = new Rectangle(0, 0, 48, 64);  //x and y are being overwritten.

        [ContentSerializer(Optional = true)]
        public Rectangle BoundingBox
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }


        #endregion

        #region Methods


        /// <summary>
        /// Initialize to a ready state
        /// </summary>
        /// <param name="currentMap">The map containing this character</param>
        public virtual void Initialize(Map currentMap)
        {
            CurrentAnimation = IdleAnimation;

            if (CurrentAnimation != null)
                CurrentAnimation.Play(Direction.ToString());

            //currentMap.SetOccupancy(TilePosition, true);

            isMoving = false;
            velocity = Vector2.Zero;
        }


        /// <summary>
        /// Load any required content
        /// </summary>
        public virtual void LoadContent()
        {
            Portrait = MyContentManager.LoadTexture(ContentPath.ToPortraits + PortraitName);

            Shadow = MyContentManager.LoadTexture(ContentPath.ToMapCharacterTextures + "CharacterShadow");

            if (IdleAnimation != null)
            {
                IdleAnimation.LoadContent(ContentPath.ToMapCharacterTextures);
            }

            if (WalkingAnimation != null)
            {
                WalkingAnimation.LoadContent(ContentPath.ToMapCharacterTextures);
            }
        }

        public abstract void Update(double elapsedTime, Map currentMap);

        public virtual void AltDraw(SpriteBatch spriteBatch)
        {
            if (CurrentAnimation != null)
            {
                CurrentAnimation.Draw(spriteBatch, WorldPosition + new Vector2(Camera.Singleton.OffsetX, Camera.Singleton.OffsetY));
            }
        }

        public virtual void AltDraw(SpriteBatch spriteBatch, Color color)
        {
            if (CurrentAnimation != null)
            {
                CurrentAnimation.Draw(spriteBatch, WorldPosition + new Vector2(Camera.Singleton.OffsetX, Camera.Singleton.OffsetY), color);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (CurrentAnimation != null)
            {
                CurrentAnimation.Draw(spriteBatch, WorldPosition);
            }
        }

        public virtual void DrawForGameMap(SpriteBatch spriteBatch, Vector2 pos)
        {
            if (CurrentAnimation != null)
            {
                CurrentAnimation.DrawForGameMap(spriteBatch, pos);
            }
        }

        public virtual void AltDrawShadow(SpriteBatch spriteBatch, Vector2 offSet)
        {
            if (IsCastingShadow)
            {
//                spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, transform);
                spriteBatch.Draw(Shadow, new Vector2(worldPosition.X + offSet.X + Camera.Singleton.OffsetX, worldPosition.Y + offSet.Y + Camera.Singleton.OffsetY), Color.White);
//                spriteBatch.End();
            }

            if (GlobalSettings.CharDebugInfo)
            {
//                spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, transform);
                Rectangle destinationRectangle = new Rectangle(boundingBox.X + (int)Camera.Singleton.OffsetX, boundingBox.Y + (int)Camera.Singleton.OffsetY, boundingBox.Width, boundingBox.Height);
                spriteBatch.Draw(ScreenManager.Singleton.BlankTexture, destinationRectangle, Color.Lime);
//                spriteBatch.End();
            }
        }

        //public virtual void DrawShadow(SpriteBatch spriteBatch, Matrix transform, Vector2 offSet)
        //{
        //    if (IsCastingShadow)
        //    {
        //        spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, transform);
        //        spriteBatch.Draw(Shadow, new Vector2(worldPosition.X + offSet.X, worldPosition.Y + offSet.Y), Color.White);
        //        spriteBatch.End();
        //    }

        //    if (GlobalSettings.CharDebugInfo)
        //    {
        //        spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, transform);
        //        spriteBatch.Draw(ScreenManager.Singleton.BlankTexture, boundingBox, Color.Lime);
        //        spriteBatch.End();
        //    }
        //}


        /// <summary>
        /// Change the character's direction to face a target
        /// </summary>
        /// <param name="targetTilePosition">The tile position of the target</param>
        public virtual void FaceTarget(Point targetTilePosition)
        {
            if (targetTilePosition.X < TilePosition.X)
            {
                Direction = Direction.West;
            }
            else if (targetTilePosition.X > tilePosition.X)
            {
                Direction = Direction.East;
            }
            else
            {
                Direction = (targetTilePosition.Y < tilePosition.Y ? Direction.North : Direction.South);
            }

            if (CurrentAnimation != null)
                CurrentAnimation.Play(Direction.ToString());
        }


        public virtual void FaceDirection(Direction direction)
        {
            Direction = direction;

            if (CurrentAnimation != null)
                CurrentAnimation.Play(Direction.ToString());
        }


        public virtual void SetAutoMovement(Direction direction, Map currentMap)
        {
            if (isMoving)
                return;

            this.Direction = direction;

            if (Direction == Direction.North)
            {
                destinationPosition = new Vector2(WorldPosition.X, WorldPosition.Y - currentMap.TileDimensions.Y);
                velocity = new Vector2(0, -MoveSpeed * currentMap.GetCollisionValue(Utility.ToTileCoordinates(destinationPosition, currentMap.TileDimensions)));
            }
            else if (Direction == Direction.East)
            {
                destinationPosition = new Vector2(WorldPosition.X + currentMap.TileDimensions.X, WorldPosition.Y);
                velocity = new Vector2(MoveSpeed * currentMap.GetCollisionValue(Utility.ToTileCoordinates(destinationPosition, currentMap.TileDimensions)), 0);
            }
            else if (Direction == Direction.South)
            {
                destinationPosition = new Vector2(WorldPosition.X, WorldPosition.Y + currentMap.TileDimensions.Y);
                velocity = new Vector2(0, MoveSpeed * currentMap.GetCollisionValue(Utility.ToTileCoordinates(destinationPosition, currentMap.TileDimensions)));
            }
            else
            {
                destinationPosition = new Vector2(WorldPosition.X - currentMap.TileDimensions.X, WorldPosition.Y);
                velocity = new Vector2(-MoveSpeed * currentMap.GetCollisionValue(Utility.ToTileCoordinates(destinationPosition, currentMap.TileDimensions)), 0);
            }

            Point destinationPoint = Utility.ToTileCoordinates(destinationPosition, currentMap.TileDimensions);

            /// If the destination is impassable the velocity will be zero. Check 
            /// that it isn't and that no other entities occupy the destination 
            if (!currentMap.IsTileOccupied(destinationPoint) && velocity.LengthSquared() > 0.00001f)
            {
                CurrentAnimation = WalkingAnimation;

                /// Set the occupancy flag at my next location
                //currentMap.SetOccupancy(destinationPoint, true);

                IsMoving = true;
            }

            CurrentAnimation.Play(Direction.ToString());
        }


        protected virtual void UpdateAutoMovement(double elapsedTime, Map currentMap)
        {
            WorldPosition += Velocity;

            if (Vector2.Distance(WorldPosition, DestinationPosition) <= Velocity.Length())
            {
                /// Clear the occupancy flag at my previous position
                //currentMap.SetOccupancy(TilePosition, false);

                /// Recalculate positions
                TilePosition = Utility.ToTileCoordinates(DestinationPosition, currentMap.TileDimensions);
                WorldPosition = DestinationPosition;

                CurrentAnimation = IdleAnimation;
                CurrentAnimation.Play(Direction.ToString());
                IsMoving = false;

                NewTileReachedEvent(null, null);

                Velocity = Vector2.Zero;
            }
        }


        /// <summary>
        /// Determines if two MapCharacters are colliding
        /// </summary>
        public static bool AreColliding(MapCharacter a, MapCharacter b)
        {
            Vector2 distance = b.WorldPosition - a.WorldPosition;

            return (distance.Length() < (b.CollisionRadius + a.CollisionRadius));
        }


        protected virtual void UpdateBoundingBox()
        {
            boundingBox.X = (int)worldPosition.X;
            boundingBox.Y = (int)worldPosition.Y;
        }


        #endregion
    }
}
