using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class SuspiciousGuyController : NPCMapCharacterController
    {
        Dialog dialog;

        static readonly Dialog feetDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA686);

        static readonly Dialog niceBootsDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA687, Strings.ZA688);

        static readonly Dialog rewardDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA689);

        static readonly Dialog feelBetterDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA690);

        internal const string myAchievement = "compass";

        public override void Interact()
        {
            if (Party.Singleton.GameState.Inventory.ItemCount(typeof(SnowBoots)) == 1)
            {
                dialog = niceBootsDialog;
                dialog.DialogCompleteEvent += Reward;
            }
            else
            {
                dialog = feetDialog;

                Party.Singleton.AddLogEntry("Snowy Ridge", "Villager", dialog.Text);
            }

            if (Party.Singleton.PartyAchievements.Contains(myAchievement))
            {
                dialog = feelBetterDialog;
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "SuspiciousGuy"));
        }


        void Reward(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= Reward;
            Party.Singleton.GameState.Inventory.AddItem(typeof(Compass), 1);
            Party.Singleton.GameState.Inventory.RemoveItem(typeof(SnowBoots), 1);
            Party.Singleton.AddAchievement(myAchievement); 
            dialog = rewardDialog; 
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
        }
    }
}
