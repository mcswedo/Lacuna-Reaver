using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class AgoraRiftSceneB : Scene
    {
         double ticker = 0;

         int currentFrame = 0;

         Vector2 position;

         Texture2D spriteSheet;

         bool playCharging;

         Vector2 offSet = new Vector2(-32, -32);

         static readonly FrameAnimation nathanChargeing = new FrameAnimation()
         {
             FrameDelay = .075,
             Frames = new List<Rectangle>()
                {
                    new Rectangle (0,0,128,128), 
                    new Rectangle (128,0,128,128), 
                    new Rectangle (256,0,128,128),
                    new Rectangle (384,0,128,128),
                    new Rectangle (512,0,128,128),
                    new Rectangle (640,0,128,128),
                    new Rectangle (512,0,128,128),
                    new Rectangle (384,0,128,128),
                    new Rectangle (256,0,128,128),
                    new Rectangle (128,0,128,128), 
                    new Rectangle (0,0,128,128),           
                }
         };

        public AgoraRiftSceneB(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

 
        public override void Initialize()
        {
            ticker = TimeSpan.FromSeconds(nathanChargeing.FrameDelay).TotalMilliseconds;

            position = Utility.ToWorldCoordinates(Party.Singleton.Leader.TilePosition, Party.Singleton.CurrentMap.TileDimensions);

            spriteSheet = MyContentManager.LoadTexture(ContentPath.ToMapCharacterTextures + "nathan_charging");

            Party.Singleton.Leader.CurrentAnimation = null;

            SoundSystem.Play(AudioCues.Focus);

            playCharging = true;            
        }

        public override void Update(GameTime gameTime)
        {

            if (playCharging)
            {
                ticker -= gameTime.ElapsedGameTime.TotalMilliseconds;

                if (ticker <= 0)
                {
                    ticker = TimeSpan.FromSeconds(nathanChargeing.FrameDelay).TotalMilliseconds;

                    currentFrame++;

                    if (currentFrame >= nathanChargeing.Frames.Count)
                    {
                        currentFrame = 5;

                        playCharging = false;


                        Party.Singleton.Leader.CurrentAnimation = Party.Singleton.Leader.IdleAnimation;

                        state = SceneState.Complete;
                    }
                }
            }
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (playCharging)
            {
                spriteBatch.Draw(
                    spriteSheet,
                    position + new Vector2(Camera.Singleton.OffsetX, Camera.Singleton.OffsetY) + offSet,
                    nathanChargeing.Frames[currentFrame],
                    Color.White);
            }
        }

    }
}
