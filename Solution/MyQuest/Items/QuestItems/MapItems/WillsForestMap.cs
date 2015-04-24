using System.Collections.Generic;
namespace MyQuest
{
    public class WillsForestMap : QuestItem
    {
        public WillsForestMap()
        {
            DisplayName = Strings.ZA312;
            MapName = "blind_mans_forest_1";
            SubMapNames = new List<string>()
                {
                    "blind_mans_forest_2",  
                    "blind_mans_forest_3",  
                    "blind_mans_forest_4",  
                    "blind_mans_forest_5",
                    "blind_mans_forest_boss"  
                };
            Description = Strings.ZA313;
        }
    }
}
