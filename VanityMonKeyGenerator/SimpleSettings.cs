using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Forms;

namespace VanityMonKeyGenerator
{
    public partial class SimpleSettings : Form
    {
        public SimpleSettings()
        {
            InitializeComponent();
            LoadSavedMonKey();
        }

        private void LoadSavedMonKey()
        {
            if (Properties.Settings.Default.SavedAccessories == null)
            {
                Drawing.DrawMonKey(GetAccessories(), monKeyPictureBox);
                return;
            }

            Dictionary<string, ComboBox> categoryDictionary =
                new Dictionary<string, ComboBox>()
                {
                    { "Glasses", glassesComboBox },
                    { "Hats", hatsComboBox },
                    { "Misc", miscComboBox },
                    { "Mouths", mouthsComboBox },
                    { "ShirtsPants", shirtsPantsComboBox },
                    { "Shoes", shoesComboBox },
                    { "Tails", tailsComboBox }
                };

            List<string> accessoryList = Properties.Settings.Default.
                SavedAccessories.Cast<string>().ToList();

            foreach (var pair in categoryDictionary)
            {
                if (accessoryList.Any(acc => acc.Contains(pair.Key)))
                {
                    pair.Value.Text = accessoryList.First(acc => acc.Contains(pair.Key)).
                        OnlyAccessory().RemoveSpaces();
                }
            }

            accessoryList = GetAccessories();
            Drawing.DrawMonKey(accessoryList, monKeyPictureBox);
            rarityLabel.Text = $"Rarity: 1 in {Accessories.GetMonKeyRarity(accessoryList):#,#}";
        }
        private List<string> GetAccessories()
        {
            return new List<string>()
            {
                $"Glasses-{glassesComboBox.Text.Replace(" ", "")}",
                $"Hats-{hatsComboBox.Text.Replace(" ", "")}",
                $"Misc-{miscComboBox.Text.Replace(" ", "")}",
                $"Mouths-{mouthsComboBox.Text.Replace(" ", "")}",
                $"ShirtsPants-{shirtsPantsComboBox.Text.Replace(" ", "")}",
                $"Shoes-{shoesComboBox.Text.Replace(" ", "")}",
                $"Tails-{tailsComboBox.Text.Replace(" ", "")}",
            };
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle filling when opening the form.
            if (((ComboBox)sender).CanSelect)
            {
                List<string> accessories = GetAccessories();
                Drawing.DrawMonKey(accessories, monKeyPictureBox);
                rarityLabel.Text = $"Rarity: 1 in {Accessories.GetMonKeyRarity(accessories):#,#}";
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            StringCollection stringCollection = new StringCollection();
            stringCollection.AddRange(GetAccessories().ToArray());
            Properties.Settings.Default.SavedAccessories = stringCollection;
            Properties.Settings.Default.Save();
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SwitchButton_Click(object sender, EventArgs e)
        {
            StringCollection stringCollection = new StringCollection();
            stringCollection.AddRange(GetAccessories().ToArray());
            Properties.Settings.Default.SavedAccessories = stringCollection;
            Properties.Settings.Default.SimpleMode = false;
            Properties.Settings.Default.Save();
            Dispose();
            ExpertSettings expertSettings = new ExpertSettings();
            expertSettings.ShowDialog();
        }
    }

}
