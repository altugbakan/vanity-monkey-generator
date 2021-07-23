using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace VanityMonKeyGenerator
{
    public partial class MonKeyForm : Form
    {
        public MonKeyForm()
        {
            InitializeComponent();
            LoadDefaultMonKey();
        }

        private async void GetRandomMonKeyButton_Click(object sender, EventArgs e)
        {
            MonKey monKey = new MonKey();
            try
            {
                monKey.Svg = await monKey.RequestMonKey();
            }
            catch
            {
                MessageBox.Show("No internet connection.", "Error");
                return;
            }
            Drawing.DrawSvg(monKey.Svg, monKeyPictureBox);
            addressTextBox.Text = monKey.Address;
            seedTextBox.Text = monKey.Seed;
        }

        private void FindSpecificMonKeyButton_Click(object sender, EventArgs e)
        {
            if (findSpecificMonKeyButton.Text == "Find Specific MonKey")
            {
                if (Properties.Settings.Default.SavedAccessories == null)
                {
                    DialogResult dialogResult = MessageBox.Show("Seems like you haven't yet created a" +
                    " specific MonKey. Want to create one?", "Warning", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Settings settingsForm = new Settings();
                        settingsForm.ShowDialog();
                    }
                    else
                    {
                        GetRandomMonKeyButton_Click(null, null);
                        return;
                    }
                }
                monKeySearcher.RunWorkerAsync();
                findSpecificMonKeyButton.Text = "Cancel";
            }
            else
            {
                monKeySearcher.CancelAsync();
            }
        }

        private void LoadDefaultMonKey()
        {
            Drawing.DrawMonKey(new List<string>() { "Mouths-SmileNormal" }, monKeyPictureBox);
        }
        private void SettingsButton_Click(object sender, EventArgs e)
        {
            Settings settingsForm = new Settings();
            settingsForm.ShowDialog();
        }

        private void MonKeySearcher_DoWork(object sender, DoWorkEventArgs e)
        {
            int iterations = 0;
            while (!monKeySearcher.CancellationPending)
            {
                MonKey monKey = new MonKey();
                monKey.Svg = monKey.RequestMonKeySync();
                if (monKey.Svg == "")
                {
                    e.Cancel = true;
                    return;
                }
                if (Accessories.AccessoriesMatching(Properties.Settings.Default.SavedAccessories
                    .Cast<string>().ToList(), Accessories.ObtainedAccessories(monKey.Svg)))
                {
                    e.Result = new Result(monKey, iterations);
                    break;
                }
                monKeySearcher.ReportProgress(0, ++iterations);
            }
            if (monKeySearcher.CancellationPending)
            {
                e.Cancel = true;
            }  
        }

        private void MonKeySearcher_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            searchedLabel.Text = $"Searched {(int)e.UserState} MonKeys.";
        }

        private void MonKeySearcher_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                Result result = (Result)e.Result;
                Drawing.DrawSvg(result.MonKey.Svg, monKeyPictureBox);
                addressTextBox.Text = result.MonKey.Address;
                seedTextBox.Text = result.MonKey.Seed;
                searchedLabel.Text = $"Found MonKey after {result.Iterations} MonKeys.";
            }
            else
            {
                searchedLabel.Text = "";
            }
            findSpecificMonKeyButton.Text = "Find Specific MonKey";
        }

        public class Result
        {
            public MonKey MonKey;
            public int Iterations;

            public Result(MonKey monKey, int iterations)
            {
                MonKey = monKey;
                Iterations = iterations;
            }
        }
    }
}
