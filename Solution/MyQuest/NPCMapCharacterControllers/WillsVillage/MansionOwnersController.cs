using System.Collections.Generic;

namespace MyQuest
{
    public class MansionOwnersController : NPCMapCharacterController
    {
        static readonly Dialog dontJustdialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z375);

        static readonly Dialog youGuysdialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z376);

        public override void Interact()
        {
            Dialog dialog;

            dialog = Party.Singleton.PartyAchievements.Contains("defeatBurtle") ? youGuysdialog : dontJustdialog;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "MansionOwner"));
        }
    }
}
