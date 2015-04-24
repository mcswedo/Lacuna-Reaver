using System.Collections.Generic;

namespace MyQuest
{
    public class BurlamsController : NPCMapCharacterController
    {
        #region Fields

        Dialog dialog;

        #endregion

        #region Dialogs

        static readonly Dialog plantDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA677);

        static readonly Dialog foundDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA678, Strings.ZA679);

        static readonly Dialog rewardDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA680);

        static readonly Dialog thanksDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA681);

        static readonly Dialog willDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z509);

        #endregion

        #region Interact

        internal const string myAchievement = "snowBoots";

        public override void Interact()
        {
            if (Party.Singleton.CurrentMap.Name.Equals(Maps.mageTownNight))
            {
                dialog = willDialog;
            }
            else
            {
                if (Party.Singleton.GameState.Inventory.ItemCount(typeof(MysteriaPlant)) == 1)
                {
                    dialog = foundDialog;
                    dialog.DialogCompleteEvent += Reward;
                }
                else
                {
                    dialog = plantDialog;
                    Party.Singleton.AddLogEntry("Celindar", "Burlam", dialog.Text);
                }

                if (Party.Singleton.PartyAchievements.Contains(myAchievement))
                {
                    dialog = thanksDialog;
                }
            }
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Burlam"));
        }

        #endregion

        void Reward(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= Reward;
            Party.Singleton.GameState.Inventory.AddItem(typeof(SnowBoots), 1);
            Party.Singleton.GameState.Inventory.RemoveItem(typeof(MysteriaPlant), 1);
            Party.Singleton.AddAchievement(myAchievement); 
            dialog = rewardDialog; 
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
        }

    }
}
      

     
