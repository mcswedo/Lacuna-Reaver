using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class CarasController : NPCMapCharacterController
    {
        static readonly Dialog beCarefullDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z014);

        static readonly Dialog braveDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z015);

        public override void Interact()
        {
            Dialog dialog;

            IList<string> achievements = Party.Singleton.PartyAchievements;

            dialog = (achievements.Contains(HealersBattleSceneA.savedVillageAchievement) ? braveDialog : beCarefullDialog);

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));
        }
    }
}
