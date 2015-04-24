using System.Collections.Generic;
using Microsoft.Xna.Framework;

//celindar is a mage town name we came up with
//still working on this NPC.

namespace MyQuest
{
    public class MoleTrainersController : NPCMapCharacterController
    {
        Dialog dialog;

        Portal portal;

        internal const string defeatBossAchievement = "defeatLibraryBoss";

        #region Dialogs

        static readonly Dialog moleDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z227, Strings.Z228, Strings.Z229);
        //Old dialog:You interested in these moles here? We discovered them when the great mages flipped Celindar underground. ,, When one of us needs to go to the surface, we ride the moles to get past the mountain range.
        //  Once you've cleansed the library, I'll let you take one of these to the other side.

        static readonly Dialog rideDialog = new Dialog(DialogPrompt.YesNo, Strings.Z230, Strings.Z231);
        //Z231 old dialog: Would you like to take a mole to the other side of Ellaethia?

        static readonly Dialog yesDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z232);

        static readonly Dialog noDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z233);

        static readonly Dialog willDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z506);
        //The moles are asleep for the night. Try coming back in the morning.

        #endregion

        #region Interact

        public override void Interact()
        {
            if (Party.Singleton.CurrentMap.Name.Equals(Maps.mageTownNight))
            {
                dialog = willDialog;
            }
            else if (Party.Singleton.PartyAchievements.Contains(defeatBossAchievement))
            {
                dialog = rideDialog;
                dialog.DialogCompleteEvent += MoleRide;
            }

            else
            {
                dialog = moleDialog;
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "MoleTrainer"));
        }

        #endregion

        void MoleRide(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= MoleRide;

            if (e.Response == PartyResponse.Yes)
            {
                dialog = yesDialog;
                dialog.DialogCompleteEvent += PortalTaken;
                ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "MoleTrainer"));
            }

            else
            {
                dialog = noDialog;

                ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "MoleTrainer"));
            }
        }

        void PortalTaken(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= PortalTaken;
            portal = new Portal { DestinationMap = "overworld", DestinationPosition = new Point(75, 12), Position = Point.Zero };
            Party.Singleton.PortalToMap(portal);
        }
    }
}
      

     
