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
    public class WillTalksScene : Scene
    {
        //Portal portal;

        static readonly Dialog willDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z740, Strings.Z741, Strings.Z742, Strings.Z743);

        public WillTalksScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            SceneHelper willHelper1 = new MovePCMapCharacterHelper(new Point(6, 4), 2.8f);

            willHelper1.OnCompleteEvent += new EventHandler(moveHelper1_OnCompleteEvent);
            helpers.Add(willHelper1);
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        void moveHelper1_OnCompleteEvent(object sender, EventArgs e)
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(willDialog, DialogScreen.Location.TopLeft, Party.will));

            willDialog.DialogCompleteEvent += willDialogComplete;
        }

        void willDialogComplete(object sender, EventArgs e)
        {
            SceneHelper willHelper2 = new MovePCMapCharacterHelper(new Point(6, 8), 2.8f);

            willHelper2.OnCompleteEvent += new EventHandler(moveHelper2_OnCompleteEvent);
            helpers.Add(willHelper2);
        }

        void moveHelper2_OnCompleteEvent(object sender, EventArgs e)
        {
            state = SceneState.Complete;
        }
    }
}
 