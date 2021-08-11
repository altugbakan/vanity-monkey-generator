using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using VanityMonKeyGenerator;
using static VanityMonKeyGenerator.Requests;

namespace GUI
{
    public partial class MonKeyForm : Form
    {
        private CancellationTokenSource cancellationTokenSource;

        public MonKeyForm()
        {
            InitializeComponent();
            LoadDefaultMonKey();
        }

        private void MonKeyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            if (cancellationTokenSource != null && !cancellationTokenSource.IsCancellationRequested)
            {
                cancellationTokenSource.Cancel();
            }
        }

        private async void GetRandomMonKeyButton_Click(object sender, EventArgs e)
        {
            MonKey monKey = new MonKey();
            try
            {
                Drawing.DrawSvg(await monKey.GetMonKeySvg(monKeyPictureBox.Width), monKeyPictureBox);
            }
            catch
            {
                MessageBox.Show("No internet connection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            addressTextBox.Text = monKey.Address;
            seedTextBox.Text = monKey.Seed;
        }

        public delegate void UpdateProgress(Progress progress);

        private void ReportProgress(Progress progress)
        {
            if (searchedLabel.InvokeRequired)
            {
                searchedLabel.Invoke(new UpdateProgress(ReportProgress), new object[] { progress });
            }
            else
            {
                searchedLabel.Text = $"Searched {progress.Iterations:#,#} MonKeys. Estimated: {progress.Expectation:#,#}";
            }
        }

        private async void FindSpecificMonKeyButton_Click(object sender, EventArgs e)
        {
            if (findSpecificMonKeyButton.Text == "Find Specific MonKey")
            {
                cancellationTokenSource = new CancellationTokenSource();
                while (Properties.Settings.Default.SavedAccessories == null)
                {
                    DialogResult dialogResult = MessageBox.Show("Seems like you haven't yet created a" +
                        " specific MonKey. Want to create one?", "Warning", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SimpleSettings settingsForm = new SimpleSettings();
                        settingsForm.ShowDialog();
                    }
                    else
                    {
                        GetRandomMonKeyButton_Click(null, null);
                        return;
                    }
                }
                findSpecificMonKeyButton.Text = "Cancel";
                Result result = await Task.Run(
                    () => SearchMonKeys(
                        cancellationTokenSource.Token,
                        Properties.Settings.Default.SavedAccessories.Cast<string>().ToList(),
                        Properties.Settings.Default.MonKeyRequestAmount,
                        delegate (Progress progress) { ReportProgress(progress); }
                    )
                );

                if (cancellationTokenSource.IsCancellationRequested)
                {
                    searchedLabel.Text = "";
                }
                else if (result == null)
                {
                    MessageBox.Show("No internet connection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Drawing.DrawSvg(await result.MonKey.GetMonKeySvg(monKeyPictureBox.Width), monKeyPictureBox);
                    addressTextBox.Text = result.MonKey.Address;
                    seedTextBox.Text = result.MonKey.Seed;
                    searchedLabel.Text = $"Found MonKey after {result.Iterations:#,#} MonKeys.";
                }

                findSpecificMonKeyButton.Text = "Find Specific MonKey";
            }
            else
            {
                cancellationTokenSource.Cancel();
            }
        }

        private void LoadDefaultMonKey()
        {
            Drawing.DrawMonKey(new List<string>() { "Mouths-SmileNormal" }, monKeyPictureBox);
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.SimpleMode)
            {
                SimpleSettings simpleSettings = new SimpleSettings();
                simpleSettings.ShowDialog();
            }
            else
            {
                ExpertSettings expertSettings = new ExpertSettings();
                expertSettings.ShowDialog();
            }
        }
    }
}
