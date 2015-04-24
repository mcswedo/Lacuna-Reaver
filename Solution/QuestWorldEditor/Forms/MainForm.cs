using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyQuest;
using Color = Microsoft.Xna.Framework.Color;

/// Check execution flow
/// (Has probably been resolved) BUG: somehow the tileContentViewer isnt displaying the tiles when the map is loaded...Some weird combination
/// of actions prior to loading the map causes this...

/// The creation forms should resemble the createmapform ... if the dialog is cancelled, it should return existingInstance
/// in its original form (which it does) even if it is null. If its null, a new instance is returned

namespace QuestWorldEditor
{   
    public partial class MainForm : Form
    {
        public ListView combatListView = new ListView();

        #region Colors

        System.Drawing.Color[] colors = new System.Drawing.Color[]
        {
            System.Drawing.Color.Blue,
            System.Drawing.Color.Red,
            System.Drawing.Color.Yellow,
            System.Drawing.Color.Green,
            System.Drawing.Color.Purple,
            System.Drawing.Color.Orange,
            System.Drawing.Color.HotPink,
            System.Drawing.Color.Lime,
            System.Drawing.Color.Coral,
            System.Drawing.Color.SkyBlue,
            System.Drawing.Color.Gold,
            System.Drawing.Color.MediumVioletRed,
            System.Drawing.Color.Turquoise,
            System.Drawing.Color.DeepPink,
            System.Drawing.Color.SteelBlue,
            System.Drawing.Color.Olive
        };

        #endregion

        #region Portal Data


        const string portalDataFile = "PortalData.xml";

        List<Portal> portalEntries = new List<Portal>();

        public List<Portal> PortalEntries
        {
            get { return portalEntries; }
        }


        #endregion

        #region ContentController and ContentViewer names

        /// <summary>
        /// These names are used to identify both the controller's and their viewers.
        /// </summary>

        const int ContentTabPages = 4;
        internal const string TileTabPage = "Tiles";
        internal const string NPCTabPage = "NPCS";
        internal const string PortalTabPage = "Portals";
        internal const string CombatZoneTabPage = "CombatZones";
        internal const string CutSceneTabPage = "CutScenes";


        #endregion
                     
        #region Fields


        #region Appearance


        internal static Texture2D ThinTileOutline;

        internal static Texture2D ThickTileOutline;

        internal static Texture2D ThickTile;

        internal static Color TileOutlineColor = Color.White;

        internal static Color MapGridColor = Color.White;

        internal static bool DrawMapGrid = false;


        #endregion

        static string rootContentFolder;

        public static string RootContentFolder
        {
            get { return rootContentFolder; }
        }


        static Layers currentLayer;

        public static Layers CurrentLayer
        {
            get { return currentLayer; }
        }


        MapViewer currentMapViewer;
        //ContentViewer currentContentViewer;

        ContentBuilder contentBuilder;
        ContentManager contentManager;

        int tempFilename = 0;

        bool menuActive = false;


        SpriteFont spriteFont;

        public SpriteFont SpriteFont
        {
            get { return spriteFont; }
        }


        #endregion

        #region Properties


        /// <summary>
        /// Provides direct access to the currentMapViewer's map
        /// </summary>
        internal Map CurrentMap
        {
            get
            {
                return (currentMapViewer == null ? null : currentMapViewer.Map);
            }
        }


        internal MapViewer CurrentMapViewer
        {
            get { return currentMapViewer; }
        }


        internal TabPage CurrentTabPage
        {
            get { return contentTabControl.SelectedTab; }
        }


        #region Editor Tools


        internal bool DrawGround
        {
            get { return drawGround.Checked; }
        }

        internal bool DrawCollision
        {
            get { return drawCollision.Checked; }
        }

        internal bool DrawForeGround
        {
            get { return drawForeground.Checked; }
        }

        internal bool DrawFringe
        {
            get { return drawFringe.Checked; }
        }

        internal bool DrawDamage
        {
            get { return drawDamage.Checked; }
        }

        internal bool DrawPortal
        {
            get { return drawPortal.Checked; }
        }

        internal bool DrawCombat
        {
            get { return drawMonsterZones.Checked; }
        }

        internal bool DrawNPC
        {
            get { return drawNPC.Checked; }
        }

        internal bool DrawCutscene
        {
            get { return drawCutscene.Checked; }
        }

        internal bool MenuActive
        {
            get { return menuActive; }
        }

        internal float CollisionModifier
        {
            get { return collisionModifier.Value; }
        }

        internal float DamageModifier
        {
            get { return damageModifier.Value; }
        }


        #endregion


        #endregion

        #region MainForm Constructor


        internal MainForm()
        {
            InitializeComponent();

            /// All mouse coordinates will be relative to the main form
            Mouse.WindowHandle = this.Handle;

//            rootContentFolder = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("QuestWorldEditor")) + "MyQuestGame\\MyQuestGameContent\\";
//            rootContentFolder = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("QuestWorldEditor")) + "MyQuestGame\\";
            rootContentFolder = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("QuestWorldEditor"));
            
            openMapFileDialog.Filter = "Quest Map File|*.map";
            saveMapFileDialog.Filter = "Quest Map File|*.map";
            //openMapFileDialog.InitialDirectory = rootContentFolder + ContentPath.ToMaps;
            openMapFileDialog.InitialDirectory = rootContentFolder + "LacunaMaps\\";
            saveMapFileDialog.InitialDirectory = openMapFileDialog.InitialDirectory;

            /// We need to add the path to the QuestClassLibrary dll to the content builders pipeline assemblies in order for
            /// the editor to be able to load our .map files. Since the location of that .dll will be different on every
            /// machine, we use the startup path of the application to find it.             
            string questClassLibraryDLL = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("QuestWorldEditor"));
            questClassLibraryDLL += "MyQuest\\bin\\x86\\Debug\\MyQuest.dll";

            ContentBuilder.pipelineAssemblies = new string[5]
            {            
                questClassLibraryDLL,
                "Microsoft.Xna.Framework.Content.Pipeline.FBXImporter" + ContentBuilder.xnaVersion,
                "Microsoft.Xna.Framework.Content.Pipeline.XImporter" + ContentBuilder.xnaVersion,
                "Microsoft.Xna.Framework.Content.Pipeline.TextureImporter" + ContentBuilder.xnaVersion,
                "Microsoft.Xna.Framework.Content.Pipeline.EffectImporter" + ContentBuilder.xnaVersion,
            };


            /// Now we can create the content builder
            contentBuilder = new ContentBuilder();


            #region Event Handlers


            Application.Idle += new EventHandler(Application_Idle);

            collisionModifier.ValueChanged += new EventHandler(collisionModifier_ValueChanged);
            damageModifier.ValueChanged += new EventHandler(damageModifier_ValueChanged);

            menuStrip1.MenuActivate += new EventHandler(menuStrip1_MenuActivate);
            menuStrip1.MenuDeactivate += new EventHandler(menuStrip1_MenuDeactivate);

            mapTabControl.SelectedIndexChanged += new EventHandler(mapTabControl_SelectedIndexChanged);
            contentTabControl.SelectedIndexChanged += new EventHandler(contentTabControl_SelectedIndexChanged);

            this.Shown += new EventHandler(MainForm_Shown);
            this.Load += new EventHandler(MainForm_Load);
            this.Resize += new EventHandler(MainForm_Resize);
            this.Activated += new EventHandler(MainForm_Activated);
            this.Deactivate += new EventHandler(MainForm_Deactivate);

            //MainForm_Load(this, null);

            #endregion
        }


        #endregion

        #region MainForm Load and Idle


        void MainForm_Load(object sender, EventArgs e)
        {
            string contentPath = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("bin"));

            Stream stream = File.Open(contentPath + "\\" + portalDataFile, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Portal>));
            portalEntries = (List<Portal>)serializer.Deserialize(stream);
            stream.Close();

            /// Create the content pages for the content tab control. These pages consist of
            /// ContentViewers which handle the input and rendering logic for map content
            AddContentControlsToContentTabControl();

            ThinTileOutline = LoadContent<Texture2D>(
                contentPath + "\\thintileoutline.bmp",
                "TextureImporter",
                "SpriteTextureProcessor");

            ThickTileOutline = LoadContent<Texture2D>(
                contentPath + "\\thicktileoutline.bmp",
                "TextureImporter",
                "SpriteTextureProcessor");

            ThickTile = LoadContent<Texture2D>(
                contentPath + "\\thicktile.bmp",
                "TextureImporter",
                "SpriteTextureProcessor");

            spriteFont = LoadContent<SpriteFont>(
                contentPath + "\\Arial12.spritefont",
                "FontDescriptionImporter",
                "FontDescriptionProcessor");

            layerDropDown.Items.Add(Layers.Ground);
            layerDropDown.Items.Add(Layers.ForeGround);
            layerDropDown.Items.Add(Layers.Fringe);
            layerDropDown.Items.Add(Layers.Collision);
            layerDropDown.Items.Add(Layers.Damage);
            layerDropDown.Items.Add(Layers.Portal);
            layerDropDown.Items.Add(Layers.CombatZone);
            layerDropDown.Items.Add(Layers.NPC);
            layerDropDown.Items.Add(Layers.CutScene);
            layerDropDown.SelectedIndex = 0;

            collisionTextBox.Text = "0.0";
            damageTextBox.Text = "0.0";
            previousSize = this.Size;
        }

        /// <summary>
        /// This event fires just once, right after the Form is created and displayed.        
        /// </summary>
        void MainForm_Shown(object sender, EventArgs e)
        {
            /// This might be annoying to do everytime so
            /// for now it's disabled
            //GetRootContentFolder();

            MainForm_Resize(this, null);
        }

        /// <summary>
        /// When the application is idling, we invalide our custom
        /// controls so that they will get updated and redrawn
        /// </summary>
        void Application_Idle(object sender, EventArgs e)
        {
            if (currentMapViewer != null)
                currentMapViewer.Invalidate(true);
            
            if(contentTabControl.SelectedTab.Name == TileTabPage)
                contentTabControl.SelectedTab.Invalidate(true);
        }

        void MainForm_Deactivate(object sender, EventArgs e)
        {
            menuActive = true;
        }

        void MainForm_Activated(object sender, EventArgs e)
        {
            menuActive = false;
        }

        void GetRootContentFolder()
        {
            MessageBox.Show(
                    "Please select the Content Folder of your game project",
                    "Content Folder",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            if (gameContentFolderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                DialogResult result = MessageBox.Show(
                    "You must select the Content Folder of your game project.\n\tPress Retry or Cancel to exit",
                    "Content Folder",
                    MessageBoxButtons.RetryCancel,
                    MessageBoxIcon.Question);

                if (result == System.Windows.Forms.DialogResult.Retry)
                    GetRootContentFolder();
                else
                    this.Close();
            }

            rootContentFolder = gameContentFolderBrowser.SelectedPath;
        }


        #endregion

        #region Resizing


        /// <summary>
        /// The previous size of the MainForm. We use this to
        /// calculate the amount of change in dimensions
        /// </summary>
        Size previousSize;

        internal Size PreviousSize
        {
            get { return previousSize; }
        }

        /// <summary>
        /// Callback for when the MainForm Resize Event is triggered. Recalculates
        /// the dimensions of the TileDisplay and its ScrollBar
        /// </summary>
        void MainForm_Resize(object sender, EventArgs e)
        {   
            /// We keep track of the previous size so we can calculate
            /// how much the form has changed in both directions
            previousSize = Size;

            UpdateContentPageControlSizes();
        }

        void UpdateContentPageControlSizes()
        {
            if (CurrentMap != null)
            {
                TileContentViewer view = (TileContentViewer)contentTabControl.TabPages[0].Controls[0];

                view.UpdateSize(contentTabControl.TabPages[0].Size);
                view.BuildSourceFrames();
                view.BuildDestinationFrames();
            }
        }


        #endregion

        #region Content Tab Control Creation


        /// <summary>
        /// Generate a TabPage with some default settings
        /// </summary>
        /// <param name="pageText">The displayed title of the page</param>
        /// <returns>A new TabPage</returns>
        TabPage GenerateTabPage(string pageText)
        {
            TabPage page = new TabPage();
            page.Margin = new System.Windows.Forms.Padding(0);
            page.Padding = new System.Windows.Forms.Padding(0);
            page.Text = pageText;
            page.Name = pageText;
            page.AutoScroll = true;
            return page;
        }

        void AddContentToTabPage(TabPage dest, ContentViewer content)
        {
            content.MainForm = this;
            content.PageContainer = dest;
            content.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            dest.Controls.Add(content);
            contentTabControl.Controls.Add(dest);

            /// Test without this
            content.Size = dest.Size;
        }

        /// <summary>
        /// Creates the entire contentTabControl
        /// </summary>
        void AddContentControlsToContentTabControl()
        {
            #region Tiles

            TabPage tilePage = GenerateTabPage(TileTabPage);
            TileContentViewer tileView = new TileContentViewer();
            AddContentToTabPage(tilePage, tileView);

            #endregion

            contentManager = new ContentManager(
                tileView.Services,
                contentBuilder.OutputDirectory);

            #region NPCs

            TabPage npcPage = GenerateTabPage(NPCTabPage);
            ListBox npcBox = new ListBox();
//            string contentPath = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("QuestWorldEditor")) + "MyQuestGame\\MyQuestGameContent\\";
            string contentPath = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("QuestWorldEditor"));
            string npcPath = contentPath + "LacunaCharacters\\" + ContentPath.ToNPCMapCharacters;
            string[] npcFiles = Directory.GetFiles(npcPath);
            foreach (string file in npcFiles)
            {
                npcBox.Items.Add(Path.GetFileNameWithoutExtension(file));
            }
            npcBox.Dock = DockStyle.Fill;
            npcBox.Font = new System.Drawing.Font("Times New Roman", 12);
            if (npcBox.Items.Count > 0)
                npcBox.SelectedIndex = 0;
            npcBox.Sorted = true;
            npcPage.Controls.Add(npcBox);
            contentTabControl.Controls.Add(npcPage);

            #endregion

            #region Portals

            TabPage portalPage = GenerateTabPage(PortalTabPage);
            PortalListBox portalBox = new PortalListBox();
            portalBox.Items.AddRange(portalEntries.ToArray());
            portalBox.Dock = DockStyle.Fill;
            portalBox.Font = new System.Drawing.Font("Times New Roman", 12);
            if (portalBox.Items.Count > 0)
                portalBox.SelectedIndex = 0;
            portalBox.Sorted = true;
            portalPage.Controls.Add(portalBox);
            contentTabControl.TabPages.Add(portalPage);

            #endregion

            #region CombatZones

            int index = 0;
            TabPage combatPage = GenerateTabPage(CombatZoneTabPage);
            combatListView.Dock = DockStyle.Fill;
            combatListView.Font = new System.Drawing.Font("Times New Roman", 14, FontStyle.Bold);
            combatListView.View = View.Details;
            combatListView.GridLines = true;
            combatListView.Sorting = SortOrder.Ascending;
            combatListView.Columns.Add("Zone Name", 200);
            foreach (CombatZone zone in CombatZonePool.Singleton.Zones)
            {
                bool save = true;
                ListViewItem item = new ListViewItem(zone.ToString());
                item.Name = zone.ToString();
                if (item.Name == "Empty")
                    continue;
                foreach (ListViewItem i in combatListView.Items)
                {
                    if (i.Name == item.Name)
                    {
                        save = false;
                        break;
                    }
                }
                if (save)
                {
                    item.BackColor = colors[index++];
                    combatListView.Items.Add(item);

                    if (index >= colors.Length)
                        index = 0;
                }
            }

            combatPage.Controls.Add(combatListView);
            contentTabControl.TabPages.Add(combatPage);


            //TabPage combatPage = GenerateTabPage(CombatZoneTabPage);
            //CombatListBox combatBox = new CombatListBox();
            //combatBox.DrawItem += new DrawItemEventHandler(combatBox_DrawItem);
            //combatBox.DrawMode = DrawMode.OwnerDrawFixed;
            //foreach (CombatZone zone in CombatZonePool.Singleton.Zones)
            //{
            //    if (!combatBox.Items.Contains(zone))
            //        combatBox.Items.Add(zone);
            //}
            //combatBox.Dock = DockStyle.Fill;
            //combatBox.Font = new System.Drawing.Font("Times New Roman", 12);
            //if (combatBox.Items.Count > 0)
            //    combatBox.SelectedIndex = 0;
            //combatBox.Sorted = true;
            //combatPage.Controls.Add(combatBox);
            //contentTabControl.TabPages.Add(combatPage);

            #endregion

            #region CutScenes

            contentPath = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("QuestWorldEditor")) + "MyQuest\\ScreenManager\\CutScenes";            
            TabPage cutscenePage = GenerateTabPage(CutSceneTabPage);
            ListBox cutsceneBox = new ListBox();
            
            string[] directories = Directory.GetDirectories(contentPath);
            foreach (string directory in directories)
            {
                SearchDirectory(directory, cutsceneBox);               
            }

            cutsceneBox.Sorted = true;
            cutsceneBox.Dock = DockStyle.Fill;
            cutsceneBox.Font = new System.Drawing.Font("Times New Roman", 12);

            if (cutsceneBox.Items.Count > 0)
                cutsceneBox.SelectedIndex = 0;
            cutsceneBox.Sorted = true;
            cutscenePage.Controls.Add(cutsceneBox);
            contentTabControl.TabPages.Add(cutscenePage);

            #endregion

            contentTabControl.SelectedIndex = 0;
        }

        void SearchDirectory(string dirName, ListBox cutsceneBox)
        {
            foreach (string file in Directory.GetFiles(dirName))
            {
                string fileName = Path.GetFileName(file);
                if (fileName.Contains("CutSceneScreen") && !fileName.Contains("svn"))
                {
                    cutsceneBox.Items.Add(Path.GetFileNameWithoutExtension(file));
                }
            }

            string[] directories = Directory.GetDirectories(dirName);
            foreach (string d in directories)
            {
                SearchDirectory(d, cutsceneBox);
            }
        }


        #endregion

        #region Menu Strip


        #region File Strip


        void newMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateMapForm form = new CreateMapForm();

            menuActive = true;

            Map map = form.ShowDialog(null);

            if (map != null)
            {
                Cursor = Cursors.WaitCursor;

                string path = map.TextureSheetName;

                map.TileSheet = LoadContent<Texture2D>(
                    path,
                    "TextureImporter",
                    "SpriteTextureProcessor");

                map.TextureSheetName = Path.GetFileNameWithoutExtension(path);

                MapViewer mapViewer = new MapViewer();
                mapViewer.MainForm = this;
                mapViewer.Dock = DockStyle.Fill;
                mapViewer.Map = map;

                TabPage mapPage = GenerateTabPage(mapViewer.Map.Name);
                mapPage.Controls.Add(mapViewer);
                mapTabControl.TabPages.Add(mapPage);

                mapTabControl.SelectedIndex = mapTabControl.TabCount - 1;
        
                mapViewer.Size = mapPage.Size;

                currentMapViewer = mapViewer;

                OnOpenMap();

                Cursor = Cursors.Default;
            }

            menuActive = false;
        }

        void openMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuActive = true;

            if (openMapFileDialog.ShowDialog() == DialogResult.OK)
            {                   
                Cursor = Cursors.WaitCursor;

                foreach (TabPage page in mapTabControl.TabPages)
                {
                    MapViewer mapView = (MapViewer)page.Controls[0];

                    if (mapView.Map.Name == Path.GetFileNameWithoutExtension(openMapFileDialog.FileName))
                    {
                        currentMapViewer = mapView;
                        mapTabControl.SelectedTab = page;
                        Cursor = Cursors.Default;
                        return;
                    }
                }

                AddNewMapPage(openMapFileDialog.FileName);
                
                OnOpenMap();

                Cursor = Cursors.Default;
            }

            menuActive = false;
        }

        void saveMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentMap == null)
                return;

            menuActive = true;

            saveMapFileDialog.FileName = CurrentMap.Name + ".map";

            if (saveMapFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SaveMap(saveMapFileDialog.FileName);
            }

            menuActive = false;
        }

        void closeMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentMapViewer != null)
            {
                TabPage page = mapTabControl.SelectedTab;
                mapTabControl.TabPages.Remove(page);

                if (mapTabControl.TabCount > 0)
                {
                    currentMapViewer = (MapViewer)mapTabControl.TabPages[mapTabControl.TabCount - 1].Controls[0];
                    // Change selectedPage ?
                }
                else
                    currentMapViewer = null;
            }
        }


        void menuStrip1_MenuDeactivate(object sender, EventArgs e)
        {
            menuActive = false;
        }

        void menuStrip1_MenuActivate(object sender, EventArgs e)
        {
            menuActive = true;
        }


        void SaveMap(string filename)
        {
            if (CurrentMap == null)
                return;

            if (!Path.HasExtension(filename))
            {
                filename += ".map";
            }

            if (CurrentMap.Portals == null)
                CurrentMap.Portals = new List<Portal>();
            
            if (CurrentMap.CutSceneEntries == null)
                CurrentMap.CutSceneEntries = new List<CutSceneEntry>();
            
            if (CurrentMap.NpcEntries == null)
                CurrentMap.NpcEntries = new List<NPCEntry>();

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create(filename, settings))
            {
                IntermediateSerializer.Serialize(writer, this.CurrentMap, null);
            }
        }

        void OnOpenMap()
        {
            CurrentMap.Initialize();

            EnableMapTools(true);
            drawGround.Checked = true;

            UpdateContentPageControlSizes();
        }


        void AddNewMapPage(string mapFileName)
        {
            MapViewer mapViewer = new MapViewer();
            mapViewer.MainForm = this;
            mapViewer.Dock = DockStyle.Fill;

            /// Load the mapViewer's map
            mapViewer.Map = LoadContent<Map>(
                    mapFileName,
                    "XmlImporter",
                    null);

            string texturePath = RootContentFolder + "LacunaTextures\\" + ContentPath.ToMapTextures + mapViewer.Map.TextureSheetName;

            /// The filename of the tilesheet has no file extension so we need to add one
            Utility.AppendImageFileExtension(ref texturePath);

            if (!File.Exists(texturePath))
            {
                MessageBox.Show(
                    "Could not find the texture file!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                return;
            }

            /// Load the tilesheet
            mapViewer.Map.TileSheet = LoadContent<Texture2D>(
                texturePath,
                "TextureImporter",
                "SpriteTextureProcessor");

            TabPage mapPage = GenerateTabPage(mapViewer.Map.Name);
            mapPage.Controls.Add(mapViewer);
            mapTabControl.TabPages.Add(mapPage);

            mapTabControl.SelectedIndex = mapTabControl.TabCount - 1;

            /// For some unknown reason, the controller's height gets set to 1194
            /// which is larger than the height of the App Window. It could have
            /// something to do with the GraphicsDeviceControl base class or more
            /// likely has to do with me setting up the properties incorrectly. 
            /// This cheap fix is working for now!!
            /// NOTE: the height goes hay-wire somewhere after one of the adds directly above            
            mapViewer.Size = mapPage.Size;

            currentMapViewer = mapViewer;
        }


        #endregion

        #region View Strip


        private void toolStripMenuZoom25_Click(object sender, EventArgs e)
        {
            if (CurrentMapViewer != null)
                CurrentMapViewer.ZoomFactor = 0.25f;
        }

        private void toolStripMenuZoom50_Click(object sender, EventArgs e)
        {
            if (CurrentMapViewer != null)
                CurrentMapViewer.ZoomFactor = 0.5f;
        }

        private void toolStripMenuZoom75_Click(object sender, EventArgs e)
        {
            if (CurrentMapViewer != null)
                CurrentMapViewer.ZoomFactor = 0.75f;
        }

        private void toolStripMenuZoom100_Click(object sender, EventArgs e)
        {
            if (CurrentMapViewer != null)
                CurrentMapViewer.ZoomFactor = 1.0f;
        }

        #endregion

        #region Layer Strip


        private void clearCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(CurrentMap != null)
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to clear the " + CurrentLayer.ToString() + " layer?",
                    "Clear Current Layer",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    switch (CurrentLayer)
                    {
                        case Layers.Collision:
                            ClearLayerTo<float>(CurrentMap.CollisionLayer, 1f);
                            break;
                        case Layers.Damage:
                            ClearLayerTo<float>(CurrentMap.CollisionLayer, 0f);
                            break;
                        case Layers.ForeGround:
                            ClearLayerTo<short>(CurrentMap.ForeGroundLayer, -1);
                            break;
                        case Layers.Fringe:
                            ClearLayerTo<short>(CurrentMap.FringeLayer, -1);
                            break;
                        case Layers.Ground:
                            ClearLayerTo<short>(CurrentMap.GroundLayer, -1);
                            break;
                    }
                }
            }
        }

        private void ClearLayerTo<T>(T[] layer, T newValue)
        {
            for (int x = 0; x < CurrentMap.DimensionsInTiles.X; ++x)
            {
                for (int y = 0; y < CurrentMap.DimensionsInTiles.Y; ++y)
                {
                    int index = CurrentMap.GetIndex(x, y);
                    layer[index] = newValue;
                }
            }
        }


        #endregion

        #region Content Strip


        void nPCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rootContentFolder))
            {
                GetRootContentFolder();

                if (string.IsNullOrEmpty(rootContentFolder))
                {
                    MessageBox.Show(
                        "You cannot create content without specifying the content directory!",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);

                    return;
                }
            }

            menuActive = true;

            CreateNPCForm form = new CreateNPCForm();
            
            DialogResult result;
            NPCMapCharacter npc = form.ShowDialog(null, out result);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;

                using (XmlWriter writer = XmlWriter.Create(rootContentFolder + @"\Characters\NPC\" + npc.Name + ".xml", settings))
                {
                    IntermediateSerializer.Serialize<NPCMapCharacter>(writer, npc, null);
                }
            }

            menuActive = false;
        }

        void nPCToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        void portalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreatePortalForm form = new CreatePortalForm();

            menuActive = true;

            DialogResult result;
            Portal portal = form.ShowDialog(null, out result);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                portalEntries.Add(portal);

                Stream stream = File.Open(
                    Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("bin")) + "\\" + portalDataFile, 
                    FileMode.Open);
                
                XmlSerializer serializer = new XmlSerializer(typeof(List<Portal>));
                serializer.Serialize(stream, portalEntries);
                stream.Close();

                TabPage portals = contentTabControl.TabPages[2];
                PortalListBox box = (PortalListBox)portals.Controls[0];
                box.Items.Add(portal);
            }

            menuActive = false;
        }

        void portalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (contentTabControl.SelectedTab.Name != PortalTabPage)
                return;

            PortalListBox box = (PortalListBox)contentTabControl.SelectedTab.Controls[0];

            if (box.Items.Count < 0 || box.SelectedIndex < 0)
                return;

            CreatePortalForm form = new CreatePortalForm();

            menuActive = true;

            DialogResult result;
            Portal portal = form.ShowDialog((Portal)box.SelectedItem, out result);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                box.Items.RemoveAt(box.SelectedIndex);
                portalEntries.Remove((Portal)box.SelectedItem);

                box.Items.Add(portal);              
                portalEntries.Add(portal);

                Stream stream = File.Open(
                    Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("bin")) + "\\" + portalDataFile,
                    FileMode.Open);

                XmlSerializer serializer = new XmlSerializer(typeof(List<Portal>));
                serializer.Serialize(stream, portalEntries);
                stream.Close();
            }

            menuActive = false;
        }


        #endregion

        #region Appearance


        private void contentTileOutlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Color c = colorDialog1.Color;

                TileOutlineColor.R = c.R;
                TileOutlineColor.G = c.G;
                TileOutlineColor.B = c.B;
            }
        }

        private void mapGridColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Color c = colorDialog1.Color;

                MapGridColor.R = c.R;
                MapGridColor.G = c.G;
                MapGridColor.B = c.B;
            }
        }

        private void viewGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawMapGrid = viewGridToolStripMenuItem.Checked;
        }


        #endregion


        #endregion

        #region World Map Tools


        void collisionModifier_ValueChanged(object sender, EventArgs e)
        {
            collisionTextBox.Text = (collisionModifier.Value / 100f).ToString();
        }

        void damageModifier_ValueChanged(object sender, EventArgs e)
        {
            damageTextBox.Text = (damageModifier.Value / 100f).ToString();
        }

        void layerDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentLayer = (Layers)layerDropDown.SelectedItem;
        }
      
        void EnableMapTools(bool enable)
        {
            layerDropDown.Enabled = enable;
            collisionModifier.Enabled = enable;
            damageModifier.Enabled = enable;

            drawGround.Enabled = enable;
            drawForeground.Enabled = enable;
            drawFringe.Enabled = enable;
            drawCollision.Enabled = enable;
            drawDamage.Enabled = enable;
            drawMonsterZones.Enabled = enable;
            drawPortal.Enabled = enable;
            drawNPC.Enabled = enable;
            drawCutscene.Enabled = enable;
        }


        #endregion

        #region Content Loader


        internal T LoadContent<T>(
            string fileName,
            string contentImporter,
            string contentProcessor)
        {
            string buildError;
            string contentName = "tempContent" + tempFilename++;
            object content = null;

            // contentManager.Unload();

            contentBuilder.Clear();

            contentBuilder.Add(fileName, contentName, contentImporter, contentProcessor);

            /// Build the content. The temporary .xnb file is stored in 
            /// contentBuilder.OutputDirectory
            buildError = contentBuilder.Build();

            if (string.IsNullOrEmpty(buildError))
            {
                // If the build succeeded, use the ContentManager to
                // load the temporary .xnb file that we just created. 
                content = contentManager.Load<T>(contentName);
            }
            else
                throw new Exception(buildError);

            return (T)content;
        }


        #endregion

        #region TabControl


        void mapTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mapTabControl.TabCount > 0)
            {
                currentMapViewer = (MapViewer)mapTabControl.SelectedTab.Controls[0];

                TileContentViewer tileView = (TileContentViewer)contentTabControl.TabPages[0].Controls[0];
                tileView.UpdateSize(contentTabControl.TabPages[0].Size);
                tileView.BuildSourceFrames();
                tileView.BuildDestinationFrames();
            }
        }

        void contentTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //currentContentViewer.LostMouse();
            if (contentTabControl.SelectedTab.Name == TileTabPage)
            {
                ContentViewer view = (ContentViewer)contentTabControl.TabPages[0].Controls[0];
                view.LostMouse();
            }       //currentContentViewer = (ContentViewer)contentTabControl.SelectedTab.Controls[0];
        }


        #endregion

        private void fillCombatLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentMap == null)
                return;

            Random rng = new Random();

            CombatFillForm form = new CombatFillForm();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ListBox box = (ListBox)form.Controls["destBox"];

                for (int x = 0; x < CurrentMap.DimensionsInTiles.X; ++x)
                {
                    for (int y = 0; y < CurrentMap.DimensionsInTiles.Y; ++y)
                    {
                        int rand = rng.Next(box.Items.Count);
                        CombatZone zone = (CombatZone)box.Items[rand];

                        int index = CurrentMap.GetIndex(x, y);

                        if (CurrentMap.CollisionLayer[index] > 0.000001f)
                        {
                            CurrentMap.CombatLayer[index] = CombatZonePool.Singleton.ToIndex(zone.ZoneName); ;
                        }
                    }
                }
            }
        }
    }
}