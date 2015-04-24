using System.Collections.Generic;

namespace MyQuest
{
    public class SarahsController : NPCMapCharacterController
    {
        const string curedTownAchievement = ChepetawaSceneD.achievement;

        static readonly Dialog afraidDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z317, Strings.Z318);

        static readonly Dialog thankYouDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z319);

        public override void Interact()
        {
            if (Party.Singleton.PartyAchievements.Contains(curedTownAchievement))
            {
                ScreenManager.Singleton.AddScreen(new DialogScreen(thankYouDialog, DialogScreen.Location.TopLeft, "Sarah"));
            }
            else
            {
                ScreenManager.Singleton.AddScreen(new DialogScreen(afraidDialog, DialogScreen.Location.TopLeft, "SarahSick"));
            }
        }
    }
}
