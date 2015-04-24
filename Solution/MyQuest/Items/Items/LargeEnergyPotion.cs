
namespace MyQuest
{
    public class LargeEnergyPotion : ConsumableItem
    {
        public LargeEnergyPotion()
        {
            DisplayName = Strings.ZA071;
            Description = Strings.ZA072;
            DropChance = 0.15f;
        }

        public override void Consume(FighterStats stats)
        {
            stats.AddEnergy(500);
            SoundSystem.Play(AudioCues.ConsumeItem);
        }
    }
}
