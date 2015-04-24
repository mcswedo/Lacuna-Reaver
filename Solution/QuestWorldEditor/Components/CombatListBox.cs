using System.Windows.Forms;
using MyQuest;

namespace QuestWorldEditor
{
    internal class CombatListBox : ListBox
    {
        public CombatListBox()
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
                        CombatZone a = (CombatZone)Items[counter];
                        CombatZone b = (CombatZone)Items[counter - 1];

                        if (string.Compare(a.ZoneName, b.ZoneName) < 0)
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