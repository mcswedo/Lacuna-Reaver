﻿using System.Collections.Generic;
using System.Diagnostics;

namespace MyQuest
{
    public class WillsInnClerksController : NPCMapCharacterController
    {
        #region Fields

        Dialog dialog;
        const int charge = 50;
        bool hasSaved = false;

        #endregion

        #region Achievement

        const string firstNightAchievement = "FirstTimeSleepingAtInn";
        internal const string scytheRepareAchievement = "reparingScythe";
        internal const string myAchievement = "nextDayInWillsVillage";
        internal const string brokenScytheAchievement = "brokenScythe";

        #endregion

        #region Dialogs

        static readonly Dialog likeToSaveDialog = new Dialog(DialogPrompt.YesNo, Strings.Z002);

        static readonly Dialog likeToRestDialog = new Dialog(DialogPrompt.YesNo, Strings.Z003 + " " + charge + " " + Strings.Z004);

        static readonly Dialog yesToRestDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z005);

        static readonly Dialog noToRestDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z006);

        static readonly Dialog yesToSaveDialog = new Dialog(DialogPrompt.YesNo,
            Strings.Z007 + " " + charge + " " + Strings.Z004);

        static readonly Dialog noMoneyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z008);

        static readonly Dialog roomsOccupiedDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z001);


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

        void AskedInnClerkToSave(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= AskedInnClerkToSave;

            if (e.Response == PartyResponse.Yes)
            {
                dialog = yesToSaveDialog;
                dialog.DialogCompleteEvent += AskedInnClerkToRest;
                if (Party.Singleton.PartyAchievements.Contains(firstNightAchievement))
                {
                    hasSaved = true;
                    Party.Singleton.SaveGameState(Party.saveFileName);
                }
                else
                {
                    Party.Singleton.SaveGameState(Party.saveFileName);
                }
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
                    if (achievements.Contains(scytheRepareAchievement))
                    {
                        Party.Singleton.AddAchievement(myAchievement);
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
                    Debug.Assert(Party.Singleton.PartyAchievements.Contains(firstNightAchievement));
                    hasSaved = false;
                    Party.Singleton.SaveGameState(Party.saveFileName);
                }
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "HealersInn"));
        }

        void Resting(object sender, PartyResponseEventArgs e)
        {
            yesToRestDialog.DialogCompleteEvent -= Resting;
            if (Party.Singleton.PartyAchievements.Contains(firstNightAchievement))
            {
                CutSceneScreen screen = Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "WakeUpWillsInnCutSceneScreen");
                ScreenManager.Singleton.AddScreen(screen);
            }

            else
            {
                CutSceneScreen screen = Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "SwitchPartyLeaderCutSceneScreen");
                ScreenManager.Singleton.AddScreen(screen);
                Party.Singleton.AddAchievement(firstNightAchievement);
            }

            MusicSystem.Stop();         
            MusicSystem.Play(AudioCues.sleep);
        }

        #endregion
    }
}
