using System.Collections.Generic;

namespace MyQuest
{
    public class SwampGirlsController : NPCMapCharacterController
    {
        const string curedTownAchievement = ChepetawaSceneD.achievement;

        static readonly Dialog feelBetterDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z323);

        static readonly Dialog playDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z324);

        public override void Interact()
        {
            if (Party.Singleton.PartyAchievements.Contains(curedTownAchievement))
            {
                ScreenManager.Singleton.AddScreen(new DialogScreen(playDialog, DialogScreen.Location.TopLeft, "SwampGirl"));
            }
            else
            {
                ScreenManager.Singleton.AddScreen(new DialogScreen(feelBetterDialog, DialogScreen.Location.TopLeft, "SwampGirlSick"));
            }
        }
    }
}
