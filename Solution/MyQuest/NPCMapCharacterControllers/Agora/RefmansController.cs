using System.Collections.Generic;

namespace MyQuest
{
    public class RefmansController : NPCMapCharacterController
    {
        #region Achivements

        const string savedVillageAchievement = HealersBattleSceneA.savedVillageAchievement;
        const string returnAchievement = WillsBurtleInitiateSceneC.achievement;
        const string receivedSwordAchievement = HealersBlacksmithsController.receivedSwordAchievement;

        #endregion

        #region Dialog

        static readonly Dialog dialog = new Dialog(
            DialogPrompt.NeedsClose, Strings.ZA520);

        #endregion

        public override void Interact()
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Refman"));
        }
    }
}
