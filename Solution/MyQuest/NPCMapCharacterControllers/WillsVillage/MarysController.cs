using System.Collections.Generic;

namespace MyQuest
{
    public class MarysController : NPCMapCharacterController
    {
        const int reward = 150;

        #region Achievements

        internal const string purseFoundAchievement = "purseFound";

        internal const string victimCompleteAchievement = "victimComplete";

        internal const string logAchievement = "logUsed";

        #endregion

        #region Dialog

        static readonly Dialog rewardDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA289);

        static readonly Dialog stolenDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z377);

        static readonly Dialog thankYouDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z378, Strings.Z379);

        static readonly Dialog victimCompleteDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z380);

        static readonly Dialog nightDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z381, Strings.Z382, Strings.Z383);

        #endregion 

        #region Interact

        public override void Interact()
        {
            Dialog dialog;
            IList<string> achievements = Party.Singleton.PartyAchievements;

            if (Party.Singleton.CurrentMap.Name.Equals(Maps.blindMansTownNight))
            {
                dialog = nightDialog;
            }
            else if (Party.Singleton.PartyAchievements.Contains(victimCompleteAchievement))
            {
                dialog = victimCompleteDialog;
            }
            else if (Party.Singleton.PartyAchievements.Contains("foundPurse"))
            {
                dialog = thankYouDialog;
                thankYouDialog.DialogCompleteEvent += RewardCallback;

                Party.Singleton.GameState.Inventory.RemoveItem(typeof(Purse), 1);

                Party.Singleton.GameState.Gold += reward;
                Party.Singleton.GameState.Inventory.AddItem(typeof(LapizLazuliRing), 1);

                Party.Singleton.AddAchievement(victimCompleteAchievement);
            }
            else
            {
                dialog = stolenDialog;
                if (!Party.Singleton.PartyAchievements.Contains(logAchievement))
                {
                    Party.Singleton.AddLogEntry("Tamarel", "Mary", stolenDialog.Text);
                    Party.Singleton.AddAchievement(logAchievement);
                }
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Woman2"));
        }
        #endregion

        void RewardCallback(object sender, PartyResponseEventArgs e)
        {
            Dialog dialog;

            thankYouDialog.DialogCompleteEvent -= RewardCallback;
            dialog = rewardDialog;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
        }
    }
}
