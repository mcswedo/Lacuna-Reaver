using System.Collections.Generic;

namespace MyQuest
{
    public class SwampQueensController : NPCMapCharacterController
    {
        const string curedTownAchievement = ChepetawaSceneD.achievement;

        static readonly Dialog stopDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z331);

        static readonly Dialog welcomeDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z332);
       
        public override void Interact()
        {
            if (Party.Singleton.PartyAchievements.Contains(curedTownAchievement))
            {
                ScreenManager.Singleton.AddScreen(new DialogScreen(welcomeDialog, DialogScreen.Location.TopLeft, "SwampQueen"));
            }
            else
            {
                ScreenManager.Singleton.AddScreen(new DialogScreen(stopDialog, DialogScreen.Location.TopLeft, "SwampQueenSick"));
                Party.Singleton.AddLogEntry("Chapaka", "Swamp Queen", Strings.Z331);
            }
        }
    }
}
