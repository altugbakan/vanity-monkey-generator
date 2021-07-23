﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Linq;
using System.Text.RegularExpressions;

namespace VanityMonKeyGenerator
{
    public partial class Settings : Form
    {
        public Settings()
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

            List<string> accessoryList = Properties.Settings.Default.
                SavedAccessories.Cast<string>().ToList();

            // Glasses
            if (accessoryList.Any(acc => acc.Contains("Glasses")))
            {
                glassesComboBox.Text = Regex.Replace(accessoryList.First(acc => acc.Contains("Glasses"))
                    .Replace("Glasses-", ""), "([a-z])([A-Z])", "$1 $2");
            }
            else
            {
                glassesComboBox.Text = "None";
            }
            // Hats
            if (accessoryList.Any(acc => acc.Contains("Hats")))
            {
                hatsComboBox.Text = Regex.Replace(accessoryList.First(acc => acc.Contains("Hats"))
                    .Replace("Hats-", ""), "([a-z])([A-Z])", "$1 $2");
            }
            else
            {
                hatsComboBox.Text = "None";
            }
            // Misc
            if (accessoryList.Any(acc => acc.Contains("Misc")))
            {
                miscComboBox.Text = Regex.Replace(accessoryList.First(acc => acc.Contains("Misc"))
                    .Replace("Misc-", ""), "([a-z])([A-Z])", "$1 $2");
            }
            else
            {
                miscComboBox.Text = "None";
            }
            // Mouths
            if (accessoryList.Any(acc => acc.Contains("Mouths")))
            {
                mouthsComboBox.Text = Regex.Replace(accessoryList.First(acc => acc.Contains("Mouths"))
                    .Replace("Mouths-", ""), "([a-z])([A-Z])", "$1 $2");
            }
            else
            {
                mouthsComboBox.Text = "None";
            }
            // ShirtsPants
            if (accessoryList.Any(acc => acc.Contains("ShirtsPants")))
            {
                shirtPantsComboBox.Text = Regex.Replace(accessoryList.First(acc => acc.Contains("ShirtsPants"))
                    .Replace("ShirtsPants-", ""), "([a-z])([A-Z])", "$1 $2");
            }
            else
            {
                shirtPantsComboBox.Text = "None";
            }
            // Shoes
            if (accessoryList.Any(acc => acc.Contains("Shoes")))
            {
                shoesComboBox.Text = Regex.Replace(accessoryList.First(acc => acc.Contains("Shoes"))
                    .Replace("Shoes-", ""), "([a-z])([A-Z])", "$1 $2");
            }
            else
            {
                shoesComboBox.Text = "None";
            }
            // Tails
            if (accessoryList.Any(acc => acc.Contains("Tails")))
            {
                tailsComboBox.Text = Regex.Replace(accessoryList.First(acc => acc.Contains("Tails"))
                    .Replace("Tails-", ""), "([a-z])([A-Z])", "$1 $2");
            }
            else
            {
                tailsComboBox.Text = "None";
            }

            Drawing.DrawMonKey(GetAccessories(), monKeyPictureBox);
        }
        private List<string> GetAccessories()
        {
            return new List<string>()
            {
                "Glasses-" + glassesComboBox.Text.Replace(" ", ""),
                "Hats-" + hatsComboBox.Text.Replace(" ", ""),
                "Misc-" + miscComboBox.Text.Replace(" ", ""),
                "Mouths-" + mouthsComboBox.Text.Replace(" ", ""),
                "ShirtsPants-" + shirtPantsComboBox.Text.Replace(" ", ""),
                "Shoes-" + shoesComboBox.Text.Replace(" ", ""),
                "Tails-" + tailsComboBox.Text.Replace(" ", ""),
            };
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Drawing.DrawMonKey(GetAccessories(), monKeyPictureBox);
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
    }

}
