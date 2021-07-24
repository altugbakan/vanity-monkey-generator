using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace VanityMonKeyGenerator
{
    public partial class MonKeyForm : Form
    {
        public MonKeyForm()
        {
            InitializeComponent();
            LoadDefaultMonKey();
        }

        private void GetRandomMonKeyButton_Click(object sender, EventArgs e)
        {
            MonKey monKey = new MonKey();
            if (monKey.Svg == null)
            {
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
            List<string> requestedAccessories = Properties.Settings.Default.SavedAccessories
                .Cast<string>().ToList();
            int expected = (int)(1.0 / Accessories.GetMonKeyChance(requestedAccessories));
            while (!monKeySearcher.CancellationPending)
            {
                MonKey monKey = new MonKey();
                if (monKey.Svg == null)
                {
                    e.Cancel = true;
                    return;
                }

                List<string> obtainedAccessories = Accessories.ObtainedAccessories(monKey.Svg);
                if (Accessories.AccessoriesMatching(requestedAccessories, obtainedAccessories))
                {
                    e.Result = new Result(monKey, iterations);
                    break;
                }
                monKeySearcher.ReportProgress(0, new ProgressResult(expected, ++iterations));
            }
            if (monKeySearcher.CancellationPending)
            {
                e.Cancel = true;
            }  
        }

        private void MonKeySearcher_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressResult result = (ProgressResult)e.UserState;
            searchedLabel.Text = $"Searched {result.Iterations} MonKeys. Expected: {result.Expected}";
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

        public class ProgressResult
        {
            public int Expected;
            public int Iterations;

            public ProgressResult(int expected, int iterations)
            {
                Expected = expected;
                Iterations = iterations;
            }
        }

    }
}
