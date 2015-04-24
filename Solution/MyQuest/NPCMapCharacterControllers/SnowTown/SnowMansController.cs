using System.Collections.Generic;

namespace MyQuest
{
    public class SnowMansController : NPCMapCharacterController
    {
        const string foundBlacksmithAchievement = "rescuedMasterBlacksmith";

        static readonly Dialog blacksmithDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z290, Strings.Z291, Strings.Z292, Strings.Z293, Strings.Z294 );

        static readonly Dialog realyIsDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z295,  Strings.Z296);

        public override void Interact()
        {
            if (Party.Singleton.PartyAchievements.Contains(foundBlacksmithAchievement))
            {
                ScreenManager.Singleton.AddScreen(new DialogScreen(realyIsDialog, DialogScreen.Location.TopLeft, "SnowMan"));
            }

            else
            {
                ScreenManager.Singleton.AddScreen(new DialogScreen(blacksmithDialog, DialogScreen.Location.TopLeft, "SnowMan"));

                Party.Singleton.AddLogEntry("Snowy Ridge", "Villager", blacksmithDialog.Text);
            }
        }
    }
}
