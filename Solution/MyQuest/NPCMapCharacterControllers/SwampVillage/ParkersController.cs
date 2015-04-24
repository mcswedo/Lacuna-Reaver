using System.Collections.Generic;

namespace MyQuest
{
    public class ParkersController : NPCMapCharacterController
    {
        const string curedTownAchievement = ChepetawaSceneD.achievement;

        static readonly Dialog dontFeelDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z313);

        static readonly Dialog betterDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z314);

        public override void Interact()
        {
            if (Party.Singleton.PartyAchievements.Contains(curedTownAchievement))
            {
                ScreenManager.Singleton.AddScreen(new DialogScreen(betterDialog, DialogScreen.Location.TopLeft, "Parker"));
            }
            else
            {
                ScreenManager.Singleton.AddScreen(new DialogScreen(dontFeelDialog, DialogScreen.Location.TopLeft, "ParkerSick"));
            }
        }
    }
}
