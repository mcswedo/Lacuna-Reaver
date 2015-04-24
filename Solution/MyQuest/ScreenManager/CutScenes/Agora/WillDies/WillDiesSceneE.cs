using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    class WillDiesSceneE : Scene
    {
        double ticker = 0;

        int currentFrame = 0;

        Texture2D spriteSheet;

        Vector2 position;

        static readonly FrameAnimation guardExplosion = new FrameAnimation()
        {
            FrameDelay = .1,
            Frames = new List<Rectangle>()
                {
                    new Rectangle (0,0,64,92), 
                    new Rectangle (64,0,64,92), 
                    new Rectangle (128,0,64,92),
                    new Rectangle (192,0,64,92),
                    new Rectangle (256,0,64,92),
                    new Rectangle (320,0,64,92),
                    new Rectangle (384,0,64,92),
                    new Rectangle (448,0,64,92),
                    new Rectangle (512,0,64,92),
                    new Rectangle (576,0,64,92),
                    new Rectangle (640,0,64,92),
                    new Rectangle (704,0,64,92),
                    new Rectangle (768,0,64,92),
                    new Rectangle (832,0,64,92),
                    new Rectangle (896,0,64,92),
                    new Rectangle (960,0,64,92),
                    new Rectangle (1024,0,64,92),
                    new Rectangle (1088,0,64,92)
                }
        };
        public WillDiesSceneE(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }
       
       public override void Initialize()
        {
            ticker = TimeSpan.FromSeconds(guardExplosion.FrameDelay).TotalSeconds;

            position = Utility.ToWorldCoordinates(new Point(21, 4), Party.Singleton.CurrentMap.TileDimensions);

            spriteSheet = MyContentManager.LoadTexture(ContentPath.ToMapCharacterTextures + "will_explosion");
            
            SoundSystem.Play(AudioCues.MaxDeath);
            
            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Will",
                Point.Zero,
                ModAction.Remove,
                false);
        }

        public override void Update(GameTime gameTime)
        {
            ticker -= gameTime.ElapsedGameTime.TotalMilliseconds;

            if (ticker <= 0)
            {
                ticker = TimeSpan.FromSeconds(guardExplosion.FrameDelay).TotalMilliseconds;

                currentFrame++;

                if (currentFrame >= guardExplosion.Frames.Count)
                {
                    currentFrame = guardExplosion.Frames.Count - 1;

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
                guardExplosion.Frames[currentFrame],
                Color.White);
           
            //spriteBatch.End();
        }
    }
}