using System.Collections.Generic;

namespace MyQuest
{
    public class SwampOldWomansController : NPCMapCharacterController
    {
        const string curedTownAchievement = ChepetawaSceneD.achievement;

        static readonly Dialog plagueDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z329);

        static readonly Dialog thankYouDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z330);

        public override void Interact()
        {
            if (Party.Singleton.PartyAchievements.Contains(curedTownAchievement))
            {
                ScreenManager.Singleton.AddScreen(new DialogScreen(thankYouDialog, DialogScreen.Location.TopLeft, "SwampOldWoman"));
            }
            else
            {
                ScreenManager.Singleton.AddScreen(new DialogScreen(plagueDialog, DialogScreen.Location.TopLeft, "SwampOldWomanSick"));
            }
        }
    }
}
