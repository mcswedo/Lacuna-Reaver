using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MyQuest
{
    public class CutSceneEntry
    {
        string assetName;

        public string AssetName
        {
            get { return assetName; }
            set { assetName = value; }
        }


        Point triggerPosition;

        public Point TriggerPosition
        {
            get { return triggerPosition; }
            set { triggerPosition = value; }
        }
    }
}
