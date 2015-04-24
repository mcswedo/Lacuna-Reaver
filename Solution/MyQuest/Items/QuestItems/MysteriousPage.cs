
namespace MyQuest
{
    public class MysteriousPage: QuestItem
    {
        public MysteriousPage()
        {
            DisplayName = Strings.ZA600;
            Description = Strings.ZA233; //This Item isn't viewable in the inventory. No description is needed.
            DropChance = 1f;
        }
    }
}
