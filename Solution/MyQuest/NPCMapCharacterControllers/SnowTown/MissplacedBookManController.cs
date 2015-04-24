using System.Collections.Generic;
using System;

using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
/// An example of how a controller is implemented

namespace MyQuest
{
    public class MissplacedBookManController : NPCMapCharacterController
    {
        const int reward = 400;

        #region Achievements
        internal const string missplaceManTalkAchievement = "missplacedmantalk";
        internal const string foundMissplacedBookAchievement = "foundmissplacedbook";
        internal const string returnedMissplacedBookAchievement = "returnedmissplacedbook";
        



        #endregion

        #region Dialogs

        Dialog dialog;

        static readonly Dialog MissplacedBookManDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z257, Strings.Z258, Strings.Z259, Strings.Z260);

        static readonly Dialog MissplacedBookManDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z261, Strings.Z262);

        static readonly Dialog MissplacedBookManDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z263, Strings.Z264, Strings.Z265, Strings.Z266);

        static readonly Dialog MissplacedBookManDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.Z267);

        static readonly Dialog rewardDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA603);

        #endregion


        #region Interact

        public override void Interact()
        {
            if (Party.Singleton.PartyAchievements.Contains(returnedMissplacedBookAchievement))
            {
                dialog = MissplacedBookManDialog4;
            }
            else if (Party.Singleton.PartyAchievements.Contains(foundMissplacedBookAchievement))
            {
                dialog = MissplacedBookManDialog3;
                Party.Singleton.GameState.Inventory.RemoveItem(typeof(MissplacedBook), 1);
                Party.Singleton.PartyAchievements.Add(returnedMissplacedBookAchievement);

                dialog.DialogCompleteEvent += Reward;
            }
            else if (Party.Singleton.PartyAchievements.Contains(missplaceManTalkAchievement))
            {
                dialog = MissplacedBookManDialog2;
            }
            else 
            {
                Party.Singleton.PartyAchievements.Add(missplaceManTalkAchievement);
                dialog = MissplacedBookManDialog1;
                Party.Singleton.AddLogEntry("Snowy Ridge", "Upset Woman", dialog.Text);
            }
          
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "MisplacedBookMan"));
        }

        #endregion


        void Reward(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= Reward;

            dialog = rewardDialog;
            //Party.Singleton.GameState.Inventory.AddItem(typeof(NatureCharm), 1);
            Party.Singleton.GameState.Gold += reward;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
        }
    }
}
