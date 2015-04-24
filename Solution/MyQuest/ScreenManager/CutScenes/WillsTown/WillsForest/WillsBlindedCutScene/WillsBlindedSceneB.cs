using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class WillsBlindedSceneB : Scene
    {
        public WillsBlindedSceneB(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.Leader.FaceDirection(Direction.North);

            //alpha = 0; 

            InputState.SetVibration(.5f, .5f);

            Camera.Singleton.Shake(TimeSpan.FromSeconds(1.5), 5);

            SoundSystem.Play(AudioCues.Earthquake);       
        }

        public override void Update(GameTime gameTime)
        {
            Dialog screamDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z962);
            //alpha +=.05f;

            //if (alpha >= 1)
            //{
            //    alpha = 0;
                InputState.SetVibration(0f, 0f);
                ScreenManager.Singleton.AddScreen(new DialogScreen(screamDialog, DialogScreen.Location.TopLeft));
                state = SceneState.Complete;
            //}

        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
           //ScreenManager.Singleton.TintBackBuffer(alpha, Color.White, spriteBatch); 
        }

       
    }
}
