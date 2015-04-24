using System.Collections.Generic;

namespace MyQuest
{
    public class BobsController : NPCMapCharacterController
    {
        #region Achivements

        const string savedVillageAchievement = HealersBattleSceneA.savedVillageAchievement;
        const string returnAchievement = WillsBurtleInitiateSceneC.achievement;
        const string receivedSwordAchievement = HealersBlacksmithsController.receivedSwordAchievement;

        #endregion

        #region Dialog

        static readonly Dialog cantGoDialog = new Dialog(
            DialogPrompt.NeedsClose, Strings.Z009);

        static readonly Dialog greatDayDialog = new Dialog(
            DialogPrompt.NeedsClose, Strings.Z010);

        static readonly Dialog heroDialog = new Dialog(
            DialogPrompt.NeedsClose,Strings.Z011);

        static readonly Dialog goodDialog = new Dialog(
            DialogPrompt.NeedsClose, Strings.Z012);

        #endregion

        public override void Interact()
        {
            Dialog dialog;
            IList<string> achievements = Party.Singleton.PartyAchievements;

            dialog = (achievements.Contains(receivedSwordAchievement) ? cantGoDialog : greatDayDialog);

            if (achievements.Contains(savedVillageAchievement))
            {
                dialog = heroDialog; 
            }

            if (achievements.Contains(returnAchievement))
            {
                dialog = goodDialog;
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Bob"));
        }
    }
}
