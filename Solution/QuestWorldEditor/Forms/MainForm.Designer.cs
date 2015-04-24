namespace QuestWorldEditor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuZoom25 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuZoom50 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuZoom75 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuZoom100 = new System.Windows.Forms.ToolStripMenuItem();
            this.layerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillCombatLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nPCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nPCToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.portalToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.appearanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentTileOutlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapGridColorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.currentMapTile = new System.Windows.Forms.ToolStripTextBox();
            this.openMapFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveMapFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.layerDropDown = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.collisionModifier = new System.Windows.Forms.TrackBar();
            this.damageModifier = new System.Windows.Forms.TrackBar();
            this.collisionTextBox = new System.Windows.Forms.TextBox();
            this.damageTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.drawFringe = new System.Windows.Forms.CheckBox();
            this.drawCollision = new System.Windows.Forms.CheckBox();
            this.drawDamage = new System.Windows.Forms.CheckBox();
            this.drawMonsterZones = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.drawGround = new System.Windows.Forms.CheckBox();
            this.drawForeground = new System.Windows.Forms.CheckBox();
            this.mapTabControl = new System.Windows.Forms.TabControl();
            this.contentTabControl = new System.Windows.Forms.TabControl();
            this.gameContentFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.drawPortal = new System.Windows.Forms.CheckBox();
            this.drawNPC = new System.Windows.Forms.CheckBox();
            this.drawCutscene = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.collisionModifier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.damageModifier)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.layerToolStripMenuItem,
            this.contentToolStripMenuItem,
            this.appearanceToolStripMenuItem,
            this.currentMapTile});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1264, 27);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMapToolStripMenuItem,
            this.openMapToolStripMenuItem,
            this.saveMapToolStripMenuItem,
            this.closeMapToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 23);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newMapToolStripMenuItem
            // 
            this.newMapToolStripMenuItem.Name = "newMapToolStripMenuItem";
            this.newMapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newMapToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.newMapToolStripMenuItem.Text = "New Map";
            this.newMapToolStripMenuItem.Click += new System.EventHandler(this.newMapToolStripMenuItem_Click);
            // 
            // openMapToolStripMenuItem
            // 
            this.openMapToolStripMenuItem.Name = "openMapToolStripMenuItem";
            this.openMapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openMapToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.openMapToolStripMenuItem.Text = "Open Map";
            this.openMapToolStripMenuItem.Click += new System.EventHandler(this.openMapToolStripMenuItem_Click);
            // 
            // saveMapToolStripMenuItem
            // 
            this.saveMapToolStripMenuItem.Name = "saveMapToolStripMenuItem";
            this.saveMapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveMapToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.saveMapToolStripMenuItem.Text = "Save Map";
            this.saveMapToolStripMenuItem.Click += new System.EventHandler(this.saveMapToolStripMenuItem_Click);
            // 
            // closeMapToolStripMenuItem
            // 
            this.closeMapToolStripMenuItem.Name = "closeMapToolStripMenuItem";
            this.closeMapToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.closeMapToolStripMenuItem.Text = "Close Map";
            this.closeMapToolStripMenuItem.Click += new System.EventHandler(this.closeMapToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 23);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // zoomToolStripMenuItem
            // 
            this.zoomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuZoom25,
            this.toolStripMenuZoom50,
            this.toolStripMenuZoom75,
            this.toolStripMenuZoom100});
            this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            this.zoomToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.zoomToolStripMenuItem.Text = "Zoom";
            // 
            // toolStripMenuZoom25
            // 
            this.toolStripMenuZoom25.Name = "toolStripMenuZoom25";
            this.toolStripMenuZoom25.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuZoom25.Text = "25%";
            this.toolStripMenuZoom25.Click += new System.EventHandler(this.toolStripMenuZoom25_Click);
            // 
            // toolStripMenuZoom50
            // 
            this.toolStripMenuZoom50.Name = "toolStripMenuZoom50";
            this.toolStripMenuZoom50.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuZoom50.Text = "50%";
            this.toolStripMenuZoom50.Click += new System.EventHandler(this.toolStripMenuZoom50_Click);
            // 
            // toolStripMenuZoom75
            // 
            this.toolStripMenuZoom75.Name = "toolStripMenuZoom75";
            this.toolStripMenuZoom75.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuZoom75.Text = "75%";
            this.toolStripMenuZoom75.Click += new System.EventHandler(this.toolStripMenuZoom75_Click);
            // 
            // toolStripMenuZoom100
            // 
            this.toolStripMenuZoom100.Name = "toolStripMenuZoom100";
            this.toolStripMenuZoom100.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuZoom100.Text = "100%";
            this.toolStripMenuZoom100.Click += new System.EventHandler(this.toolStripMenuZoom100_Click);
            // 
            // layerToolStripMenuItem
            // 
            this.layerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearCurrentToolStripMenuItem,
            this.fillCombatLayerToolStripMenuItem});
            this.layerToolStripMenuItem.Name = "layerToolStripMenuItem";
            this.layerToolStripMenuItem.Size = new System.Drawing.Size(47, 23);
            this.layerToolStripMenuItem.Text = "Layer";
            // 
            // clearCurrentToolStripMenuItem
            // 
            this.clearCurrentToolStripMenuItem.Name = "clearCurrentToolStripMenuItem";
            this.clearCurrentToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.clearCurrentToolStripMenuItem.Text = "Clear Current";
            this.clearCurrentToolStripMenuItem.Click += new System.EventHandler(this.clearCurrentToolStripMenuItem_Click);
            // 
            // fillCombatLayerToolStripMenuItem
            // 
            this.fillCombatLayerToolStripMenuItem.Name = "fillCombatLayerToolStripMenuItem";
            this.fillCombatLayerToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.fillCombatLayerToolStripMenuItem.Text = "Fill Combat Layer";
            this.fillCombatLayerToolStripMenuItem.Click += new System.EventHandler(this.fillCombatLayerToolStripMenuItem_Click);
            // 
            // contentToolStripMenuItem
            // 
            this.contentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createToolStripMenuItem,
            this.modifyToolStripMenuItem});
            this.contentToolStripMenuItem.Name = "contentToolStripMenuItem";
            this.contentToolStripMenuItem.Size = new System.Drawing.Size(62, 23);
            this.contentToolStripMenuItem.Text = "Content";
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nPCToolStripMenuItem,
            this.portalToolStripMenuItem});
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.createToolStripMenuItem.Text = "Create";
            // 
            // nPCToolStripMenuItem
            // 
            this.nPCToolStripMenuItem.Name = "nPCToolStripMenuItem";
            this.nPCToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.nPCToolStripMenuItem.Text = "NPC";
            this.nPCToolStripMenuItem.Click += new System.EventHandler(this.nPCToolStripMenuItem_Click);
            // 
            // portalToolStripMenuItem
            // 
            this.portalToolStripMenuItem.Name = "portalToolStripMenuItem";
            this.portalToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.portalToolStripMenuItem.Text = "Portal";
            this.portalToolStripMenuItem.Click += new System.EventHandler(this.portalToolStripMenuItem_Click);
            // 
            // modifyToolStripMenuItem
            // 
            this.modifyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nPCToolStripMenuItem1,
            this.portalToolStripMenuItem1});
            this.modifyToolStripMenuItem.Name = "modifyToolStripMenuItem";
            this.modifyToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.modifyToolStripMenuItem.Text = "Modify";
            // 
            // nPCToolStripMenuItem1
            // 
            this.nPCToolStripMenuItem1.Name = "nPCToolStripMenuItem1";
            this.nPCToolStripMenuItem1.Size = new System.Drawing.Size(105, 22);
            this.nPCToolStripMenuItem1.Text = "NPC";
            this.nPCToolStripMenuItem1.Click += new System.EventHandler(this.nPCToolStripMenuItem1_Click);
            // 
            // portalToolStripMenuItem1
            // 
            this.portalToolStripMenuItem1.Name = "portalToolStripMenuItem1";
            this.portalToolStripMenuItem1.Size = new System.Drawing.Size(105, 22);
            this.portalToolStripMenuItem1.Text = "Portal";
            this.portalToolStripMenuItem1.Click += new System.EventHandler(this.portalToolStripMenuItem1_Click);
            // 
            // appearanceToolStripMenuItem
            // 
            this.appearanceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentTileOutlineToolStripMenuItem,
            this.mapGridToolStripMenuItem});
            this.appearanceToolStripMenuItem.Name = "appearanceToolStripMenuItem";
            this.appearanceToolStripMenuItem.Size = new System.Drawing.Size(82, 23);
            this.appearanceToolStripMenuItem.Text = "Appearance";
            // 
            // contentTileOutlineToolStripMenuItem
            // 
            this.contentTileOutlineToolStripMenuItem.Name = "contentTileOutlineToolStripMenuItem";
            this.contentTileOutlineToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.contentTileOutlineToolStripMenuItem.Text = "Content Tile Outline Color";
            this.contentTileOutlineToolStripMenuItem.Click += new System.EventHandler(this.contentTileOutlineToolStripMenuItem_Click);
            // 
            // mapGridToolStripMenuItem
            // 
            this.mapGridToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewGridToolStripMenuItem,
            this.mapGridColorToolStripMenuItem1});
            this.mapGridToolStripMenuItem.Name = "mapGridToolStripMenuItem";
            this.mapGridToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.mapGridToolStripMenuItem.Text = "Map Grid";
            // 
            // viewGridToolStripMenuItem
            // 
            this.viewGridToolStripMenuItem.CheckOnClick = true;
            this.viewGridToolStripMenuItem.Name = "viewGridToolStripMenuItem";
            this.viewGridToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.viewGridToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.viewGridToolStripMenuItem.Text = "View Grid";
            this.viewGridToolStripMenuItem.Click += new System.EventHandler(this.viewGridToolStripMenuItem_Click);
            // 
            // mapGridColorToolStripMenuItem1
            // 
            this.mapGridColorToolStripMenuItem1.Name = "mapGridColorToolStripMenuItem1";
            this.mapGridColorToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
            this.mapGridColorToolStripMenuItem1.Text = "Grid Color";
            this.mapGridColorToolStripMenuItem1.Click += new System.EventHandler(this.mapGridColorToolStripMenuItem1_Click);
            // 
            // currentMapTile
            // 
            this.currentMapTile.Enabled = false;
            this.currentMapTile.Name = "currentMapTile";
            this.currentMapTile.Size = new System.Drawing.Size(50, 23);
            this.currentMapTile.Text = "0,0";
            this.currentMapTile.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // openMapFileDialog
            // 
            this.openMapFileDialog.FileName = "openMapFileDialog";
            // 
            // layerDropDown
            // 
            this.layerDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layerDropDown.Enabled = false;
            this.layerDropDown.FormattingEnabled = true;
            this.layerDropDown.Location = new System.Drawing.Point(1003, 27);
            this.layerDropDown.Name = "layerDropDown";
            this.layerDropDown.Size = new System.Drawing.Size(188, 21);
            this.layerDropDown.TabIndex = 1;
            this.layerDropDown.TabStop = false;
            this.layerDropDown.SelectedIndexChanged += new System.EventHandler(this.layerDropDown_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(852, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Current Layer to Edit:";
            // 
            // collisionModifier
            // 
            this.collisionModifier.Enabled = false;
            this.collisionModifier.Location = new System.Drawing.Point(960, 59);
            this.collisionModifier.Maximum = 100;
            this.collisionModifier.Name = "collisionModifier";
            this.collisionModifier.Size = new System.Drawing.Size(201, 45);
            this.collisionModifier.TabIndex = 2;
            this.collisionModifier.TickFrequency = 25;
            // 
            // damageModifier
            // 
            this.damageModifier.Enabled = false;
            this.damageModifier.Location = new System.Drawing.Point(960, 105);
            this.damageModifier.Maximum = 100;
            this.damageModifier.Name = "damageModifier";
            this.damageModifier.Size = new System.Drawing.Size(201, 45);
            this.damageModifier.TabIndex = 3;
            this.damageModifier.TickFrequency = 25;
            // 
            // collisionTextBox
            // 
            this.collisionTextBox.Enabled = false;
            this.collisionTextBox.Location = new System.Drawing.Point(1163, 59);
            this.collisionTextBox.Name = "collisionTextBox";
            this.collisionTextBox.ReadOnly = true;
            this.collisionTextBox.Size = new System.Drawing.Size(28, 20);
            this.collisionTextBox.TabIndex = 11;
            // 
            // damageTextBox
            // 
            this.damageTextBox.Enabled = false;
            this.damageTextBox.Location = new System.Drawing.Point(1163, 105);
            this.damageTextBox.Name = "damageTextBox";
            this.damageTextBox.ReadOnly = true;
            this.damageTextBox.Size = new System.Drawing.Size(28, 20);
            this.damageTextBox.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(852, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "Damage Value:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(852, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "Collision Value:";
            // 
            // drawFringe
            // 
            this.drawFringe.AutoSize = true;
            this.drawFringe.Enabled = false;
            this.drawFringe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drawFringe.Location = new System.Drawing.Point(959, 198);
            this.drawFringe.Name = "drawFringe";
            this.drawFringe.Size = new System.Drawing.Size(65, 20);
            this.drawFringe.TabIndex = 6;
            this.drawFringe.Text = "Fringe";
            this.drawFringe.UseVisualStyleBackColor = true;
            // 
            // drawCollision
            // 
            this.drawCollision.AutoSize = true;
            this.drawCollision.Enabled = false;
            this.drawCollision.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drawCollision.Location = new System.Drawing.Point(1067, 144);
            this.drawCollision.Name = "drawCollision";
            this.drawCollision.Size = new System.Drawing.Size(78, 20);
            this.drawCollision.TabIndex = 7;
            this.drawCollision.Text = "Collision";
            this.drawCollision.UseVisualStyleBackColor = true;
            // 
            // drawDamage
            // 
            this.drawDamage.AutoSize = true;
            this.drawDamage.Enabled = false;
            this.drawDamage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drawDamage.Location = new System.Drawing.Point(1067, 172);
            this.drawDamage.Name = "drawDamage";
            this.drawDamage.Size = new System.Drawing.Size(80, 20);
            this.drawDamage.TabIndex = 8;
            this.drawDamage.Text = "Damage";
            this.drawDamage.UseVisualStyleBackColor = true;
            // 
            // drawMonsterZones
            // 
            this.drawMonsterZones.AutoSize = true;
            this.drawMonsterZones.Enabled = false;
            this.drawMonsterZones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drawMonsterZones.Location = new System.Drawing.Point(1067, 198);
            this.drawMonsterZones.Name = "drawMonsterZones";
            this.drawMonsterZones.Size = new System.Drawing.Size(108, 20);
            this.drawMonsterZones.TabIndex = 9;
            this.drawMonsterZones.Text = "Combat Zone";
            this.drawMonsterZones.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(840, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "Enable Drawing:";
            // 
            // drawGround
            // 
            this.drawGround.AutoSize = true;
            this.drawGround.Enabled = false;
            this.drawGround.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drawGround.Location = new System.Drawing.Point(959, 144);
            this.drawGround.Name = "drawGround";
            this.drawGround.Size = new System.Drawing.Size(71, 20);
            this.drawGround.TabIndex = 4;
            this.drawGround.Text = "Ground";
            this.drawGround.UseVisualStyleBackColor = true;
            // 
            // drawForeground
            // 
            this.drawForeground.AutoSize = true;
            this.drawForeground.Enabled = false;
            this.drawForeground.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drawForeground.Location = new System.Drawing.Point(959, 172);
            this.drawForeground.Name = "drawForeground";
            this.drawForeground.Size = new System.Drawing.Size(99, 20);
            this.drawForeground.TabIndex = 5;
            this.drawForeground.Text = "ForeGround";
            this.drawForeground.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.drawForeground.UseVisualStyleBackColor = true;
            // 
            // mapTabControl
            // 
            this.mapTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.mapTabControl.Location = new System.Drawing.Point(0, 24);
            this.mapTabControl.Name = "mapTabControl";
            this.mapTabControl.SelectedIndex = 0;
            this.mapTabControl.Size = new System.Drawing.Size(832, 656);
            this.mapTabControl.TabIndex = 15;
            // 
            // contentTabControl
            // 
            this.contentTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.contentTabControl.Location = new System.Drawing.Point(838, 232);
            this.contentTabControl.Name = "contentTabControl";
            this.contentTabControl.SelectedIndex = 0;
            this.contentTabControl.Size = new System.Drawing.Size(426, 448);
            this.contentTabControl.TabIndex = 16;
            // 
            // gameContentFolderBrowser
            // 
            this.gameContentFolderBrowser.ShowNewFolderButton = false;
            // 
            // drawPortal
            // 
            this.drawPortal.AutoSize = true;
            this.drawPortal.Enabled = false;
            this.drawPortal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drawPortal.Location = new System.Drawing.Point(1178, 144);
            this.drawPortal.Name = "drawPortal";
            this.drawPortal.Size = new System.Drawing.Size(62, 20);
            this.drawPortal.TabIndex = 7;
            this.drawPortal.Text = "Portal";
            this.drawPortal.UseVisualStyleBackColor = true;
            // 
            // drawNPC
            // 
            this.drawNPC.AutoSize = true;
            this.drawNPC.Enabled = false;
            this.drawNPC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drawNPC.Location = new System.Drawing.Point(1178, 172);
            this.drawNPC.Name = "drawNPC";
            this.drawNPC.Size = new System.Drawing.Size(62, 20);
            this.drawNPC.TabIndex = 7;
            this.drawNPC.Text = "NPCs";
            this.drawNPC.UseVisualStyleBackColor = true;
            // 
            // drawCutscene
            // 
            this.drawCutscene.AutoSize = true;
            this.drawCutscene.Enabled = false;
            this.drawCutscene.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drawCutscene.Location = new System.Drawing.Point(1178, 198);
            this.drawCutscene.Name = "drawCutscene";
            this.drawCutscene.Size = new System.Drawing.Size(92, 20);
            this.drawCutscene.TabIndex = 7;
            this.drawCutscene.Text = "CutScenes";
            this.drawCutscene.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.contentTabControl);
            this.Controls.Add(this.mapTabControl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.drawForeground);
            this.Controls.Add(this.drawMonsterZones);
            this.Controls.Add(this.drawCutscene);
            this.Controls.Add(this.drawNPC);
            this.Controls.Add(this.drawPortal);
            this.Controls.Add(this.drawCollision);
            this.Controls.Add(this.drawGround);
            this.Controls.Add(this.drawDamage);
            this.Controls.Add(this.drawFringe);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.damageTextBox);
            this.Controls.Add(this.collisionTextBox);
            this.Controls.Add(this.damageModifier);
            this.Controls.Add(this.collisionModifier);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.layerDropDown);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Quest World Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.collisionModifier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.damageModifier)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMapToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openMapFileDialog;
        private System.Windows.Forms.ToolStripMenuItem saveMapToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveMapFileDialog;
        private System.Windows.Forms.ComboBox layerDropDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar collisionModifier;
        private System.Windows.Forms.TrackBar damageModifier;
        private System.Windows.Forms.TextBox collisionTextBox;
        private System.Windows.Forms.TextBox damageTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox drawFringe;
        private System.Windows.Forms.CheckBox drawCollision;
        private System.Windows.Forms.CheckBox drawDamage;
        private System.Windows.Forms.CheckBox drawMonsterZones;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuZoom25;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuZoom50;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuZoom75;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuZoom100;
        private System.Windows.Forms.ToolStripMenuItem layerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearCurrentToolStripMenuItem;
        private System.Windows.Forms.CheckBox drawGround;
        private System.Windows.Forms.CheckBox drawForeground;
        private System.Windows.Forms.ToolStripMenuItem contentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nPCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nPCToolStripMenuItem1;
        private System.Windows.Forms.TabControl mapTabControl;
        private System.Windows.Forms.TabControl contentTabControl;
        private System.Windows.Forms.ToolStripMenuItem closeMapToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog gameContentFolderBrowser;
        private System.Windows.Forms.ToolStripMenuItem appearanceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contentTileOutlineToolStripMenuItem;
        private System.Windows.Forms.ColorDialog colorDialog1;
        internal System.Windows.Forms.ToolStripTextBox currentMapTile;
        private System.Windows.Forms.ToolStripMenuItem mapGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapGridColorToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem portalToolStripMenuItem;
        private System.Windows.Forms.CheckBox drawPortal;
        private System.Windows.Forms.ToolStripMenuItem portalToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fillCombatLayerToolStripMenuItem;
        private System.Windows.Forms.CheckBox drawNPC;
        private System.Windows.Forms.CheckBox drawCutscene;
    }
}

