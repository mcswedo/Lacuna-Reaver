using System.Collections.Generic;

namespace MyQuest
{
    public class SwampBoysController : NPCMapCharacterController
    {
        const string curedTownAchievement = ChepetawaSceneD.achievement;

        static readonly Dialog coughDialog = new Dialog(
            DialogPrompt.NeedsClose,
            Strings.Z321);

        static readonly Dialog thankYouDialog = new Dialog(
            DialogPrompt.NeedsClose,
            Strings.Z322);

        public override void Interact()
        {
            if (Party.Singleton.PartyAchievements.Contains(curedTownAchievement))
            {
                ScreenManager.Singleton.AddScreen(new DialogScreen(thankYouDialog, DialogScreen.Location.TopLeft, "SwampBoy"));
            }
            else
            {
                ScreenManager.Singleton.AddScreen(new DialogScreen(coughDialog, DialogScreen.Location.TopLeft, "SwampBoySick"));
            }
        }
    }
}
