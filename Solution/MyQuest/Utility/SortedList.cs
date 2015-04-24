using System;
using System.Collections.Generic;

namespace MyQuest
{
    public class MapModification<V>
    {
        string mapName;

        public string MapName
        {
            get { return mapName; }
            set { mapName = value; }
        }

        List<V> modifications = new List<V>();

        public List<V> Modifications
        {
            get { return modifications; }
            set { modifications = value; }
        }
    }


    public class SortedList<V>
    {
        public List<MapModification<V>> modifications = new List<MapModification<V>>();

        #region Public Methods


        public bool ContainsKey(string mapName)
        {
            if(Search(0, modifications.Count - 1, mapName) == -1)
                return false;
            
            return true;
        }

        public void Add(string mapName, List<V> modifications)
        {
            int index = Insert(0, this.modifications.Count - 1, mapName);

            MapModification<V> newMod = new MapModification<V>() { MapName = mapName, Modifications = modifications };

            if (index < 0)
                this.modifications.Add(newMod);
            else
                this.modifications.Insert(index, newMod);
        }

        public List<V> this[string mapName]
        {
            get 
            {
                try
                {
                    int index = Search(0, modifications.Count - 1, mapName);
                    return modifications[index].Modifications;
                }
                catch (ArgumentOutOfRangeException)
                {
                    return default(List<V>);
                }
            }
        }

        public bool TryGetValue(string mapName, out List<V> value)
        {
            value = null;

            if(ContainsKey(mapName))
            {
                value = this[mapName];
                return true;
            }

            return false;
        }


        #endregion

        #region Helpers


        int Search(int low, int high, string mapName)
        {
            if (high < low)
            {
                return -1;
            }

            int mid = low + (high - low) / 2;

            if(string.Compare(modifications[mid].MapName, mapName) > 0)
            {
                return Search(low, mid - 1, mapName);
            }
            else if (string.Compare(modifications[mid].MapName, mapName) < 0)
            {
                return Search(mid + 1, high, mapName);
            }
            else
            {
                return mid;
            }
        }

        int Insert(int low, int high, string mapName)
        {
            if (high < low)
            {
                return -1;
            }

            int mid = low + (high - low) / 2;

            if (string.Compare(mapName, modifications[mid].MapName) < 0)
            {
                if (mid == 0)
                    return 0;

                if (string.Compare(mapName, modifications[mid - 1].MapName) > 0)
                    return mid;

                return Insert(low, mid - 1, mapName);
            }
            else if (string.Compare(mapName, modifications[mid].MapName) > 0)
            {
                if (mid == modifications.Count - 1)
                    return mid + 1;

                if (string.Compare(mapName, modifications[mid + 1].MapName) < 0)
                    return mid + 1;

                return Insert(mid + 1, high, mapName);
            }
            else
            {
                return mid;
            }
        }


        #endregion
    }
}
