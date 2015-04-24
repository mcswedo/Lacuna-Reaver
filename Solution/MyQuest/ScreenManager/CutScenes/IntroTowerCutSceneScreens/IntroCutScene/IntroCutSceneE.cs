using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class IntroCutSceneE : Scene
    {
        static readonly Dialog dialog = new Dialog(
            DialogPrompt.NeedsClose,
            Strings.Z557);

        public IntroCutSceneE(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            MoveNpcCharacterHelper helper1 = new MoveNpcCharacterHelper(
                "ScaredCivilian1",
                new Point(6, 24),
                false,
                new Point(6, 31),
                3.5f);

            helper1.OnCompleteEvent += new EventHandler(helper1_OnCompleteEvent);
            helper1.SetIdleOnlyOnComplete();

            helpers.Add(helper1);
        }

        void helper1_OnCompleteEvent(object sender, EventArgs e)
        {
            state = SceneState.Complete;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Woman"));
            Party.Singleton.AddLogEntry("The Keep", "Woman", dialog.Text);
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
