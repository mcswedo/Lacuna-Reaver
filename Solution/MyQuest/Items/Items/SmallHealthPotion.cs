
namespace MyQuest
{
    public class SmallHealthPotion : ConsumableItem
    {
        public SmallHealthPotion()
        {
            DisplayName = Strings.ZA061;
            Description = Strings.ZA062;
            DropChance = 0.06f;
        }

        public override void Consume(FighterStats stats)
        {
            stats.AddHealth(200);
            SoundSystem.Play(AudioCues.ConsumeItem);
        }
    }
}
