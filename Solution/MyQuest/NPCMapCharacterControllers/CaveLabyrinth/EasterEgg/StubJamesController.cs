using System.Collections.Generic;

namespace MyQuest
{
    public class StubJamesController : NPCMapCharacterController
    {
        #region Achivements

        internal const string spokeToFirstStub = "SpokeToStub";

        #endregion

        #region Dialog

        static readonly Dialog talkedToFirstStub = new Dialog(DialogPrompt.NeedsClose, Strings.ZA704);

        static readonly Dialog jamesDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA716);

        #endregion

        public override void Interact()
        {
            Dialog dialog;
            IList<string> achievements = Party.Singleton.PartyAchievements;
            NPCMapCharacter stub = Party.Singleton.CurrentMap.GetNPC("StubJames");

            if (!Party.Singleton.PartyAchievements.Contains("SpokeToStub"))
            {
                dialog = talkedToFirstStub;
                Party.Singleton.AddAchievement(spokeToFirstStub);                
                ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Stub"));
                stub.IdleOnly = true;
            }
            else if (Party.Singleton.PartyAchievements.Contains("SpokeToStub") && !Party.Singleton.PartyAchievements.Contains(TalkedToAllStubsCutSceneScreen.playedAchievement))
            {
                stub.IdleOnly = true;

                bool playCutScene = true;
                foreach (NPCMapCharacter stubs in Party.Singleton.CurrentMap.NPC)
                {
                    if (!stubs.IdleOnly)
                    {
                        playCutScene = false;
                    }
                }
                if (playCutScene) //nobody is moving. Talked to all characters.
                {
                    ScreenManager.Singleton.AddScreen((CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "TalkedToAllStubsCutSceneScreen"));
                }
            }
            else
            {
                dialog = jamesDialog;
                ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Stub"));
            }
        }
    }
}
