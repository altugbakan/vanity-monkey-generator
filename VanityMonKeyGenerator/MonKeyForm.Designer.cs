
namespace VanityMonKeyGenerator
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
            this.GetMonKeyButton = new System.Windows.Forms.Button();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.addressLabel = new System.Windows.Forms.Label();
            this.seedLabel = new System.Windows.Forms.Label();
            this.seedTextBox = new System.Windows.Forms.TextBox();
            this.monKeyPictureBox = new System.Windows.Forms.PictureBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.monKeyPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // GetMonKeyButton
            // 
            this.GetMonKeyButton.Location = new System.Drawing.Point(144, 267);
            this.GetMonKeyButton.Name = "GetMonKeyButton";
            this.GetMonKeyButton.Size = new System.Drawing.Size(85, 23);
            this.GetMonKeyButton.TabIndex = 0;
            this.GetMonKeyButton.Text = "Get MonKey";
            this.GetMonKeyButton.UseVisualStyleBackColor = true;
            this.GetMonKeyButton.Click += new System.EventHandler(this.GetMonKeyButton_Click);
            // 
            // addressTextBox
            // 
            this.addressTextBox.Location = new System.Drawing.Point(13, 160);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.Size = new System.Drawing.Size(346, 23);
            this.addressTextBox.TabIndex = 1;
            // 
            // addressLabel
            // 
            this.addressLabel.AutoSize = true;
            this.addressLabel.Location = new System.Drawing.Point(13, 139);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Size = new System.Drawing.Size(49, 15);
            this.addressLabel.TabIndex = 2;
            this.addressLabel.Text = "Address";
            // 
            // seedLabel
            // 
            this.seedLabel.AutoSize = true;
            this.seedLabel.Location = new System.Drawing.Point(13, 195);
            this.seedLabel.Name = "seedLabel";
            this.seedLabel.Size = new System.Drawing.Size(32, 15);
            this.seedLabel.TabIndex = 4;
            this.seedLabel.Text = "Seed";
            // 
            // seedTextBox
            // 
            this.seedTextBox.Location = new System.Drawing.Point(13, 216);
            this.seedTextBox.Name = "seedTextBox";
            this.seedTextBox.Size = new System.Drawing.Size(346, 23);
            this.seedTextBox.TabIndex = 3;
            // 
            // monKeyPictureBox
            // 
            this.monKeyPictureBox.Location = new System.Drawing.Point(124, 12);
            this.monKeyPictureBox.Name = "monKeyPictureBox";
            this.monKeyPictureBox.Size = new System.Drawing.Size(125, 125);
            this.monKeyPictureBox.TabIndex = 5;
            this.monKeyPictureBox.TabStop = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(259, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(100, 96);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            // 
            // MonKeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 317);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.monKeyPictureBox);
            this.Controls.Add(this.seedLabel);
            this.Controls.Add(this.seedTextBox);
            this.Controls.Add(this.addressLabel);
            this.Controls.Add(this.addressTextBox);
            this.Controls.Add(this.GetMonKeyButton);
            this.Name = "MonKeyForm";
            this.Text = "Vanity MonKey Generator";
            ((System.ComponentModel.ISupportInitialize)(this.monKeyPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GetMonKeyButton;
        private System.Windows.Forms.TextBox addressTextBox;
        private System.Windows.Forms.Label addressLabel;
        private System.Windows.Forms.Label seedLabel;
        private System.Windows.Forms.TextBox seedTextBox;
        private System.Windows.Forms.PictureBox monKeyPictureBox;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

