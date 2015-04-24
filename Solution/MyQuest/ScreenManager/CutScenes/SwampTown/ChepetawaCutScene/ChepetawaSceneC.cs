using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class ChepetawaSceneC : Scene
    {
        #region Fields

        float alpha;

        bool isRisen;

        Vector2 position;

        double ticker = 0;

        int currentFrame = 0;

        Texture2D spriteSheet;

        static readonly FrameAnimation boggimusIdle = new FrameAnimation()
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
                    new Rectangle (768,0,128,128),
                    new Rectangle (896,0,128,128),
                    new Rectangle (1024,0,128,128),
                    new Rectangle (1152,0,128,128),
                    new Rectangle (0,0,128,128), 
                    new Rectangle (128,0,128,128), 
                    new Rectangle (256,0,128,128),
                    new Rectangle (384,0,128,128),
                    new Rectangle (512,0,128,128),
                    new Rectangle (640,0,128,128),
                    new Rectangle (768,0,128,128),
                    new Rectangle (896,0,128,128),
                    new Rectangle (1024,0,128,128),
                    new Rectangle (1152,0,128,128),
                   // new Rectangle (1280,0,128,128)
                }
        };

        #endregion

        #region Dialog

        Dialog roarDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z874);
        
        Dialog wrathDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z875);

        #endregion 

        public ChepetawaSceneC(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {         
        }

        public override void Initialize()
        {
            ticker = TimeSpan.FromSeconds(boggimusIdle.FrameDelay).TotalMilliseconds;

            position = Utility.ToWorldCoordinates(new Point(2, 1), Party.Singleton.CurrentMap.TileDimensions);

            spriteSheet = MyContentManager.LoadTexture(ContentPath.ToMapCharacterTextures + "Boggimus");

            Party.Singleton.Leader.FaceDirection(Direction.North);

            alpha = 0;

            InputState.SetVibration(.5f, .5f);

            Camera.Singleton.Shake(TimeSpan.FromSeconds(1.5), 5);

            SoundSystem.Play(AudioCues.Earthquake);

            SoundSystem.Play(AudioCues.MonsterRoar);       
        }

        public override void Update(GameTime gameTime)
        {
            if (!isRisen)
            {
                alpha += .05f;
            }

            if (alpha >= 1)
            {
                alpha = 0;
                InputState.SetVibration(0f, 0f);
                isRisen = true;
            }

            if (isRisen)
            {
                ticker -= gameTime.ElapsedGameTime.TotalMilliseconds;

                if (ticker <= 0)
                {
                    ticker = TimeSpan.FromSeconds(boggimusIdle.FrameDelay).TotalMilliseconds;

                    currentFrame++;

                    if (currentFrame >= boggimusIdle.Frames.Count)
                    {
                        currentFrame = 0; 

                        Party.Singleton.CurrentMap.GetNPC("Chepetawa").FaceDirection(Direction.South);

                        ScreenManager.Singleton.AddScreen(
                            new DialogScreen(wrathDialog, DialogScreen.Location.BottomRight, "Chepetawa"));

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
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Camera.Singleton.Transformation);

            spriteBatch.Draw(
                spriteSheet,
                position + new Vector2(Camera.Singleton.OffsetX, Camera.Singleton.OffsetY),
                boggimusIdle.Frames[currentFrame],
                Color.White);

            //spriteBatch.End();

            GameLoop.Instance.BeginNonPremultipliedBlendStateDraw();
            ScreenManager.Singleton.TintBackBuffer(alpha, Color.White, spriteBatch);
            GameLoop.Instance.RestoreNormalDraw();
        }      
    }
}
