using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace MyQuest
{
    public class JanniesController : NPCMapCharacterController
    {
        #region Fields

        Dialog dialog;

        #endregion

        #region Achievements

        internal const string spokeToJannieAchievement = "spokeToJannie";
        internal const string movedJannieAchievement = "jannieMoved";
        internal const string tippersAchievement1 = "tippers1";
        internal const string tippersAchievement2 = "tippers2";
        internal const string tippersAchievement3 = "tippers3";
        internal const string tippersDefeatAchievement = "defeatedTippers";
        internal const string jannieCompleteAchievement = "jannieComplete";

        #endregion

        #region Dialogs

        static readonly Dialog teddyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z192, Strings.Z193, Strings.Z194);

        static readonly Dialog seenTippersDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z195);

        static readonly Dialog reunitedDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z196);

        static readonly Dialog goAwayDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z197);

        static readonly Dialog friendsDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z198);

        static readonly Dialog threatDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z199, Strings.Z200);

        static readonly Dialog fightDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z201, Strings.Z202);

        static readonly Dialog sadDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z203);

        static readonly Dialog happyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z204, Strings.Z205);

        static readonly Dialog willDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z510);

        static readonly Dialog strangeDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA270);

        #endregion

        public void CompleteAchievement()
        {
            Party.Singleton.AddAchievement(spokeToJannieAchievement);
        }
        public override void Interact()
        {
            IList<string> achievements = Party.Singleton.PartyAchievements;

            if (Party.Singleton.PartyAchievements.Contains(jannieCompleteAchievement))
            {
                dialog = happyDialog;
            }

            else if (Party.Singleton.PartyAchievements.Contains(tippersDefeatAchievement))
            {
                dialog = sadDialog;
                dialog.DialogCompleteEvent += SadJannie;
            }

            else if (Party.Singleton.PartyAchievements.Contains(tippersAchievement3))
            {
                dialog = fightDialog;
                dialog.DialogCompleteEvent += TippersFight;
            }

            else if(Party.Singleton.PartyAchievements.Contains(tippersAchievement2))
            {
                dialog = threatDialog;
                Party.Singleton.AddAchievement(tippersAchievement3);
            }

            else if (Party.Singleton.PartyAchievements.Contains(tippersAchievement1))
            {
                dialog = friendsDialog;
                Party.Singleton.AddAchievement(tippersAchievement2);
            }

            else if (Party.Singleton.PartyAchievements.Contains(movedJannieAchievement))
            {
                dialog = goAwayDialog;
                dialog.DialogCompleteEvent += WillReact;
            }

            if (!Party.Singleton.PartyAchievements.Contains(movedJannieAchievement))
            {
                if (Party.Singleton.CurrentMap.Name.Equals(Maps.mageTownNight))
                {
                    dialog = willDialog;
                    dialog.DialogCompleteEvent += StrangeGirl;
                }

                else if (Party.Singleton.PartyAchievements.Contains(TippersController.tippersFoundAchievement))
                {
                    Party.Singleton.GameState.Inventory.RemoveItem(typeof(Tippers), 1);

                    dialog = reunitedDialog;
                    dialog.DialogCompleteEvent += MoveJannie;
                }

                else if (Party.Singleton.PartyAchievements.Contains(spokeToJannieAchievement))
                {
                    dialog = seenTippersDialog;
                }

                else
                {
                    dialog = teddyDialog;
                    Party.Singleton.AddLogEntry("Celindar", "Jannie", Strings.Z192, Strings.Z193, Strings.Z194);
                    Party.Singleton.AddAchievement(spokeToJannieAchievement);
                }

                ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Jannie"));
            }
            else
            {
                if (Party.Singleton.CurrentMap.Name.Equals(Maps.mageTownNight))
                {
                    dialog = willDialog;
                    dialog.DialogCompleteEvent += StrangeGirl;
                }

                ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Jannie"));
            }
        }

        void MoveJannie(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= MoveJannie;

            Party.Singleton.AddAchievement(movedJannieAchievement);

            ScreenManager.Singleton.AddScreen(
                (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "MoveJannieCutSceneScreen"));
        }

        void WillReact(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= WillReact;

            Party.Singleton.AddAchievement(tippersAchievement1);

            ScreenManager.Singleton.AddScreen(
           (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "WillReactCutSceneScreen"));
        }

        CombatScreen combatScreen;
        void TippersFight(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= TippersFight;

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Jannie_Tippers",
                Point.Zero,
                ModAction.Remove,
                true);

            Monster[] tippers = new Monster[] { new Monster(Monster.tippers, 1, 1) };
            CombatZone zone = new CombatZone(
                "TippersFight",
                1,
                CombatZonePool.libraryBG,
                AudioCues.minibossCue,
                false,
                CombatZonePool.oneMediumLayoutCollection,
                tippers);

            combatScreen = new CombatScreen(zone);
            combatScreen.ExitScreenEvent += SadJannie;

            ScreenManager.Singleton.AddScreen(combatScreen);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Jannie_RippedTippers",
                new Point(2,1),
                ModAction.Add,
                true);

            Party.Singleton.AddAchievement(tippersDefeatAchievement);
        }

        void SadJannie(object sender, EventArgs e)
        {
            combatScreen.ExitScreenEvent -= SadJannie;

            ScreenManager.Singleton.AddScreen(
           (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "PartyReactCutSceneScreen"));

            Party.Singleton.AddAchievement(jannieCompleteAchievement);
        }

        void StrangeGirl(object sender, PartyResponseEventArgs e)
        {
             dialog.DialogCompleteEvent -= StrangeGirl;

            dialog = strangeDialog;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopRight, "Will"));
        }
    }
}
