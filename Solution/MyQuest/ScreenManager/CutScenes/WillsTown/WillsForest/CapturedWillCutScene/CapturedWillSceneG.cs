using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class CapturedWillSceneG : Scene
    {
        #region Fields
        NPCMapCharacter will;
        #endregion

        #region Dialog

        Dialog luckyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z952);

        Dialog joinDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z953, Strings.Z954, Strings.Z955, Strings.Z956);

        Dialog talkingToTreeDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z957);

        Dialog iKnewThatDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z958);

        #endregion

        public CapturedWillSceneG(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            will = Party.Singleton.CurrentMap.GetNPC(Party.will);
        }

        public override void Update(GameTime gameTime)
        {
            luckyDialog.DialogCompleteEvent += nextDialog;
            ScreenManager.Singleton.AddScreen(new DialogScreen(luckyDialog, DialogScreen.Location.TopLeft, "Cara"));
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        #region Callbacks
        void nextDialog(object sender, PartyResponseEventArgs e)
        {
            luckyDialog.DialogCompleteEvent -= nextDialog;

            joinDialog.DialogCompleteEvent += nextDialog2;

            ScreenManager.Singleton.AddScreen(new DialogScreen(joinDialog, DialogScreen.Location.TopLeft, "Will"));
        }

        void nextDialog2(object sender, PartyResponseEventArgs e)
        {
            joinDialog.DialogCompleteEvent -= nextDialog2;

            talkingToTreeDialog.DialogCompleteEvent += finalDialog;

            ScreenManager.Singleton.AddScreen(new DialogScreen(talkingToTreeDialog, DialogScreen.Location.TopLeft, "Cara"));
        }

        void finalDialog(object sender, PartyResponseEventArgs e)
        {
            talkingToTreeDialog.DialogCompleteEvent -= finalDialog;

            will.FaceDirection(Direction.North);

            ScreenManager.Singleton.AddScreen(new DialogScreen(iKnewThatDialog, DialogScreen.Location.TopLeft, "Will"));

            state = SceneState.Complete;
        }

        #endregion 

    }
}
