
namespace MyQuest
{
    public class LargeHealthPotion : ConsumableItem
    {
        public LargeHealthPotion()
        {
            DisplayName = Strings.ZA065;
            Description = Strings.ZA066;
            DropChance = 0.15f;
        }

        public override void Consume(FighterStats stats)
        {
            stats.AddHealth(2000);
            SoundSystem.Play(AudioCues.ConsumeItem);
        }
    }
}
