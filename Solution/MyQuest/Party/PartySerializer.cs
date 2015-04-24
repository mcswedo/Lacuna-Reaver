using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using System.Collections.Generic;

/// Finished under current specifications. May we require
/// more work after further tests are run

namespace MyQuest
{
    /// <summary>
    /// Provides asynchronous access to the storage container. 
    /// Used for saving and loading the Party.
    /// </summary>
    /// <remarks>When this class is performing storage operations, it
    /// notifies the GameEngine through the GameEngine's WaitingOnCallback
    /// property. When this flag is set, the GameEngine stops updating the
    /// screen manager. For more information, see Dr. Turner's video</remarks>
    static class PartySerializer
    {
        private enum State 
        { 
            Inactive, 
            Loading, 
            Saving, 
            SearchingForFiles 
        };

        #region Fields


        static State state = State.Inactive;

        static List<string> saveGameFiles = new List<string>();

        public static IList<string> SaveGameFiles
        {
            get { return saveGameFiles.AsReadOnly(); }
        }

        static bool waitingOnStorage = false;

        public static bool WaitingOnStorage
        {
            get { return waitingOnStorage; }
        }


        #endregion

        #region Initialization


        public static void Initialize()
        {
        }


        #endregion

        #region Public Methods


        public static void LoadFrom(string filename)
        {
            Debug.Assert(state == State.Inactive);
            state = State.Loading;
            RegisterCallback(filename);
        }

        public static void SaveTo(string filename)
        {
            Debug.Assert(state == State.Inactive);
            state = State.Saving;
            RegisterCallback(filename);
        }

        public static void RefreshSaveGameListing()
        {
            Debug.Assert(state == State.Inactive);
            state = State.SearchingForFiles;
            RegisterCallback(null);
        }


        #endregion

        #region Helpers


        private static void RegisterCallback(string filename)
        {
            Debug.Assert(PartySerializer.WaitingOnStorage == false);
            waitingOnStorage = true;
            StorageDevice.BeginShowSelector(StorageDeviceCallback, filename);
        }

        private static void StorageDeviceCallback(IAsyncResult result)
        {
            StorageDevice storageDevice = StorageDevice.EndShowSelector(result);
            string filename = (string)result.AsyncState;
            if (storageDevice == null || !storageDevice.IsConnected)
            {
                /// failed to get a storage device
                /// Put up a message box and allow the user to fix the problem and exit outta here
            }

            switch (state)
            {
                case State.Loading:
                    {
                        Debug.Assert(!string.IsNullOrEmpty(filename));
                        StorageContainer storageContainer = GetStorageContainer(storageDevice);
                        if (!storageContainer.FileExists(filename))
                        {
                            Debug.Assert(false);

                            /// if this is ever happens it shouldn't be our fault.
                            /// Inform the user and then ... ?
                        }
                        else
                        {
                            Stream stream = storageContainer.OpenFile(filename, FileMode.Open);

                            XmlSerializer serializer = new XmlSerializer(typeof(GameState));
                            Party.Singleton.GameState = (GameState)serializer.Deserialize(stream);

                            stream.Close();
                        }
                        storageContainer.Dispose();  // Commit changes.
                        state = State.Inactive;
                        break;
                    }

                case State.Saving:
                    {
                        Debug.Assert(!string.IsNullOrEmpty(filename));
                        StorageContainer storageContainer = GetStorageContainer(storageDevice);
                        if (storageContainer.FileExists(filename))
                        {
                            storageContainer.DeleteFile(filename);
                        }
                        Stream stream = storageContainer.CreateFile(filename);

                        XmlSerializer serializer = new XmlSerializer(typeof(GameState));
                        serializer.Serialize(stream, Party.Singleton.GameState);

                        stream.Close();
                        storageContainer.Dispose();  // Commit changes.
                        state = State.Inactive;
                        break;
                    }

                case State.SearchingForFiles:
                    {
                        StorageContainer storageContainer = GetStorageContainer(storageDevice);
                        
                        string[] filenames = storageContainer.GetFileNames("*.xml");

                        saveGameFiles.Clear();                        
                        saveGameFiles.AddRange(filenames);

                        storageContainer.Dispose();
                        state = State.Inactive;
                        break;
                    }

                case State.Inactive:
                    break;
            }

            waitingOnStorage = false;
        }

        private static StorageContainer GetStorageContainer(StorageDevice storageDevice)
        {
            IAsyncResult result = storageDevice.BeginOpenContainer("StorageContainer", null, null);
            result.AsyncWaitHandle.WaitOne();
            StorageContainer container = storageDevice.EndOpenContainer(result);
            result.AsyncWaitHandle.Close();
            return container;
        }


        #endregion
    }
}