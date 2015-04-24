using System.Collections.Generic;

namespace MyQuest
{
    public class RefGuard1Controller : NPCMapCharacterController
    {

        #region Dialog

        static readonly Dialog dialog = new Dialog(
            DialogPrompt.NeedsClose, Strings.ZA519);

        #endregion

        public override void Interact()
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Guard"));
        }
    }
}
