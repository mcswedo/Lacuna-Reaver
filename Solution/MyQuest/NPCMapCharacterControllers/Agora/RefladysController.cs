using System.Collections.Generic;

namespace MyQuest
{
    public class RefladysController : NPCMapCharacterController
    {
        #region Dialog

        static readonly Dialog dialog = new Dialog(
            DialogPrompt.NeedsClose, Strings.ZA521);

        #endregion

        public override void Interact()
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Reflady"));
        }
    }
}
