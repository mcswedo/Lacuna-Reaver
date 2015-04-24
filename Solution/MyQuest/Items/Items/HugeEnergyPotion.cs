
namespace MyQuest
{
    public class HugeEnergyPotion : ConsumableItem
    {
        public HugeEnergyPotion()
        {
            DisplayName = Strings.ZA608;
            Description = Strings.ZA609;
            DropChance = 0.15f;
        }

        public override void Consume(FighterStats stats)
        {
            stats.AddEnergy(1000);
            SoundSystem.Play(AudioCues.ConsumeItem);
        }
    }
}
