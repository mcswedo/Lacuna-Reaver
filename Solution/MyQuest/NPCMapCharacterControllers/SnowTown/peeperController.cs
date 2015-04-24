using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class PeeperController : NPCMapCharacterController
    {
        Dialog dialog;
        const string peeperMoved = "PeeperMovedToCaveLabyrinth";
        internal const string stubSpawned = "stubSpawned";

        static readonly Dialog peeperDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z269, Strings.Z270, Strings.Z271);

        static readonly Dialog peeperDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z272, Strings.Z273, Strings.Z274);

        static readonly Dialog peeperDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z275, Strings.Z276);

        public override void Interact()
        {
            if (Party.Singleton.PartyAchievements.Contains(peeperMoved))
            {
                dialog = peeperDialog3;
                if(!Party.Singleton.PartyAchievements.Contains(stubSpawned))
                {
                    Party.Singleton.AddAchievement(stubSpawned);

                    Party.Singleton.ModifyNPC(
                        Maps.caveLabyrinth,
                        "Stub",
                        new Point(21, 2),
                        ModAction.Add,
                        true);


                    NPCMapCharacter stub = Party.Singleton.CurrentMap.GetNPC("Stub");
                    Party.Singleton.ModifyMapLayer(Maps.caveLabyrinth, Layer.Collision, new Point(21, 1), 1, true);
                    stub.IdleOnly = true;
                    stub.FaceDirection(Direction.East);
                    //Party.Singleton.ModifyMapLayer(Maps.caveLabyrinth, Layer.Fringe, new Point(13, 2), 1, true);
                }

                Party.Singleton.AddLogEntry("Cave Labyrinth", "Traveler", Strings.Z275, Strings.Z276); 
            }
            else if (Party.Singleton.PartyAchievements.Contains(PoorGuysController.maxGoldGivenAchievement))
            {
                dialog = peeperDialog2;
                Party.Singleton.AddAchievement(peeperMoved);
                ScreenManager.Singleton.AddScreen(
                (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "MovePeeperCutSceneScreen"));
            }
            else
            {
                dialog = peeperDialog1;
            }
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Peeper"));

        }
    }
}
