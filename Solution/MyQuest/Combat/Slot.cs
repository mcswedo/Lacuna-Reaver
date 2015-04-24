using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public enum SlotSize
    {
        Medium, //Can fit 5 medium enemies
        Large, //Can fit 2 large enemies
        Huge, //Can fit 1 huge enemy
    }

    public class Slot
    {
        SlotSize size;

        public SlotSize Size
        {
            get { return size; }
            set { size = value; }
        }

        Vector2 center;

        public Vector2 Center
        {
            get { return center; }
            set { center = value; }
        }

        public Slot(SlotSize size, Vector2 center)
        {
            this.size = size;
            this.center = center;
        }
    }
}
