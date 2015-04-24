using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class FinalBattleSceneF : Scene
    {
        #region Dialog

        static readonly Dialog malDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA271, Strings.ZA272);

        static readonly Dialog arlanDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA273, Strings.ZA274, Strings.ZA275, Strings.ZA276);

        static readonly Dialog nathanDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA277);

        static readonly Dialog arlanDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA278, Strings.ZA279);

        static readonly Dialog willDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA280);

        static readonly Dialog caraDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA281);

        static readonly Dialog arlanDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA282);

        #endregion

        Dialog dialog;

        public FinalBattleSceneF(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            MusicSystem.Play(AudioCues.finalBossPT2);
            dialog = malDialog1;
            dialog.DialogCompleteEvent += ArlanResponse1;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Malticar"));
        }


        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        void ArlanResponse1(object sender, PartyResponseEventArgs e)
        {
            Party.Singleton.Leader.FaceDirection(Direction.North);
            Party.Singleton.CurrentMap.GetNPC("Cara").FaceDirection(Direction.North);

            dialog.DialogCompleteEvent -= ArlanResponse1;

            dialog = arlanDialog1;
            dialog.DialogCompleteEvent += NathanResponse1;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "InjuredArlan"));
        }

        void NathanResponse1(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= NathanResponse1;

            dialog = nathanDialog1;
            dialog.DialogCompleteEvent += ArlanResponse2;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Nathan"));
        }

        void ArlanResponse2(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= ArlanResponse2;

            dialog = arlanDialog2;
            dialog.DialogCompleteEvent += WillResponse1;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "InjuredArlan"));
        }

        void WillResponse1(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= WillResponse1;

            dialog = willDialog1;
            dialog.DialogCompleteEvent += CaraResponse1;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Will"));
        }

        void CaraResponse1(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= CaraResponse1;

            dialog = caraDialog1;
            dialog.DialogCompleteEvent += ArlanResponse3;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Cara"));
        }

        void ArlanResponse3(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= ArlanResponse3;

            dialog = arlanDialog3;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "InjuredArlan"));

            state = SceneState.Complete;
        }
    }
}
