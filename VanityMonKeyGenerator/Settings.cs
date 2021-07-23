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


            if (glassesComboBox.Text != "None" )
            {
                accessories.Add("Glasses-" + glassesComboBox.Text);
            }

            if (hatsComboBox.Text != "None")
            {
                accessories.Add("Hats-" + hatsComboBox.Text);
            }

            if (miscComboBox.Text != "None")
            {
                accessories.Add("Misc-" + miscComboBox.Text);
            }

            if (mouthsComboBox.Text != "None")
            {
                accessories.Add("Mouths-" + mouthsComboBox.Text);
            }

            if (shirtPantsComboBox.Text != "None")
            {
                accessories.Add("Shirt-Pants-" + shirtPantsComboBox.Text);
            }

            if (shoesComboBox.Text != "None")
            {
                accessories.Add("Shoes" + shoesComboBox.Text);
            }

            if (tailsComboBox.Text != "None")
            {
                accessories.Add("Tails" + tailsComboBox.Text);
            }
            
            return accessories;
        }

        private void HatsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Drawing.DrawMonKey(GetAccessories(), monKeyPictureBox);
        }

        private void MiscComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Drawing.DrawMonKey(GetAccessories(), monKeyPictureBox);
        }

        private void MouthsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Drawing.DrawMonKey(GetAccessories(), monKeyPictureBox);
        }

        private void ShirtPantsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Drawing.DrawMonKey(GetAccessories(), monKeyPictureBox);
        }

        private void ShoesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Drawing.DrawMonKey(GetAccessories(), monKeyPictureBox);
        }

        private void TailsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Drawing.DrawMonKey(GetAccessories(), monKeyPictureBox);
        }
    }
}
