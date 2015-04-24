using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class RandomCave2Chest1Controller : NPCMapCharacterController
    {
        Dialog surpriseDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z133);

        public override void Interact()  //The interact method will activate when the player presses the A button(or the space bar) on the npc.
        {
            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,  //Select the map where the modification is applied. Current map in this case.
                "RandomCave2Chest1",  //The name of the npc being modified
                Point.Zero,  //Position where the modification is being done. Point.Zero is used when removing an npc.
                ModAction.Remove,  //Add or Remove the npc
                true);

            surpriseDialog.DialogCompleteEvent += MonsterChestBattle;

            ScreenManager.Singleton.AddScreen(new DialogScreen(surpriseDialog, DialogScreen.Location.TopLeft));           
        }

        void MonsterChestBattle(object sender, PartyResponseEventArgs e)
        {
            CombatZone zone = CombatZonePool.feesh5Zone;

            ScreenManager.Singleton.AddScreen(new CombatScreen(zone));

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "OpenChest1",
                new Point(9, 3),
                ModAction.Add,
                true);
        }
    }
}

//  Each closed chest needs it's own xml. To make an xml file, go to MyQuestGameContent, characters, and then NPCMapCharacters. You will see a list of NPCMapCharacters
//that are used in the game. Find Keepf2Chest.xml and become familiar with how xml sheets work. You'll see the <name></name> trait, this is the string the code will
//expect when ModifyNPC asks for the asset name. At the bottom of the xml sheet, you'll see that the xml sheet has a <ControllerName></ControllerName>. This name will be different
//for every chest. Every chest will have idleonly set to true. 

//The easiest way to make a chest is to copy and paste the Keepf2Chest and change anything that will cause the new chest to conflict with Keepf2Chest.