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
    public class RiftManController : NPCMapCharacterController
    {
        const int reward = 50;

        #region Achievements

        internal const string foundRiftKeyAchievement = "foundriftkey";
        internal const string foundSecondRiftKeyAchievement = "foundsecondriftkey";
        internal const string talkedToRiftManAchievement = "talkedtoriftman";
        internal const string solvedFirstRiddleAchievement = "solvedfirstriddle";
        internal const string openedRiftChestAchievement = "openedriftchest";

        #endregion

        #region Dialogs

        Dialog dialog;

        static readonly Dialog RiftManDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z278, Strings.Z279, Strings.Z280, Strings.Z281, Strings.Z282, Strings.Z283);

        static readonly Dialog RiftManDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA604, Strings.Z284, Strings.ZA225);

        static readonly Dialog RiftManDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z286, Strings.Z287);

        static readonly Dialog RiftManDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.Z288);

        static readonly Dialog RiftManDialog5 = new Dialog(DialogPrompt.NeedsClose, Strings.Z289);

        #endregion

        #region Interact

        public override void Interact()
        {
            Party.Singleton.Leader.FaceDirection(Direction.East);

            if (Party.Singleton.PartyAchievements.Contains(openedRiftChestAchievement))
            {
                dialog = RiftManDialog4;
            }
            else if (Party.Singleton.PartyAchievements.Contains(foundSecondRiftKeyAchievement))
            {
                dialog = RiftManDialog3;

                Party.Singleton.GameState.Inventory.RemoveItem(typeof(RiftNote2), 1);
                Party.Singleton.PartyAchievements.Add(openedRiftChestAchievement);
            }
            else if (Party.Singleton.PartyAchievements.Contains(solvedFirstRiddleAchievement))
            {
                dialog = RiftManDialog5;
            }
            else if (Party.Singleton.PartyAchievements.Contains(foundRiftKeyAchievement))
            {
                dialog = RiftManDialog2;

                Party.Singleton.GameState.Inventory.RemoveItem(typeof(RiftNote1), 1);
                Party.Singleton.PartyAchievements.Add(solvedFirstRiddleAchievement);
                Party.Singleton.AddLogEntry("Snowy Ridge", "Rift Man", dialog.Text);

                Party.Singleton.ModifyNPC(
                   Maps.mushroomForest,
                   "RiftNote2",
                   new Point(38, 10),
                   ModAction.Add,
                   true);
            }
            else if (Party.Singleton.PartyAchievements.Contains(talkedToRiftManAchievement))
            {
                dialog = RiftManDialog5;
            }
            else
            {
                dialog = RiftManDialog1;

                Party.Singleton.PartyAchievements.Add(talkedToRiftManAchievement);
                Party.Singleton.AddLogEntry("Snowy Ridge", "Rift Man", dialog.Text);

                Party.Singleton.ModifyNPC(
                   "swamp_village_inn_fl2",
                   "RiftNote1",
                   new Point(1, 7),
                   ModAction.Add,
                   true);
            }
           
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "RiftMan"));
        }

        #endregion
    }
}
