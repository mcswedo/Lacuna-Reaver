namespace QuestWorldEditor
{
    partial class SpriteAnimationForm
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
            this.animationName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.openTextureFileButton = new System.Windows.Forms.Button();
            this.animationList = new System.Windows.Forms.ListBox();
            this.addAnimation = new System.Windows.Forms.Button();
            this.removeAnimation = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.textureName = new System.Windows.Forms.TextBox();
            this.copyButton = new System.Windows.Forms.Button();
            this.modifyButton = new System.Windows.Forms.Button();
            this.openTextureFile = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sprite Animation Name:";
            // 
            // animationName
            // 
            this.animationName.Location = new System.Drawing.Point(132, 16);
            this.animationName.Name = "animationName";
            this.animationName.Size = new System.Drawing.Size(170, 20);
            this.animationName.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Texture Filename";
            // 
            // openTextureFileButton
            // 
            this.openTextureFileButton.Location = new System.Drawing.Point(103, 61);
            this.openTextureFileButton.Name = "openTextureFileButton";
            this.openTextureFileButton.Size = new System.Drawing.Size(26, 23);
            this.openTextureFileButton.TabIndex = 1;
            this.openTextureFileButton.Text = "...";
            this.openTextureFileButton.UseVisualStyleBackColor = true;
            this.openTextureFileButton.Click += new System.EventHandler(this.openTextureFileButton_Click);
            // 
            // animationList
            // 
            this.animationList.FormattingEnabled = true;
            this.animationList.Location = new System.Drawing.Point(12, 104);
            this.animationList.Name = "animationList";
            this.animationList.Size = new System.Drawing.Size(183, 186);
            this.animationList.TabIndex = 8;
            // 
            // addAnimation
            // 
            this.addAnimation.Location = new System.Drawing.Point(201, 113);
            this.addAnimation.Name = "addAnimation";
            this.addAnimation.Size = new System.Drawing.Size(85, 23);
            this.addAnimation.TabIndex = 2;
            this.addAnimation.Text = "Add";
            this.addAnimation.UseVisualStyleBackColor = true;
            // 
            // removeAnimation
            // 
            this.removeAnimation.Location = new System.Drawing.Point(201, 158);
            this.removeAnimation.Name = "removeAnimation";
            this.removeAnimation.Size = new System.Drawing.Size(85, 23);
            this.removeAnimation.TabIndex = 3;
            this.removeAnimation.Text = "Remove";
            this.removeAnimation.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(76, 326);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(174, 326);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // textureName
            // 
            this.textureName.Location = new System.Drawing.Point(139, 61);
            this.textureName.Name = "textureName";
            this.textureName.ReadOnly = true;
            this.textureName.Size = new System.Drawing.Size(173, 20);
            this.textureName.TabIndex = 8;
            // 
            // copyButton
            // 
            this.copyButton.Location = new System.Drawing.Point(201, 203);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(85, 23);
            this.copyButton.TabIndex = 4;
            this.copyButton.Text = "Copy";
            this.copyButton.UseVisualStyleBackColor = true;
            // 
            // modifyButton
            // 
            this.modifyButton.Location = new System.Drawing.Point(201, 248);
            this.modifyButton.Name = "modifyButton";
            this.modifyButton.Size = new System.Drawing.Size(85, 23);
            this.modifyButton.TabIndex = 5;
            this.modifyButton.Text = "Modify";
            this.modifyButton.UseVisualStyleBackColor = true;
            // 
            // openTextureFile
            // 
            this.openTextureFile.FileName = "openTexture";
            // 
            // SpriteAnimationForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(324, 362);
            this.Controls.Add(this.textureName);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.modifyButton);
            this.Controls.Add(this.copyButton);
            this.Controls.Add(this.removeAnimation);
            this.Controls.Add(this.addAnimation);
            this.Controls.Add(this.animationList);
            this.Controls.Add(this.openTextureFileButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.animationName);
            this.Controls.Add(this.label1);
            this.Name = "SpriteAnimationForm";
            this.Text = "NewSpriteAnimation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox animationName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button openTextureFileButton;
        private System.Windows.Forms.ListBox animationList;
        private System.Windows.Forms.Button addAnimation;
        private System.Windows.Forms.Button removeAnimation;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox textureName;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.Button modifyButton;
        private System.Windows.Forms.OpenFileDialog openTextureFile;
    }
}