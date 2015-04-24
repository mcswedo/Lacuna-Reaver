using System.Collections.Generic;

namespace MyQuest
{
    public class MolesController : NPCMapCharacterController
    {
        Dialog dialog;

        static readonly Dialog tempDialog = new Dialog(DialogPrompt.NeedsClose,
            "I'm a mole, I won't have dialog when my art is complete.");

        #region Interact

        public override void Interact()
        {
         
            dialog = tempDialog; 

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, NPCPool.stub));
        }

        #endregion
    }
}
      

     
