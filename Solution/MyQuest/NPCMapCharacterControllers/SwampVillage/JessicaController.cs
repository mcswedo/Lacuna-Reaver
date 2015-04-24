using System.Collections.Generic;

namespace MyQuest
{
    public class JessicasController : NPCMapCharacterController
    {
        const string curedTownAchievement = ChepetawaSceneD.achievement;

        static readonly Dialog helpDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z301, Strings.Z302);

        static readonly Dialog greatfulDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z303);
       
        public override void Interact()
        {
            if(Party.Singleton.PartyAchievements.Contains(curedTownAchievement))
            {
                ScreenManager.Singleton.AddScreen(new DialogScreen(greatfulDialog, DialogScreen.Location.TopLeft, "Jessica"));
            }
            else
            {
                ScreenManager.Singleton.AddScreen(new DialogScreen(helpDialog, DialogScreen.Location.TopLeft, "JessicaSick"));
            }
        }
    }
}
