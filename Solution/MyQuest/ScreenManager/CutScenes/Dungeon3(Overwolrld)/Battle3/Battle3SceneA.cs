using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace MyQuest
{
    public class Battle3SceneA : Scene
    {
        double ticker = 0;

        int currentFrame = 0;

        Texture2D spriteSheet;

        Vector2 position1;
        Vector2 position2;

        static readonly FrameAnimation banditSmoke = new FrameAnimation()
        {
            FrameDelay = .1,
            Frames = new List<Rectangle>()
                {
                    new Rectangle (0,0,64,128), 
                    new Rectangle (64,0,64,128), 
                    new Rectangle (128,0,64,128),
                    new Rectangle (192,0,64,128),
                    new Rectangle (256,0,64,128),
                    new Rectangle (320,0,64,128)
                }
        };

        public Battle3SceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {         
        }

        public override void Initialize()
        {
            ticker = TimeSpan.FromSeconds(banditSmoke.FrameDelay).TotalSeconds;

            position1 = Utility.ToWorldCoordinates(new Point(0, 9), Party.Singleton.CurrentMap.TileDimensions);
            position2 = Utility.ToWorldCoordinates(new Point(2, 9), Party.Singleton.CurrentMap.TileDimensions);


            spriteSheet = MyContentManager.LoadTexture(ContentPath.ToMapCharacterTextures + "bandit_smoke");
        }

        public override void Update(GameTime gameTime)
        {
            ticker -= gameTime.ElapsedGameTime.TotalMilliseconds;

            if (ticker <= 0)
            {
                ticker = TimeSpan.FromSeconds(banditSmoke.FrameDelay).TotalMilliseconds;

                currentFrame++;

                if (currentFrame >= banditSmoke.Frames.Count)
                {

                    currentFrame = banditSmoke.Frames.Count - 1;

                    state = SceneState.Complete;

                }
            }
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                spriteSheet,
                position1 + new Vector2(Camera.Singleton.OffsetX, Camera.Singleton.OffsetY),
                banditSmoke.Frames[currentFrame],
                Color.White);

            spriteBatch.Draw(
                spriteSheet,
                position2 + new Vector2(Camera.Singleton.OffsetX, Camera.Singleton.OffsetY),
                banditSmoke.Frames[currentFrame],
                Color.White);
        }
    }
}
