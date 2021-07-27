using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VanityMonKeyGenerator
{
    public partial class MonKeyForm : Form
    {
        private const int MaxRequestCount = 32;

        public MonKeyForm()
        {
            InitializeComponent();
            LoadDefaultMonKey();
        }

        private void GetRandomMonKeyButton_Click(object sender, EventArgs e)
        {
            MonKey monKey = new MonKey();
            monKey.RequestSvg();
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

        private void MonKeySearcher_DoWork(object sender, DoWorkEventArgs e)
        {
            ulong iterations = 0;
            HttpClient client = new HttpClient();
            List<string> requestedAccessories = Properties.Settings.Default.SavedAccessories
                .Cast<string>().ToList();
            List<Task<MonKey>> tasks = new List<Task<MonKey>>();
            ulong expectation = Accessories.GetMonKeyRarity(requestedAccessories);

            while (!monKeySearcher.CancellationPending)
            {
                for (int i = 0; i < MaxRequestCount - tasks.Where(t => !t.IsCompleted).Count(); i++)
                {
                    tasks.Add(GetMonKeyAsync(client));
                }

                Task.WaitAny(tasks.ToArray());

                foreach (Task<MonKey> task in tasks.Where(t => t.IsCompleted))
                {
                    MonKey monKey;
                    try
                    {
                        monKey = task.Result;
                    }
                    catch
                    {
                        MessageBox.Show("Connection error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                        return;
                    }
                    List<string> obtainedAccessories = Accessories.ObtainedAccessories(monKey.Svg);
                    if (Accessories.AccessoriesMatching(requestedAccessories, obtainedAccessories))
                    {
                        e.Result = new Result(monKey, iterations);
                        return;
                    }
                    monKeySearcher.ReportProgress(0, new ProgressResult(expectation, ++iterations));
                }
            }
            if (monKeySearcher.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private void MonKeySearcher_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressResult result = (ProgressResult)e.UserState;
            searchedLabel.Text = $"Searched {result.Iterations:#,#} MonKeys. " +
                $"Estimated: {result.Expectation:#,#}";
        }

        private void MonKeySearcher_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                Result result = (Result)e.Result;
                Drawing.DrawSvg(result.MonKey.Svg, monKeyPictureBox);
                addressTextBox.Text = result.MonKey.Address;
                seedTextBox.Text = result.MonKey.Seed;
                searchedLabel.Text = $"Found MonKey after {result.Iterations:#,#} MonKeys.";
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
            public ulong Iterations;

            public Result(MonKey monKey, ulong iterations)
            {
                MonKey = monKey;
                Iterations = iterations;
            }
        }

        public class ProgressResult
        {
            public ulong Expectation;
            public ulong Iterations;

            public ProgressResult(ulong expectation, ulong iterations)
            {
                Expectation = expectation;
                Iterations = iterations;
            }
        }

        private async Task<MonKey> GetMonKeyAsync(HttpClient client)
        {
            MonKey monKey = new MonKey();
            await monKey.RequestSvgAsync(client);
            return monKey;
        }

        public async Task<List<MonKey>> RequestMultipleMonKeys(int count)
        {
            HttpClient client = new HttpClient();
            List<MonKey> monKeys = new List<MonKey>();
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < count; i++)
            {
                MonKey monKey = new MonKey();
                monKeys.Add(monKey);
                tasks.Add(monKey.RequestSvgAsync(client));
            }

            await Task.WhenAll(tasks);

            return monKeys;
        }
    }
}
