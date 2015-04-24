﻿using System.Collections.Generic;

namespace MyQuest
{
    public class HealersInnClerksController : NPCMapCharacterController
    {
        #region Fields

        Dialog dialog;
        const int charge = 25;
        bool hasSaved = false;

        #endregion

        #region Achievement

        internal const string myAchievement = "nextDayInHealersVillage";

        #endregion

        #region Dialogs

        static readonly Dialog likeToSaveDialog = new Dialog(DialogPrompt.YesNo, Strings.Z002);

        static readonly Dialog likeToRestDialog = new Dialog(DialogPrompt.YesNo,
            Strings.Z003 + " " + charge + " " + Strings.Z004);

        static readonly Dialog yesToRestDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z005);

        static readonly Dialog noToRestDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z006);

        static readonly Dialog yesToSaveDialog = new Dialog(DialogPrompt.YesNo,
            Strings.Z007 + " " + charge + " " + Strings.Z004);

        static readonly Dialog noMoneyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z008);


        #endregion

        #region Interact

        public override void Interact()
        {
            dialog = likeToSaveDialog;
            dialog.DialogCompleteEvent += AskedInnClerkToSave;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "HealersInn"));
        }

        #endregion

        #region Callbacks

        public void CompleteNextDayAchievement()
        {
            Party.Singleton.AddAchievement(myAchievement);
        }

        void AskedInnClerkToSave(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= AskedInnClerkToSave;

            if (e.Response == PartyResponse.Yes)
            {
                dialog = yesToSaveDialog;
                dialog.DialogCompleteEvent += AskedInnClerkToRest;
                hasSaved = true;
                Party.Singleton.SaveGameState(Party.saveFileName); //Save now in case somebody turns the game off before conversation is complete.             
            }
            else
            {
                dialog = likeToRestDialog;
                dialog.DialogCompleteEvent += AskedInnClerkToRest;
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "HealersInn"));
        }

        void AskedInnClerkToRest(object sender, PartyResponseEventArgs e)
        {
            IList<string> achievements = Party.Singleton.PartyAchievements;
            dialog.DialogCompleteEvent -= AskedInnClerkToRest;

            if (e.Response == PartyResponse.Yes)
            {
                if (Party.Singleton.GameState.Gold < charge)
                {
                    dialog = noMoneyDialog;
                }
                else
                {
                    if(Party.Singleton.PartyAchievements.Contains(HealersBlacksmithsController.turnInSwordHiltAchievement))
                    {
                        CompleteNextDayAchievement();
                    }
                    Party.Singleton.GameState.Gold -= charge;
                    foreach (PCFightingCharacter character in Party.Singleton.GameState.Fighters)
                    {
                        character.FighterStats.Health = character.FighterStats.ModifiedMaxHealth;
                        character.FighterStats.Energy = character.FighterStats.ModifiedMaxEnergy;
                    }
                    dialog = yesToRestDialog;
                    dialog.DialogCompleteEvent += Resting;
                }
            }
            else
            {
                dialog = noToRestDialog;
            }

            if (hasSaved)
            {
                hasSaved = false;
                Party.Singleton.SaveGameState(Party.saveFileName); //Save to have the party at full health in case they turn the game off too soon.
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "HealersInn"));
        }

        void Resting(object sender, PartyResponseEventArgs e)
        {
            yesToRestDialog.DialogCompleteEvent -= Resting;

            CutSceneScreen screen = Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "WakeUpCutSceneScreen");
            ScreenManager.Singleton.AddScreen(screen);

            MusicSystem.Stop();         
            MusicSystem.Play(AudioCues.sleep);            
        }

        #endregion
    }
}
