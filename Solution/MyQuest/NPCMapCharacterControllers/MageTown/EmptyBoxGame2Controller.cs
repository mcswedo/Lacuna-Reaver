using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class EmptyBoxGame2Controller : NPCMapCharacterController
    {
        Dialog EmptyTwoDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z171);  //Declare the dialog variable

        public override void Interact()  //The interact method will activate when the player presses the A button(or the space bar) on the npc.
        {





            ScreenManager.Singleton.AddScreen(new DialogScreen(EmptyTwoDialog, DialogScreen.Location.TopLeft));  //Brings the Dialog up, Location at the topleft.
        }
    }
}

//  Each closed chest needs it's own xml. To make an xml file, go to MyQuestGameContent, characters, and then NPCMapCharacters. You will see a list of NPCMapCharacters
//that are used in the game. Find Keepf2Chest.xml and become familiar with how xml sheets work. You'll see the <name></name> trait, this is the string the code will
//expect when ModifyNPC asks for the asset name. At the bottom of the xml sheet, you'll see that the xml sheet has a <ControllerName></ControllerName>. This name will be different
//for every chest. Every chest will have idleonly set to true. 

//The easiest way to make a chest is to copy and paste the Keepf2Chest and change anything that will cause the new chest to conflict with Keepf2Chest.