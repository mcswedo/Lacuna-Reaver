
namespace MyQuest
{
    public class MediumEnergyPotion : ConsumableItem
    {
        public MediumEnergyPotion()
        {
            DisplayName = Strings.ZA069;
            Description = Strings.ZA070;
        }

        public override void Consume(FighterStats stats)
        {
            stats.AddEnergy(250);
            SoundSystem.Play(AudioCues.ConsumeItem);
        }
    }
}
