namespace QuestWorldEditor
{
    partial class CreatePortalForm
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
            this.destinationName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.destinationX = new System.Windows.Forms.TextBox();
            this.destinationY = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.soundEffect = new System.Windows.Forms.TextBox();
            this.dest = new System.Windows.Forms.Label();
            this.destinationDirection = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Destination Map Name:";
            // 
            // destinationName
            // 
            this.destinationName.Location = new System.Drawing.Point(137, 19);
            this.destinationName.Name = "destinationName";
            this.destinationName.Size = new System.Drawing.Size(208, 20);
            this.destinationName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Destination TileX:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Destination TileY:";
            // 
            // destinationX
            // 
            this.destinationX.Location = new System.Drawing.Point(137, 54);
            this.destinationX.Name = "destinationX";
            this.destinationX.Size = new System.Drawing.Size(100, 20);
            this.destinationX.TabIndex = 1;
            // 
            // destinationY
            // 
            this.destinationY.Location = new System.Drawing.Point(137, 89);
            this.destinationY.Name = "destinationY";
            this.destinationY.Size = new System.Drawing.Size(100, 20);
            this.destinationY.TabIndex = 2;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(93, 233);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(201, 233);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Sound Effect:";
            // 
            // soundEffect
            // 
            this.soundEffect.Location = new System.Drawing.Point(137, 124);
            this.soundEffect.Name = "soundEffect";
            this.soundEffect.Size = new System.Drawing.Size(208, 20);
            this.soundEffect.TabIndex = 3;
            // 
            // dest
            // 
            this.dest.AutoSize = true;
            this.dest.Location = new System.Drawing.Point(13, 162);
            this.dest.Name = "dest";
            this.dest.Size = new System.Drawing.Size(108, 13);
            this.dest.TabIndex = 2;
            this.dest.Text = "Destination Direction:";
            // 
            // destinationDirection
            // 
            this.destinationDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.destinationDirection.FormattingEnabled = true;
            this.destinationDirection.Items.AddRange(new object[] {
            "N/A",
            "North",
            "East",
            "South",
            "West"});
            this.destinationDirection.Location = new System.Drawing.Point(137, 159);
            this.destinationDirection.Name = "destinationDirection";
            this.destinationDirection.Size = new System.Drawing.Size(139, 21);
            this.destinationDirection.TabIndex = 4;
            // 
            // CreatePortalForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(386, 268);
            this.Controls.Add(this.destinationDirection);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.soundEffect);
            this.Controls.Add(this.dest);
            this.Controls.Add(this.destinationY);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.destinationX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.destinationName);
            this.Controls.Add(this.label1);
            this.Name = "CreatePortalForm";
            this.Text = "CreatePortal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox destinationName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox destinationX;
        private System.Windows.Forms.TextBox destinationY;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox soundEffect;
        private System.Windows.Forms.Label dest;
        private System.Windows.Forms.ComboBox destinationDirection;
    }
}