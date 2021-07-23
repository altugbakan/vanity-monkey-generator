using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VanityMonKeyGenerator
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void GlassesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Drawing.DrawMonKey(GetAccessories(), monKeyPictureBox);
        }

        private List<string> GetAccessories()
        {
            List<string> accessories = new List<string>();

            accessories.Add("Glasses-" + glassesComboBox.Text);

            return accessories;
        }
    }
}
