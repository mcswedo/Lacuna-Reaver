using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class AgoraRiftSceneC : Scene
    {
        #region Fields

        float alpha;

        #endregion

        public AgoraRiftSceneC(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }
        public override void Complete()
        {
            Party.Singleton.ModifyNPC(Maps.agoraPortalRoom2, "RiftPortal", new Point(4, 13), ModAction.Add, true); 
        }
        public override void Initialize()
        {
            alpha = 0; 

            InputState.SetVibration(.5f, .5f);

            Camera.Singleton.Shake(TimeSpan.FromSeconds(1.5), 5);

            SoundSystem.Play(AudioCues.Earthquake);     
        }

        public override void Update(GameTime gameTime)
        {
            alpha +=.05f;

            if (alpha >= 1)
            {
                InputState.SetVibration(0f, 0f);
                alpha = 0;
                Complete();
                state = SceneState.Complete;
            }

        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ScreenManager.Singleton.TintBackBuffer(alpha, Color.White, spriteBatch); 
        }

       
    }
}