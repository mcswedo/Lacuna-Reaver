using System.Windows.Forms;
using MyQuest;

namespace QuestWorldEditor
{
    internal class PortalListBox : ListBox
    {
        public PortalListBox()
            : base()
        {
        }

        // Overrides the parent class Sort to perform a simple
        // bubble sort on the length of the string contained in each item.
        protected override void Sort()
        {
            if (Items.Count > 1)
            {
                bool swapped;
                do
                {
                    int counter = Items.Count - 1;
                    swapped = false;

                    while (counter > 0)
                    {
                        Portal a = (Portal)Items[counter];
                        Portal b = (Portal)Items[counter - 1];

                        if(string.Compare(a.DestinationMap, b.DestinationMap) < 0)
                        {
                            // Swap the items.
                            object temp = Items[counter];
                            Items[counter] = Items[counter - 1];
                            Items[counter - 1] = temp;
                            swapped = true;
                        }
                        // Decrement the counter.
                        counter -= 1;
                    }
                }
                while ((swapped == true));
            }
        }
    }
}