using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class MotherController : NPCMapCharacterController
    {
        internal const string spokeToMomAchievement = "spokeToMom";

        static readonly Dialog MotherDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z312);

        public override void Interact()

        {
            Party.Singleton.AddAchievement(spokeToMomAchievement);
            ScreenManager.Singleton.AddScreen(new DialogScreen(MotherDialog, DialogScreen.Location.TopLeft, NPCPool.stub));

        }
    }
}
