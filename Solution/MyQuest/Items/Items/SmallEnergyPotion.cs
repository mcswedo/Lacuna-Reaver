
namespace MyQuest
{
    public class SmallEnergyPotion : ConsumableItem
    {
        public SmallEnergyPotion()
        {
            DisplayName = Strings.ZA067;
            Description = Strings.ZA068;
            DropChance = 0.06f;
        }

        public override void Consume(FighterStats stats)
        {
            stats.AddEnergy(100);
            SoundSystem.Play(AudioCues.ConsumeItem);
        }
    }
}
