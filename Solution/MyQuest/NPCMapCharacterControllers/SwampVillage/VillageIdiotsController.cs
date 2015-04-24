using System.Collections.Generic;

namespace MyQuest
{
    public class VillageIdiotsController : NPCMapCharacterController
    {
        const string curedTownAchievement = ChepetawaSceneD.achievement;

        Dialog dialog; 
        static readonly Dialog codeDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA564);

        static readonly Dialog lostDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA693,
            Strings.ZA694);

        static readonly Dialog compassDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA695, Strings.ZA696);

        static readonly Dialog rewardDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA697);

        static readonly Dialog findAnythingDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA698, "...", Strings.ZA699);

        internal const string myAchievement = "eyeOfShambala";

        public override void Interact()
        {
            if (Party.Singleton.PartyAchievements.Contains(curedTownAchievement))
            {              
                if (Party.Singleton.GameState.Inventory.ItemCount(typeof(Compass)) == 1)
                {
                    dialog = compassDialog;
                    dialog.DialogCompleteEvent += Reward;
                }
                else
                {
                    dialog = lostDialog;
                    Party.Singleton.AddLogEntry("Chapaka", "Village Idiot", dialog.Text);
                }

                if (Party.Singleton.PartyAchievements.Contains(myAchievement))
                {
                    dialog = findAnythingDialog;
                }

                ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "VillageIdiot"));
            }
            
            else
            {
                dialog = codeDialog;
                Party.Singleton.AddLogEntry("Chapaka", "Village Idiot", dialog.Text);
                ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "VillageIdiotSick"));

            }
        }

        void Reward(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= Reward;
            Party.Singleton.GameState.Inventory.AddItem(typeof(EyeOfShambala), 1);
            Party.Singleton.GameState.Inventory.RemoveItem(typeof(Compass), 1);
            Party.Singleton.AddAchievement(myAchievement); 
            dialog = rewardDialog; 
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
        }
    }
}
