using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class EnterCombatTransitionScreen : Screen
    {
        CombatZone combatZone;
        ScreenState tileMapScreenState;

        Texture2D starImage;
        Texture2D blankWhite;

        float globalTimer = 0.0f;
        float starOverlayAlpha = 0.15f;

        public EnterCombatTransitionScreen(CombatZone zone, ScreenState screenState)
        {
            combatZone = zone;
            tileMapScreenState = screenState;
        }

        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.05);
        }

        public override void LoadContent(ContentManager content)
        {
            starImage = content.Load<Texture2D>(backgroundTextureFolder + "adarkstar");
            blankWhite = content.Load<Texture2D>(backgroundTextureFolder + "BlankWhite");
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            globalTimer += (gameTime.ElapsedGameTime.Milliseconds * 1.2f);

            if (starOverlayAlpha < 1000)
            {
                starOverlayAlpha += ((float)gameTime.ElapsedGameTime.Milliseconds / 3.0f);
                if (starOverlayAlpha > 1000)
                    starOverlayAlpha = 1000;
            }

            if (globalTimer > 2000)
            {
                ScreenManager.AddScreen(new CombatScreen(combatZone));
                tileMapScreenState = MyQuest.ScreenState.Hidden;
                ExitAfterTransition();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;

            if (starImage != null)
            {
                spriteBatch.Draw(
                    starImage,
                    new Vector2(640, 360),
                    null,
                    new Color(255, 255, 255, starOverlayAlpha / 1000.0f),
                    globalTimer / 525.0f / 2.2f,
                    new Vector2(starImage.Width / 2, starImage.Height / 2),
                    new Vector2(globalTimer / 320.0f, globalTimer / 320.0f),
                    SpriteEffects.None,
                    0.0f);
            }
        }
    }
}
