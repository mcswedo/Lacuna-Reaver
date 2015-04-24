using System.Collections.Generic;
namespace MyQuest
{
    public class SewnBogMap : QuestItem
    {
        public SewnBogMap()
        {
            DisplayName = Strings.ZA306;
            MapName = "sewnBog_1";
            SubMapNames = new List<string>()
                {
                    "sewnBog_2",  
                    "sewnBog_3",  
                    "sewnBog_4",  
                    "sewnBog_5",
                    "sewnBog_6",
                    "sewnBog_7",
                    "sewn_bog_boss"
                };
            Description = Strings.ZA307;
        }
    }
}
