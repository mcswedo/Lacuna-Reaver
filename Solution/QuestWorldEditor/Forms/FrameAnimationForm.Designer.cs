namespace QuestWorldEditor
{
    partial class FrameAnimationForm
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
            this.label6 = new System.Windows.Forms.Label();
            this.animationName = new System.Windows.Forms.TextBox();
            this.xoffset = new System.Windows.Forms.TextBox();
            this.yoffset = new System.Windows.Forms.TextBox();
            this.frameCount = new System.Windows.Forms.TextBox();
            this.frameWidth = new System.Windows.Forms.TextBox();
            this.frameHeight = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.frameDelay = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Animation Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "X-Offset into SpriteSheet:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Y-Offset into SpriteSheet:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Number Of Frames:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(75, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Frame Width:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(72, 201);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Frame Height:";
            // 
            // animationName
            // 
            this.animationName.Location = new System.Drawing.Point(151, 26);
            this.animationName.MaxLength = 38;
            this.animationName.Name = "animationName";
            this.animationName.Size = new System.Drawing.Size(160, 20);
            this.animationName.TabIndex = 1;
            // 
            // xoffset
            // 
            this.xoffset.Location = new System.Drawing.Point(151, 60);
            this.xoffset.MaxLength = 3;
            this.xoffset.Name = "xoffset";
            this.xoffset.Size = new System.Drawing.Size(38, 20);
            this.xoffset.TabIndex = 2;
            // 
            // yoffset
            // 
            this.yoffset.Location = new System.Drawing.Point(151, 94);
            this.yoffset.MaxLength = 3;
            this.yoffset.Name = "yoffset";
            this.yoffset.Size = new System.Drawing.Size(38, 20);
            this.yoffset.TabIndex = 3;
            // 
            // frameCount
            // 
            this.frameCount.Location = new System.Drawing.Point(151, 128);
            this.frameCount.MaxLength = 3;
            this.frameCount.Name = "frameCount";
            this.frameCount.Size = new System.Drawing.Size(38, 20);
            this.frameCount.TabIndex = 4;
            // 
            // frameWidth
            // 
            this.frameWidth.Location = new System.Drawing.Point(151, 162);
            this.frameWidth.MaxLength = 3;
            this.frameWidth.Name = "frameWidth";
            this.frameWidth.Size = new System.Drawing.Size(38, 20);
            this.frameWidth.TabIndex = 5;
            // 
            // frameHeight
            // 
            this.frameHeight.Location = new System.Drawing.Point(151, 196);
            this.frameHeight.MaxLength = 3;
            this.frameHeight.Name = "frameHeight";
            this.frameHeight.Size = new System.Drawing.Size(38, 20);
            this.frameHeight.TabIndex = 6;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(90, 292);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 8;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(190, 292);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(72, 236);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Frame Delay:";
            // 
            // frameDelay
            // 
            this.frameDelay.Location = new System.Drawing.Point(151, 230);
            this.frameDelay.MaxLength = 5;
            this.frameDelay.Name = "frameDelay";
            this.frameDelay.Size = new System.Drawing.Size(55, 20);
            this.frameDelay.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(212, 237);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "(seconds)";
            // 
            // FrameAnimationForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(354, 327);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.frameDelay);
            this.Controls.Add(this.frameHeight);
            this.Controls.Add(this.frameWidth);
            this.Controls.Add(this.frameCount);
            this.Controls.Add(this.yoffset);
            this.Controls.Add(this.xoffset);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.animationName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FrameAnimationForm";
            this.Text = "NewFrameAnimation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox animationName;
        private System.Windows.Forms.TextBox xoffset;
        private System.Windows.Forms.TextBox yoffset;
        private System.Windows.Forms.TextBox frameCount;
        private System.Windows.Forms.TextBox frameWidth;
        private System.Windows.Forms.TextBox frameHeight;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox frameDelay;
        private System.Windows.Forms.Label label8;
    }
}