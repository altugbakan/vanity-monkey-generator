using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

using static VanityMonKeyGenerator.Accessories;

namespace GUI
{
    public partial class ExpertSettings : Form
    {
        private const int SmileNormalIndex = 6;
        public ExpertSettings()
        {
            InitializeComponent();
            LoadSavedMonKey();
            requestAmountSlider.Value = Math.Min(Properties.Settings.Default.MonKeyRequestAmount, 250);
            requestAmountNumeric.Value = Properties.Settings.Default.MonKeyRequestAmount;
        }

        private void LoadSavedMonKey()
        {
            if (Properties.Settings.Default.SavedAccessories == null)
            {
                glassesCheckedListBox.SetItemChecked(0, true);
                hatsCheckedListBox.SetItemChecked(0, true);
                miscCheckedListBox.SetItemChecked(0, true);
                mouthsCheckedListBox.SetItemChecked(SmileNormalIndex, true);
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

            rarityLabel.Text = $"Rarity: 1 in {GetMonKeyRarity(accessoryList):#,#}";
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
            Properties.Settings.Default.MonKeyRequestAmount = requestAmountSlider.Value;
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
            Properties.Settings.Default.SimpleMode = true;
            Properties.Settings.Default.Save();
            Dispose();
            SimpleSettings simpleSettings = new SimpleSettings();
            simpleSettings.ShowDialog();
        }

        private void CheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Invocation is added here to handle the late update of the CheckedListBox after clicking.
            BeginInvoke((Action)(() =>
            {
                rarityLabel.Text = $"Rarity: 1 in {GetMonKeyRarity(GetAccessories()):#,#}";
            }));
        }

        private void RequestAmountNumeric_ValueChanged(object sender, EventArgs e)
        {
            requestAmountSlider.Value = Math.Min((int)requestAmountNumeric.Value, 250);
        }

        private void RequestAmountSlider_Scroll(object sender, EventArgs e)
        {
            requestAmountNumeric.Value = requestAmountSlider.Value;
        }

        private void ExpertSettings_Shown(object sender, EventArgs e)
        {
            SubscribeCheckedListBoxEvents();
        }

        private void ExpertSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnsubscribeCheckedListBoxEvents();
        }

        private void DeselectAllButton_Click(object sender, EventArgs e)
        {
            UnsubscribeCheckedListBoxEvents();
            glassesCheckedListBox.SetAllItemsChecked(false, 0);
            hatsCheckedListBox.SetAllItemsChecked(false, 0);
            miscCheckedListBox.SetAllItemsChecked(false, 0);
            mouthsCheckedListBox.SetAllItemsChecked(false, SmileNormalIndex);
            shirtsPantsCheckedListBox.SetAllItemsChecked(false, 0);
            shoesCheckedListBox.SetAllItemsChecked(false, 0);
            tailsCheckedListBox.SetAllItemsChecked(false, 0);
            SubscribeCheckedListBoxEvents();
            CheckedListBox_ItemCheck(null, null); // To update rarity.
        }

        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            UnsubscribeCheckedListBoxEvents();
            glassesCheckedListBox.SetAllItemsChecked(true);
            hatsCheckedListBox.SetAllItemsChecked(true);
            miscCheckedListBox.SetAllItemsChecked(true);
            mouthsCheckedListBox.SetAllItemsChecked(true);
            shirtsPantsCheckedListBox.SetAllItemsChecked(true);
            shoesCheckedListBox.SetAllItemsChecked(true);
            tailsCheckedListBox.SetAllItemsChecked(true);
            SubscribeCheckedListBoxEvents();
            CheckedListBox_ItemCheck(null, null); // To update rarity.
        }

        private void UnsubscribeCheckedListBoxEvents()
        {
            glassesCheckedListBox.ItemCheck -= CheckedListBox_ItemCheck;
            hatsCheckedListBox.ItemCheck -= CheckedListBox_ItemCheck;
            miscCheckedListBox.ItemCheck -= CheckedListBox_ItemCheck;
            mouthsCheckedListBox.ItemCheck -= CheckedListBox_ItemCheck;
            shirtsPantsCheckedListBox.ItemCheck -= CheckedListBox_ItemCheck;
            shoesCheckedListBox.ItemCheck -= CheckedListBox_ItemCheck;
            tailsCheckedListBox.ItemCheck -= CheckedListBox_ItemCheck;
        }

        private void SubscribeCheckedListBoxEvents()
        {
            glassesCheckedListBox.ItemCheck += CheckedListBox_ItemCheck;
            hatsCheckedListBox.ItemCheck += CheckedListBox_ItemCheck;
            miscCheckedListBox.ItemCheck += CheckedListBox_ItemCheck;
            mouthsCheckedListBox.ItemCheck += CheckedListBox_ItemCheck;
            shirtsPantsCheckedListBox.ItemCheck += CheckedListBox_ItemCheck;
            shoesCheckedListBox.ItemCheck += CheckedListBox_ItemCheck;
            tailsCheckedListBox.ItemCheck += CheckedListBox_ItemCheck;
        }
    }
}
