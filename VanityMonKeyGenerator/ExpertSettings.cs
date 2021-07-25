using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace VanityMonKeyGenerator
{
    public partial class ExpertSettings : Form
    {
        public ExpertSettings()
        {
            InitializeComponent();
            LoadSavedMonKey();
        }

        private void LoadSavedMonKey()
        {
            if (Properties.Settings.Default.SavedAccessories == null)
            {
                return;
            }

            List<string> accessoryList = Properties.Settings.Default.
                SavedAccessories.Cast<string>().ToList();

            foreach (BetterCheckedListBox checkedListBox in Controls.OfType<BetterCheckedListBox>())
            {
                List<string> itemsToCheck = new List<string>();
                foreach (var item in checkedListBox.Items)
                {
                    if (accessoryList.Any(accessory => accessory.Contains(
                        item.ToString().Replace(" ", ""))))
                    {
                        itemsToCheck.Add(item.ToString());
                    }
                }
                foreach (string item in itemsToCheck)
                {
                    checkedListBox.SetItemChecked(checkedListBox.Items.IndexOf(item), true);
                }
            }

            rarityLabel.Text = $"Rarity: 1 in {Accessories.GetMonKeyRarity(accessoryList):#,#}";
        }

        private void ExpertButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SimpleMode = true;
            Properties.Settings.Default.Save();
            Dispose();
            SimpleSettings simpleSettings = new SimpleSettings();
            simpleSettings.ShowDialog();
        }
    }
}
