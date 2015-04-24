using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class RendilsController : NPCMapCharacterController
    {
        #region Achievements

        internal const string rendilCompleteAchievement = "rendilComplete";

        #endregion

        int reward = 500;

        Dialog dialog;

        #region Dialog

        static readonly Dialog lostHatDialog = new Dialog(DialogPrompt.NeedsClose,  Strings.Z234, Strings.Z235, Strings.Z236);

        static readonly Dialog thankYouDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z237, Strings.Z238, Strings.Z239);

        static readonly Dialog itemsReceived = new Dialog(DialogPrompt.NeedsClose, Strings.Z240, Strings.Z241);

        static readonly Dialog rendilCompleteDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z242);

        #endregion

        #region Interact

        public override void Interact()
        {
            IList<string> achievements = Party.Singleton.PartyAchievements;

            if (Party.Singleton.PartyAchievements.Contains(rendilCompleteAchievement))
            {
                dialog = rendilCompleteDialog;
          
            }

            else if (Party.Singleton.PartyAchievements.Contains("hatFound"))
            {
                dialog = thankYouDialog;

                dialog.DialogCompleteEvent += ReceivingItems;
            }

            else
            {
                dialog = lostHatDialog;
                Party.Singleton.AddLogEntry("Celindar", "Rendil", lostHatDialog.Text);
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Rendil"));
        }

        void ReceivingItems(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= ReceivingItems;

            Party.Singleton.AddAchievement(rendilCompleteAchievement);

            Party.Singleton.GameState.Inventory.RemoveItem(typeof(WizardHat), 1);

            Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 2);
            Party.Singleton.GameState.Gold += reward;
            MusicSystem.InterruptMusic(AudioCues.ChestOpen);

            ScreenManager.Singleton.AddScreen(new DialogScreen(itemsReceived, DialogScreen.Location.TopLeft, "Rendil"));
        }
    }

        #endregion
}
