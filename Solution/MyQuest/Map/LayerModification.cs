using Microsoft.Xna.Framework;

/// Prototype in use

namespace MyQuest
{
    public struct LayerModification
    {
        public Layer TargetLayer;
        public Point Location;
        public float NewValue;

        public LayerModification(Layer target, Point location, float newValue)
        {
            TargetLayer = target;
            Location = location;
            NewValue = newValue;
        }
    }

    public struct NPCModification
    {
        public ModAction Action;
        public NPCEntry Entry;

        public NPCModification(ModAction action, NPCEntry entry)
        {
            this.Action = action;
            this.Entry = entry;
        }
    }
}
