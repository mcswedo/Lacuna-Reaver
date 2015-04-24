namespace QuestWorldEditor
{
    partial class CreateNPCForm
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
            this.idleOnly = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.npcName = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.idleAnimationButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.walkingAnimationButton = new System.Windows.Forms.Button();
            this.previewIdleAnimation = new System.Windows.Forms.Button();
            this.previewWalkingAnimation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Name of NPC:";
            // 
            // idleOnly
            // 
            this.idleOnly.AutoSize = true;
            this.idleOnly.Location = new System.Drawing.Point(15, 65);
            this.idleOnly.Name = "idleOnly";
            this.idleOnly.Size = new System.Drawing.Size(67, 17);
            this.idleOnly.TabIndex = 2;
            this.idleOnly.Text = "Idle Only";
            this.idleOnly.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(77, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 3;
            // 
            // npcName
            // 
            this.npcName.Location = new System.Drawing.Point(103, 23);
            this.npcName.MaxLength = 38;
            this.npcName.Name = "npcName";
            this.npcName.Size = new System.Drawing.Size(179, 20);
            this.npcName.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(118, 202);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 7;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(217, 202);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Idle Animation:";
            // 
            // idleAnimationButton
            // 
            this.idleAnimationButton.Location = new System.Drawing.Point(113, 106);
            this.idleAnimationButton.Name = "idleAnimationButton";
            this.idleAnimationButton.Size = new System.Drawing.Size(27, 23);
            this.idleAnimationButton.TabIndex = 3;
            this.idleAnimationButton.Text = "...";
            this.idleAnimationButton.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(285, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "(38 Character Limit)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Walking Animation:";
            // 
            // walkingAnimationButton
            // 
            this.walkingAnimationButton.Location = new System.Drawing.Point(113, 141);
            this.walkingAnimationButton.Name = "walkingAnimationButton";
            this.walkingAnimationButton.Size = new System.Drawing.Size(27, 23);
            this.walkingAnimationButton.TabIndex = 5;
            this.walkingAnimationButton.Text = "...";
            this.walkingAnimationButton.UseVisualStyleBackColor = true;
            // 
            // previewIdleAnimation
            // 
            this.previewIdleAnimation.Location = new System.Drawing.Point(166, 106);
            this.previewIdleAnimation.Name = "previewIdleAnimation";
            this.previewIdleAnimation.Size = new System.Drawing.Size(75, 23);
            this.previewIdleAnimation.TabIndex = 4;
            this.previewIdleAnimation.Text = "Preview";
            this.previewIdleAnimation.UseVisualStyleBackColor = true;
            // 
            // previewWalkingAnimation
            // 
            this.previewWalkingAnimation.Location = new System.Drawing.Point(166, 141);
            this.previewWalkingAnimation.Name = "previewWalkingAnimation";
            this.previewWalkingAnimation.Size = new System.Drawing.Size(75, 23);
            this.previewWalkingAnimation.TabIndex = 6;
            this.previewWalkingAnimation.Text = "Preview";
            this.previewWalkingAnimation.UseVisualStyleBackColor = true;
            // 
            // CreateNPCForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(410, 241);
            this.Controls.Add(this.previewWalkingAnimation);
            this.Controls.Add(this.previewIdleAnimation);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.walkingAnimationButton);
            this.Controls.Add(this.idleAnimationButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.npcName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.idleOnly);
            this.Controls.Add(this.label1);
            this.Name = "CreateNPCForm";
            this.Text = "CreateNPCForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox idleOnly;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox npcName;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button idleAnimationButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button walkingAnimationButton;
        private System.Windows.Forms.Button previewIdleAnimation;
        private System.Windows.Forms.Button previewWalkingAnimation;
    }
}