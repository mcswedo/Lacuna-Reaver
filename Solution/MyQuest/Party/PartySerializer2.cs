using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace MyQuest
{
    public class PartySerializer2
    {
        public static void LoadFrom(string filename)
        {
            Debug.Assert(!string.IsNullOrEmpty(filename));
            FileStream fileStream = new FileStream(filename, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(GameState));
            Party.Singleton.GameState = (GameState)serializer.Deserialize(fileStream);
            //Party.Singleton.GameState.CopyPartyInstances();
            foreach (PCFightingCharacter fighter in Party.Singleton.GameState.Fighters)
            {
                fighter.GenerateStatModifiersFromEquipment();
                //fighter.LevelUpReEquip(); //This is used to reapply equipment stat modifiers.
            }
            fileStream.Close();
        }

        public static void SaveTo(string filename)
        {
            Debug.Assert(!string.IsNullOrEmpty(filename));
            FileStream fileStream = null;
            if (File.Exists(filename))
            {
                fileStream = new FileStream(filename, FileMode.Truncate, FileAccess.Write);
            }
            else
            {
                fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write);
            }
            XmlSerializer serializer = new XmlSerializer(typeof(GameState));
            serializer.Serialize(fileStream, Party.Singleton.GameState);
            fileStream.Close();
        }

    }
}
