using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace MyQuest
{
    public class Map
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool IsWorldMap()
        {
            return name.Equals(Maps.overworld);
        }

        #region Graphics

        string textureSheetName;

        public string TextureSheetName
        {
            get { return textureSheetName; }
            set { textureSheetName = value; }
        }

        Texture2D tileSheet;

        [ContentSerializerIgnore]
        public Texture2D TileSheet
        {
            get { return tileSheet; }
            set { tileSheet = value; }
        }

        Vector2 firstGameMapTilePos = new Vector2();
        [ContentSerializerIgnore]
        public Vector2 FirstGameMapTilePos
        {
            get { return firstGameMapTilePos; }
        }

        Vector2 lastGameMapTilePos = new Vector2();
        [ContentSerializerIgnore]
        public Vector2 LastGameMapTilePos
        {
            get { return lastGameMapTilePos; }
        }

        List<Rectangle> sourceRect = new List<Rectangle>();

        List<Rectangle> destinationRect = new List<Rectangle>();


        #endregion

        #region Dimensions


        Point tileDimensions;

        public Point TileDimensions
        {
            get { return tileDimensions; }
            set { tileDimensions = value; }
        }

        Point dimensionsInTiles;

        public Point DimensionsInTiles
        {
            get { return dimensionsInTiles; }
            set { dimensionsInTiles = value; }
        }

        Point dimensionsInPixels;

        [ContentSerializerIgnore]
        public Point DimensionsInPixels
        {
            get 
            { 
                return new Point(tileDimensions.X * dimensionsInTiles.X, tileDimensions.Y * dimensionsInTiles.Y); 
            }
            set { dimensionsInPixels = value; }
        }


        #endregion

        #region Layers


        short[] groundLayer;

        public short[] GroundLayer
        {
            get { return groundLayer; }
            set { groundLayer = value; }
        }


        short[] foregroundLayer;

        public short[] ForeGroundLayer
        {
            get { return foregroundLayer; }
            set { foregroundLayer = value; }
        }


        short[] fringeLayer;

        public short[] FringeLayer
        {
            get { return fringeLayer; }
            set { fringeLayer = value; }
        }


        float[] collisionLayer;

        public float[] CollisionLayer
        {
            get { return collisionLayer; }
            set { collisionLayer = value; }
        }


        float[] damageLayer;

        public float[] DamageLayer
        {
            get { return damageLayer; }
            set { damageLayer = value; }
        }


        short[] combatLayer;

        public short[] CombatLayer
        {
            get { return combatLayer; }
            set { combatLayer = value; }
        }


        bool[] occupancyLayer;


        #endregion

        #region NPCs


        List<NPCEntry> npcEntries;

        /// <summary>
        /// A listing of the npcs that will appear in this map along with their initial positions
        /// </summary>        
        public List<NPCEntry> NpcEntries
        {
            get { return npcEntries; }
            set { npcEntries = value; }
        }


        List<NPCMapCharacter> npcs;

        public IList<NPCMapCharacter> NPC
        {
            get { return npcs.AsReadOnly(); }
        }


        public void ResetSpawnPosition(string npcName, Point newSpawnPosition)
        {
            foreach (NPCEntry entry in npcEntries)
            {
                if (entry.AssetName == npcName)
                {
                    entry.SpawnPosition = newSpawnPosition;
                }
            }
        }

        public void ResetSpawnDirection(string npcName, Direction newSpawnDirection)
        {
            foreach (NPCEntry entry in npcEntries)
            {
                if (entry.AssetName == npcName)
                {
                    entry.SpawnDirection = newSpawnDirection;
                }
            }
        }


        #endregion

        #region CutScenes


        List<CutSceneEntry> cutSceneEntries;

        public List<CutSceneEntry> CutSceneEntries
        {
            get { return cutSceneEntries; }
            set { cutSceneEntries = value; }
        }


        #endregion

        #region Portals


        List<Portal> portals;

        public List<Portal> Portals
        {
            get { return portals; }
            set { portals = value; }
        }


        #endregion

        #region Music


        string musicCueName;

        //[ContentSerializer(Optional = true)]
        public string MusicCueName
        {
            get { return musicCueName; }
            set { musicCueName = value; }
        }


        #endregion

        #region Initialization


        public void LoadContent()
        {
            tileSheet = MyContentManager.LoadMapTexture(ContentPath.ToMapTextures + TextureSheetName);
        }

        /// <summary>
        /// Perform all of the one-time initialization stuff
        /// </summary>
        public void Initialize()
        {
            occupancyLayer = new bool[groundLayer.Length];

            dimensionsInPixels = new Point(
                dimensionsInTiles.X * tileDimensions.X,
                dimensionsInTiles.Y * tileDimensions.Y);

            /// Here we do some pre-calculations to speed up the draw method

            int cols = TileSheet.Width / tileDimensions.X;
            int rows = TileSheet.Height / tileDimensions.Y;

            /// Calculate the source rectangles from the tileSheet. Since the 
            /// tileSheet for a particular map doesn't change at run-time, doing 
            /// this now means we don't ever have to do it again
            for (int y = 0; y < rows; ++y)
            {
                for (int x = 0; x < cols; ++x)
                {
                    sourceRect.Add(new Rectangle(
                        x * tileDimensions.X,
                        y * tileDimensions.Y,
                        tileDimensions.X,
                        tileDimensions.Y));
                }
            }

            /// Calculate the destination rectangles. The destination of a tile graphic to
            /// the screen never changes so if we calculate it here we never have to do it again
            for (int y = 0; y < dimensionsInTiles.Y; ++y)
            {
                for (int x = 0; x < dimensionsInTiles.X; ++x)
                {
                    destinationRect.Add(new Rectangle(
                        x * tileDimensions.X,
                        y * tileDimensions.Y,
                        tileDimensions.X,
                        tileDimensions.Y));
                }
            }
        }

        public void Reset()
        {
            npcs = new List<NPCMapCharacter>();
            foreach (NPCEntry entry in npcEntries)
            {
                NPCMapCharacter npc = MyContentManager.LoadNPCMapCharacter(ContentPath.ToNPCMapCharacters + entry.AssetName);
                npc.TilePosition = entry.SpawnPosition;
                npc.Direction = entry.SpawnDirection;
                if (entry.ChangeIdle)
                {
                    if (!entry.IdleOnly) 
                    {
                        Debug.Assert(npc.WalkingAnimation != null); /*only need to check if npc has WalkingAnimation 
                                                                      if npc.IdleOnly is going to be set to false*/
                    }

                    npc.IdleOnly = entry.IdleOnly;
                }

                npc.Initialize(this);
                npcs.Add(npc);
            }
        }

        public void ResetIdle(string assetName, bool idleOnly)
        {
            NPCMapCharacter npc = MyContentManager.LoadNPCMapCharacter(ContentPath.ToNPCMapCharacters + assetName);
            npc.IdleOnly = idleOnly;
        }

        #endregion

        #region Query Methods

        public float GetDamageValue(Point mapPosition)
        {
            int index = GetIndex(mapPosition);

            if (index < 0 || index >= damageLayer.Length)
                return 0;
           
            return damageLayer[index];           
        }

        public float GetCollisionValue(Point mapPosition)
        {
            int index = GetIndex(mapPosition);

            if (index < 0 || index >= collisionLayer.Length)
                return 0;

            return collisionLayer[index];
        }

        public bool IsTileOccupied(Point mapPosition)
        {
            int index = GetIndex(mapPosition);

            try
            {
                return occupancyLayer[index];
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }

        public void SetOccupancy(Point mapPosition, bool value)
        {
            int index = GetIndex(mapPosition);

            try
            {
                occupancyLayer[index] = value;
            }
            catch (IndexOutOfRangeException)
            {
            }
        }

        public bool CanEnterTile(Point mapPosition)
        {
            float collisionValue = GetCollisionValue(mapPosition);

            if (collisionValue < 0.001f)
                return false;

            return !IsTileOccupied(mapPosition);
        }

        public Portal GetPortalAt(Point mapPosition)
        {
            for (int i = 0; i < Portals.Count; ++i)
                if (mapPosition == Portals[i].Position)
                    return Portals[i];

            return null;
        }

        public NPCMapCharacter GetNPC(string npcName)
        {
            foreach (NPCMapCharacter npc in npcs)
            {
                if (npc.Name == npcName)
                {
                    return npc;
                }
            }

            return null;
        }

        public NPCMapCharacter GetNPCAt(Point mapPosition)
        {
            for (int i = 0; i < npcs.Count; ++i)
                if (mapPosition == npcs[i].TilePosition && !npcs[i].IsMoving)
                    return npcs[i];

            return null;
        }

        public short GetCombatId(Point mapPosition)
        {
            if (combatLayer == null)
            {
                return 0;
            }

            int index = GetIndex(mapPosition);

            if (index < 0 || index >= combatLayer.Length)
            {
                throw new Exception("Invalid index into combat layer");
            }

            return combatLayer[index];
        }

        public CutSceneEntry GetCutSceneAt(Point mapPosition)
        {
            foreach (CutSceneEntry entry in cutSceneEntries)
            {
                if (mapPosition == entry.TriggerPosition)
                {
                    return entry;
                }
            }

            return null;
        }

        public int GetIndex(Point tilePosition)
        {
            return GetIndex(tilePosition.X, tilePosition.Y);
        }

        public int GetIndex(int x, int y)
        {
            if (x < 0 || y < 0 || x >= DimensionsInTiles.X || y >= DimensionsInTiles.Y)
                return -1;

            return (y * DimensionsInTiles.X + x);
        }


        #endregion

        #region Update and Draw


        public void Update(GameTime gameTime)
        {
            foreach (NPCMapCharacter npc in npcs)
                npc.Update(gameTime.ElapsedGameTime.TotalMilliseconds, this);
        }

        public void AltDrawFringeLayer(SpriteBatch spriteBatch)
        {
            AltDrawLayer(fringeLayer, spriteBatch);
        }

        public void DrawFringeLayer(SpriteBatch spriteBatch, Matrix transform, Matrix centeringTranslation)
        {
            DrawLayer(fringeLayer, spriteBatch, transform, centeringTranslation, SpriteSortMode.Deferred, BlendState.AlphaBlend);
        }

        public void AltDrawGroundLayer(SpriteBatch spriteBatch)
        {
            AltDrawLayer(groundLayer, spriteBatch);
        }

        public void DrawGroundLayer(SpriteBatch spriteBatch, Matrix transform, Matrix centeringTranslation)
        {
            DrawLayer(groundLayer, spriteBatch, transform, centeringTranslation, SpriteSortMode.Deferred, BlendState.AlphaBlend);
        }

        public void AltDrawForeGroundLayer(SpriteBatch spriteBatch)
        {
            AltDrawLayer(foregroundLayer, spriteBatch);
        }

        public void DrawForeGroundLayer(SpriteBatch spriteBatch, Matrix transform, Matrix centeringTranslation)
        {
            DrawLayer(foregroundLayer, spriteBatch, transform, centeringTranslation, SpriteSortMode.Deferred, BlendState.AlphaBlend);
        }

        public void DrawFringeLayerForViewMap(SpriteBatch spriteBatch)
        {
            DrawLayerForViewMap(fringeLayer, spriteBatch);
        }

        public void DrawGroundLayerForViewMap(SpriteBatch spriteBatch)
        {
            DrawLayerForViewMap(groundLayer, spriteBatch);
        }

        public void DrawForeGroundLayerForViewMap(SpriteBatch spriteBatch)
        {
            DrawLayerForViewMap(foregroundLayer, spriteBatch);
        }

        public void AltDrawNPCCharacters(SpriteBatch spriteBatch)
        {
            foreach (NPCMapCharacter npc in npcs)
            {
                npc.AltDrawShadow(spriteBatch, new Vector2(8, 50));
            }
            foreach (NPCMapCharacter npc in npcs)
            {
                npc.AltDraw(spriteBatch);
            }
        }

        // To do: check that texture size is big enough.
        public void AltDrawCollisionLayer(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < dimensionsInTiles.X; ++x)
            {
                for (int y = 0; y < dimensionsInTiles.Y; ++y)
                {
                    int index = GetIndex(x, y);

                    if (collisionLayer[index] > 0.001f)
                    {
                        continue;
                    }

                    Rectangle destinationRectangle = new Rectangle(destinationRect[index].X + (int)Camera.Singleton.OffsetX, destinationRect[index].Y + (int)Camera.Singleton.OffsetY, destinationRect[index].Width, destinationRect[index].Height);
                    spriteBatch.Draw(ScreenManager.Singleton.BlankTexture, destinationRectangle, new Color(255, 0, 0, 128));
                }
            }
        }

        public void AltDrawPortals(SpriteBatch spriteBatch)
        {
            Rectangle destinationRectangle = new Rectangle(0, 0, TileDimensions.X, TileDimensions.Y);
            foreach (Portal portal in portals)
            {
                destinationRectangle.X = portal.Position.X * TileDimensions.X + (int)Camera.Singleton.OffsetX;
                destinationRectangle.Y = portal.Position.Y * TileDimensions.Y + (int)Camera.Singleton.OffsetY;
                spriteBatch.Draw(ScreenManager.Singleton.BlankTexture, destinationRectangle, new Color(0, 0, 255, 128));
            }
        }

        public void AltDrawDamageLayer(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < dimensionsInTiles.X; ++x)
            {
                for (int y = 0; y < dimensionsInTiles.Y; ++y)
                {
                    int index = GetIndex(x, y);

                    if (damageLayer[index] <= 0.000001f)
                    {
                        continue;
                    }
                    Rectangle destinationRectangle = new Rectangle(destinationRect[index].X + (int)Camera.Singleton.OffsetX, destinationRect[index].Y + (int)Camera.Singleton.OffsetY, destinationRect[index].Width, destinationRect[index].Height);
                    spriteBatch.Draw(ScreenManager.Singleton.BlankTexture, destinationRect[index], new Color(255, 255, 0, 128));
                }
            }
        }

        void AltDrawLayer(short[] layer, SpriteBatch spriteBatch)
        {
            Matrix transform = Camera.Singleton.Transformation;
            Matrix centeringTranslation = Camera.Singleton.MapCenteringTranslation;

            Vector3 scale, translation;
  
            Quaternion rotation;  // ignored
            transform.Decompose(out scale, out rotation, out translation);

            int scaledTileWidth = tileDimensions.X;
            int scaledTileHeight = tileDimensions.Y;

            int index = GetIndex(
                (int)Math.Abs(translation.X - centeringTranslation.Translation.X) / scaledTileWidth,
                (int)Math.Abs(translation.Y - centeringTranslation.Translation.Y) / scaledTileHeight);

            /// Calculate the first tile that should be drawn
            int startX = index % dimensionsInTiles.X;
            int startY = index / dimensionsInTiles.X;

            int screenWidthInTiles = 1280 / scaledTileWidth;
            int screenHeightInTiles = 720 / scaledTileHeight;

            /// There are situations where we need to draw an extra tile in both directions
            /// because the camera can be in between tiles. Account for that here
            int lastX = Math.Min(startX + screenWidthInTiles + 2, dimensionsInTiles.X);
            int lastY = Math.Min(startY + screenHeightInTiles + 2, dimensionsInTiles.Y);

            startX = Math.Max(startX - 1, 0);
            startY = Math.Max(startY - 1, 0);

            for (int x = startX; x < lastX; ++x)
            {
                for (int y = startY; y < lastY; ++y)
                {
                    int i = GetIndex(x, y);

                    if (layer[i] == -1)
                        continue;
                    Vector2 destinationVector = new Vector2(destinationRect[i].X + Camera.Singleton.OffsetXInt, destinationRect[i].Y + Camera.Singleton.OffsetYInt);
                    spriteBatch.Draw(TileSheet, destinationVector, sourceRect[layer[i]], Color.White);
                }
            }
        }



        // Used by world editor.
        void DrawLayer(short[] layer, SpriteBatch spriteBatch, Matrix transform, Matrix centeringTranslation, SpriteSortMode sort, BlendState blend)
        {
            spriteBatch.Begin(sort, blend, null, null, null, null, transform);

            Vector3 scale, translation;
            Quaternion rotation;  // ignored
            transform.Decompose(out scale, out rotation, out translation);

            int scaledTileWidth = (int)(tileDimensions.X * scale.X);
            int scaledTileHeight = (int)(tileDimensions.Y * scale.Y);

            int index = GetIndex(
                (int)Math.Abs(translation.X - centeringTranslation.Translation.X) / scaledTileWidth,
                (int)Math.Abs(translation.Y - centeringTranslation.Translation.Y) / scaledTileHeight);

            /// Calculate the first tile that should be drawn
            int startX = index % dimensionsInTiles.X;
            int startY = index / dimensionsInTiles.X;

            int screenWidthInTiles = 1280 / scaledTileWidth;
            int screenHeightInTiles = 720 / scaledTileHeight;

            /// There are situations where we need to draw an extra tile in both directions
            /// because the camera can be in between tiles. Account for that here
            int lastX = Math.Min(startX + screenWidthInTiles + 2, dimensionsInTiles.X);
            int lastY = Math.Min(startY + screenHeightInTiles + 2, dimensionsInTiles.Y);

            startX = Math.Max(startX - 1, 0);
            startY = Math.Max(startY - 1, 0);

            for (int x = startX; x < lastX; ++x)
            {
                for (int y = startY; y < lastY; ++y)
                {
                    int i = GetIndex(x, y);

                    if (layer[i] == -1)
                        continue;

                    spriteBatch.Draw(TileSheet, destinationRect[i], sourceRect[layer[i]], Color.White);
                }
            }

            spriteBatch.End();
        }
       
        void DrawLayerForViewMap(short[] layer, SpriteBatch spriteBatch)
        {            
            firstGameMapTilePos = new Vector2(destinationRect[0].X / 2 + Camera.Singleton.OffsetX,
                     destinationRect[0].Y / 2 + Camera.Singleton.OffsetY);

            lastGameMapTilePos = new Vector2(dimensionsInPixels.X / 2 + Camera.Singleton.OffsetX,
                dimensionsInPixels.Y / 2 + Camera.Singleton.OffsetY);

            Vector2 nathanMapPosition = new Vector2(Party.Singleton.Leader.WorldPosition.X / 2 + Camera.Singleton.OffsetX,
                Party.Singleton.Leader.WorldPosition.Y / 2 + Camera.Singleton.OffsetY); 

            for (int x = 0; x < dimensionsInTiles.X; ++x)
            {
                for (int y = 0; y < dimensionsInTiles.Y; ++y)
                {
                    int i = GetIndex(x, y);

                    if (layer[i] == -1)
                        continue;

                    Vector2 destinationVector = new Vector2(destinationRect[i].X*0.5f + Camera.Singleton.OffsetX, destinationRect[i].Y*0.5f + Camera.Singleton.OffsetY);
              
                    spriteBatch.Draw(TileSheet,  destinationVector, sourceRect[layer[i]], Color.White, 0, Vector2.Zero, 0.5f,SpriteEffects.None,0);
                }
            } 

            Party.Singleton.Leader.DrawForGameMap(spriteBatch, nathanMapPosition);

            if (Party.Singleton.GameState.Inventory.ItemCount(typeof(EyeOfShambala)) == 1)
            {
                foreach (NPCMapCharacter npc in npcs)
                {
                    npc.DrawForGameMap(spriteBatch);
                }
            }

        }
        #endregion      
    }
}
