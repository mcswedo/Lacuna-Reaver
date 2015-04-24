using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class WillsWanderSceneH : Scene
    {

        #region Fields
        
        float alpha; 

        #endregion

        public WillsWanderSceneH(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {

            Party.Singleton.Leader.FaceDirection(Direction.North);

            alpha = 0;

            InputState.SetVibration(.5f, .5f);

            Camera.Singleton.Shake(TimeSpan.FromSeconds(1.5), 5);

            SoundSystem.Play(AudioCues.Earthquake);

            SoundSystem.Play(AudioCues.MonsterRoar);
        }

        public override void Update(GameTime gameTime)
        {
           
            alpha += .05f;
            Dialog screamDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z874);
            if (alpha >= 1)
            {
                alpha = 0;
                InputState.SetVibration(0f, 0f);
                ScreenManager.Singleton.AddScreen(new DialogScreen(screamDialog, DialogScreen.Location.TopLeft));
                state = SceneState.Complete;
            }
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }


       
    }
}

