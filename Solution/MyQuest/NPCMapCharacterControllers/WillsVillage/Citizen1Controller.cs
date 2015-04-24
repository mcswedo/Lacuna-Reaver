using System.Collections.Generic;

namespace MyQuest
{
    public class Citizen1Controller : NPCMapCharacterController
    {
        #region Dialog
        internal const string foundCriminal2Achievement = "foundCriminal2";
        static readonly Dialog heyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z348, Strings.Z349);

        static readonly Dialog whatTheDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z350, Strings.Z351);

        static readonly Dialog nightDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z352, Strings.Z353, Strings.Z354, Strings.Z355);

        #endregion 

        #region Interact

        public override void Interact()
        {
            Dialog dialog;
            if (Party.Singleton.PartyAchievements.Contains(foundCriminal2Achievement))
            {
                dialog = whatTheDialog;
            }
            else if (Party.Singleton.CurrentMap.Name.Equals(Maps.blindMansTownNight))
            {
                dialog = nightDialog;
            }
            else
                dialog = heyDialog;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Citizen1"));
        }
        #endregion
    }
}
