using System.Collections.Generic;

/// An example of how a controller is implemented

namespace MyQuest
{
    public class BoxGameManController : NPCMapCharacterController
    {
        #region Fields

        const int reward = 450;

        #endregion

        #region Achievements
        internal const string foundGoldAchievement = "foundgold";
        internal const string foundEmptyChestAchievement = "foundemptychest";
        internal const string foundGhostAchievement = "foundghost";
        internal const string BoxTalkAchievement = "boxtalk";
        internal const string GoldResponseAchievement = "goldresponse";
        internal const string EmptyResponseAchievement = "emptyresponse";
        internal const string GhostResponseAchievement = "ghostresponse";
        internal const string gamePlayedAchievement = "gamePlayed";
        
        #endregion

        #region Dialogs

        Dialog dialog;
        
        static readonly Dialog ManDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z148, Strings.Z149, Strings.Z150);

        static readonly Dialog ManDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z151);

        static readonly Dialog ManDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z152, Strings.Z153);

        static readonly Dialog ManDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.Z154, Strings.Z155);

        static readonly Dialog ManDialog5 = new Dialog(DialogPrompt.NeedsClose, Strings.Z156);

        static readonly Dialog ManDialog6 = new Dialog(DialogPrompt.NeedsClose, Strings.Z157);

        static readonly Dialog ManDialog7 = new Dialog(DialogPrompt.NeedsClose, Strings.Z158);

        static readonly Dialog ManDialog8 = new Dialog(DialogPrompt.NeedsClose, Strings.Z159);

        static readonly Dialog gameDialog = new Dialog(DialogPrompt.YesNo, Strings.Z490);

        static readonly Dialog yesDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z491, Strings.Z492, Strings.Z493);

        static readonly Dialog readyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z494);

        static readonly Dialog noDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z495, Strings.Z496);

        static readonly Dialog winDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z499);

        static readonly Dialog loseDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z500);

        static readonly Dialog goodRewardDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA522);

        static readonly Dialog rewardDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA523);
        

        #endregion

        #region Interact

        public override void Interact()
        {
            Party.Singleton.Leader.FaceDirection(Direction.North);

            // Will's side quest
            if (Party.Singleton.CurrentMap.Name.Equals(Maps.mageTownNight))
            {
                if (!Party.Singleton.PartyAchievements.Contains(gamePlayedAchievement))
                {
                    dialog = gameDialog;
                    dialog.DialogCompleteEvent += Response;
                }
                else if (Party.Singleton.PartyAchievements.Contains(BoxGame3Controller.willFoundTreasureAchievement))
                {
                    dialog = winDialog;
                }
                else
                {
                    dialog = loseDialog;
                }
            }
            else if (Party.Singleton.PartyAchievements.Contains(GoldResponseAchievement))
            {
                dialog = ManDialog5;
            }
            else if (Party.Singleton.PartyAchievements.Contains(EmptyResponseAchievement))
            {
                dialog = ManDialog6;
            }
            else if (Party.Singleton.PartyAchievements.Contains(GhostResponseAchievement))
            {
                dialog = ManDialog7;
            }
            else if (Party.Singleton.PartyAchievements.Contains(foundGoldAchievement))
            {
                dialog = ManDialog2;
                Party.Singleton.AddAchievement(GoldResponseAchievement);

                dialog.DialogCompleteEvent += GoodReward;
            }
            else if (Party.Singleton.PartyAchievements.Contains(foundEmptyChestAchievement))
            {
                dialog = ManDialog3;
                Party.Singleton.AddAchievement(EmptyResponseAchievement);
            }
            else if (Party.Singleton.PartyAchievements.Contains(foundGhostAchievement))
            {
                dialog = ManDialog4;
                Party.Singleton.AddAchievement(GhostResponseAchievement);

                dialog.DialogCompleteEvent += Reward;
            }
            else if (Party.Singleton.PartyAchievements.Contains(BoxTalkAchievement))
            {
                dialog = ManDialog8;
            }
            else
            {
                Party.Singleton.AddAchievement(BoxTalkAchievement);

                ScreenManager.Singleton.AddScreen(
                    (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "BoxGameCutSceneScreen"));

                return;
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "BoxGuy"));            
        }

        #endregion

        #region Callbacks
        
        void Response(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= Response;

            if (e.Response == PartyResponse.Yes)
            {
                dialog = yesDialog;
                dialog.DialogCompleteEvent += Ready;
            }
            else
            {
                dialog = noDialog;
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "BoxGuy")); 
        }

        void Ready(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= Ready;

            dialog = readyDialog;
            dialog.DialogCompleteEvent += StartGame;
            Party.Singleton.AddAchievement(gamePlayedAchievement);
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "BoxGuy")); 
        }

        void StartGame(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= StartGame;

            ScreenManager.Singleton.AddScreen(
                    (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "WillBoxGameCutSceneScreen"));
        }

        void GoodReward(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= GoodReward;

            dialog = goodRewardDialog;
            Party.Singleton.GameState.Inventory.AddItem(typeof(TranslucentRing), 1);
            Party.Singleton.GameState.Gold += reward;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
        }

        void Reward(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= Reward;

            dialog = rewardDialog;
            Party.Singleton.GameState.Gold += reward;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
        }
        
        #endregion
    }
}
