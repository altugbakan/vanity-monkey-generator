
namespace GUI
{
    partial class ExpertSettings
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
            this.glassesLabel = new System.Windows.Forms.Label();
            this.hatsLabel = new System.Windows.Forms.Label();
            this.miscLabel = new System.Windows.Forms.Label();
            this.mouthsLabel = new System.Windows.Forms.Label();
            this.shirtsPantsLabel = new System.Windows.Forms.Label();
            this.shoesLabel = new System.Windows.Forms.Label();
            this.tailsLabel = new System.Windows.Forms.Label();
            this.switchButton = new System.Windows.Forms.Button();
            this.hatsCheckedListBox = new GUI.BetterCheckedListBox();
            this.mouthsCheckedListBox = new GUI.BetterCheckedListBox();
            this.shirtsPantsCheckedListBox = new GUI.BetterCheckedListBox();
            this.shoesCheckedListBox = new GUI.BetterCheckedListBox();
            this.tailsCheckedListBox = new GUI.BetterCheckedListBox();
            this.glassesCheckedListBox = new GUI.BetterCheckedListBox();
            this.miscCheckedListBox = new GUI.BetterCheckedListBox();
            this.rarityLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.requestAmountSlider = new System.Windows.Forms.TrackBar();
            this.requestAmountLabel = new System.Windows.Forms.Label();
            this.requestAmountNumeric = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.requestAmountSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.requestAmountNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // glassesLabel
            // 
            this.glassesLabel.AutoSize = true;
            this.glassesLabel.Location = new System.Drawing.Point(440, 47);
            this.glassesLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.glassesLabel.Name = "glassesLabel";
            this.glassesLabel.Size = new System.Drawing.Size(45, 15);
            this.glassesLabel.TabIndex = 7;
            this.glassesLabel.Text = "Glasses";
            // 
            // hatsLabel
            // 
            this.hatsLabel.AutoSize = true;
            this.hatsLabel.Location = new System.Drawing.Point(77, 11);
            this.hatsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.hatsLabel.Name = "hatsLabel";
            this.hatsLabel.Size = new System.Drawing.Size(31, 15);
            this.hatsLabel.TabIndex = 9;
            this.hatsLabel.Text = "Hats";
            // 
            // miscLabel
            // 
            this.miscLabel.AutoSize = true;
            this.miscLabel.Location = new System.Drawing.Point(448, 273);
            this.miscLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.miscLabel.Name = "miscLabel";
            this.miscLabel.Size = new System.Drawing.Size(32, 15);
            this.miscLabel.TabIndex = 11;
            this.miscLabel.Text = "Misc";
            // 
            // mouthsLabel
            // 
            this.mouthsLabel.AutoSize = true;
            this.mouthsLabel.Location = new System.Drawing.Point(255, 11);
            this.mouthsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mouthsLabel.Name = "mouthsLabel";
            this.mouthsLabel.Size = new System.Drawing.Size(48, 15);
            this.mouthsLabel.TabIndex = 13;
            this.mouthsLabel.Text = "Mouths";
            // 
            // shirtsPantsLabel
            // 
            this.shirtsPantsLabel.AutoSize = true;
            this.shirtsPantsLabel.Location = new System.Drawing.Point(244, 185);
            this.shirtsPantsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.shirtsPantsLabel.Name = "shirtsPantsLabel";
            this.shirtsPantsLabel.Size = new System.Drawing.Size(70, 15);
            this.shirtsPantsLabel.TabIndex = 15;
            this.shirtsPantsLabel.Text = "Shirts-Pants";
            // 
            // shoesLabel
            // 
            this.shoesLabel.AutoSize = true;
            this.shoesLabel.Location = new System.Drawing.Point(260, 339);
            this.shoesLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.shoesLabel.Name = "shoesLabel";
            this.shoesLabel.Size = new System.Drawing.Size(38, 15);
            this.shoesLabel.TabIndex = 17;
            this.shoesLabel.Text = "Shoes";
            // 
            // tailsLabel
            // 
            this.tailsLabel.AutoSize = true;
            this.tailsLabel.Location = new System.Drawing.Point(265, 489);
            this.tailsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tailsLabel.Name = "tailsLabel";
            this.tailsLabel.Size = new System.Drawing.Size(29, 15);
            this.tailsLabel.TabIndex = 19;
            this.tailsLabel.Text = "Tails";
            // 
            // switchButton
            // 
            this.switchButton.Location = new System.Drawing.Point(406, 12);
            this.switchButton.Name = "switchButton";
            this.switchButton.Size = new System.Drawing.Size(139, 23);
            this.switchButton.TabIndex = 20;
            this.switchButton.Text = "Switch to Simple Mode";
            this.switchButton.UseVisualStyleBackColor = true;
            this.switchButton.Click += new System.EventHandler(this.SwitchButton_Click);
            // 
            // hatsCheckedListBox
            // 
            this.hatsCheckedListBox.CheckOnClick = true;
            this.hatsCheckedListBox.FormattingEnabled = true;
            this.hatsCheckedListBox.Items.AddRange(new object[] {
            "None",
            "Bandana",
            "Beanie",
            "Beanie Banano",
            "Beanie Hippie",
            "Beanie Long",
            "Beanie Long Banano",
            "Cap",
            "Cap Backwards",
            "Cap Banano",
            "Cap Bebe",
            "Cap Carlos",
            "Cap Hng",
            "Cap Hng Plus",
            "Cap Kappa",
            "Cap Pepe",
            "Cap Rick",
            "Cap Smug",
            "Cap Smug Green",
            "Cap Thonk",
            "Crown",
            "Fedora",
            "Fedora Long",
            "Hat Cowboy",
            "Hat Jester",
            "Helmet Viking"});
            this.hatsCheckedListBox.Location = new System.Drawing.Point(12, 27);
            this.hatsCheckedListBox.Name = "hatsCheckedListBox";
            this.hatsCheckedListBox.Size = new System.Drawing.Size(165, 472);
            this.hatsCheckedListBox.TabIndex = 22;
            this.hatsCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CheckedListBox_ItemCheck);
            // 
            // mouthsCheckedListBox
            // 
            this.mouthsCheckedListBox.CheckOnClick = true;
            this.mouthsCheckedListBox.FormattingEnabled = true;
            this.mouthsCheckedListBox.Items.AddRange(new object[] {
            "Cigar",
            "Confused",
            "Joint",
            "Meh",
            "Pipe",
            "Smile Big Teeth",
            "Smile Normal",
            "Smile Tongue"});
            this.mouthsCheckedListBox.Location = new System.Drawing.Point(197, 27);
            this.mouthsCheckedListBox.Name = "mouthsCheckedListBox";
            this.mouthsCheckedListBox.Size = new System.Drawing.Size(165, 148);
            this.mouthsCheckedListBox.TabIndex = 23;
            this.mouthsCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CheckedListBox_ItemCheck);
            // 
            // shirtsPantsCheckedListBox
            // 
            this.shirtsPantsCheckedListBox.CheckOnClick = true;
            this.shirtsPantsCheckedListBox.FormattingEnabled = true;
            this.shirtsPantsCheckedListBox.Items.AddRange(new object[] {
            "None",
            "Overalls Blue",
            "Overalls Red",
            "Pants Business Blue",
            "Pants Flower",
            "Tshirt Long Stripes",
            "Tshirt Short White"});
            this.shirtsPantsCheckedListBox.Location = new System.Drawing.Point(197, 201);
            this.shirtsPantsCheckedListBox.Name = "shirtsPantsCheckedListBox";
            this.shirtsPantsCheckedListBox.Size = new System.Drawing.Size(165, 130);
            this.shirtsPantsCheckedListBox.TabIndex = 24;
            this.shirtsPantsCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CheckedListBox_ItemCheck);
            // 
            // shoesCheckedListBox
            // 
            this.shoesCheckedListBox.CheckOnClick = true;
            this.shoesCheckedListBox.FormattingEnabled = true;
            this.shoesCheckedListBox.Items.AddRange(new object[] {
            "None",
            "Sneakers Blue",
            "Sneakers Green",
            "Sneakers Red",
            "Sneakers Swagger",
            "Socks H Stripe",
            "Socks V Stripe"});
            this.shoesCheckedListBox.Location = new System.Drawing.Point(197, 355);
            this.shoesCheckedListBox.Name = "shoesCheckedListBox";
            this.shoesCheckedListBox.Size = new System.Drawing.Size(165, 130);
            this.shoesCheckedListBox.TabIndex = 25;
            this.shoesCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CheckedListBox_ItemCheck);
            // 
            // tailsCheckedListBox
            // 
            this.tailsCheckedListBox.CheckOnClick = true;
            this.tailsCheckedListBox.FormattingEnabled = true;
            this.tailsCheckedListBox.Items.AddRange(new object[] {
            "None",
            "Tail Sock"});
            this.tailsCheckedListBox.Location = new System.Drawing.Point(197, 505);
            this.tailsCheckedListBox.Name = "tailsCheckedListBox";
            this.tailsCheckedListBox.Size = new System.Drawing.Size(165, 40);
            this.tailsCheckedListBox.TabIndex = 26;
            this.tailsCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CheckedListBox_ItemCheck);
            // 
            // glassesCheckedListBox
            // 
            this.glassesCheckedListBox.CheckOnClick = true;
            this.glassesCheckedListBox.FormattingEnabled = true;
            this.glassesCheckedListBox.Items.AddRange(new object[] {
            "None",
            "Eye Patch",
            "Glasses Nerd Cyan",
            "Glasses Nerd Green",
            "Glasses Nerd Pink",
            "Monocle",
            "Sunglasses Aviator Cyan",
            "Sunglasses Aviator Green",
            "Sunglasses Aviator Yellow",
            "Sunglasses Thug"});
            this.glassesCheckedListBox.Location = new System.Drawing.Point(380, 63);
            this.glassesCheckedListBox.Name = "glassesCheckedListBox";
            this.glassesCheckedListBox.Size = new System.Drawing.Size(165, 184);
            this.glassesCheckedListBox.TabIndex = 27;
            this.glassesCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CheckedListBox_ItemCheck);
            // 
            // miscCheckedListBox
            // 
            this.miscCheckedListBox.CheckOnClick = true;
            this.miscCheckedListBox.FormattingEnabled = true;
            this.miscCheckedListBox.Items.AddRange(new object[] {
            "None",
            "Banana Hands",
            "Banana Right Hand",
            "Bowtie",
            "Camera",
            "Club",
            "Flamethrower",
            "Gloves White",
            "Guitar",
            "Microphone",
            "Necklace Boss",
            "Tie Cyan",
            "Tie Pink",
            "Whisky Right"});
            this.miscCheckedListBox.Location = new System.Drawing.Point(382, 289);
            this.miscCheckedListBox.Name = "miscCheckedListBox";
            this.miscCheckedListBox.Size = new System.Drawing.Size(165, 256);
            this.miscCheckedListBox.TabIndex = 28;
            this.miscCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CheckedListBox_ItemCheck);
            // 
            // rarityLabel
            // 
            this.rarityLabel.Location = new System.Drawing.Point(197, 573);
            this.rarityLabel.Name = "rarityLabel";
            this.rarityLabel.Size = new System.Drawing.Size(165, 20);
            this.rarityLabel.TabIndex = 31;
            this.rarityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(469, 571);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(78, 25);
            this.cancelButton.TabIndex = 30;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(382, 571);
            this.okButton.Margin = new System.Windows.Forms.Padding(2);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(78, 25);
            this.okButton.TabIndex = 29;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // requestAmountSlider
            // 
            this.requestAmountSlider.LargeChange = 50;
            this.requestAmountSlider.Location = new System.Drawing.Point(12, 534);
            this.requestAmountSlider.Maximum = 250;
            this.requestAmountSlider.Minimum = 1;
            this.requestAmountSlider.Name = "requestAmountSlider";
            this.requestAmountSlider.Size = new System.Drawing.Size(165, 45);
            this.requestAmountSlider.SmallChange = 5;
            this.requestAmountSlider.TabIndex = 32;
            this.requestAmountSlider.TickFrequency = 5;
            this.requestAmountSlider.Value = 100;
            this.requestAmountSlider.Scroll += new System.EventHandler(this.RequestAmountSlider_Scroll);
            // 
            // requestAmountLabel
            // 
            this.requestAmountLabel.AutoSize = true;
            this.requestAmountLabel.Location = new System.Drawing.Point(13, 515);
            this.requestAmountLabel.Name = "requestAmountLabel";
            this.requestAmountLabel.Size = new System.Drawing.Size(162, 15);
            this.requestAmountLabel.TabIndex = 33;
            this.requestAmountLabel.Text = "Amount of MonKey Requests";
            // 
            // requestAmountNumeric
            // 
            this.requestAmountNumeric.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.requestAmountNumeric.Location = new System.Drawing.Point(12, 572);
            this.requestAmountNumeric.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.requestAmountNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.requestAmountNumeric.Name = "requestAmountNumeric";
            this.requestAmountNumeric.Size = new System.Drawing.Size(162, 23);
            this.requestAmountNumeric.TabIndex = 34;
            this.requestAmountNumeric.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.requestAmountNumeric.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.requestAmountNumeric.ValueChanged += new System.EventHandler(this.RequestAmountNumeric_ValueChanged);
            // 
            // ExpertSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 609);
            this.Controls.Add(this.requestAmountNumeric);
            this.Controls.Add(this.requestAmountLabel);
            this.Controls.Add(this.requestAmountSlider);
            this.Controls.Add(this.rarityLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.miscCheckedListBox);
            this.Controls.Add(this.glassesCheckedListBox);
            this.Controls.Add(this.tailsCheckedListBox);
            this.Controls.Add(this.shoesCheckedListBox);
            this.Controls.Add(this.shirtsPantsCheckedListBox);
            this.Controls.Add(this.mouthsCheckedListBox);
            this.Controls.Add(this.hatsCheckedListBox);
            this.Controls.Add(this.switchButton);
            this.Controls.Add(this.tailsLabel);
            this.Controls.Add(this.shoesLabel);
            this.Controls.Add(this.shirtsPantsLabel);
            this.Controls.Add(this.mouthsLabel);
            this.Controls.Add(this.miscLabel);
            this.Controls.Add(this.hatsLabel);
            this.Controls.Add(this.glassesLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExpertSettings";
            this.Text = "Create Your MonKey";
            ((System.ComponentModel.ISupportInitialize)(this.requestAmountSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.requestAmountNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label glassesLabel;
        private System.Windows.Forms.Label hatsLabel;
        private System.Windows.Forms.Label miscLabel;
        private System.Windows.Forms.Label mouthsLabel;
        private System.Windows.Forms.Label shirtsPantsLabel;
        private System.Windows.Forms.Label shoesLabel;
        private System.Windows.Forms.Label tailsLabel;
        private System.Windows.Forms.Button switchButton;
        private BetterCheckedListBox hatsCheckedListBox;
        private BetterCheckedListBox mouthsCheckedListBox;
        private BetterCheckedListBox shirtsPantsCheckedListBox;
        private BetterCheckedListBox shoesCheckedListBox;
        private BetterCheckedListBox tailsCheckedListBox;
        private BetterCheckedListBox glassesCheckedListBox;
        private BetterCheckedListBox miscCheckedListBox;
        private System.Windows.Forms.Label rarityLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TrackBar requestAmountSlider;
        private System.Windows.Forms.Label requestAmountLabel;
        private System.Windows.Forms.NumericUpDown requestAmountNumeric;
    }
}