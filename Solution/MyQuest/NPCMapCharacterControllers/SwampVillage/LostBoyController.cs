using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class LostBoyController : NPCMapCharacterController
    {
        static readonly Dialog LostBoyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z309);
        static readonly Dialog LostBoyDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z310);

        public override void Interact()
        {
            if (Party.Singleton.PartyAchievements.Contains(MotherController.spokeToMomAchievement))
            {
               // ScreenManager.Singleton.AddScreen(new DialogScreen(LostBoyDialog2, DialogScreen.Location.TopLeft, NPCPool.stub));
                ScreenManager.Singleton.AddScreen(
          (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "LostboyCutSceneScreen"));
            }
            else  ScreenManager.Singleton.AddScreen(new DialogScreen(LostBoyDialog, DialogScreen.Location.TopLeft, NPCPool.stub));

        }
    }
}
