namespace QuestWorldEditor
{
    partial class CreateMapForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tileWidth = new System.Windows.Forms.TextBox();
            this.tileHeight = new System.Windows.Forms.TextBox();
            this.widthInTiles = new System.Windows.Forms.TextBox();
            this.heightInTiles = new System.Windows.Forms.TextBox();
            this.textureButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.mapName = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.texturePath = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tile Width:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tile Height:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Width In Tiles:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 221);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Height In Tiles:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Tile Sheet:";
            // 
            // tileWidth
            // 
            this.tileWidth.Location = new System.Drawing.Point(135, 101);
            this.tileWidth.MaxLength = 3;
            this.tileWidth.Name = "tileWidth";
            this.tileWidth.Size = new System.Drawing.Size(36, 20);
            this.tileWidth.TabIndex = 2;
            // 
            // tileHeight
            // 
            this.tileHeight.Location = new System.Drawing.Point(135, 140);
            this.tileHeight.MaxLength = 3;
            this.tileHeight.Name = "tileHeight";
            this.tileHeight.Size = new System.Drawing.Size(36, 20);
            this.tileHeight.TabIndex = 3;
            // 
            // widthInTiles
            // 
            this.widthInTiles.Location = new System.Drawing.Point(135, 179);
            this.widthInTiles.MaxLength = 3;
            this.widthInTiles.Name = "widthInTiles";
            this.widthInTiles.Size = new System.Drawing.Size(36, 20);
            this.widthInTiles.TabIndex = 4;
            // 
            // heightInTiles
            // 
            this.heightInTiles.Location = new System.Drawing.Point(135, 218);
            this.heightInTiles.MaxLength = 3;
            this.heightInTiles.Name = "heightInTiles";
            this.heightInTiles.Size = new System.Drawing.Size(36, 20);
            this.heightInTiles.TabIndex = 5;
            // 
            // textureButton
            // 
            this.textureButton.Location = new System.Drawing.Point(335, 60);
            this.textureButton.Name = "textureButton";
            this.textureButton.Size = new System.Drawing.Size(26, 23);
            this.textureButton.TabIndex = 1;
            this.textureButton.Text = "...";
            this.textureButton.UseVisualStyleBackColor = true;
            this.textureButton.Click += new System.EventHandler(this.textureButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(45, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Map Name:";
            // 
            // mapName
            // 
            this.mapName.Location = new System.Drawing.Point(135, 23);
            this.mapName.MaxLength = 38;
            this.mapName.Name = "mapName";
            this.mapName.Size = new System.Drawing.Size(156, 20);
            this.mapName.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(91, 275);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(206, 275);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // texturePath
            // 
            this.texturePath.Location = new System.Drawing.Point(135, 62);
            this.texturePath.MaxLength = 38;
            this.texturePath.Name = "texturePath";
            this.texturePath.ReadOnly = true;
            this.texturePath.Size = new System.Drawing.Size(194, 20);
            this.texturePath.TabIndex = 13;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // CreateMapForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(373, 311);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.textureButton);
            this.Controls.Add(this.heightInTiles);
            this.Controls.Add(this.widthInTiles);
            this.Controls.Add(this.tileHeight);
            this.Controls.Add(this.texturePath);
            this.Controls.Add(this.mapName);
            this.Controls.Add(this.tileWidth);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Name = "CreateMapForm";
            this.Text = "CreateMap";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tileWidth;
        private System.Windows.Forms.TextBox tileHeight;
        private System.Windows.Forms.TextBox widthInTiles;
        private System.Windows.Forms.TextBox heightInTiles;
        private System.Windows.Forms.Button textureButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox mapName;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox texturePath;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}