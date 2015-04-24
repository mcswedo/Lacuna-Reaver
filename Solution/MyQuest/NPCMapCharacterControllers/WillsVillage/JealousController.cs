using System.Collections.Generic;

namespace MyQuest
{
    public class JealousController : NPCMapCharacterController
    {
        static readonly Dialog girlDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z359);
                                            
        static readonly Dialog boyDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z360);

        static readonly Dialog girlDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z361);

        static readonly Dialog boyDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z362);

        static readonly Dialog girlDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z363);

        static readonly Dialog boyDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z364, Strings.Z365);

        static readonly Dialog girlDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.Z366, Strings.Z367);

        static readonly Dialog boyDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.Z368);

        static readonly Dialog girlDialog5 = new Dialog(DialogPrompt.NeedsClose, Strings.Z369);

        public override void Interact()
        {
            girlDialog1.DialogCompleteEvent += nextDialog;
            ScreenManager.Singleton.AddScreen(new DialogScreen(girlDialog1, DialogScreen.Location.TopRight, "JealousGirl"));
        }

        #region Callbacks
        void nextDialog(object sender, PartyResponseEventArgs e)
        {
            girlDialog1.DialogCompleteEvent -= nextDialog; 
            boyDialog1.DialogCompleteEvent += nextDialog2;
            ScreenManager.Singleton.AddScreen(new DialogScreen(boyDialog1, DialogScreen.Location.TopLeft, "JealousBoy"));
        }

        void nextDialog2(object sender, PartyResponseEventArgs e)
        {
            boyDialog1.DialogCompleteEvent -= nextDialog2;
            girlDialog2.DialogCompleteEvent += nextDialog3;
            ScreenManager.Singleton.AddScreen(new DialogScreen(girlDialog2, DialogScreen.Location.TopRight, "JealousGirl"));
        }

        void nextDialog3(object sender, PartyResponseEventArgs e)
        {
            girlDialog2.DialogCompleteEvent -= nextDialog3;
            boyDialog2.DialogCompleteEvent += nextDialog4;
            ScreenManager.Singleton.AddScreen(new DialogScreen(boyDialog2, DialogScreen.Location.TopLeft, "JealousBoy"));
        }

        void nextDialog4(object sender, PartyResponseEventArgs e)
        {
            boyDialog2.DialogCompleteEvent -= nextDialog4;
            girlDialog3.DialogCompleteEvent += nextDialog5;
            ScreenManager.Singleton.AddScreen(new DialogScreen(girlDialog3, DialogScreen.Location.TopRight, "JealousGirl"));
        }

        void nextDialog5(object sender, PartyResponseEventArgs e)
        {
            girlDialog3.DialogCompleteEvent -= nextDialog5;
            boyDialog3.DialogCompleteEvent += nextDialog6;
            ScreenManager.Singleton.AddScreen(new DialogScreen(boyDialog3, DialogScreen.Location.TopLeft, "JealousBoy"));
        }

        void nextDialog6(object sender, PartyResponseEventArgs e)
        {
            boyDialog3.DialogCompleteEvent -= nextDialog6;
            girlDialog4.DialogCompleteEvent += nextDialog7;
            ScreenManager.Singleton.AddScreen(new DialogScreen(girlDialog4, DialogScreen.Location.TopRight, "JealousGirl"));
        }

        void nextDialog7(object sender, PartyResponseEventArgs e)
        {
            girlDialog4.DialogCompleteEvent -= nextDialog7;
            boyDialog4.DialogCompleteEvent += nextDialog8;
            ScreenManager.Singleton.AddScreen(new DialogScreen(boyDialog4, DialogScreen.Location.TopLeft, "JealousBoy"));
        }

        void nextDialog8(object sender, PartyResponseEventArgs e)
        {
            boyDialog4.DialogCompleteEvent -= nextDialog8;
            ScreenManager.Singleton.AddScreen(new DialogScreen(girlDialog5, DialogScreen.Location.TopRight, "JealousGirl"));
        }

        #endregion
    }
}