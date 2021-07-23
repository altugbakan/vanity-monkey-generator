using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VanityMonKeyGenerator
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            Drawing.DrawMonKey(GetAccessories(), monKeyPictureBox);
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
                accessories.Add("Glasses-" + glassesComboBox.Text.Replace(" ", ""));
            }

            if (hatsComboBox.Text != "None")
            {
                accessories.Add("Hats-" + hatsComboBox.Text.Replace(" ", ""));
            }

            if (miscComboBox.Text != "None")
            {
                accessories.Add("Misc-" + miscComboBox.Text.Replace(" ", ""));
            }

            if (mouthsComboBox.Text != "None")
            {
                accessories.Add("Mouths-" + mouthsComboBox.Text.Replace(" ", ""));
            }

            if (shirtPantsComboBox.Text != "None")
            {
                accessories.Add("Shirt-Pants-" + shirtPantsComboBox.Text.Replace(" ", ""));
            }

            if (shoesComboBox.Text != "None")
            {
                accessories.Add("Shoes-" + shoesComboBox.Text.Replace(" ", ""));
            }

            if (tailsComboBox.Text != "None")
            {
                accessories.Add("Tails-" + tailsComboBox.Text.Replace(" ", ""));
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
