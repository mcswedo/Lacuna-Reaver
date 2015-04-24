using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace MyQuest
{
    public class AstralClerksController : NPCMapCharacterController
    {
        #region Fields

        Dialog dialog;
        //const int charge = 50;

        #endregion

        #region Achievement

        internal const string spokenToAstralClerk = "spokenToAstralClerk";

        #endregion

        #region Dialogs

        static readonly Dialog helloDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA665, Strings.ZA666, Strings.ZA667);

        static readonly Dialog healDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA668);

        static readonly Dialog healConfirmationDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA530, Strings.ZA531);
        
        static readonly Dialog likeToSaveDialog = new Dialog(DialogPrompt.YesNo, Strings.Z002);

        //static readonly Dialog likeToRestDialog = new Dialog(DialogPrompt.YesNo, Strings.Z003 + " " + charge + " " + Strings.Z004);

        //static readonly Dialog yesToRestDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z005);

        //static readonly Dialog noToRestDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z006);

        static readonly Dialog yesToSaveDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA529);

        //static readonly Dialog noMoneyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z008);

        #endregion

        #region Interact

        public override void Interact()
        {
            if (Party.Singleton.PartyAchievements.Contains(spokenToAstralClerk))
            {
                dialog = healDialog;
                dialog.DialogCompleteEvent += HealParty;
            }
            else
            {
                dialog = helloDialog;
                dialog.DialogCompleteEvent += SpokenToAstralClerk;
                Party.Singleton.AddAchievement(spokenToAstralClerk);
            }
            
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "AstralClerk"));
        }

        #endregion

        #region Callbacks

        void SpokenToAstralClerk(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= SpokenToAstralClerk;
            dialog = healDialog;
            dialog.DialogCompleteEvent += HealParty;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "AstralClerk"));
        }

        void HealParty(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= HealParty;
            dialog = healConfirmationDialog;
            dialog.DialogCompleteEvent += AfterHealedCallback;
            foreach (PCFightingCharacter character in Party.Singleton.GameState.Fighters)
            {
                character.FighterStats.Health = character.FighterStats.ModifiedMaxHealth;
                character.FighterStats.Energy = character.FighterStats.ModifiedMaxEnergy;
            }
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
        }

        void AfterHealedCallback(object sender, EventArgs e)
        {            
            dialog.DialogCompleteEvent -= AfterHealedCallback;
            dialog = likeToSaveDialog;
            dialog.DialogCompleteEvent += AfterSaveCallback;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "AstralClerk"));
        }

        void AfterSaveCallback(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= AfterSaveCallback;
            if (e.Response == PartyResponse.Yes)
            {
                dialog = yesToSaveDialog;
                Party.Singleton.SaveGameState(Party.saveFileName);
                ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "AstralClerk"));
            }
            else
            {
            }
        }




        //void AskedInnClerkToSave(object sender, PartyResponseEventArgs e)
        //{
        //    dialog.DialogCompleteEvent -= AskedInnClerkToSave;

        //    if (e.Response == PartyResponse.Yes)
        //    {
        //        dialog = yesToSaveDialog;
        //        dialog.DialogCompleteEvent += AskedInnClerkToRest;

        //        hasSaved = true;
        //        Party.Singleton.SaveGameState(Party.saveFileName);
        //    }
        //    else
        //    {
        //        dialog = likeToRestDialog;
        //        dialog.DialogCompleteEvent += AskedInnClerkToRest;
        //    }

        //    ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "HealersInn"));
        //}

        //void AskedInnClerkToRest(object sender, PartyResponseEventArgs e)
        //{
        //    IList<string> achievements = Party.Singleton.PartyAchievements;
        //    dialog.DialogCompleteEvent -= AskedInnClerkToRest;

        //    if (e.Response == PartyResponse.Yes)
        //    {

        //        if (Party.Singleton.GameState.Gold < charge)
        //        {
        //            dialog = noMoneyDialog;
        //        }
        //        else
        //        {
        //            Party.Singleton.GameState.Gold -= charge;
        //            foreach (PCFightingCharacter character in Party.Singleton.GameState.Fighters)
        //            {
        //                character.FighterStats.Health = character.FighterStats.ModifiedMaxHealth;
        //                character.FighterStats.Energy = character.FighterStats.ModifiedMaxEnergy;
        //            }
        //            dialog = yesToRestDialog;
        //            dialog.DialogCompleteEvent += Resting;
        //        }
        //    }
        //    else
        //    {
        //        dialog = noToRestDialog;
        //    }

        //    if (hasSaved)
        //    {
        //        Debug.Assert(Party.Singleton.PartyAchievements.Contains(firstNightAchievement));
        //        hasSaved = false;
        //        Party.Singleton.SaveGameState(Party.saveFileName);
        //    }

        //    ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "HealersInn"));
        //}

        //void Resting(object sender, PartyResponseEventArgs e)
        //{
        //    yesToRestDialog.DialogCompleteEvent -= Resting;

        //     if (Party.Singleton.PartyAchievements.Contains(firstNightAchievement))
        //    {
        //        CutSceneScreen screen = Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "WakeUpMagesInnCutSceneScreen");
        //        ScreenManager.Singleton.AddScreen(screen);
        //    }
        //    else
        //    {
        //        CutSceneScreen screen = Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "WillPartyLeaderCutSceneScreen");
        //        ScreenManager.Singleton.AddScreen(screen);
        //        Party.Singleton.AddAchievement(firstNightAchievement);
        //    }

        //    MusicSystem.Stop();         
        //    MusicSystem.Play(AudioCues.sleep);
        //}

        #endregion
    }
}
