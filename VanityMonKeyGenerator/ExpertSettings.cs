using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
                glassesCheckedListBox.SetItemChecked(0, true);
                hatsCheckedListBox.SetItemChecked(0, true);
                miscCheckedListBox.SetItemChecked(0, true);
                mouthsCheckedListBox.SetItemChecked(6, true);
                shirtsPantsCheckedListBox.SetItemChecked(0, true);
                shoesCheckedListBox.SetItemChecked(0, true);
                tailsCheckedListBox.SetItemChecked(0, true);
                return;
            }

            List<string> accessoryList = Properties.Settings.Default.
                SavedAccessories.Cast<string>().ToList();

            Dictionary<string, BetterCheckedListBox> categoryDictionary =
                new Dictionary<string, BetterCheckedListBox>()
                {
                    { "Glasses", glassesCheckedListBox },
                    { "Hats", hatsCheckedListBox },
                    { "Misc", miscCheckedListBox },
                    { "Mouths", mouthsCheckedListBox },
                    { "ShirtsPants", shirtsPantsCheckedListBox },
                    { "Shoes", shoesCheckedListBox },
                    { "Tails", tailsCheckedListBox }
                };

            foreach (var pair in categoryDictionary)
            {
                foreach (string accessory in accessoryList.Where(acc => acc.Contains(pair.Key)))
                {
                    if (accessory.Contains("Any"))
                    {
                        for (int i = 0; i < pair.Value.Items.Count; i++)
                        {
                            pair.Value.SetItemChecked(i, true);
                        }
                        break;
                    }
                    else
                    {
                        pair.Value.SetItemChecked(pair.Value.Items.
                            IndexOf(Regex.Replace(accessory.Split('-').Last(),
                            "([A-Z])", " $1").TrimStart()), true);
                    }
                }
            }

            rarityLabel.Text = $"Rarity: 1 in {Accessories.GetMonKeyRarity(accessoryList):#,#}";
        }

        private List<string> GetAccessories()
        {
            List<string> accessories = new List<string>();
            Dictionary<string, BetterCheckedListBox> categoryDictionary =
                new Dictionary<string, BetterCheckedListBox>()
                {
                    { "Glasses", glassesCheckedListBox },
                    { "Hats", hatsCheckedListBox },
                    { "Misc", miscCheckedListBox },
                    { "Mouths", mouthsCheckedListBox },
                    { "ShirtsPants", shirtsPantsCheckedListBox },
                    { "Shoes", shoesCheckedListBox },
                    { "Tails", tailsCheckedListBox }
                };

            foreach (var pair in categoryDictionary)
            {
                if (pair.Value.CheckedItems.Count == pair.Value.Items.Count)
                {
                    accessories.Add($"{pair.Key}-Any");
                }
                else
                {
                    foreach (var checkedItem in pair.Value.CheckedItems)
                    {
                        accessories.Add($"{pair.Key}-{checkedItem.ToString().Replace(" ", "")}");
                    }
                }
            }

            return accessories;
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
            Properties.Settings.Default.SimpleMode = true;
            Properties.Settings.Default.Save();
            Dispose();
            SimpleSettings simpleSettings = new SimpleSettings();
            simpleSettings.ShowDialog();
        }

        private void CheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (IsHandleCreated)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    rarityLabel.Text = $"Rarity: 1 in {Accessories.GetMonKeyRarity(GetAccessories()):#,#}";
                });
            }
        }
    }
}
