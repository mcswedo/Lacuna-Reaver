using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class IntroCutSceneA : Scene
    {
        static readonly Dialog dialog = new Dialog(
            DialogPrompt.NeedsClose,
            Strings.Z553);

        public IntroCutSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.GameState.Inventory.AddItem(typeof(LargeHealthPotion), 6);
            MovePCMapCharacterHelper moveHelper = new MovePCMapCharacterHelper(new Point(6, 32));

            MoveNpcCharacterHelper moveHelper1 = new MoveNpcCharacterHelper(
                "Friend1",
                new Point(7, 35),
                false,
                new Point(7, 34),
                1.7f);            

            moveHelper1.OnCompleteEvent += new EventHandler(moveHelper1_OnCompleteEvent);
            helpers.Add(moveHelper1);
            helpers.Add(moveHelper);
        }

        void moveHelper1_OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.Leader.FaceDirection(Direction.South);
            state = SceneState.Complete;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Max"));
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}