using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class FinalBattleSceneJ : Scene
    {
        #region Dialog

        static readonly Dialog caraDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA641);

        static readonly Dialog willDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA642);

        static readonly Dialog nathanDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA643);

        static readonly Dialog caraDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA644);

        static readonly Dialog nathanDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA645);

        static readonly Dialog caraDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA646);

        static readonly Dialog willDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA647, Strings.ZA648);

        static readonly Dialog nathanDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA649);

        #endregion

        Dialog dialog;

        public FinalBattleSceneJ(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.CurrentMap.GetNPC("Cara").FaceDirection(Direction.East);
            Party.Singleton.Leader.FaceDirection(Direction.South);
            dialog = caraDialog1;
            dialog.DialogCompleteEvent += WillResponse1;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Cara"));
        }


        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        void WillResponse1(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= WillResponse1;

            dialog = willDialog1;
            dialog.DialogCompleteEvent += NathanResponse1;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Will"));
        }

        void NathanResponse1(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= NathanResponse1;

            dialog = nathanDialog1;
            dialog.DialogCompleteEvent += CaraResponse2;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Nathan"));
        }

        void CaraResponse2(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= CaraResponse2;

            dialog = caraDialog2;
            dialog.DialogCompleteEvent += NathanResponse2;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Cara"));
        }

        void NathanResponse2(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= NathanResponse2;

            dialog = nathanDialog2;
            dialog.DialogCompleteEvent += CaraResponse3;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopRight, "Nathan"));
        }

        void CaraResponse3(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= CaraResponse3;

            dialog = caraDialog3;
            dialog.DialogCompleteEvent += WillResponse2;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Cara"));
        }

        void WillResponse2(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= WillResponse2;

            dialog = willDialog2;
            dialog.DialogCompleteEvent += NathanResponse3;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Will"));
        }

        void NathanResponse3(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= NathanResponse3;

            dialog = nathanDialog3;
            dialog.DialogCompleteEvent += SceneDone;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Nathan"));
        }

        void SceneDone(object sender, PartyResponseEventArgs e)
        {
            state = SceneState.Complete;
        }
    }
}