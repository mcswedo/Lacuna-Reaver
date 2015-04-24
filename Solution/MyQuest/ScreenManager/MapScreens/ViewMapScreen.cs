using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class ViewMapScreen : Screen
    {
        Texture2D background;
        Vector2 backgroundLocation;
        Map map = Party.Singleton.CurrentMap;
        Vector2 startPosition;
        const float normalMapSpeed = 4;
        const float worldMapSpeed = 10;

        float GetMapSpeed()
        {
            if (Party.Singleton.CurrentMap.IsWorldMap())
            {
                return worldMapSpeed;
            }
            else
            {
                float mapSpeed = normalMapSpeed + Party.Singleton.CurrentMap.TileDimensions.X / 8;
                return mapSpeed;
            }
        }

        public override void Initialize()
        {
           // TransitionOffTime = TimeSpan.FromSeconds(0.15);
         //   TransitionOnTime = TimeSpan.FromSeconds(0.15);

            backgroundLocation = new Vector2(
                ScreenManager.ScreenResolution.X,
                ScreenManager.ScreenResolution.Y);

            startPosition = new Vector2(Party.Singleton.Leader.WorldPosition.X /2, 
            Party.Singleton.Leader.WorldPosition.Y /2);

            Camera.Singleton.CenterOnTarget(
                    startPosition,
                    new Point(Party.Singleton.CurrentMap.DimensionsInPixels.X / 2, Party.Singleton.CurrentMap.DimensionsInPixels.Y / 2),
                     ScreenManager.Singleton.ScreenResolution);
        }

        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(backgroundTextureFolder + "blackBackground");
        }

        public override void HandleInput(GameTime gameTime)
        {
            base.HandleInput(gameTime);
            if (InputState.IsMenuCancel() || InputState.IsViewMap())
            {
                ExitAfterTransition();
                ScreenManager.AddScreen(TileMapScreen.Instance);
                return;
            }
            if (InputState.IsScrollUp())
            {
                if (map.Name.Equals(Maps.overworld))
                {
                    if (!Party.Singleton.GameState.Inventory.containsMapItem("ellaethia"))
                    {
                        if (map.FirstGameMapTilePos.Y + 55 * 64 <= ScreenManager.ScreenResolution.Y)
                        {
                            Camera.Singleton.position.Y -= GetMapSpeed();
                        }
                    }
                    else
                    {
                        if (map.FirstGameMapTilePos.Y <= ScreenManager.ScreenResolution.Y)
                        {
                            Camera.Singleton.position.Y -= GetMapSpeed();
                        }
                    }
                }
                else
                {
                    if (map.FirstGameMapTilePos.Y <= ScreenManager.ScreenResolution.Y)
                    {
                        Camera.Singleton.position.Y -= GetMapSpeed();
                    }
                }
            }
            if (InputState.IsScrollDown())
            {
                if (map.Name == "overworld")
                {
                    if (!Party.Singleton.GameState.Inventory.containsMapItem("agora"))
                    {
                        if (map.LastGameMapTilePos.Y - 20 * 64 >= ScreenManager.ScreenResolution.Height)
                        {
                            Camera.Singleton.position.Y += GetMapSpeed();
                        }
                    }

                    else
                    {
                        if (map.LastGameMapTilePos.Y >= ScreenManager.ScreenResolution.Height)
                        {
                            Camera.Singleton.position.Y += GetMapSpeed();
                        }
                    } 
                }
                else
                {
                    if (map.LastGameMapTilePos.Y >= ScreenManager.ScreenResolution.Height)
                    {
                        Camera.Singleton.position.Y += GetMapSpeed();
                    }
                }                
            }
            if (InputState.IsScrollLeft())
            {
                if (map.FirstGameMapTilePos.X <= ScreenManager.ScreenResolution.X)
                {
                    Camera.Singleton.position.X -= GetMapSpeed();
                }

            }
            if (InputState.IsScrollRight())
            {
                if (map.LastGameMapTilePos.X >= ScreenManager.ScreenResolution.Width)
                {
                    Camera.Singleton.position.X += GetMapSpeed();
                }
            }

            if (InputState.IsReset())
            {
                Initialize();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;

            //Color whiteColor = Color.White * TransitionAlpha;

            //spriteBatch.Draw(background, backgroundLocation, whiteColor);

            GameLoop.Instance.BeginTileMapDraw();

            map.DrawGroundLayerForViewMap(spriteBatch);

            map.DrawForeGroundLayerForViewMap(spriteBatch);

            map.DrawFringeLayerForViewMap(spriteBatch);

            GameLoop.Instance.RestoreNormalDraw();
            
        }
    }
}
