
namespace MyQuest
{
    public class MediumHealthPotion : ConsumableItem
    {
        public MediumHealthPotion()
        {
            DisplayName = Strings.ZA063;
            Description = Strings.ZA064;
        }

        public override void Consume(FighterStats stats)
        {
            stats.AddHealth(450);
            SoundSystem.Play(AudioCues.ConsumeItem);
        }
    }
}
