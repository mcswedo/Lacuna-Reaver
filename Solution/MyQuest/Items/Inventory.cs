using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace MyQuest
{
    #region Inventory Item


    public class InventoryItem
    {
        string itemName;

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        string mapName;


        public string MapName
        {
            get { return mapName; }
            set { mapName = value; }
        }

        int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        List<string> subMapNames;

        public List<string> SubMapNames
        {
            get { return subMapNames; }
            set { subMapNames = value; }
        }

        public InventoryItem()
        {
        }

        
        public InventoryItem(string itemName, int quantity, string mapName, List<string>subMapNames = null)
        {
            this.itemName = itemName;
            this.quantity = quantity;
            this.mapName = mapName;
            this.subMapNames = subMapNames; 
        }
    }


    #endregion

    public class Inventory
    {
        #region Items


        List<InventoryItem> items = new List<InventoryItem>();

        public List<InventoryItem> Items
        {
            get { return items; }
            set { items = value; }
        }


        #endregion

        public void AddItem(Type itemType, int quantity)
        {
            string itemName = itemType.Name;
                     
            for (int i = 0; i < items.Count; ++i)
            {
                if (items[i].ItemName == itemName)
                {
                    items[i].Quantity += quantity;
                    return;
                }
            }

            items.Add(new InventoryItem(itemName, quantity, ""));
        }

        public void AddItem(Type itemType, int quantity, string mapName, List<string> subMapNames)
        {
            string itemName = itemType.Name;

            for (int i = 0; i < items.Count; ++i)
            {
                if (items[i].ItemName == itemName)
                {
                    items[i].Quantity += quantity;
                    return;
                }
            }

            items.Add(new InventoryItem(itemName, quantity, mapName, subMapNames));
        }

        public void AddItem(Item item, int quantity)
        {
            AddItem(item.GetType(), quantity);
        }

        public void RemoveItem(Item item, int quantity)
        {
            RemoveItem(item.GetType(), quantity);
        }

        public void RemoveItem(Type itemType, int quantity)
        {
            string itemName = itemType.Name;

            for (int i = 0; i < items.Count; ++i)
            {
                if (items[i].ItemName == itemName)
                {
                    Debug.Assert(items[i].Quantity >= quantity);

                    items[i].Quantity -= quantity;

                    if (items[i].Quantity == 0)
                        items.RemoveAt(i);

                    return;
                }
            }

            /// Something went terribly wrong
            Debug.Assert(false);
        }

        public bool containsMapItem(string itemMapName)
        {
        
            for (int i = 0; i < items.Count; ++i)
            {
                if (items[i].MapName == itemMapName)
                {
                    return true;
                }
            }

            return false; 
        }


        public bool containsSubMap(string itemMapName)
        {

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].SubMapNames == null)
                {
                    continue;
                }
                else
                {

                    for (int j = 0; j < items[i].SubMapNames.Count; j++)
                    {

                        if (items[i].SubMapNames[j] == itemMapName)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
   
        public void RemoveItem(int itemIndex)
        {
            Debug.Assert(itemIndex < items.Count);

            if (--items[itemIndex].Quantity <= 0)
                items.RemoveAt(itemIndex);
        }

        public int ItemCount(Item item)
        {
            return ItemCount(item.GetType());
        }

        public int ItemCount(Type itemType)
        {
            string itemName = itemType.Name;

            for (int i = 0; i < items.Count; ++i)
            {
                if (items[i].ItemName == itemName)
                {
                    return items[i].Quantity;
                }
            }

            return 0;
        }

        public void SwapItems(int source, int destination)
        {
            Debug.Assert(source >= 0 && source < items.Count);
            Debug.Assert(destination >= 0 && destination < items.Count);

            InventoryItem temp = items[source];
            items[source] = items[destination];
            items[destination] = temp;
        }
    }
}
