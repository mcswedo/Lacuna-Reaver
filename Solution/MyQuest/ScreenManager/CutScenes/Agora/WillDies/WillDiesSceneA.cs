using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class WillDiesSceneA : Scene
    {
        internal const string willDiedAchievement = "willDied";

        #region Dialog

        static readonly Dialog malDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA034);

        #endregion

        Dialog dialog;

        public WillDiesSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.AddAchievement(willDiedAchievement);
            MusicSystem.Play(AudioCues.finalBossPT2);
            dialog = malDialog;
 
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Malticar"));

            state = SceneState.Complete;
        }


        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
