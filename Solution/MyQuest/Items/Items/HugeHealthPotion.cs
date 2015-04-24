
namespace MyQuest
{
    public class HugeHealthPotion : ConsumableItem
    {
        public HugeHealthPotion()
        {
            DisplayName = Strings.ZA606;
            Description = Strings.ZA607;
            DropChance = 0.15f;
        }

        public override void Consume(FighterStats stats)
        {
            stats.AddHealth(8000);
            SoundSystem.Play(AudioCues.ConsumeItem);
        }
    }
}
