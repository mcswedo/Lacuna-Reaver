using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class BoxGame3Controller : NPCMapCharacterController
    {

        internal const string willFoundTreasureAchievement = "willFoundTreasure";
        internal const string foundGoldAchievement = "foundgold";

        Dialog threeDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z147);  //Declare the dialog variable
        Dialog willDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z498);

        public override void Interact()  //The interact method will activate when the player presses the A button(or the space bar) on the npc.
        {
            Party.Singleton.GameState.Inventory.AddItem(typeof(SmallHealthPotion), 1);  //Adds the Small Health Potion item to the inventory

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,  //Select the map where the modification is applied. Current map in this case.
                "BoxGame3",  //The name of the npc being modified
                Point.Zero,  //Position where the modification is being done. Point.Zero is used when removing an npc.
                ModAction.Remove,  //Add or Remove the npc
                true);  //Is the change being done permanent? If it is set, this to true. If it's false, the modification will revert to it's original state when the player leaves the map.

            Party.Singleton.ModifyNPC(
               Party.Singleton.CurrentMap.Name,  //Select the map where the modification is applied. Current map in this case.
               "BoxGame1",  //The name of the npc being modified
               Point.Zero,  //Position where the modification is being done. Point.Zero is used when removing an npc.
               ModAction.Remove,  //Add or Remove the npc
               true);  //Is the change being done permanent? If it is set, this to true. If it's false, the modification will revert to it's original state when the player leaves the map.

            Party.Singleton.ModifyNPC(
              Party.Singleton.CurrentMap.Name,  //Select the map where the modification is applied. Current map in this case.
              "BoxGame2",  //The name of the npc being modified
              Point.Zero,  //Position where the modification is being done. Point.Zero is used when removing an npc.
              ModAction.Remove,  //Add or Remove the npc
              true);  //Is the change being done permanent? If it is set, this to true. If it's false, the modification will revert to it's original state when the player leaves the map.


            Party.Singleton.ModifyNPC(
                 Party.Singleton.CurrentMap.Name,  //Select the map where the modification is applied. Current map in this case.
                 "OpenChest1",  //The name of the npc being modified
                 new Point(3, 26),  //Position where the modification is being done. Point.Zero is used when removing an npc.
                 ModAction.Add,  //Add or Remove the npc
                 true);
            Party.Singleton.ModifyNPC(
               Party.Singleton.CurrentMap.Name,
               "OpenChest2",
               new Point(7, 26),
               ModAction.Add,
               true);

            Party.Singleton.ModifyNPC(
               Party.Singleton.CurrentMap.Name,  //Select the map where the modification is applied. Current map in this case.
               "OpenChest3",  //The name of the npc being modified
               new Point(5, 26),  //Position where the modification is being done. Point.Zero is used when removing an npc.
               ModAction.Add,  //Add or Remove the npc
               true);

            if (Party.Singleton.CurrentMap.Name.Equals(Maps.mageTownNight))
            {
                Party.Singleton.GameState.Inventory.AddItem(typeof (ShadowStrikeRing), 1);
                Party.Singleton.PartyAchievements.Add(willFoundTreasureAchievement);
                ScreenManager.Singleton.AddScreen(new DialogScreen(willDialog, DialogScreen.Location.TopLeft));
            }
            else
            {
                Party.Singleton.PartyAchievements.Add(foundGoldAchievement);
                ScreenManager.Singleton.AddScreen(new DialogScreen(threeDialog, DialogScreen.Location.TopLeft));  //Brings the Dialog up, Location at the topleft.
            }

            MusicSystem.InterruptMusic(AudioCues.ChestOpen);  //adds the ChestOpen audio when you interact             
        }
    }
}

//  Each closed chest needs it's own xml. To make an xml file, go to MyQuestGameContent, characters, and then NPCMapCharacters. You will see a list of NPCMapCharacters
//that are used in the game. Find Keepf2Chest.xml and become familiar with how xml sheets work. You'll see the <name></name> trait, this is the string the code will
//expect when ModifyNPC asks for the asset name. At the bottom of the xml sheet, you'll see that the xml sheet has a <ControllerName></ControllerName>. This name will be different
//for every chest. Every chest will have idleonly set to true. 

//The easiest way to make a chest is to copy and paste the Keepf2Chest and change anything that will cause the new chest to conflict with Keepf2Chest.