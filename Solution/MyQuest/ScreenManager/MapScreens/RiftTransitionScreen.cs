using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    enum TransitionState
    {
        Filling,
        Emptying
    }

    struct DestinationTown
    {
        //public string name;
        //public string filename;
        //public int xDestination;
        //public int yDestination;
    }

    public class RiftTransitionScreen : Screen
    {
        #region Fields


        //DestinationTown[] destinationInfo = new DestinationTown[3];

        Rift.RiftDestination riftDestination;

        const int tileScale = 2;
        int tileWidth;
        int width;
        int height;

        TransitionState state = TransitionState.Filling;

        Texture2D[] tileImage = new Texture2D[14];

        short[,] tile;

        //string townName;

        //int destinationIndex = -1;

        float globalTimer = 0.0f;


        #endregion

        #region Initialization


        public RiftTransitionScreen(Rift.RiftDestination riftDestination)
        {
            this.riftDestination = riftDestination;
        }

        public override void Initialize()
        {
            // Set up tiling info
            tileWidth = (int)(16 * (Math.Pow(2, tileScale-1)));
            width = 1280 / tileWidth + 1;
            height = 720 / tileWidth + 1;
            tile = new short[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    tile[i, j] = 0;
                }
            }

            TransitionOnTime = TimeSpan.FromSeconds(0.05);
        }

        public override void LoadContent(ContentManager content)
        {
            for(int i = 0; i < 14; i++)
            {
                tileImage[i] = content.Load<Texture2D>(interfaceTextureFolder + "TiledTransition" + (i+1).ToString());
            }
        }


        #endregion

        #region Update and Draw


        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            globalTimer += (gameTime.ElapsedGameTime.Milliseconds);

            if (!(tile[0,0] == 14 && tile[height-1,width-1] == 14))
            {
                globalTimer = 0.0f;
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        UpdateTile(j, i);
                    }
                }
            }
            else if (tile[0, 0] == 14 && tile[height-1, width-1] == 14 && state == TransitionState.Filling)
            {
                if(globalTimer > 500)
                {
                    // *Perform map change*
                    Portal portal = new Portal
                    {
                        DestinationMap = riftDestination.MapName,
                        DestinationPosition = riftDestination.TilePosition,
                        Position = Point.Zero
                    };
                    Party.Singleton.PortalToMap(portal);
                    Camera.Singleton.CenterOnTarget(
                        Party.Singleton.Leader.WorldPosition,
                        Party.Singleton.CurrentMap.DimensionsInPixels,
                        ScreenManager.Singleton.ScreenResolution);
                    // *Perform map change*

                    state = TransitionState.Emptying;
                    globalTimer = 0.0f;
                    for (int i = 0; i < height; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            UpdateTile(j, i);
                        }
                    }
                }
            }
            else if (!(tile[0, 0] == 0 && tile[height-1, width-1] == 0) && state == TransitionState.Emptying)
            {
                globalTimer = 0.0f;
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        UpdateTile(j, i);
                    }
                }
            }

            if (tile[0, 0] == 0 && tile[height-1, width-1] == 0 && state == TransitionState.Emptying)
            {
                globalTimer = 0.0f;
                ExitAfterTransition();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;

            Color color = Color.White * TransitionAlpha;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (tile[i, j] > 0)
                    {
                        spriteBatch.Draw(
                            tileImage[tile[i, j] - 1],
                            new Rectangle(
                                (state == TransitionState.Emptying) ? (j + 1) * tileWidth : j * tileWidth,
                                (state == TransitionState.Emptying) ? (i + 1) * tileWidth : i * tileWidth,
                                tileWidth,
                                tileWidth),
                            null,
                            color,
                            (state == TransitionState.Emptying) ? (float)Math.PI : 0.0f,
                            Vector2.Zero,
                            SpriteEffects.None,
                            0.0f);
                    }
                }
            }
        }


        void UpdateTile(int x, int y)
        {
            if (state == TransitionState.Filling)
            {
                if (tile[y, x] == 0)
                {
                    if ((x == 0 || tile[y, x - 1] > 2) && (y == 0 || tile[y - 1, x] > 1))
                        ++tile[y, x];
                }
                else
                {
                    tile[y, x] = (short)MathHelper.Min(tile[y, x] + 1, 14);
                }
            }
            else
            {
                if (tile[y, x] == 14)
                {
                    if ((x == 0 || tile[y, x - 1] < 12) && (y == 0 || tile[y - 1, x] < 13))
                        --tile[y, x];
                }
                else
                {
                    tile[y, x] = (short)MathHelper.Max(tile[y, x] - 1, 0);
                }
            }
        }


        #endregion
    }
}
