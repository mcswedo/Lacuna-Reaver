using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{   
    public class LibraryBossSceneB : Scene
    {  
        float alpha;

        public LibraryBossSceneB(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            alpha = 0;

            InputState.SetVibration(.5f, .5f);

            Camera.Singleton.Shake(TimeSpan.FromSeconds(1.5), 5);

            SoundSystem.Play(AudioCues.Earthquake);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Apprentice1",
                new Point(6,4),
                ModAction.Add,
                false);

            Party.Singleton.ModifyNPC(
               Party.Singleton.CurrentMap.Name,
               "Apprentice2",
               new Point(4, 4),
               ModAction.Add,
               false);

            //Party.Singleton.CurrentMap.GetNPC("Will").FaceDirection(Direction.East);
        }


        public override void HandleInput(GameTime gameTime)
        {
        }
        public override void Update(GameTime gameTime)
        {
            alpha += .05f;
            
            if (alpha >= 1)
            {
                alpha = 0;
                InputState.SetVibration(0f, 0f);
                state = SceneState.Complete;
            }                         
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            ScreenManager.Singleton.TintBackBuffer(alpha, Color.White, spriteBatch);
        }
    }
}
