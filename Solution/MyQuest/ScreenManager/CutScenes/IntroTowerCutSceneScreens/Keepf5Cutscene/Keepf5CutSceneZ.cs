using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    class Keepf5CutSceneZ : Scene
    {
        double ticker = 0;

        int currentFrame = 0;

        Texture2D spriteSheet;

        Vector2 position;

        float alpha; 

        static readonly FrameAnimation arlanBlast = new FrameAnimation()
            {
                FrameDelay = .1,
                Frames = new List<Rectangle>()
                {
                    new Rectangle (704,0,192,175),
                    new Rectangle (896,0,192,175),
                    new Rectangle (1088,0,192,175),
                    new Rectangle (1280,0,192,175),
                    new Rectangle (1472,0,192,175),
                    new Rectangle (1664,0,192,175),
                    new Rectangle (0,175,192,175),
                    new Rectangle (192,175,192,175),
                    new Rectangle (384,175,192,175),
                    new Rectangle (576,175,192,175),
                    new Rectangle (768,175,192,175),
                    new Rectangle (960,175,192,175),
                    new Rectangle (1152,175,192,175),
                    new Rectangle (1344,175,192,175),
                    new Rectangle (1536,175,192,175)
                }
            };

        public Keepf5CutSceneZ(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            
        }

        public override void Initialize()
        {            
            ticker = TimeSpan.FromSeconds(arlanBlast.FrameDelay).TotalSeconds;

            position = Utility.ToWorldCoordinates(new Point(4, 0), Party.Singleton.CurrentMap.TileDimensions);

            spriteSheet = MyContentManager.LoadTexture(ContentPath.ToMapCharacterTextures + "arlan_blast");

            Party.Singleton.Leader.CurrentAnimation = Party.Singleton.Leader.IdleAnimation;
            alpha = 0;
                              
        }

        public override void Update(GameTime gameTime)
        {
            ticker -= gameTime.ElapsedGameTime.TotalMilliseconds;

            if (ticker <= 0)
            {
                ticker += TimeSpan.FromSeconds(arlanBlast.FrameDelay).TotalMilliseconds;

                currentFrame++;

                if (currentFrame >= arlanBlast.Frames.Count)
                {
                    currentFrame = 0;
                }
            }

            alpha += .01f;
            
            if (alpha >= 1)
            {
                alpha = 1;
                state = SceneState.Complete;
            }
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                spriteSheet,
                position + new Vector2(Camera.Singleton.OffsetX, Camera.Singleton.OffsetY),
                arlanBlast.Frames[currentFrame],
                Color.White);

            GameLoop.Instance.BeginNonPremultipliedBlendStateDraw();
            ScreenManager.Singleton.TintBackBuffer(alpha, Color.White, spriteBatch);
            GameLoop.Instance.RestoreNormalDraw();
        }
    }
}