using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class Keepf5CutSceneA : Scene
    {
        static readonly Dialog dialog = new Dialog(DialogPrompt.NeedsClose, false, Strings.Z561);

        public Keepf5CutSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            MovePCMapCharacterHelper helper = new MovePCMapCharacterHelper(
                new Point(5, 5),
                Party.Singleton.Leader.MoveSpeed * 0.5f);

            helper.OnCompleteEvent += new EventHandler(helper_OnCompleteEvent);

            helpers.Add(helper);
        }

        void helper_OnCompleteEvent(object sender, EventArgs e)
        {
            MoveCameraHelper cameraHelper = new MoveCameraHelper(new Point(5, 4), Party.Singleton.Leader.MoveSpeed * 0.7f);
            Party.Singleton.Leader.FaceDirection(Direction.North);

            cameraHelper.OnCompleteEvent += new EventHandler(cameraHelper_OnCompleteEvent);

            helpers.Add(cameraHelper);
        }

        void cameraHelper_OnCompleteEvent(object sender, EventArgs e)
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Nathan"));
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
