using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace MyQuest
{
    public class CaraTalksScene : Scene
    {
        //Portal portal;

        static readonly Dialog caraDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z931, Strings.Z932);

        public CaraTalksScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            SceneHelper caraHelper1 = new MovePCMapCharacterHelper(new Point(4, 4), 2.8f);

            caraHelper1.OnCompleteEvent += new EventHandler(moveHelper1_OnCompleteEvent);
            helpers.Add(caraHelper1);
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        void moveHelper1_OnCompleteEvent(object sender, EventArgs e)
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(caraDialog, DialogScreen.Location.TopLeft, Party.cara));

            caraDialog.DialogCompleteEvent += caraDialogComplete;
        }

        void caraDialogComplete(object sender, EventArgs e)
        {
            SceneHelper caraHelper2 = new MovePCMapCharacterHelper(new Point(4, 8), 2.8f);

            caraHelper2.OnCompleteEvent += new EventHandler(moveHelper2_OnCompleteEvent);
            helpers.Add(caraHelper2);
        }

        void moveHelper2_OnCompleteEvent(object sender, EventArgs e)
        {
            state = SceneState.Complete;
        }
    }
}
 