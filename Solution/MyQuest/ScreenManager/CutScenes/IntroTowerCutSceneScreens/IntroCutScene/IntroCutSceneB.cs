using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class IntroCutSceneB : Scene
    {
        static readonly Dialog dialog = new Dialog(
            DialogPrompt.NeedsClose,
            Strings.Z554);

        public IntroCutSceneB(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            SceneHelper moveHelper1 = new MoveNpcCharacterHelper(
                "Friend2",
                new Point(5, 35),
                false,
                new Point(5, 34),
                1.7f);

            moveHelper1.OnCompleteEvent += new EventHandler(moveHelper1_OnCompleteEvent);
            helpers.Add(moveHelper1);
        }

        void moveHelper1_OnCompleteEvent(object sender, EventArgs e)
        {
            state = SceneState.Complete;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Sid"));
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}