using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    class Keepf5CutSceneP : Scene
    {
        Dialog dialog = new Dialog(DialogPrompt.NeedsClose, false, Strings.Z572);

        public Keepf5CutSceneP(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Arlan",
                new Point(5, 7),
                ModAction.Add,
                false);

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Arlan"));

            dialog.DialogCompleteEvent += MoveNathan;
        }

        void MoveNathan(object sender, PartyResponseEventArgs e)
        {
            MovePCMapCharacterHelper helper = new MovePCMapCharacterHelper(
                new Point(3, 5),
                3.5f);

            helper.OnCompleteEvent += new EventHandler(helper_OnCompleteEvent);

            helpers.Add(helper);
        }

        void helper_OnCompleteEvent(object sender, EventArgs e)
        {
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
