using System.Collections.Generic;

namespace MyQuest
{
    public class RefGuard2Controller : NPCMapCharacterController
    {
        #region Achievements

        internal const string firstTalkAchievement = "firstTalk";
        internal const string greenCompleteAchievement = "greenComplete";
        internal const string redCompleteAchievement = "redComplete";
        internal const string blueCompleteAchievement = "blueComplete";
        internal const string questDoneAchievement = "questDone";

        #endregion

        #region Dialog

        Dialog dialog;

        readonly Dialog firstDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA516, Strings.ZA517, Strings.ZA544, Strings.ZA545);

        readonly Dialog greenFirstCompleteDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA559);

        readonly Dialog greenCompleteDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA556);

        readonly Dialog redFirstCompleteDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA560);

        readonly Dialog redCompleteDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA557);

        readonly Dialog blueFirstCompleteDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA561);

        readonly Dialog blueCompleteDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA558);

        readonly Dialog questDoneDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA562);

        #endregion

        public override void Interact()
        {
            int greenCount = 10 - Party.Singleton.GameState.Inventory.ItemCount(typeof(GreenMark));

            if (Party.Singleton.PartyAchievements.Contains(greenCompleteAchievement) && Party.Singleton.PartyAchievements.Contains(firstTalkAchievement))
            {
                dialog = greenCompleteDialog;
                
                dialog.DialogCompleteEvent += RedMarks;
            }

            else if (Party.Singleton.PartyAchievements.Contains(firstTalkAchievement) && greenCount == 0)
            {
                Party.Singleton.AddAchievement(greenCompleteAchievement);
                Party.Singleton.GameState.Inventory.RemoveItem(typeof(GreenMark), 10);
                Party.Singleton.GameState.Inventory.AddItem(typeof(BattleLordsRing), 1);

                dialog = greenFirstCompleteDialog;

                if (Party.Singleton.PartyAchievements.Contains(greenCompleteAchievement) && Party.Singleton.PartyAchievements.Contains(redCompleteAchievement) && Party.Singleton.PartyAchievements.Contains(blueCompleteAchievement))
                {
                    dialog.DialogCompleteEvent += QuestDone;
                }

                else
                {
                    dialog.DialogCompleteEvent += RedMarks;
                }
            }

            else if (Party.Singleton.PartyAchievements.Contains(firstTalkAchievement))
            {
                Dialog greenDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA546 + greenCount.ToString());

                dialog = greenDialog;
                dialog.DialogCompleteEvent += RedMarks;
            }

            else
            {
                Party.Singleton.AddAchievement(firstTalkAchievement);

                dialog = firstDialog;
                Party.Singleton.AddLogEntry("Refugee Camp", "Camp Guard", dialog.Text);
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Guard"));
        }

        void RedMarks(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= RedMarks;

            int redCount = 10 - Party.Singleton.GameState.Inventory.ItemCount(typeof(RedMark));

            if (Party.Singleton.PartyAchievements.Contains(redCompleteAchievement))
            {
                dialog = redCompleteDialog;
                
                dialog.DialogCompleteEvent += BlueMarks;
            }

            else if (redCount == 0)
            {
                Party.Singleton.AddAchievement(redCompleteAchievement);
                Party.Singleton.GameState.Inventory.RemoveItem(typeof(RedMark), 10);
                Party.Singleton.GameState.Inventory.AddItem(typeof(BattleThiefRing), 1);

                dialog = redFirstCompleteDialog;

                if (Party.Singleton.PartyAchievements.Contains(greenCompleteAchievement) && Party.Singleton.PartyAchievements.Contains(redCompleteAchievement) && Party.Singleton.PartyAchievements.Contains(blueCompleteAchievement))
                {
                    dialog.DialogCompleteEvent += QuestDone;
                }

                else
                {
                    dialog.DialogCompleteEvent += BlueMarks;
                }
            }

            else
            {
                Dialog redDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA547 + redCount.ToString());

                dialog = redDialog;
                dialog.DialogCompleteEvent += BlueMarks;
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Guard"));
        }

        void BlueMarks(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= BlueMarks;

            int blueCount = 10 - Party.Singleton.GameState.Inventory.ItemCount(typeof(BlueMark));

            if (Party.Singleton.PartyAchievements.Contains(blueCompleteAchievement))
            {
                dialog = blueCompleteDialog;
            }

            else if (blueCount == 0)
            {
                Party.Singleton.AddAchievement(blueCompleteAchievement);
                Party.Singleton.GameState.Inventory.RemoveItem(typeof(BlueMark), 10);
                Party.Singleton.GameState.Inventory.AddItem(typeof(BattleMageRing), 1);

                dialog = blueFirstCompleteDialog;

                if (Party.Singleton.PartyAchievements.Contains(greenCompleteAchievement) && Party.Singleton.PartyAchievements.Contains(redCompleteAchievement) && Party.Singleton.PartyAchievements.Contains(blueCompleteAchievement))
                {
                    dialog.DialogCompleteEvent += QuestDone;
                }
            }

            else
            {
                Dialog blueDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA548 + blueCount.ToString());

                dialog = blueDialog;
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Guard"));
        }

        void QuestDone(object sender, PartyResponseEventArgs e)
        {
            Party.Singleton.AddAchievement(questDoneAchievement);

            dialog = questDoneDialog;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Guard"));
        }
    }
}
