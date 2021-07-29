
namespace GUI
{
    partial class MonKeyForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonKeyForm));
            this.getRandomMonKeyButton = new System.Windows.Forms.Button();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.addressLabel = new System.Windows.Forms.Label();
            this.seedLabel = new System.Windows.Forms.Label();
            this.seedTextBox = new System.Windows.Forms.TextBox();
            this.monKeyPictureBox = new System.Windows.Forms.PictureBox();
            this.settingsButton = new System.Windows.Forms.Button();
            this.findSpecificMonKeyButton = new System.Windows.Forms.Button();
            this.searchedLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.monKeyPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // getRandomMonKeyButton
            // 
            this.getRandomMonKeyButton.Location = new System.Drawing.Point(12, 337);
            this.getRandomMonKeyButton.Name = "getRandomMonKeyButton";
            this.getRandomMonKeyButton.Size = new System.Drawing.Size(155, 23);
            this.getRandomMonKeyButton.TabIndex = 0;
            this.getRandomMonKeyButton.Text = "Get Random MonKey";
            this.getRandomMonKeyButton.UseVisualStyleBackColor = true;
            this.getRandomMonKeyButton.Click += new System.EventHandler(this.GetRandomMonKeyButton_Click);
            // 
            // addressTextBox
            // 
            this.addressTextBox.Location = new System.Drawing.Point(12, 222);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.Size = new System.Drawing.Size(346, 23);
            this.addressTextBox.TabIndex = 1;
            // 
            // addressLabel
            // 
            this.addressLabel.AutoSize = true;
            this.addressLabel.Location = new System.Drawing.Point(12, 201);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Size = new System.Drawing.Size(49, 15);
            this.addressLabel.TabIndex = 2;
            this.addressLabel.Text = "Address";
            // 
            // seedLabel
            // 
            this.seedLabel.AutoSize = true;
            this.seedLabel.Location = new System.Drawing.Point(12, 257);
            this.seedLabel.Name = "seedLabel";
            this.seedLabel.Size = new System.Drawing.Size(32, 15);
            this.seedLabel.TabIndex = 4;
            this.seedLabel.Text = "Seed";
            // 
            // seedTextBox
            // 
            this.seedTextBox.Location = new System.Drawing.Point(12, 278);
            this.seedTextBox.Name = "seedTextBox";
            this.seedTextBox.Size = new System.Drawing.Size(346, 23);
            this.seedTextBox.TabIndex = 3;
            // 
            // monKeyPictureBox
            // 
            this.monKeyPictureBox.Location = new System.Drawing.Point(85, 0);
            this.monKeyPictureBox.Name = "monKeyPictureBox";
            this.monKeyPictureBox.Size = new System.Drawing.Size(200, 200);
            this.monKeyPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.monKeyPictureBox.TabIndex = 5;
            this.monKeyPictureBox.TabStop = false;
            // 
            // settingsButton
            // 
            this.settingsButton.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.settingsButton.Image = ((System.Drawing.Image)(resources.GetObject("settingsButton.Image")));
            this.settingsButton.Location = new System.Drawing.Point(330, 11);
            this.settingsButton.Margin = new System.Windows.Forms.Padding(2);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(30, 30);
            this.settingsButton.TabIndex = 8;
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // findSpecificMonKeyButton
            // 
            this.findSpecificMonKeyButton.Location = new System.Drawing.Point(203, 337);
            this.findSpecificMonKeyButton.Name = "findSpecificMonKeyButton";
            this.findSpecificMonKeyButton.Size = new System.Drawing.Size(155, 23);
            this.findSpecificMonKeyButton.TabIndex = 9;
            this.findSpecificMonKeyButton.Text = "Find Specific MonKey";
            this.findSpecificMonKeyButton.UseVisualStyleBackColor = true;
            this.findSpecificMonKeyButton.Click += new System.EventHandler(this.FindSpecificMonKeyButton_Click);
            // 
            // searchedLabel
            // 
            this.searchedLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.searchedLabel.Location = new System.Drawing.Point(15, 317);
            this.searchedLabel.Name = "searchedLabel";
            this.searchedLabel.Size = new System.Drawing.Size(344, 20);
            this.searchedLabel.TabIndex = 10;
            this.searchedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MonKeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 368);
            this.Controls.Add(this.searchedLabel);
            this.Controls.Add(this.findSpecificMonKeyButton);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.monKeyPictureBox);
            this.Controls.Add(this.seedLabel);
            this.Controls.Add(this.seedTextBox);
            this.Controls.Add(this.addressLabel);
            this.Controls.Add(this.addressTextBox);
            this.Controls.Add(this.getRandomMonKeyButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MonKeyForm";
            this.Text = "Vanity MonKey Generator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MonKeyForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.monKeyPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button getRandomMonKeyButton;
        private System.Windows.Forms.TextBox addressTextBox;
        private System.Windows.Forms.Label addressLabel;
        private System.Windows.Forms.Label seedLabel;
        private System.Windows.Forms.TextBox seedTextBox;
        private System.Windows.Forms.PictureBox monKeyPictureBox;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.Button findSpecificMonKeyButton;
        private System.Windows.Forms.Label searchedLabel;
    }
}

