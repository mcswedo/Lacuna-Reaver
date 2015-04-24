using System.Collections.Generic;

/// An example of how a controller is implemented

namespace MyQuest
{
    public class KidTagController : NPCMapCharacterController
    {
        const int reward = 50;

        #region Fields




        #endregion

        #region Achievements
        internal const string finishedTagAchievement = "finishedtag";
        internal const string tagAchievement = "tagAchievement";
       

        #endregion

        #region Dialogs

        Dialog dialog;

        static readonly Dialog Tag1Dialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z304);

        static readonly Dialog Tag2Dialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z305, Strings.Z306, Strings.Z307);

        static readonly Dialog Tag3Dialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z308);

        #endregion

        #region Interact

        public override void Interact()
        {
            if (Party.Singleton.PartyAchievements.Contains(finishedTagAchievement))
            {
                dialog = Tag3Dialog;
            }
            else if (Party.Singleton.PartyAchievements.Contains(tagAchievement))
            {
                dialog = Tag2Dialog;

                Party.Singleton.GameState.Gold += reward;

                Party.Singleton.AddAchievement(finishedTagAchievement);

            }
            else
            {
                Party.Singleton.AddAchievement("taginitiated");
                Party.Singleton.AddAchievement(tagAchievement);
               
                ScreenManager.Singleton.AddScreen(
             (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "TagCutSceneScreen"));
                return;
            }
           
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopLeft, NPCPool.stub));

        }

        #endregion

        #region Callbacks
      
        #endregion
    }
}
