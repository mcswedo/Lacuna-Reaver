using System.Collections.Generic;
namespace MyQuest
{
    public class PossessedLibraryMap : QuestItem
    {
        public PossessedLibraryMap()
        {
            DisplayName = Strings.ZA302;
            MapName = "possessed_library_1ground";
            SubMapNames = new List<string>()
                {
                    "possessed_library_1ledge",  
                    "possessed_library_2ground",  
                    "possessed_library_2ledge",
                    "possessed_library_3", 
                    "possessed_library_3ledge",
                    "possessed_library_4ground",
                    "possessed_library_4ledge",
                    "possessed_library_4secret1",
                    "possessed_library_4secret2",
                    "possessed_library_4secret3",
                    "possessed_library_5",
                    "possessed_library_5secret",
                    "possessed_library_boss"
                };
            Description = Strings.ZA303;
        }
    }
}
