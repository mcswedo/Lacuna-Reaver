using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class StolenItem1Controller : NPCMapCharacterController
    {
        #region Interact

        public override void Interact()
        {
            
            Party.Singleton.GameState.Inventory.AddItem(typeof(StolenItem), 1);

            SoundSystem.Play("ItemPickup"); //temporary soundeffect
        
            Party.Singleton.ModifyNPC(
                    Party.Singleton.CurrentMap.Name,
                    "StolenItem1",
                    Point.Zero,
                    ModAction.Remove,
                    true);
        }

        #endregion

    }
}
