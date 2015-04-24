using System.Collections.Generic;
namespace MyQuest
{
    public class ForbiddenCavernMap : QuestItem
    {
        public ForbiddenCavernMap()
        {
            DisplayName = Strings.ZA702;
            MapName = "forbidden_cavern_high";
            SubMapNames = new List<string>()
                {
                    "forbidden_cavern_low",  
                    "forbidden_cavern_rooma",  
                    "forbidden_cavern_roomb_ground",
                    "forbidden_cavern_roomb_ledge", 
                    "forbidden_cavern_roomc",
                    "forbidden_cavern_roomd",
                    "forbidden_cavern_roome",
                    "forbidden_cavern_roomf"
                };
            Description = Strings.ZA703;
        }
    }
}
