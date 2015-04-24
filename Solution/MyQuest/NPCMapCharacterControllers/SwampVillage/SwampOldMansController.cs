using System.Collections.Generic;

namespace MyQuest
{
    public class SwampOldMansController : NPCMapCharacterController
    {
        const string curedTownAchievement = ChepetawaSceneD.achievement;

        static readonly Dialog damnDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z326);

        static readonly Dialog honorDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z327, Strings.Z328);

        public override void Interact()
        {
            if (Party.Singleton.PartyAchievements.Contains(curedTownAchievement))
            {
                ScreenManager.Singleton.AddScreen(new DialogScreen(honorDialog, DialogScreen.Location.TopLeft, "SwampOldMan"));
            }
            else
            {
                ScreenManager.Singleton.AddScreen(new DialogScreen(damnDialog, DialogScreen.Location.TopLeft, "SwampOldManSick"));
            }
        }

    }
}
