using System.Collections.Generic;

/// An example of how a controller is implemented

namespace MyQuest
{
    public class UpsetBoyController : NPCMapCharacterController
    {
        const int reward = 50;

        #region Fields




        #endregion

        #region Achievements
        internal const string firsttalkachievement = "firsttalk";
        internal const string returnedToyAchievement = "returnedItem";
        internal const string retrievedToyAchievement = "retrievedtoy";

        #endregion

        #region Dialogs
        
        Dialog dialog;
      
        static readonly Dialog MonsterDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z333, Strings.Z334, Strings.Z335);

        static readonly Dialog meanMonsterDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z336 );

        static readonly Dialog retrievedToyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z337, Strings.Z338, Strings.Z339);

        static readonly Dialog returnedToyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z340);
        

        #endregion

        #region Interact

        public override void Interact()
        {
            
           
            
            if (Party.Singleton.PartyAchievements.Contains(returnedToyAchievement))
            {
                dialog = returnedToyDialog;
            }
            else if (Party.Singleton.PartyAchievements.Contains(retrievedToyAchievement))
            {
                dialog = retrievedToyDialog;

                Party.Singleton.GameState.Inventory.RemoveItem(typeof(LostToy), 1);

                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 2);

                Party.Singleton.GameState.Gold += reward;

                Party.Singleton.AddAchievement(returnedToyAchievement);
                NPCMapCharacter UpsetBoy = Party.Singleton.CurrentMap.GetNPC("UpsetBoy");
                UpsetBoy.IdleOnly = false;
            }
            else if (Party.Singleton.PartyAchievements.Contains(firsttalkachievement))
            {
                dialog = meanMonsterDialog;
            }
            else
            {
                
                dialog = MonsterDialog;
                ScreenManager.Singleton.AddScreen(
              (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "LostToyCutSceneScreen"));
                Party.Singleton.Leader.FaceDirection(Direction.North);
                Party.Singleton.AddAchievement(firsttalkachievement);
                Party.Singleton.AddLogEntry("Mushroom Forest", "Drifter", meanMonsterDialog.Text);
            }

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopLeft, NPCPool.stub));
            
        }

        #endregion

        #region Callbacks
        /*
        void FirstTalk(object sender, PartyResponseEventArgs e)
        {
            meanMonsterDialog.DialogCompleteEvent -= FirstTalk;

            Party.Singleton.AddAchievement(firsttalkachievement);

           
        }*/
        void Reward(object sender, PartyResponseEventArgs e)
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(retrievedToyDialog, DialogScreen.Location.TopLeft, NPCPool.stub));
        }

        #endregion
    }
}
