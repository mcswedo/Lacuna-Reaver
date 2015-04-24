using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    class Keepf5CutSceneY : Scene
    {
        double ticker = 0;

        int currentFrame = 0;

        Texture2D spriteSheet;

        Vector2 position;

        static readonly FrameAnimation arlanBlast = new FrameAnimation()
            {
                FrameDelay = .1,
                Frames = new List<Rectangle>()
                {
                    new Rectangle (0,0,64,175), 
                    new Rectangle (64,0,64,175), 
                    new Rectangle (128,0,64,175),
                    new Rectangle (192,0,64,175),
                    new Rectangle (256,0,64,175),
                    new Rectangle (320,0,64,175),
                    new Rectangle (384,0,64,175),
                    new Rectangle (448,0,64,175),
                    new Rectangle (512,0,64,175),
                }
            };

        public Keepf5CutSceneY(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            
        }

        public override void Initialize()
        {
            SoundSystem.Play(AudioCues.BeamGlass); 
            ticker = TimeSpan.FromSeconds(arlanBlast.FrameDelay).TotalSeconds;

            position = Utility.ToWorldCoordinates(new Point(5, 0), Party.Singleton.CurrentMap.TileDimensions);

            spriteSheet = MyContentManager.LoadTexture(ContentPath.ToMapCharacterTextures + "arlan_blast");
        }
       
        public override void Update(GameTime gameTime)
        {
            ticker -= gameTime.ElapsedGameTime.TotalMilliseconds;

            if (ticker <= 0)
            {
                ticker = TimeSpan.FromSeconds(arlanBlast.FrameDelay).TotalMilliseconds;

                currentFrame++;

                if (currentFrame >= arlanBlast.Frames.Count)
                {
      
                        currentFrame = arlanBlast.Frames.Count - 1;

                        state = SceneState.Complete;
                  
                }
            }
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Camera.Singleton.Transformation);
            
            spriteBatch.Draw(
                spriteSheet,
                position + new Vector2(Camera.Singleton.OffsetX, Camera.Singleton.OffsetY),
                arlanBlast.Frames[currentFrame],
                Color.White);

            //spriteBatch.End();
        }
    }
}